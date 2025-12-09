namespace _1_Application.DTOs;

public class CreateEmployeeRequest
{
    public string Documento { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public decimal Salario { get; set; }
    public int DepartmentId { get; set; }
    public string Estado { get; set; } = null!;
    public string NivelEducativo { get; set; } = null!;
    public string PerfilProfesional { get; set; } = null!;
    public string Password { get; set; } = null!;
}