using Application.Accounts.Commands;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.ComandHandlers;

public class DepositCommandHandler : IRequestHandler<DepositCommand, Account>
{
    private readonly IAccountRepository _repo;

    public DepositCommandHandler(IAccountRepository repo) => _repo = repo;
    public async Task<Account> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        return await _repo.DepositAsync(request.AccountIdOrNumber, request.Amount);
    }
}
