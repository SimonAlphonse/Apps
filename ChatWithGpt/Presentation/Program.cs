using Presenation.Extensions.DependencyInjection;
using Persistence.Extensions.DependencyInjection;
using Application.Extensions.DependencyInjection;
using Infrastructure.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurePresenationServices();
builder.Services.ConfigurePersistenceServices();
builder.Services.ConfigureInfrastructureServices();
builder.Services.ConfigureApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.ConfigureDatabase();

app.Run();