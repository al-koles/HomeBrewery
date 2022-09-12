using HomeBrewery.Domain;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Application.Interfaces;

public interface IHomeBreweryDbContext
{
    DbSet<HBUser> Users { get; set; }
    DbSet<HBRole> Roles { get; set; }
    DbSet<HBUserRole> UserRoles { get; set; }
    DbSet<Recipe> Recipes { get; set; }
    DbSet<UserRecipe> UserRecipes { get; set; }
    DbSet<Sample> Samples { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}