using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MigrateDbContextIfNecessary<TDbContext>(this WebApplication app) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TDbContext>>();
        var dbContext = scope.ServiceProvider.GetService<TDbContext>();

        try
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation($"Migrating database associated with context {typeof(TDbContext).Name}");
                dbContext.Database.Migrate();
                logger.LogInformation($"Migrated database associated with context {typeof(TDbContext).Name}");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e,
                $"An error occurred while migrating the database used on context {typeof(TDbContext).Name}");
        }
    }
}