using _1_Application.DTOs;
using _1_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TalentoPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(EmployeeLoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null) return Unauthorized(new { message = "Documento o contraseña incorrectos" });

        return Ok(result);
    }
}