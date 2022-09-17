using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Samples.Models;

public class SampleCreateModel : IMapWith<Sample>
{
    public int AttemptId { get; set; }
    public SampleType Type { get; set; }
    public DateTime Timestamp { get; set; }
}