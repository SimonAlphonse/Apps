using Presenation.Extensions.DependencyInjection;
using Persistence.Extensions.DependencyInjection;
using Application.Extensions.DependencyInjection;
using Infrastructure.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresenation();
builder.Services.AddPersistence();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UsePresentation();
app.Services.UsePersistence();

app.Run();