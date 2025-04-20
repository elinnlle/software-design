using MediatR;
using MOSZoo.Application.Extensions;
using MOSZoo.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Swagger + контроллеры
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// бизнес‑слой и инфраструктура
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// MediatR из всех сборок решения
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(MOSZoo.Domain.Common.IDomainEvent).Assembly));

// сборка приложения
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();