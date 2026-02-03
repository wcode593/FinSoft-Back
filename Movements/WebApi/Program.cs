using MinimalAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

/* builder.Services.AddMassTransit(conf =>
{
    conf.SetKebabCaseEndpointNameFormatter();
    conf.SetInMemorySagaRepositoryProvider();

    var asb = typeof(Program).Assembly;

    conf.AddConsumers(asb);
    conf.AddSagaStateMachines(asb);
    conf.AddSagas(asb);
    conf.AddActivities(asb);

    conf.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("RabbitMQ");

        cfg.Host(
            rabbit["Host"] ?? "localhost",

            "/",
            h =>
            {
                h.Username(rabbit["User"]);
                h.Password(rabbit["Pass"]);
            });

        cfg.ConfigureEndpoints(context);
    });
});
 */

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterEndpointDefinitions();
app.Run();
