using AudioStorage.Api;
using AudioStorage.Api.Extensions;
using AudioStorage.Infrastructure;
using DotNetEnv;

Env.Load("../../.env");

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
