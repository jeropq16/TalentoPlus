using _2_Domain.Enums;

namespace _2_Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string Documento { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public decimal Salario { get; set; }

    public DateTime FechaIngreso { get; set; }

    public EmployeeStatus Estado { get; set; }

    public EducationLevel NivelEducativo { get; set; }

    public string PerfilProfesional { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public string PasswordHash { get; set; } = null!;

}