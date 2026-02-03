using Application.Accounts.Queries;
using Application.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Accounts.QueryHandler;

public class GetAccountByIdOrNumberCommandHandler : IRequestHandler<GetAccountByIdOrNumberCommand, Account>
{
    private readonly IAccountRepository _repo;
    public GetAccountByIdOrNumberCommandHandler(IAccountRepository repo) => _repo = repo;

    public async Task<Account> Handle(GetAccountByIdOrNumberCommand request, CancellationToken cancellationToken) => await _repo.GetAccountByNumber(request.AccountIdOrNumber);
}
