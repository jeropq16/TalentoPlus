using _1_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Api.Models;

namespace TalentoPlus.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EmployeeLoginRequest loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            if (response == null)
                return Unauthorized("Documento o contraseña incorrectos");

            return Ok(response);
        }
        
    }
}