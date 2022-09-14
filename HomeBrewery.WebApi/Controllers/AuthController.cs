using AutoMapper;
using HomeBrewery.Application.Services.Auth;
using HomeBrewery.Application.Services.Auth.Models;
using HomeBrewery.Application.Services.Users;
using HomeBrewery.Domain.Data;
using HomeBrewery.WebApi.Models.Requests;
using HomeBrewery.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]/[action]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IUsersService _usersService;

    public AuthController(
        IAuthService authService,
        IUsersService usersService,
        ILogger<AuthController> logger,
        IMapper mapper) : base(mapper, logger)
    {
        _authService = authService;
        _usersService = usersService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> IsLoggedIn()
    {
        try
        {
            await _usersService.GetByIdAsync(int.Parse(UserId!));
            return true;
        }
        catch
        {
            return false;
        }
    }

    [HttpGet("{email}, {password}")]
    public async Task<ActionResult<LoginResponse>> Login(string email, string password)
    {
        var response = await _authService.LoginAsync(email, password);

        _logger.LogInformation(email, "logged in");

        return Ok(_mapper.Map<LoginResponse>(response));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        var userId = await _authService.RegisterAsync(_mapper.Map<UserRegisterModel>(request));

        await _usersService.AddToRolesAsync(userId, new[] { Role.User });

        _logger.LogInformation(userId, "registered");

        return Ok(userId);
    }
}