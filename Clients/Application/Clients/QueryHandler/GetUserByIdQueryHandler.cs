using System;
using Application.Clients.Queries;
using Application.DTOs;
using Application.IRepository;
using MapsterMapper;
using MediatR;

namespace Application.Clients.QueryHandler;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDataDto?>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UserDataDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await _repo.GetUserAsync(request.Id);
        return user is null ? null : _mapper.Map<UserDataDto>(user);
    }
}
