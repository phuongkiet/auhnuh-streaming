using auhnuh_server.Application.IService;
using auhnuh_server.Domain.DTO.WebRequest.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace auhnuh_server.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            var response = await _authService.Login(login);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _authService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email));

            return Ok(result);
        }
    }
}
