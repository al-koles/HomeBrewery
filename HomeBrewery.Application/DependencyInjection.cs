using HomeBrewery.Application.Services.Attempts;
using HomeBrewery.Application.Services.Auth;
using HomeBrewery.Application.Services.Recipes;
using HomeBrewery.Application.Services.Samples;
using HomeBrewery.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using IAttemptsService = HomeBrewery.Application.Services.Attempts.IAttemptsService;

namespace HomeBrewery.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IRecipesService, RecipesService>();
        services.AddTransient<IAttemptsService, AttemptsService>();
        services.AddTransient<ISamplesService, SamplesService>();

        return services;
    }
}