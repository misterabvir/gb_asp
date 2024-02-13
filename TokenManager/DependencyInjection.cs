using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TokenManager.Base;
using TokenManager.Security;
using TokenManager.Services;

namespace TokenManager;

public static class DependencyInjection
{
    public static IServiceCollection AddTokenManager(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddSwaggerService();

        services.AddSingleton<ITokenService, TokenService>();
        return services;
    }
    
    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwt = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
            ?? throw new Exception("Jwt configuration not found");

        services.AddSingleton(provider => jwt);

        services.AddAuthorization();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = jwt.PublicKey
                };
            });

        return services;
    }

    private static IServiceCollection AddSwaggerService(
    this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                options.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new()
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "Jwt Token",
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });

                options.AddSecurityRequirement(new() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
            }
        );

        return services;
    }
}
