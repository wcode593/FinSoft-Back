using Application.Accounts.Commands;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.ComandHandlers;

public class WithdrawHandler : IRequestHandler<WithdrawCommand, Account>
{
    private readonly IAccountRepository _repo;
    public WithdrawHandler(IAccountRepository repo) => _repo = repo;

    public async Task<Account> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        => await _repo.WithdrawAsync(request.AccountIdOrNumber, request.Amount);
}
