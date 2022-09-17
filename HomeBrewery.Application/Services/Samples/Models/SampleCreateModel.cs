using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain.Entities;

namespace HomeBrewery.Application.Services.Samples.Models;

public class SampleCreateModel : IMapWith<Sample>
{
    public int AttemptId { get; set; }
    public SampleType Type { get; set; }
    public string Value { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}