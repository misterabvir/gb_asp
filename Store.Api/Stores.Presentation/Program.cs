using StorePersistence;
using StorePresentation;
using StoreApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddPresentation();


builder
    .Build()
    .ConfigurateRequestPipelines()
    .Run();

