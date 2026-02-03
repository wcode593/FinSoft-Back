using Domain;

namespace Application.Abstraction;

public interface IPersonRepository
{
    Task<ICollection<Person>> GetAllPersons();
    Task<Person> GetPersonById(Guid PersonId);
    Task<Person> GetOrCreatePerson(Person toCreate);
    Task<Person> GetPersonByIdentification(string PersonIdentification);
    Task<Person> CreatePerson(Person toCreate);
    Task<Person> UpdatePerson(Person toUpdate, Guid PersonId);
    Task DeletePerson(Guid PersonId);
}
