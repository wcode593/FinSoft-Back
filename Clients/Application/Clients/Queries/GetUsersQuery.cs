using System;
using Domain;
using MediatR;

namespace Application.Clients.Queries;

public record GetUsersQuery() : IRequest<List<Client>>;
