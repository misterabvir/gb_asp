namespace Presentation;

public static class ConfigurateApp
{
    public static WebApplication Configurate(this WebApplication app)
    {
        return app
            .UseSwagger()
            .UsePipelines();
    }

    private static WebApplication UsePipelines(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        return app;
    }

    private static WebApplication UseSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}
