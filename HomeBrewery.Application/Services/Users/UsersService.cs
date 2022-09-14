using AutoMapper;
using HomeBrewery.Application.Common.Exceptions;
using HomeBrewery.Application.Interfaces;
using HomeBrewery.Application.Services.Users.Models;
using HomeBrewery.Domain;
using HomeBrewery.Domain.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.Application.Services.Users;

public class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly IHomeBreweryDbContext _dbContext;
    private readonly UserManager<HBUser> _userManager;

    public UsersService(
        IMapper mapper, 
        IHomeBreweryDbContext dbContext, 
        UserManager<HBUser> userManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<UserOutputModel> GetByIdAsync(int userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), userId);
        }

        var roleNames = await _userManager.GetRolesAsync(user);
        var roles = roleNames.Select(rn => Enum.TryParse<Role>(rn, out var role) ? role : Role.User).ToList();

        var userModel = _mapper.Map<UserOutputModel>(user);
        userModel.Roles = roles;

        return userModel;
    }

    public async Task<List<UserOutputModel>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userModels = _mapper.Map<List<UserOutputModel>>(users);
        
        foreach (var user in users)
        {
            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = roleNames.Select(rn => Enum.TryParse<Role>(rn, out var role) ? role : Role.User).ToList();

            var userModel = userModels.First(u => u.Id == user.Id);
            userModel.Roles = roles;
        }

        return userModels;
    }

    public async Task UpdateAsync(UserUpdateModel model)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == model.Id);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), model.Id);
        }

        _mapper.Map(model, user);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteAsync(int userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), userId);
        }

        await _userManager.DeleteAsync(user);
    }

    public async Task AddToRolesAsync(int userId, IEnumerable<Role> roles)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), userId);
        }

        await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToString()));
    }
    
    public async Task RemoveFromRolesAsync(int userId, IEnumerable<Role> roles)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(HBUser), userId);
        }

        await _userManager.RemoveFromRolesAsync(user, roles.Select(r => r.ToString()));
    }
}