using System;
using Domain;
using MediatR;

namespace Application.Persons.Queries;

public class GetPersonById : IRequest<Person>
{
    public Guid PersonId { get; set; }
}
