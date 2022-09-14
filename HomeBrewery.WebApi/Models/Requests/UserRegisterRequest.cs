using System.ComponentModel.DataAnnotations;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Services.Auth.Models;

namespace HomeBrewery.WebApi.Models.Requests;

public class UserRegisterRequest : IMapWith<UserRegisterModel>
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}