using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _1_Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace _1_Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateEmployeeToken(int employeeId, string email)
    {
        // 1. Claims (datos que van dentro del token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim("employeeId", employeeId.ToString()),
            new Claim(ClaimTypes.Role, "Employee"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // 2. Clave secreta
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        // 3. Credenciales de firma
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 4. Tiempo de expiración (usamos configuración)
        var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        // 5. Devolver string del token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}