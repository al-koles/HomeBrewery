﻿using System.Linq.Expressions;
using AutoMapper;
using HomeBrewery.Application.Common.Exceptions;
using HomeBrewery.Application.Interfaces;
using HomeBrewery.Application.Services.Recipes.Models;
using HomeBrewery.Domain;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Application.Services.Recipes;

public class RecipesService : IRecipesService
{
    private readonly IHomeBreweryDbContext _dbContext;
    private readonly IMapper _mapper;

    public RecipesService(IHomeBreweryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RecipeOutputModel> GetByIdAsync(int recipeId)
    {
        var recipe = await _dbContext.Recipes.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            throw new NotFoundException(nameof(Recipe), recipeId);
        }

        return _mapper.Map<RecipeOutputModel>(recipe);
    }

    public async Task<List<RecipeOutputModel>> FindAsync(Expression<Predicate<RecipeOutputModel>>? predicate = null)
    {
        var recipes = await _dbContext.Recipes.Include(r => r.Users).ToListAsync();

        if (predicate != null)
        {
            recipes = recipes.Where(r => predicate.Compile()(_mapper.Map<RecipeOutputModel>(r))).ToList();
        }

        return _mapper.Map<List<RecipeOutputModel>>(recipes);
    }

    public async Task<int> CreateAsync(RecipeCreateModel model)
    {
        var recipe = _mapper.Map<Recipe>(model);

        await _dbContext.Recipes.AddAsync(recipe);
        await _dbContext.SaveChangesAsync();

        return recipe.Id;
    }

    public async Task PatchAsync(RecipePatchModel model)
    {
        var recipe = await _dbContext.Recipes.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == model.Id);
        if (recipe == null)
        {
            throw new NotFoundException(nameof(Recipe), model.Id);
        }

        _mapper.Map(model, recipe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int recipeId)
    {
        var recipe = await _dbContext.Recipes.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            throw new NotFoundException(nameof(Recipe), recipeId);
        }

        _dbContext.Recipes.Remove(recipe);
        await _dbContext.SaveChangesAsync();
    }
}