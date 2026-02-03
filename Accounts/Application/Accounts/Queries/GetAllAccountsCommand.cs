using System;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Queries;

public class GetAllAccountsCommand : IRequest<IEnumerable<Account>> { }
