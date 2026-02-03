using Application.Abstraction;
using Application.Persons.Commands;
using Domain;
using MediatR;

namespace Application.Persons.ComandHandlers;

public class UpdatePersonHandler : IRequestHandler<UpdatePerson, Person>
{
    private readonly IPersonRepository _personRepository;
    public UpdatePersonHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public async Task<Person> Handle(UpdatePerson request, CancellationToken cancellationToken) =>
        await _personRepository.UpdatePerson(request.Person, request.PersonId);
}
