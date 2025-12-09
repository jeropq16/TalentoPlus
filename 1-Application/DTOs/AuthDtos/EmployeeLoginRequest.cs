namespace _1_Application.DTOs;

public class EmployeeLoginRequest
{
    public string Documento { get; set; } = null!;
    public string Password { get; set; } = null!;
}