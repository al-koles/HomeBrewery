using AutoMapper;
using HomeBrewery.Application.Services.Attempts;
using HomeBrewery.Application.Services.Attempts.Models;
using HomeBrewery.WebApi.Models.Requests;
using HomeBrewery.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]/[action]")]
[ApiController]
public class AttemptsController : BaseController
{
    private readonly IAttemptsService _attemptsService;

    public AttemptsController(
        IAttemptsService attemptsService,
        IMapper mapper,
        ILogger<AttemptsController> logger) : base(mapper, logger)
    {
        _attemptsService = attemptsService;
    }

    [Authorize]
    [HttpGet("{recipeId}")]
    public async Task<ActionResult<List<AttemptResponse>>> GetByRecipeId(int recipeId)
    {
        var attempts = await _attemptsService.GetByUserAndRecipeId(UserId.Value, recipeId);
        return Ok(_mapper.Map<List<AttemptResponse>>(attempts));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(AttemptCreateRequest request)
    {
        var attempt = _mapper.Map<AttemptCreateModel>(request, opt =>
            opt.Items.Add(nameof(AttemptCreateModel.StartTimestamp), DateTime.UtcNow));

        var attemptId = await _attemptsService.CreateAsync(attempt);

        return CreatedAtAction("GetByRecipeId", new { recipeId = request.RecipeId }, attemptId);
    }

    [HttpDelete("{attemptId}")]
    public async Task<IActionResult> Delete(int attemptId)
    {
        await _attemptsService.DeleteAsync(attemptId);
        return NoContent();
    }
}