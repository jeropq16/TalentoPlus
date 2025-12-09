using _1_Application.DTOs;
using _1_Application.DTOs.Dashboard;
using _1_Application.Interfaces;
using _2_Domain.Entities;
using _2_Domain.Enums;
using _2_Domain.Interfaces;

namespace _1_Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IDepartmentRepository _departmentRepo;

    public DashboardService(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
    {
        _employeeRepo = employeeRepo;
        _departmentRepo = departmentRepo;
    }

    public async Task<DashboardResponse> GetDashboardAsync()
    {
        var employees = await _employeeRepo.GetAllAsync();

        var total = employees.Count;
        var active = employees.Count(e => e.Estado == EmployeeStatus.Activo);
        var inactive = employees.Count(e => e.Estado == EmployeeStatus.Inactivo);
        var vacation = employees.Count(e => e.Estado == EmployeeStatus.Vacaciones);

        var totalPayroll = employees.Sum(e => e.Salario);
        var averageSalary = employees.Count > 0 ? employees.Average(e => e.Salario) : 0;

        // Empleados por departamento
        var departments = await _departmentRepo.GetAllAsync();

        var employeesPerDept = departments.Select(d => new DepartmentCount
        {
            Department = d.Nombre,
            Count = employees.Count(e => e.DepartmentId == d.Id)
        }).ToList();

        return new DashboardResponse
        {
            TotalEmployees = total,
            ActiveEmployees = active,
            InactiveEmployees = inactive,
            VacationEmployees = vacation,
            TotalPayroll = totalPayroll,
            AverageSalary = averageSalary,
            EmployeesPerDepartment = employeesPerDept
        };
    }
}