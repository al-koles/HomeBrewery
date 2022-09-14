using HomeBrewery.Application.Interfaces;
using HomeBrewery.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Persistence;

public class HomeBreweryDbContext : IdentityDbContext<HBUser, HBRole, int, 
    IdentityUserClaim<int>, HBUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, 
    IdentityUserToken<int>>, IHomeBreweryDbContext
{
    public HomeBreweryDbContext(DbContextOptions options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public virtual DbSet<Recipe> Recipes { get; set; } = null!;
    public virtual DbSet<UserRecipe> UserRecipes { get; set; } = null!;
    public virtual DbSet<Sample> Samples { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HBUser>()
            .HasMany(e => e.Recipes)
            .WithMany(e => e.Users)
            .UsingEntity<UserRecipe>(
                e => e.HasOne(ur => ur.Recipe).WithMany(ur => ur.UserRecipes),
                e => e.HasOne(ur => ur.User).WithMany(ur => ur.UserRecipes));

        builder.Entity<HBUser>()
            .HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(e => e.UserId);
        
        builder.Entity<HBRole>()
            .HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(e => e.RoleId);

        base.OnModelCreating(builder);
    }
}