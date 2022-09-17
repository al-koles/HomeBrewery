using HomeBrewery.Application.Services.Attempts.Models;

namespace HomeBrewery.Application.Services.Attempts;

public interface IAttemptsService
{
    Task<List<AttemptOutputModel>> GetByUserAndRecipeId(int userId, int recipeId);
    Task<int> CreateAsync(AttemptCreateModel model);
    Task DeleteAsync(int attemptId);
}