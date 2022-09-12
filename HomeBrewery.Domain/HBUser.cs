using Microsoft.AspNetCore.Identity;

namespace HomeBrewery.Domain;

public class HBUser : IdentityUser<int>
{
    public HBUser()
    {
        UserRoles = new List<HBUserRole>();
        Recipes = new HashSet<Recipe>();
        UserRecipes = new HashSet<UserRecipe>();
    }
    
    public virtual List<HBUserRole> UserRoles { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; }
    public virtual ICollection<UserRecipe> UserRecipes { get; set; }
}