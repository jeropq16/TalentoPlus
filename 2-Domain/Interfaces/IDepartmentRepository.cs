using _2_Domain.Entities;

namespace _2_Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<Department?> GetByNameAsync(string name);
    
    Task AddAsync(Department department);
    Task SaveChangesAsync();
    
}