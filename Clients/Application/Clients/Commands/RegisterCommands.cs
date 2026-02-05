using System;
using Application.DTOs;
using dv_apicommerce.Models.Dtos;
using MediatR;

namespace Application.Clients.Commands;

public record RegisterCommand(CreateUserDto Dto) : IRequest<UserDataDto>;
