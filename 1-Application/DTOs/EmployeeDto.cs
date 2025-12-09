namespace _1_Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Documento { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public decimal Salario { get; set; }
    public string Estado { get; set; } = null!;
    public string NivelEducativo { get; set; } = null!;
    public string Departamento { get; set; } = null!;
}