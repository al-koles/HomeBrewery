using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Recipes.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class RecipePatchRequest : IMapWith<RecipePatchModel>
{
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
        profile.CreateMap<RecipePatchRequest, RecipePatchModel>()
            .ForMember(dst => dst.Id,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(RecipePatchModel.Id)]));
    }
}