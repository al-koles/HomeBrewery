using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Users.Models;

public class UserOutputModel : IMapWith<HBUser>
{
    public UserOutputModel()
    {
        Roles = new List<string>();
    }
    
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public List<string> Roles { get; set; }
}