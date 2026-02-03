using System;
using Application.Abstraction;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonsDbContext _ctx;
    public PersonRepository(PersonsDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Person> CreatePerson(Person toCreate)
    {
        _ctx.Persons.Add(toCreate);
        await _ctx.SaveChangesAsync();
        return toCreate;
    }

    public async Task DeletePerson(Guid PersonId)
    {
        var post = await _ctx.Persons.FirstOrDefaultAsync(p => p.Id == PersonId);
        if (post == null) return;
        _ctx.Remove(post);
        await _ctx.SaveChangesAsync();
    }


    public async Task<ICollection<Person>> GetAllPersons() => await _ctx.Persons.OrderBy(p => p.Name).ToListAsync();


    public async Task<Person> GetPersonById(Guid PersonId) => await _ctx.Persons.FirstOrDefaultAsync(p => p.Id == PersonId);
    public async Task<Person> GetPersonByIdentification(string PersonIdentification) =>
        await _ctx.Persons.FirstOrDefaultAsync(p => p.Identification == PersonIdentification);

    public Task<Person> GetOrCreatePerson(Person toCreate)
    {
        var person = _ctx.Persons.FirstOrDefault(p => p.Identification == toCreate.Identification);
        if (person == null)
            return CreatePerson(toCreate);
        return Task.FromResult(person);
    }

    public async Task<Person> UpdatePerson(Person toUpdate, Guid PersonId)
    {

        var person = await _ctx.Persons
            .FirstOrDefaultAsync(p => p.Id == PersonId);

        if (person is null)
            throw new KeyNotFoundException("Person not found");

        person.Name = toUpdate.Name;
        person.Gender = toUpdate.Gender;
        person.Age = toUpdate.Age;
        person.Identification = toUpdate.Identification;
        person.Address = toUpdate.Address;
        person.PhoneNumber = toUpdate.PhoneNumber;

        await _ctx.SaveChangesAsync();

        return person;
    }

}
