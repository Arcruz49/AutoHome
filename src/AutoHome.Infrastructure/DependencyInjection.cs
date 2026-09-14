using AutoHome.Application.Abstractions;
using AutoHome.Application.Abstractions.Persistance;
using AutoHome.Application.Commands;
using AutoHome.Domain.Abstractions.Repositories;
using AutoHome.Infrastructure.Auth;
using AutoHome.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoHome.Infrastructure.Data;

namespace AutoHome.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));

        services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<IConfigRepository, ConfigRepository>();
        services.AddScoped<ITokenIssuer, JwtTokenIssuer>();

        return services;
    }
}