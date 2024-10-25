using api_core_library.Intefaces;
using api_core_library.Models;
using api_core_library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_core.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizationService _authService;

         public AuthController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(string username, string password)
        {
            var token = _authService.Authenticate( new Account(){ Username = username, Password = password});

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}
