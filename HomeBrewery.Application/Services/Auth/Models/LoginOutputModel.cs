namespace HomeBrewery.Application.Services.Auth.Models;

public class LoginOutputModel
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}