namespace HomeBrewery.Domain;

public class Sample : Entity
{
    public double? Extract { get; set; }
    public double? Gravity { get; set; }
    public double? Density { get; set; }
    public double? Alcohol { get; set; }
    public double? Sugar { get; set; }
    public double? Temperature { get; set; }

    public UserRecipe UserRecipe { get; set; } = null!;
}