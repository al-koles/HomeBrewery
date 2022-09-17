using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Recipes.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class RecipeCreateRequest : IMapWith<RecipeCreateModel>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Text { get; set; } = null!;
    public byte Abv { get; set; }
    public byte Ibu { get; set; }
    public byte Og { get; set; }
    public byte Fg { get; set; }
    public byte Ba { get; set; }
    public decimal Price { get; set; }
}