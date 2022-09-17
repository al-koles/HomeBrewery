using AutoMapper;
using HomeBrewery.Application.Services.Samples;
using HomeBrewery.Application.Services.Samples.Models;
using HomeBrewery.Domain.Entities;
using HomeBrewery.WebApi.Models.Requests;
using HomeBrewery.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]/[action]")]
[ApiController]
public class SamplesController : BaseController
{
    private readonly ISamplesService _samplesService;

    public SamplesController(
        ISamplesService samplesService,
        IMapper mapper,
        ILogger<SamplesController> logger) : base(mapper, logger)
    {
        _samplesService = samplesService;
    }

    [Authorize]
    [HttpGet("{attemptId}")]
    public async Task<ActionResult<List<SampleResponse>>> GetByAttemptId(int attemptId)
    {
        var samples = await _samplesService.GetByAttemptId(attemptId);
        return Ok(_mapper.Map<List<SampleResponse>>(samples));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(SampleCreateRequest request)
    {
        if (Enum.TryParse<SampleType>(request.Type, out var type))
        {
            var sample = _mapper.Map<SampleCreateModel>(request, opt =>
            {
                opt.Items.Add(nameof(SampleCreateModel.Timestamp), DateTime.UtcNow);
                opt.Items.Add(nameof(SampleCreateModel.Type), type);
            });

            var sampleId = await _samplesService.CreateAsync(sample);

            return CreatedAtAction("GetByAttemptId", new { attemptId = request.AttemptId }, sampleId);
        }

        return BadRequest($"No such sample type ({request.Type})");
    }

    [HttpDelete("{sampleId}")]
    public async Task<IActionResult> Delete(int sampleId)
    {
        await _samplesService.DeleteAsync(sampleId);
        return NoContent();
    }
}