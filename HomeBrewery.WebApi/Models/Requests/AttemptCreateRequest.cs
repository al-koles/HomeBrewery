using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Attempts.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class AttemptCreateRequest : IMapWith<AttemptCreateModel>
{
    public int UserId { get; set; }
    public int RecipeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AttemptCreateRequest, AttemptCreateModel>()
            .ForMember(dst => dst.StartTimestamp,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(AttemptCreateModel.StartTimestamp)]));
    }
}