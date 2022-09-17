using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Samples.Models;

namespace HomeBrewery.WebApi.Models.Responses;

public class SampleResponse : IMapWith<SampleOutputModel>
{
    public int Id { get; set; }
    public int AttemptId { get; set; }
    public string Type { get; set; } = null!;
    public string Value { get; set; } = null!;
    public DateTime Timestamp { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SampleOutputModel, SampleResponse>()
            .ForMember(dst => dst.Type,
                opt => opt.MapFrom(srs => srs.Type.ToString()));
    }
}