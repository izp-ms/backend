using Domain.Interfaces;
using Infrastructure.Data.Seeder;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<DataSeeder>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserStatsRepository, UserStatsRepository>();

        return services;
    }
}