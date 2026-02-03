using MassTransit;
using MinimalAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.RegisterServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.RegisterEndpointDefinitions();
app.Run();
