using System.Data;
using _1_Application.DTOs;
using _1_Application.Interfaces;
using _2_Domain.Entities;
using _2_Domain.Enums;
using _2_Domain.Interfaces;
using ExcelDataReader;

namespace _1_Application.Services;

public class ExcelImportService : IExcelImportService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IDepartmentRepository _departmentRepo;

    public ExcelImportService(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
    {
        _employeeRepo = employeeRepo;
        _departmentRepo = departmentRepo;
    }

    public async Task<ExcelImportResult> ImportEmployeesAsync(Stream fileStream)
    {
        var result = new ExcelImportResult();

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using var reader = ExcelReaderFactory.CreateReader(fileStream);
        var dataset = reader.AsDataSet();
        var table = dataset.Tables[0];

        result.TotalRows = table.Rows.Count;

        for (int i = 1; i < table.Rows.Count; i++)
        {
            try
            {
                var row = table.Rows[i];

                string documento = row[0].ToString()!;
                string nombres = row[1].ToString()!;
                string apellidos = row[2].ToString()!;
                string email = row[3].ToString()!;
                string cargo = row[4].ToString()!;
                decimal salario = decimal.Parse(row[5].ToString()!);
                string departamentoNombre = row[6].ToString()!;
                string estado = row[7].ToString()!;
                string nivelEduc = row[8].ToString()!;
                string perfil = row[9].ToString()!;

                var dept = await _departmentRepo.GetByNameAsync(departamentoNombre);
                if (dept == null)
                {
                    result.Errors.Add($"Fila {i + 1}: El departamento '{departamentoNombre}' no existe.");
                    continue;
                }

                var employee = new Employee
                {
                    Documento = documento,
                    Nombres = nombres,
                    Apellidos = apellidos,
                    Email = email,
                    Cargo = cargo,
                    Salario = salario,
                    DepartmentId = dept.Id,
                    Estado = Enum.Parse<EmployeeStatus>(estado),
                    NivelEducativo = Enum.Parse<EducationLevel>(nivelEduc),
                    PerfilProfesional = perfil,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(documento)
                };

                await _employeeRepo.AddAsync(employee);
                result.EmployeesCreated++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Fila {i + 1}: Error → {ex.Message}");
            }
        }

        await _employeeRepo.SaveChangesAsync();
        return result;
    }
}
