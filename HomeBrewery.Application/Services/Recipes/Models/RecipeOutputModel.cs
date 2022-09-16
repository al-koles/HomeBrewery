using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Recipes.Models;

public class RecipeOutputModel : IMapWith<Recipe>
{
    public RecipeOutputModel()
    {
        Users = new HashSet<int>();
    }
    
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ConfigLink { get; set; } = null!;
    public byte Abv { get; set; }
    public byte Ibu { get; set; }
    public byte Og { get; set; }
    public byte Fg { get; set; }
    public byte Ba { get; set; }
    public decimal Price { get; set; }

    public ICollection<int> Users { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Recipe, RecipeOutputModel>()
            .ForMember(dst => dst.Users,
                opt => opt.MapFrom(srs => srs.Users.Select(u => u.Id)));
    }
}