using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserStatsService, UserStatsService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IUserDetailService, UserDetailService>();
        services.AddScoped<IPostcardDataService, PostcardDataService>();
        services.AddScoped<IPostcardService, PostcardService>();
        services.AddScoped<IUserFriendsService, UserFriendsService>();
        services.AddScoped<IPostcardCollectionService, PostcardCollectionService>();

        return services;
    }
}