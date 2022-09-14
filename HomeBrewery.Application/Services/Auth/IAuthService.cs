using HomeBrewery.Application.Services.Auth.Models;

namespace HomeBrewery.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginOutputModel> LoginAsync(string email, string password);
    Task<int> RegisterAsync(UserRegisterModel registerModel);
}