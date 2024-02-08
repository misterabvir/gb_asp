using ProductPersistence;
using ProductPresentation;
using ProductApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddPresentation();


builder
    .Build()
    .ConfigurateRequestPipelines()
    .Run();

