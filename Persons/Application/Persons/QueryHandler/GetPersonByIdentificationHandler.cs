using System;
using Application.Abstraction;
using Application.Persons.Queries;
using Domain;
using MediatR;

namespace Application.Persons.QueryHandler;

public class GetPostByIdentificationHandler : IRequestHandler<GetPersonByIdentification, Person>

{
    private readonly IPersonRepository _personRepository;
    public GetPostByIdentificationHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public Task<Person> Handle(GetPersonByIdentification request, CancellationToken cancellationToken) => _personRepository.GetPersonByIdentification(request.Identification);
}
