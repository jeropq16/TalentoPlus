namespace _1_Application.DTOs;

public class ExcelImportResult
{
    public int TotalRows { get; set; }
    public int EmployeesCreated { get; set; }
    public List<string> Errors { get; set; } = new();
}