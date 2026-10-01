using Catalog.API;
using Catalog.API.Extensions;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

await app.InitialiseDatabaseAsync();

app.MapControllers();

app.Run();
