using _1_Application.DTOs;
using _1_Application.Interfaces;
using _2_Domain.Entities;
using _2_Domain.Enums;
using _2_Domain.Interfaces;
using BCrypt.Net;

namespace _1_Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IDepartmentRepository _departmentRepo;

    public EmployeeService(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
    {
        _employeeRepo = employeeRepo;
        _departmentRepo = departmentRepo;
    }

    public async Task<List<EmployeeResponse>> GetAllAsync()
    {
        var list = await _employeeRepo.GetAllAsync();

        return list.Select(e => new EmployeeResponse
        {
            Id = e.Id,
            Documento = e.Documento,
            NombreCompleto = $"{e.Nombres} {e.Apellidos}",
            Email = e.Email,
            Cargo = e.Cargo,
            Departamento = e.Department?.Nombre ?? ""
        }).ToList();
    }

    public async Task<EmployeeResponse?> GetByIdAsync(int id)
    {
        var e = await _employeeRepo.GetByIdAsync(id);
        if (e == null) return null;

        return new EmployeeResponse
        {
            Id = e.Id,
            Documento = e.Documento,
            NombreCompleto = $"{e.Nombres} {e.Apellidos}",
            Email = e.Email,
            Cargo = e.Cargo,
            Departamento = e.Department?.Nombre ?? ""
        };
    }

    public async Task<int> CreateAsync(CreateEmployeeRequest request)
    {
        var dept = await _departmentRepo.GetByIdAsync(request.DepartmentId);
        if (dept == null) throw new Exception("Departamento no existe");

        var employee = new Employee
        {
            Documento = request.Documento,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = request.Email,
            Cargo = request.Cargo,
            Salario = request.Salario,
            DepartmentId = request.DepartmentId,
            Estado = Enum.Parse<EmployeeStatus>(request.Estado),
            NivelEducativo = Enum.Parse<EducationLevel>(request.NivelEducativo),
            PerfilProfesional = request.PerfilProfesional,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _employeeRepo.AddAsync(employee);
        await _employeeRepo.SaveChangesAsync();

        return employee.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdateEmployeeRequest request)
    {
        var employee = await _employeeRepo.GetByIdAsync(id);
        if (employee == null) return false;

        employee.Nombres = request.Nombres;
        employee.Apellidos = request.Apellidos;
        employee.Email = request.Email;
        employee.Cargo = request.Cargo;
        employee.Salario = request.Salario;
        employee.DepartmentId = request.DepartmentId;
        employee.Estado = Enum.Parse<EmployeeStatus>(request.Estado);
        employee.NivelEducativo = Enum.Parse<EducationLevel>(request.NivelEducativo);
        employee.PerfilProfesional = request.PerfilProfesional;

        _employeeRepo.Update(employee);
        await _employeeRepo.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _employeeRepo.GetByIdAsync(id);
        if (employee == null) return false;

        _employeeRepo.Remove(employee);
        await _employeeRepo.SaveChangesAsync();

        return true;
    }
}
