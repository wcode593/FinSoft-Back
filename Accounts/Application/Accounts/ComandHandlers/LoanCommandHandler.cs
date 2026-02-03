using System;
using Application.Accounts.Commands;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.ComandHandlers;

public class LoanCommandHandler : IRequestHandler<LoanCommand, Account>
{
    private readonly IAccountRepository _repo;

    public LoanCommandHandler(IAccountRepository repo) => _repo = repo;
    public async Task<Account> Handle(LoanCommand request, CancellationToken cancellationToken) => await _repo.LoanAsync(request.AccountIdOrNumber, request.Amount);
}
