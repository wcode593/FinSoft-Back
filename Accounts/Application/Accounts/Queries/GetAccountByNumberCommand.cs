using System;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Queries;

public record GetAccountByIdOrNumberCommand(string AccountIdOrNumber) : IRequest<Account>;
