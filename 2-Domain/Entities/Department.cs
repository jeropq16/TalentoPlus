namespace _2_Domain.Entities;

public class Department
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}