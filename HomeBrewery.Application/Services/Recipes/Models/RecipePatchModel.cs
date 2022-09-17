using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain.Entities;

namespace HomeBrewery.Application.Services.Recipes.Models;

public class RecipePatchModel : IMapWith<Recipe>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Text { get; set; }
    public byte? Abv { get; set; }
    public byte? Ibu { get; set; }
    public byte? Og { get; set; }
    public byte? Fg { get; set; }
    public byte? Ba { get; set; }
    public decimal? Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RecipePatchModel, Recipe>()
            .ForAllMembers(opt => opt.Condition((_, _, srsMember, _) => srsMember != null));
    }
}