using HomeBrewery.Application.Services.Auth;
using HomeBrewery.Application.Services.Recipes;
using HomeBrewery.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HomeBrewery.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IRecipesService, RecipesService>();

        return services;
    }
}