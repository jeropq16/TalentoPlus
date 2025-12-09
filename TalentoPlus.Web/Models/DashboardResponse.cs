namespace TalentoPlus.Api.Models
{
    public class DashboardResponse
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal TotalPayroll { get; set; }
        public List<EmployeeDepartment> EmployeesPerDepartment { get; set; }
    }

    public class EmployeeDepartment
    {
        public string Department { get; set; }
        public int Count { get; set; }
    }
}