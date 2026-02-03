using Application.Abstraction;
using Application.Persons.Commands;
using MediatR;

namespace Application.Posts.ComandHandlers;

public class DeletePostHandler : IRequestHandler<DeletePerson>
{

    private readonly IPersonRepository _personRepository;
    public DeletePostHandler(IPersonRepository personRepository) => _personRepository = personRepository;

    public async Task<Unit> Handle(DeletePerson request, CancellationToken cancellationToken)
    {
        await _personRepository.DeletePerson(request.PersonId);
        return Unit.Value;
    }
}
