namespace HomeBrewery.Domain;

public class Attempt : Entity
{
    public Attempt()
    {
        Samples = new HashSet<Sample>();
    }
    
    public HBUser User { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
    public DateTime StartDateTime { get; set; }

    public ICollection<Sample> Samples { get; set; }
}