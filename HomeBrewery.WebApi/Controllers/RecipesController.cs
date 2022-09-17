using AutoMapper;
using HomeBrewery.Application.Services.Recipes;
using HomeBrewery.Application.Services.Recipes.Models;
using HomeBrewery.Domain.Constants;
using HomeBrewery.WebApi.Models.Requests;
using HomeBrewery.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]/[action]")]
[ApiController]
public class RecipesController : BaseController
{
    private readonly IRecipesService _recipesService;

    public RecipesController(
        IRecipesService recipesService,
        IMapper mapper,
        ILogger<RecipesController> logger) : base(mapper, logger)
    {
        _recipesService = recipesService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<RecipeResponse>>> GetAll()
    {
        var recipes = await _recipesService.FindAsync();
        return Ok(_mapper.Map<List<RecipeResponse>>(recipes));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<RecipeResponse>>> GetCurrentUserRecipes()
    {
        var recipes = await _recipesService.FindAsync(r => r.Users.Contains(UserId!.Value));
        return Ok(_mapper.Map<List<RecipeResponse>>(recipes));
    }

    [Authorize]
    [HttpGet("{recipeId}")]
    public async Task<ActionResult<RecipeResponse>> GetById(int recipeId)
    {
        var recipes = await _recipesService.GetByIdAsync(recipeId);
        return Ok(_mapper.Map<RecipeResponse>(recipes));
    }

    [Authorize(Roles = RoleConst.Admin)]
    [HttpPost]
    public async Task<ActionResult<int>> Create(RecipeCreateRequest request)
    {
        var recipe = _mapper.Map<RecipeCreateModel>(request);
        var recipeId = await _recipesService.CreateAsync(recipe);

        return CreatedAtAction("GetById", new { recipeId }, recipeId);
    }

    [Authorize(Roles = RoleConst.Admin)]
    [HttpPatch("{recipeId}")]
    public async Task<IActionResult> Patch(int recipeId, RecipePatchRequest request)
    {
        var recipe = _mapper.Map<RecipePatchModel>(request, opt =>
            opt.Items.Add(nameof(RecipePatchModel.Id), recipeId));

        await _recipesService.PatchAsync(recipe);

        return NoContent();
    }

    [Authorize(Roles = RoleConst.Admin)]
    [HttpDelete("{recipeId}")]
    public async Task<IActionResult> Delete(int recipeId)
    {
        await _recipesService.DeleteAsync(recipeId);
        return NoContent();
    }
}