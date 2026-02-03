using Application.Abstraction;
using DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Abstractions.EndpointDefinitions;
using Movements.DataAccess.Repository;
using SharedContracts.Contract;

namespace MinimalAPI.Extensions;

public static class MinimalApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<MovementsDbContext>(options =>
        options.UseNpgsql(connectionString: conn, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorCodesToAdd: null
            );
        }));

        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    public static void RegisterEndpointDefinitions(this WebApplication app) =>
        new AccountEndpointDefinition().RegisterEndpoints(app);
}
