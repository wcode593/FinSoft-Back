using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands;

public record TransferCommand(string FromAccountIdOrNumber, string ToAccountIdOrNumber, decimal Amount) : IRequest<Account>;
