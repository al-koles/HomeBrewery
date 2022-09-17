using AutoMapper;
using HomeBrewery.Application.Common.Exceptions;
using HomeBrewery.Application.Interfaces;
using HomeBrewery.Application.Services.Samples.Models;
using HomeBrewery.Domain;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Application.Services.Samples;

public class SamplesService : ISamplesService
{
    private readonly IHomeBreweryDbContext _dbContext;
    private readonly IMapper _mapper;

    public SamplesService(IHomeBreweryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<SampleOutputModel>> GetByAttemptId(int attemptId)
    {
        var samples = await _dbContext.Samples
            .Include(a => a.Attempt)
            .Where(a => a.Attempt.Id == attemptId)
            .ToListAsync();

        return _mapper.Map<List<SampleOutputModel>>(samples);
    }

    public async Task<int> CreateAsync(SampleCreateModel model)
    {
        var attempt = await _dbContext.Attempts.FirstOrDefaultAsync(a => a.Id == model.AttemptId);
        if (attempt == null)
        {
            throw new NotFoundException(nameof(Attempt), model.AttemptId);
        }

        var sample = _mapper.Map<Sample>(model);
        sample.Attempt = attempt;

        await _dbContext.Samples.AddAsync(sample);
        await _dbContext.SaveChangesAsync();

        return sample.Id;
    }

    public async Task DeleteAsync(int sampleId)
    {
        var sample = await _dbContext.Samples.FirstOrDefaultAsync(a => a.Id == sampleId);
        if (sample == null)
        {
            throw new NotFoundException(nameof(Sample), sampleId);
        }

        _dbContext.Samples.Remove(sample);
        await _dbContext.SaveChangesAsync();
    }
}