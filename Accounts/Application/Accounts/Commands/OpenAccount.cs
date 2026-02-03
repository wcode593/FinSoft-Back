using Application.Accounts.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands;

public record OpenAccountCommand(PersonRequestDto PersonRequest, string AccountType) : IRequest<Account>;
