using HomeBrewery.Domain.Data;

namespace HomeBrewery.Domain.Entities;

public class Recipe : Entity
{
    public Recipe()
    {
        Users = new HashSet<HBUser>();
        UserRecipes = new HashSet<Attempt>();
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Text { get; set; } = null!;
    public byte Abv { get; set; }
    public byte Ibu { get; set; }
    public byte Og { get; set; }
    public byte Fg { get; set; }
    public byte Ba { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<HBUser> Users { get; set; }
    public virtual ICollection<Attempt> UserRecipes { get; set; }
}