namespace HomeBrewery.Domain;

public enum SampleType
{
    Extract,
    Gravity,
    Density,
    Alcohol,
    Sugar,
    Temperature
}

public class Sample : Entity
{
    public SampleType Type { get; set; }
    public DateTime Timestamp { get; set; }

    public Attempt Attempt { get; set; } = null!;
}