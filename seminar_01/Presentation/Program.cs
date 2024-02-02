using Persistence;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddPresentation();


builder
    .Build()
    .Configurate()
    .Run();

