using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Users.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class PatchUserRequest : IMapWith<PatchUserModel>
{
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PatchUserRequest, PatchUserModel>()
            .ForMember(dst => dst.Id,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(PatchUserModel.Id)]));
    }
}