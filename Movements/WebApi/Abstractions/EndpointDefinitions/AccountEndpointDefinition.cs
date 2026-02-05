
using Application.Abstraction;
using Domain.DTOs;

namespace MinimalAPI.Abstractions.EndpointDefinitions;

public class AccountEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var url_base = "/";
        var group = app.MapGroup(url_base);

        group.MapGet("{accountNumber}/{fechaInicio}/{fechaFin}", GetMovementsByDate).WithName("GetMovementsByDate");
        group.MapPost("", CreateMovement).WithName("CreateMovement");
    }

    private static async Task<IResult> GetMovementsByDate(
          string accountNumber,
          string fechaInicio,
          string fechaFin,
          IMovementRepository repo
  )
    {
        var result = await repo.GetAccountMovementsAsync(accountNumber, fechaInicio, fechaFin);
        return TypedResults.Ok(result);
    }
    private static async Task<IResult> CreateMovement(
            MovementCreatedDto request,
            IMovementRepository repo
    )
    {
        var created = await repo.AddMovementAsync(request);

        return TypedResults.Created($"/api/movements/{created.Id}", created);
    }
}
