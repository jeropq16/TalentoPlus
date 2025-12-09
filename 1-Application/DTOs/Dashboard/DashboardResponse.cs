namespace _1_Application.DTOs.Dashboard;

public class DashboardResponse
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int InactiveEmployees { get; set; }
    public int VacationEmployees { get; set; }

    public decimal TotalPayroll { get; set; }
    public decimal AverageSalary { get; set; }

    public List<DepartmentCount> EmployeesPerDepartment { get; set; } = new();
}