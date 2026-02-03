using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands;

public record DepositCommand(string AccountIdOrNumber, decimal Amount) : IRequest<Account>;
