using _1_Application.DTOs;

namespace _1_Application.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeResponse>> GetAllAsync();
    Task<EmployeeResponse?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateEmployeeRequest request);
    Task<bool> UpdateAsync(int id, UpdateEmployeeRequest request);
    Task<bool> DeleteAsync(int id);
}