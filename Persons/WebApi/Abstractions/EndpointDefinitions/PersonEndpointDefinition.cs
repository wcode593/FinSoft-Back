using Application.Persons.Commands;
using Application.Persons.Queries;
using Domain;
using MediatR;

namespace MinimalAPI.Abstractions.EndpointDefinitions;

public class PersonEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var url_base = "/";
        var posts = app.MapGroup(url_base);

        posts.MapGet("", GetAllPerson);
        posts.MapGet("by-id/{id}", GetPersonById).WithName("GetPersonById");
        posts.MapPost("get-create", GetOrCreatePerson).WithName("GetOrCreatePerson");
        posts.MapGet("by-identification/{identification}", GetPersonByIdentification).WithName("GetPersonByIdentification");
        posts.MapPost("", CreatePerson);
        posts.MapPut("{id}", UpadatePerson);
        posts.MapDelete("{id}", RemovePerson);
    }

    private static async Task<IResult> GetOrCreatePerson(IMediator mediator, Person person)
    {
        var command = new GetOrCreatePerson { Person = person };
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetAllPerson(IMediator mediator)
    {
        var getCommand = new GetAllPerson();
        var post = await mediator.Send(getCommand);
        return TypedResults.Ok(post);
    }

    private async Task<IResult> GetPersonById(IMediator mediator, Guid id)
    {
        var getPost = new GetPersonById { PersonId = id };
        var post = await mediator.Send(getPost);
        return Results.Ok(post);
    }
    private async Task<IResult> GetPersonByIdentification(IMediator mediator, string identification)
    {
        var getPost = new GetPersonByIdentification { Identification = identification };
        var post = await mediator.Send(getPost);
        return Results.Ok(post);
    }

    private async Task<IResult> CreatePerson(IMediator mediator, Person post)
    {
        var createPost = new CreatePerson { Person = post };
        var createdPost = await mediator.Send(createPost);
        return Results.CreatedAtRoute("GetPersonById", new { id = post.Id }, createdPost);
    }

    private async Task<IResult> UpadatePerson(IMediator mediator, Person post, Guid id)
    {
        var updatePost = new UpdatePerson { Person = post, PersonId = id };
        var updatedPost = await mediator.Send(updatePost);
        return TypedResults.Ok(updatedPost);
    }

    private async Task<IResult> RemovePerson(IMediator mediator, Guid id)
    {
        var removePost = new DeletePerson { PersonId = id };
        await mediator.Send(removePost);
        return TypedResults.NoContent();
    }
}
