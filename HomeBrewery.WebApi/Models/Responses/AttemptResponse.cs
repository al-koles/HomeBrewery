using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Attempts.Models;

namespace HomeBrewery.WebApi.Models.Responses;

public class AttemptResponse : IMapWith<AttemptOutputModel>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public DateTime StartTimestamp { get; set; }
}