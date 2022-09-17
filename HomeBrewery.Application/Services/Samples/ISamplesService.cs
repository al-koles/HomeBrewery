using HomeBrewery.Application.Services.Samples.Models;

namespace HomeBrewery.Application.Services.Samples;

public interface ISamplesService
{
    Task<List<SampleOutputModel>> GetByAttemptId(int attemptId);
    Task<int> CreateAsync(SampleCreateModel model);
    Task DeleteAsync(int sampleId);
}