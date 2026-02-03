using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands;

public record LoanCommand(string AccountIdOrNumber, decimal Amount) : IRequest<Account>;
