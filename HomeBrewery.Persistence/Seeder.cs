using HomeBrewery.Domain;
using HomeBrewery.Domain.Data;
using HomeBrewery.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Persistence;

public static class Seeder
{
    public static ModelBuilder SeedAdminAndRoles(this ModelBuilder builder, DefaultAdminSettings defaultAdminSettings)
    {
        var roles = new HBRole[]
        {
            new()
            {
                Id = 1,
                Name = Role.User.ToString(),
                NormalizedName = Role.User.ToString().ToUpper()
            },
            new()
            {
                Id = 2,
                Name = Role.Admin.ToString(),
                NormalizedName = Role.Admin.ToString().ToUpper()
            },
        };

        builder.Entity<HBRole>().HasData(roles);
        
        var admin = new HBUser()
        {
            Id = 1,
            UserName = defaultAdminSettings.Email,
            Email = defaultAdminSettings.Email,
            NormalizedUserName = defaultAdminSettings.Email.ToUpper(),
            NormalizedEmail = defaultAdminSettings.Email.ToUpper(),
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        
        var passwordHasher = new PasswordHasher<HBUser>();
        admin.PasswordHash = passwordHasher.HashPassword(admin, defaultAdminSettings.Password);

        builder.Entity<HBUser>().HasData(admin);

        var adminRoles = new HBUserRole[]
        {
            new()
            {
                UserId = admin.Id,
                RoleId = roles[0].Id
            },
            new()
            {
                UserId = admin.Id,
                RoleId = roles[1].Id
            }
        };
        builder.Entity<HBUserRole>().HasData(adminRoles);

        return builder;
    }
}