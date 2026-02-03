using System;
using MediatR;

namespace Application.Persons.Commands;

public class DeletePerson : IRequest
{
    public Guid PersonId { get; set; }
}
