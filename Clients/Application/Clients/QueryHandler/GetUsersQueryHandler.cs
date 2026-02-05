using Application.Clients.Queries;
using Application.IRepository;
using Domain;
using MediatR;

namespace Application.Clients.QueryHandler;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<Client>>
{
    private readonly IUserRepository _repo;
    public GetUsersQueryHandler(IUserRepository repo) => _repo = repo;

    public async Task<List<Client>> Handle(GetUsersQuery request, CancellationToken ct) => await _repo.GetUsersAsync();
}
