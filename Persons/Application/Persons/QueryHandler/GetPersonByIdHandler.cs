using Application.Abstraction;
using Application.Persons.Queries;
using Domain;
using MediatR;

namespace Application.Persons.QueryHandler;

public class GetPersonByIdHandler : IRequestHandler<GetPersonById, Person>
{
    private readonly IPersonRepository _personRepository;
    public GetPersonByIdHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public Task<Person> Handle(GetPersonById request, CancellationToken cancellationToken) => _personRepository.GetPersonById(request.PersonId);
}
