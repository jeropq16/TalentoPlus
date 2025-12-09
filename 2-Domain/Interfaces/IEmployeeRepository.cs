using _2_Domain.Entities;
using _2_Domain.Enums;

namespace _2_Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee?> GetByDocumentoAsync(string documento);
    Task<List<Employee>> GetAllAsync();
    Task<List<Employee>> GetByDepartmentAsync(int departmentId);
    Task<int> CountAsync();
    Task<int> CountByStatusAsync(EmployeeStatus status);
    Task<int> CountByDepartmentAsync(int departmentId);

    Task AddAsync(Employee employee);
    void Update(Employee employee);
    void Remove(Employee employee);
    Task<decimal> GetTotalPayrollAsync();
    Task<decimal> GetAverageSalaryAsync();


    Task SaveChangesAsync();
}