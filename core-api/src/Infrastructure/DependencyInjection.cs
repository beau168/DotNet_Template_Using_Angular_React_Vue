using CoreApi.Application.Common.Interfaces;
using CoreApi.Domain.Interfaces;
using CoreApi.Infrastructure.Auth;
using CoreApi.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        return services;
    }
}
