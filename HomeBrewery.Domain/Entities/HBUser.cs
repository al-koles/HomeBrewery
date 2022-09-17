using Microsoft.AspNetCore.Identity;

namespace HomeBrewery.Domain.Entities;

public class HBUser : IdentityUser<int>
{
    public HBUser()
    {
        UserRoles = new HashSet<HBUserRole>();
        Recipes = new HashSet<Recipe>();
        UserRecipes = new HashSet<Attempt>();
    }

    public virtual ICollection<HBUserRole> UserRoles { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; }
    public virtual ICollection<Attempt> UserRecipes { get; set; }
}