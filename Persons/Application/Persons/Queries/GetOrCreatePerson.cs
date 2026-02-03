using System;
using Domain;
using MediatR;

namespace Application.Persons.Queries;

public class GetOrCreatePerson : IRequest<Person>
{
    public Person Person { get; set; }
}
