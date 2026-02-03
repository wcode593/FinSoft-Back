using System;
using Application.Abstraction;
using Domain;
using MediatR;

namespace Application.Persons.Queries;

public class GetAllPerson : IRequest<ICollection<Person>> { }
