using System.Linq.Expressions;
using HomeBrewery.Application.Services.Recipes.Models;

namespace HomeBrewery.Application.Services.Recipes;

public interface IRecipesService
{
    Task<RecipeOutputModel> GetByIdAsync(int recipeId);
    Task<List<RecipeOutputModel>> FindAsync(Expression<Predicate<RecipeOutputModel>>? predicate = null);
    Task<int> CreateAsync(RecipeCreateModel model);
    Task PatchAsync(RecipePatchModel model);
    Task DeleteAsync(int recipeId);
}