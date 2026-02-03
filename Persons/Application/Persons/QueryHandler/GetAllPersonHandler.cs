using System;
using Application.Abstraction;
using Application.Persons.Queries;
using Domain;
using MediatR;

namespace Application.Persons.QueryHandler;

public class GetAllPersonHandler : IRequestHandler<GetAllPerson, ICollection<Person>>
{
    private readonly IPersonRepository _personRepository;
    public GetAllPersonHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public async Task<ICollection<Person>> Handle(GetAllPerson request, CancellationToken cancellationToken) => await _personRepository.GetAllPersons();
}
