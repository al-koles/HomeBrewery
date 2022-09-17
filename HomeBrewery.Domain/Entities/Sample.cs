using HomeBrewery.Domain.Data;

namespace HomeBrewery.Domain.Entities;

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
    public string Value { get; set; } = null!;
    public DateTime Timestamp { get; set; }

    public Attempt Attempt { get; set; } = null!;
}