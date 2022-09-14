using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewery.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        
        public BaseController(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        internal string? UserId => !User.Identity!.IsAuthenticated
            ? null
            : User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
