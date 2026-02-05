using Application.Clients.Commands;
using Application.DTOs;
using Application.IRepository;
using MediatR;

namespace Application.Clients.ComandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDataDto>
{
    private readonly IUserRepository _repo;
    public RegisterCommandHandler(IUserRepository repo) => _repo = repo;
    public async Task<UserDataDto> Handle(RegisterCommand request, CancellationToken ct) => await _repo.Register(request.Dto);
}
