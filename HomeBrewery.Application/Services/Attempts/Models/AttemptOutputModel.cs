using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Attempts.Models;

public class AttemptOutputModel : IMapWith<Attempt>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public DateTime StartTimestamp { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Attempt, AttemptOutputModel>()
            .ForMember(dst => dst.UserId,
                opt => opt.MapFrom(srs => srs.User.Id))
            .ForMember(dst => dst.RecipeId,
                opt => opt.MapFrom(srs => srs.Recipe.Id));
    }
}