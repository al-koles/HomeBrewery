using HomeBrewery.Application.Interfaces;
using HomeBrewery.Domain.Entities;
using HomeBrewery.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeBrewery.Persistence;

public class HomeBreweryDbContext : IdentityDbContext<HBUser, HBRole, int,
    IdentityUserClaim<int>, HBUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
    IdentityUserToken<int>>, IHomeBreweryDbContext
{
    private readonly DefaultAdminSettings _adminSettings;

    public HomeBreweryDbContext(DbContextOptions options, IOptions<DefaultAdminSettings> adminSettings) : base(options)
    {
        _adminSettings = adminSettings.Value;
    }

    public virtual DbSet<Recipe> Recipes { get; set; } = null!;
    public virtual DbSet<Attempt> Attempts { get; set; } = null!;
    public virtual DbSet<Sample> Samples { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HBUser>()
            .HasMany(e => e.Recipes)
            .WithMany(e => e.Users)
            .UsingEntity<Attempt>(
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

        builder.SeedAdminAndRoles(_adminSettings);
    }
}