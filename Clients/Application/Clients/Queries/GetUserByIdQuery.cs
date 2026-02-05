using System;
using Application.DTOs;
using MediatR;

namespace Application.Clients.Queries;

public record GetUserByIdQuery(string Id) : IRequest<UserDataDto?>;
