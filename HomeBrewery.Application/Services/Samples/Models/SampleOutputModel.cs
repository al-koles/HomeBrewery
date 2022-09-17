using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain.Entities;

namespace HomeBrewery.Application.Services.Samples.Models;

public class SampleOutputModel : IMapWith<Sample>
{
    public int Id { get; set; }
    public int AttemptId { get; set; }
    public SampleType Type { get; set; }
    public string Value { get; set; } = null!;
    public DateTime Timestamp { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Sample, SampleOutputModel>()
            .ForMember(dst => dst.AttemptId,
                opt => opt.MapFrom(srs => srs.Attempt.Id));
    }
}