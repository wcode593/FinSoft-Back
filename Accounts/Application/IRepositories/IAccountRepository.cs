using Application.Accounts.DTOs;
using Domain.Models;

namespace Application.IRepositories;

public interface IAccountRepository
{
    Task<ICollection<Account>> GetAllPersons();
    Task<Account> OpenAccount(PersonRequestDto personRequest, string accountType);
    Task<Account> GetAccountByNumber(string accountIdOrNumber);
    // Finanzas
    Task<Account> DepositAsync(string accountIdOrNumber, decimal amount);
    Task<Account> WithdrawAsync(string accountIdOrNumber, decimal amount);
    Task<Account> LoanAsync(string accountIdOrNumber, decimal amount);
    Task<Account> TransferAsync(string fromAccountIdOrNumber, string toAccountIdOrNumber, decimal amount);
}
