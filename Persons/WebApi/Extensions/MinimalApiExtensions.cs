using Application.Abstraction;
using Application.Persons.Commands;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Abstractions;

namespace MinimalAPI.Extensions;

public static class MinimalApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<PersonsDbContext>(options =>
        options.UseNpgsql(connectionString: conn, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorCodesToAdd: null
            );
        }));
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
        builder.Services.AddMediatR(typeof(CreatePerson));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDefinition in endpointDefinitions) endpointDefinition.RegisterEndpoints(app);
    }

}
