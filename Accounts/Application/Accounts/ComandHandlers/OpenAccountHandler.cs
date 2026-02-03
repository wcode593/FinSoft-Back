using System;
using Application.Accounts.Commands;
using Application.IRepositories;
using Domain.Models;
using MediatR;
using MediatR.Pipeline;

namespace Application.Accounts.ComandHandlers;

public class OpenAccountHandler : IRequestHandler<OpenAccountCommand, Account>
{
    private readonly IAccountRepository _repo;
    public OpenAccountHandler(IAccountRepository repo) => _repo = repo;

    public async Task<Account> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
    {
        return await _repo.OpenAccount(request.PersonRequest, request.AccountType);
    }
}
