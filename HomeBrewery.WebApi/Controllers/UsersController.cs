using AutoMapper;
using HomeBrewery.Application.Services.Users;
using HomeBrewery.Application.Services.Users.Models;
using HomeBrewery.Domain.Data;
using HomeBrewery.WebApi.Models.Requests;
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
    public async Task<ActionResult<UserResponse>> GetCurrent()
    {
        var user = await _usersService.GetByIdAsync(int.Parse(UserId!));
        return Ok(_mapper.Map<UserResponse>(user));
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponse>> GetById(int userId)
    {
        var user = await _usersService.GetByIdAsync(userId);
        return Ok(_mapper.Map<UserResponse>(user));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll()
    {
        var user = await _usersService.GetAllAsync();
        return Ok(_mapper.Map<List<UserResponse>>(user));
    }

    [Authorize]
    [HttpPatch("{userId}")]
    public async Task<IActionResult> Patch(int userId, PatchUserRequest user)
    {
        var patchModel = _mapper.Map<PatchUserModel>(user, opt =>
            opt.Items.Add(nameof(PatchUserModel.Id), userId));
        await _usersService.PatchAsync(patchModel);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(int userId)
    {
        await _usersService.DeleteAsync(userId);

        return NoContent();
    }

    [Authorize]
    [HttpPost("{roleName}, {userId}")]
    public async Task<IActionResult> AddToRole(string roleName, int userId)
    {
        if (Enum.TryParse<Role>(roleName, out var role))
        {
            await _usersService.AddToRolesAsync(userId, new[] { role });
            return CreatedAtAction("GetById", new { userId }, roleName);
        }

        return BadRequest("No such role");
    }

    [Authorize(Roles = RoleConst.Admin)]
    [HttpDelete("{roleName}, {userId}")]
    public async Task<IActionResult> RemoveFromRole(string roleName, int userId)
    {
        if (Enum.TryParse<Role>(roleName, out var role))
        {
            await _usersService.RemoveFromRolesAsync(userId, new[] { role });
            return NoContent();
        }

        return BadRequest("No such role.");
    }
}