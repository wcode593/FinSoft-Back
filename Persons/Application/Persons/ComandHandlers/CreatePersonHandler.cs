using System;
using Application.Abstraction;
using Application.Persons.Commands;
using Domain;
using MediatR;

namespace Application.Persons.ComandHandlers;

public class CreatePersonHandler : IRequestHandler<CreatePerson, Person>
{
    private readonly IPersonRepository _personRepository;
    public CreatePersonHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public async Task<Person> Handle(CreatePerson request, CancellationToken cancellationToken) => await _personRepository.CreatePerson(request.Person);
}
