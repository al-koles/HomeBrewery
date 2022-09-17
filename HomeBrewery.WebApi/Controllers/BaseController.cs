using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ILogger _logger;
    protected readonly IMapper _mapper;

    public BaseController(IMapper mapper, ILogger logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    internal int? UserId => !User.Identity!.IsAuthenticated
        ? null
        : int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
}