using HomeBrewery.Application.Services.Users.Models;
using HomeBrewery.Domain.Data;

namespace HomeBrewery.Application.Services.Users;

public interface IUsersService
{
    Task<UserOutputModel> GetByIdAsync(int userId);
    Task UpdateAsync(UserUpdateModel model);
    Task DeleteAsync(int userId);
    Task AddToRolesAsync(int userId, IEnumerable<Role> roles);
    Task RemoveFromRolesAsync(int userId, IEnumerable<Role> roles);
}