using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands;

public record WithdrawCommand(string AccountIdOrNumber, decimal Amount) : IRequest<Account>;
