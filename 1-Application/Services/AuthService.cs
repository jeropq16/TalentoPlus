using _1_Application.DTOs;
using _1_Application.Interfaces;
using _2_Domain.Interfaces;
using BCrypt.Net;

namespace _1_Application.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IEmployeeRepository employeeRepository, IJwtService jwtService)
    {
        _employeeRepository = employeeRepository;
        _jwtService = jwtService;
    }

    public async Task<EmployeeLoginResponse?> LoginAsync(EmployeeLoginRequest request)
    {
        // 1. Buscar empleado por documento
        var employee = await _employeeRepository.GetByDocumentoAsync(request.Documento);
        if (employee == null)
            return null;

        // 2. Verificar contraseña
        bool passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash);
        if (!passwordOk)
            return null;

        // 3. Generar token
        string token = _jwtService.GenerateEmployeeToken(employee.Id, employee.Email);

        // 4. Devolver respuesta
        return new EmployeeLoginResponse
        {
            Token = token,
            NombreCompleto = $"{employee.Nombres} {employee.Apellidos}",
            Email = employee.Email
        };
    }
}