using System;
using Application.Abstraction;
using Application.Persons.Queries;
using Domain;
using MediatR;

namespace Application.Persons.QueryHandler;

public class GetOrCreatePersonHandler : IRequestHandler<GetOrCreatePerson, Person>
{
    private readonly IPersonRepository _personRepository;
    public GetOrCreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    public Task<Person> Handle(GetOrCreatePerson request, CancellationToken cancellationToken) => _personRepository.GetOrCreatePerson(request.Person);
}
