using _1_Application.DTOs;

namespace _1_Application.Interfaces;

public interface IExcelImportService
{
    Task<ExcelImportResult> ImportEmployeesAsync(Stream fileStream);
}