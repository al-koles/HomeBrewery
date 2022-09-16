using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Domain;

namespace HomeBrewery.Application.Services.Users.Models;

public class PatchUserModel : IMapWith<HBUser>
{
    public int Id { get; set; }
    public string? Email { get; set; } = null!;
    public string? Password { get; set; } = null!;
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PatchUserModel, HBUser>()
            .ForMember(dst => dst.UserName,
                opt =>
                {
                    opt.MapFrom(srs => srs.Email);
                    opt.PreCondition(srs => srs.Email != null);
                })
            .ForAllMembers(opt => opt.Condition((_, _, member, _) => member != null));
    }
}