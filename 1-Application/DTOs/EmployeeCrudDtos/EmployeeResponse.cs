namespace _1_Application.DTOs;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string Documento { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public string Departamento { get; set; } = null!;
}