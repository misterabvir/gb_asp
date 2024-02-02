namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        return services;
    }
}
