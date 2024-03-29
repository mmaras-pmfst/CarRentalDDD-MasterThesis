using Application;
using Application.Behaviors;
using FluentValidation;
using Infrastructure;
using Infrastructure.DataSeed;
using MediatR;
using Persistence;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddConfigurations();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);


builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ApplicationDataSeed.Seed(app);

app.Run();
