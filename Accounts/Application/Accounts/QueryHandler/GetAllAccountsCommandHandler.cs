using System;
using Application.Accounts.Queries;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.QueryHandler;

public class GetAllAccountsCommandHandler : IRequestHandler<GetAllAccountsCommand, IEnumerable<Account>>
{
    private readonly IAccountRepository _repo;
    public GetAllAccountsCommandHandler(IAccountRepository repo) => _repo = repo;

    public async Task<IEnumerable<Account>> Handle(GetAllAccountsCommand request, CancellationToken cancellationToken) => await _repo.GetAllPersons();
}
