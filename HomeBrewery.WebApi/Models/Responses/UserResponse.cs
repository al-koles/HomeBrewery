using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Users.Models;
using HomeBrewery.Domain.Data;

namespace HomeBrewery.WebApi.Models.Responses;

public class UserResponse : IMapWith<UserOutputModel>
{
    public UserResponse()
    {
        Roles = new List<Role>();
    }
    
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public List<Role> Roles { get; set; }
}