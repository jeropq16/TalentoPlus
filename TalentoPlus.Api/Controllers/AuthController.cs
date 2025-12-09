using _1_Application.DTOs;
using _1_Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalentoPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    
    public AuthController(IAuthService authService,  UserManager<IdentityUser> userManager, IJwtService jwtService)
    {
        _authService = authService;
        _userManager = userManager;
        _jwtService = jwtService;
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] EmployeeLoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos inválidos");

        var loginResponse = await _authService.LoginAsync(loginRequest);

        if (loginResponse == null)
            return Unauthorized("Documento o contraseña incorrectos");

        return Ok(loginResponse);  
    }
    
}
