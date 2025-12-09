using _2_Domain.Entities;
using _2_Domain.Enums;
using _2_Domain.Interfaces;
using _3_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByDocumentoAsync(string documento)
    {
        return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Documento == documento);
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees.Include(e => e.Department).ToListAsync();
    }

    public async Task<List<Employee>> GetByDepartmentAsync(int departmentId)
    {
        return await _context.Employees.Where(e => e.DepartmentId == departmentId).ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Employees.CountAsync();
    }

    public async Task<int> CountByStatusAsync(EmployeeStatus status)
    {
        return await _context.Employees.CountAsync(e => e.Estado == status);
    }

    public async Task<int> CountByDepartmentAsync(int departmentId)
    {
        return await _context.Employees.CountAsync(e => e.DepartmentId == departmentId);
    }

    public async Task AddAsync(Employee employee)
    { 
        await _context.Employees.AddAsync(employee);
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void Remove(Employee employee)
    {
        _context.Employees.Remove(employee);
    }
    
    public async Task<decimal> GetTotalPayrollAsync()
    {
        return await _context.Employees.SumAsync(e => e.Salario);
    }

    public async Task<decimal> GetAverageSalaryAsync()
    {
        return await _context.Employees.AverageAsync(e => e.Salario);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}