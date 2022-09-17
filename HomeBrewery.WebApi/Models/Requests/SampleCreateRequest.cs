using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Samples.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class SampleCreateRequest : IMapWith<SampleCreateModel>
{
    public int AttemptId { get; set; }
    public string Type { get; set; } = null!;
    public string Value { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SampleCreateRequest, SampleCreateModel>()
            .ForMember(dst => dst.Timestamp,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(SampleCreateModel.Timestamp)]))
            .ForMember(dst => dst.Type,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(SampleCreateModel.Type)]));
    }
}