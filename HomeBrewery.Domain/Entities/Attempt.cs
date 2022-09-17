using HomeBrewery.Domain.Data;

namespace HomeBrewery.Domain.Entities;

public class Attempt : Entity
{
    public Attempt()
    {
        Samples = new HashSet<Sample>();
    }

    public HBUser User { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
    public DateTime StartTimestamp { get; set; }
    public ICollection<Sample> Samples { get; set; }
}