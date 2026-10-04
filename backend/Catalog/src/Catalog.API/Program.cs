using Catalog.API;
using Catalog.API.Extensions;
using Catalog.Infrastructure;
using Catalog.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("Angular");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

await app.InitialiseDatabaseAsync();

app.MapControllers();

app.Run();
