using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Auth.Models;

public class UserRegisterModel : IMapWith<HBUser>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegisterModel, HBUser>()
            .ForMember(dst => dst.UserName,
                opt => opt.MapFrom(srs => srs.Email));
    }
}