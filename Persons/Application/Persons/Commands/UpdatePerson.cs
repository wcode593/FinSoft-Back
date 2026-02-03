using System;
using Domain;
using MediatR;

namespace Application.Persons.Commands;

public class UpdatePerson : IRequest<Person>
{
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
}
