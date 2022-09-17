using AutoMapper;
using HomeBrewery.Application.Common.Exceptions;
using HomeBrewery.Application.Interfaces;
using HomeBrewery.Application.Services.Attempts.Models;
using HomeBrewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Application.Services.Attempts;

public class AttemptsService : IAttemptsService
{
    private readonly IHomeBreweryDbContext _dbContext;
    private readonly IMapper _mapper;

    public AttemptsService(IHomeBreweryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<AttemptOutputModel>> GetByUserAndRecipeId(int userId, int recipeId)
    {
        var recipes = await _dbContext.Attempts
            .Include(a => a.User)
            .Include(a => a.Recipe)
            .Where(a => a.User.Id == userId && a.Recipe.Id == recipeId)
            .ToListAsync();

        return _mapper.Map<List<AttemptOutputModel>>(recipes);
    }

    public async Task<int> CreateAsync(AttemptCreateModel model)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), model.UserId);
        }

        var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(u => u.Id == model.RecipeId);
        if (recipe == null)
        {
            throw new NotFoundException(nameof(Recipe), model.RecipeId);
        }

        var attempt = _mapper.Map<Attempt>(model);
        attempt.User = user;
        attempt.Recipe = recipe;

        await _dbContext.Attempts.AddAsync(attempt);
        await _dbContext.SaveChangesAsync();

        return attempt.Id;
    }

    public async Task DeleteAsync(int attemptId)
    {
        var attempt = await _dbContext.Attempts.FirstOrDefaultAsync(a => a.Id == attemptId);
        if (attempt == null)
        {
            throw new NotFoundException(nameof(Attempt), attemptId);
        }

        _dbContext.Attempts.Remove(attempt);
        await _dbContext.SaveChangesAsync();
    }
}