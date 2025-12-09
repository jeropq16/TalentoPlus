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
        var employee = await _employeeRepository.GetByDocumentoAsync(request.Documento);
        if (employee == null)
            return null;

        bool passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash);
        if (!passwordOk)
            return null;

        string token = _jwtService.GenerateEmployeeToken(employee.Id, employee.Email);

        return new EmployeeLoginResponse
        {
            Token = token,
            NombreCompleto = $"{employee.Nombres} {employee.Apellidos}",
            Email = employee.Email
        };
    }
}