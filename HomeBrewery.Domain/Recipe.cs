namespace HomeBrewery.Domain;

public class Recipe : Entity
{
    public Recipe()
    {
        Users = new HashSet<HBUser>();
        UserRecipes = new HashSet<UserRecipe>();
    }
    
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ConfigLink { get; set; } = null!;
    public byte Abv { get; set; }
    public byte Ibu { get; set; }
    public byte Og { get; set; }
    public byte Fg { get; set; }
    public byte Ba { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<HBUser> Users { get; set; }
    public virtual ICollection<UserRecipe> UserRecipes { get; set; }
}