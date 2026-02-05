using System;
using Application.Clients.Commands;
using Application.Clients.Queries;
using Application.Clients.QueryHandler;
using MediatR;

namespace WebApi.Abstract.EndpointDefinition;

public class ClientEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var url_base = "/api/clients";
        var group = app.MapGroup(url_base);

        group.MapPost("/login", login).WithName("login");
        group.MapPost("/register", register).WithName("register");
        group.MapGet("/", GetAllClients).WithName("GetAllClients");
        group.MapGet("/{id}", GetClientById).WithName("GetClientById");
    }

    private static async Task<IResult> GetAllClients(IMediator mediator)
    {
        var getClients = new GetUsersQuery();
        var client = await mediator.Send(getClients);
        return TypedResults.Ok(client);
    }

    private static async Task<IResult> GetClientById(IMediator mediator, string id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id), ct);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private static async Task<IResult> login(LoginCommand command, IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> register(RegisterCommand command, IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Results.Ok(result);
    }
}
