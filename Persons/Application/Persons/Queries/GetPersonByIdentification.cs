using System;
using Domain;
using MediatR;

namespace Application.Persons.Queries;

public class GetPersonByIdentification : IRequest<Person>
{
    public string Identification { get; set; }
}
