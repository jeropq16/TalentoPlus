namespace _1_Application.DTOs;

public class EmployeeLoginResponse
{
    public string Token { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string Email { get; set; } = null!;
}