using dv_apicommerce.Models.Dtos;
using MediatR;

namespace Application.Clients.Commands;

public record LoginCommand(UserLoginDto Dto) : IRequest<UserLoginResponseDto>;
