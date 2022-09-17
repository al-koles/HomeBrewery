using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Users.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class PatchUserRequest : IMapWith<UserPatchModel>
{
    [DataType(DataType.EmailAddress)] public string? Email { get; set; }

    [DataType(DataType.Password)] public string? Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PatchUserRequest, UserPatchModel>()
            .ForMember(dst => dst.Id,
                opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(UserPatchModel.Id)]));
    }
}