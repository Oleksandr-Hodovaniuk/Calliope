using Authentication.Api;
using Authentication.Api.Extensions;
using Authentication.Infrastructure;
using DotNetEnv;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

await app.InitialiseDatabaseAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
