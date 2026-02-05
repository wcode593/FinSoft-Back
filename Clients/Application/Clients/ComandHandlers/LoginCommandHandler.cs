using Application.Clients.Commands;
using Application.IRepository;
using dv_apicommerce.Models.Dtos;
using MediatR;

namespace Application.Clients.ComandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, UserLoginResponseDto>
{
    private readonly IUserRepository _repo;

    public LoginCommandHandler(IUserRepository repo) => _repo = repo;

    public async Task<UserLoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken) => await _repo.Login(request.Dto);
}
