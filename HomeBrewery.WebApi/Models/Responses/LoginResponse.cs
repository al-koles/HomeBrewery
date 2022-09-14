using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Auth.Models;

namespace HomeBrewery.WebApi.Models.Responses
{
    public class LoginResponse : IMapWith<LoginOutputModel>
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
