using Application.Accounts.Commands;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.ComandHandlers;

public class TransferHandler : IRequestHandler<TransferCommand, Account>
{
    private readonly IAccountRepository _repo;
    public TransferHandler(IAccountRepository repo) => _repo = repo;

    public async Task<Account> Handle(TransferCommand request, CancellationToken cancellationToken)
        => await _repo.TransferAsync(request.FromAccountIdOrNumber, request.ToAccountIdOrNumber, request.Amount);
}
