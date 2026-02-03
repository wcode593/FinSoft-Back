using Application.Accounts.Commands;
using Application.IRepositories;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Abstract;

namespace MinimalAPI.Extensions;

public static class MinimalApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(conn))
            throw new InvalidOperationException(
                "La cadena de conexión 'conn' no está definida en appsettings.json"
            );

        builder.Services.AddDbContext<AccountsDbContext>(options =>
        options.UseNpgsql(connectionString: conn, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorCodesToAdd: null
            );
        })
    );
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<OpenAccountCommand>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }


    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointTypes = typeof(Program).Assembly
            .GetTypes()
            .Where(t =>
                typeof(IEndpointDefinitionAccount).IsAssignableFrom(t) &&
                !t.IsInterface &&
                !t.IsAbstract);

        foreach (var type in endpointTypes)
        {
            var instance = (IEndpointDefinitionAccount)Activator.CreateInstance(type)!;
            instance.RegisterAccountEndpoints(app);
        }
    }
}
