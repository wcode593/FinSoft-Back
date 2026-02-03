using System;
using Domain;
using MediatR;

namespace Application.Persons.Commands;

public class CreatePerson : IRequest<Person>
{
    public Person Person { get; set; }
}
