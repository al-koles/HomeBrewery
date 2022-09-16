using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Users.Models;

namespace HomeBrewery.WebApi.Models.Responses;

public class UserResponse : IMapWith<UserOutputModel>
{
    public UserResponse()
    {
        Roles = new List<string>();
    }
    
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public List<string> Roles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOutputModel, UserResponse>()
            .ForMember(dst => dst.Roles,
                opt => opt.MapFrom(srs => srs.Roles.Select(r => r.ToString())));
    }
}