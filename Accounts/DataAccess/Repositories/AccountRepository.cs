using Application.Accounts.DTOs;
using Application.Commons.Helpers;
using Application.DTOs;
using Application.IRepositories;
using Domain.Common;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedContracts.Contract;

namespace DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountsDbContext _ctx;

    public AccountRepository(AccountsDbContext ctx) => _ctx = ctx;

    public async Task<ICollection<Account>> GetAllPersons() => await _ctx.Accounts.OrderBy(ac => ac.CreatedAt).ToListAsync();

    public async Task<Account> GetAccountByNumber(string accountIdOrNumber)
    {
        if (string.IsNullOrWhiteSpace(accountIdOrNumber))
            throw new ArgumentException("Debe enviar un valor válido para identificar la cuenta.");

        if (Guid.TryParse(accountIdOrNumber, out Guid accountId))
        {
            var account = await _ctx.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            if (account != null) return account;
        }

        var accountByNumber = await _ctx.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountIdOrNumber);
        if (accountByNumber != null) return accountByNumber;

        throw new KeyNotFoundException("Cuenta no encontrada por Id ni por número de cuenta.");
    }
    public async Task<Account> OpenAccount(PersonRequestDto personRequest, string accountType)
    {
        var accountNumber = StringHelper.GenerateAccountNumber();
        var parsedType = AccountTypes.Parse(accountType);
        var person = await PersonApiHelper.GetOrCreatePersonAsync(personRequest);
        var isExist = await _ctx.Accounts.AnyAsync(a => a.PersonId == person.Id && a.AccountType == accountType);

        if (isExist)
            throw new InvalidOperationException($"La persona ya tiene una cuenta del tipo '{accountType}'.");

        var account = new Account
        {
            PersonId = person.Id,
            OwnerName = StringHelper.ToTitleCase(person.Name),
            AccountType = parsedType,
            AccountNumber = accountNumber,
            Balance = 0,
            Status = true,
            CreatedAt = DateTime.UtcNow
        };

        await MovementApiHelper.SendMovementAsync(new MovementNotification(
            account.AccountNumber,
            account.AccountType,
            "Account Creation",
            0,
            account.Balance,
            DateTime.UtcNow
        ));

        _ctx.Accounts.Add(account);
        await _ctx.SaveChangesAsync();
        return account;
    }

    public async Task<Account> DepositAsync(string accountIdOrNumber, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("El monto a depositar debe ser mayor a cero.");

        var account = await GetAccountByNumber(accountIdOrNumber);

        if (account.Balance < 0)
        {
            decimal deuda = -account.Balance;

            if (amount >= deuda)
            {
                account.Balance = 0;
                amount -= deuda;

                if (amount > 0)
                    account.Balance += amount;
            }
            else account.Balance += amount;

            await _ctx.SaveChangesAsync();

            return account;
        }

        account.Balance += amount;

        await _ctx.SaveChangesAsync();
        await MovementApiHelper.SendMovementAsync(new MovementNotification(
          account.AccountNumber,
          account.AccountType,
          "DEPOSIT",
          +amount,
          account.Balance,
          DateTime.UtcNow
        ));
        return account;
    }


    public async Task<Account> LoanAsync(string accountIdOrNumber, decimal amount)
    {
        var account = await GetAccountByNumber(accountIdOrNumber);

        account.Balance -= amount;

        await _ctx.SaveChangesAsync();

        await MovementApiHelper.SendMovementAsync(new MovementNotification(
            account.AccountNumber,
            account.AccountType,
            "LOAN",
            -amount,
            account.Balance,
            DateTime.UtcNow
        ));

        return account;
    }



    public async Task<Account> TransferAsync(string fromId, string toId, decimal amount)
    {
        var from = await GetAccountByNumber(fromId);
        var to = await GetAccountByNumber(toId);

        if (from.Balance < amount)
            throw new InvalidOperationException("Saldo insuficiente.");

        from.Balance -= amount;
        to.Balance += amount;

        await _ctx.SaveChangesAsync();

        await MovementApiHelper.SendMovementAsync(new MovementNotification(
            from.AccountNumber,
            from.AccountType,
            "TRANSFER_OUT",
            -amount,
            from.Balance,
            DateTime.UtcNow
        ));

        await MovementApiHelper.SendMovementAsync(new MovementNotification(
            to.AccountNumber,
            to.AccountType,
            "TRANSFER_IN",
            +amount,
            to.Balance,
            DateTime.UtcNow
        ));

        return from;
    }


    public async Task<Account> WithdrawAsync(string accountIdOrNumber, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("El monto debe ser mayor a cero.");

        var account = await GetAccountByNumber(accountIdOrNumber);

        if (account.Balance < amount)
            throw new InvalidOperationException("Saldo insuficiente.");

        account.Balance -= amount;

        await _ctx.SaveChangesAsync();

        await MovementApiHelper.SendMovementAsync(new MovementNotification(
            account.AccountNumber,
            account.AccountType,
            "WITHDRAW",
            -amount,
            account.Balance,
            DateTime.UtcNow
        ));

        return account;
    }

}
