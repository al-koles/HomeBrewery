using AutoMapper;
using HomeBrewery.Application.Services.Users;
using HomeBrewery.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IUsersService _usersService;

    public UsersController(
        IMapper mapper, 
        ILogger<UsersController> logger,
        IUsersService usersService) : base(mapper, logger)
    {
        _usersService = usersService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var user = await _usersService.GetByIdAsync(int.Parse(UserId!));
        return Ok(_mapper.Map<UserResponse>(user));
    }
    
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponse>> GetUserById(int userId)
    {
        var user = await _usersService.GetByIdAsync(int.Parse(UserId!));
        return Ok(_mapper.Map<UserResponse>(user));
    }
}