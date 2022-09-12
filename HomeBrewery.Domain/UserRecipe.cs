namespace HomeBrewery.Domain;

public class UserRecipe : Entity
{
    public UserRecipe()
    {
        Samples = new HashSet<Sample>();
    }
    
    public HBUser User { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
    
    public ICollection<Sample> Samples { get; set; }
}