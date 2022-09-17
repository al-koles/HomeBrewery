using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Attempts.Models;

public class AttemptCreateModel : IMapWith<Attempt>
{
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public DateTime StartTimestamp { get; set; }
}