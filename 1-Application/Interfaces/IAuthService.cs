using _1_Application.DTOs;

namespace _1_Application.Interfaces;

public interface IAuthService
{
    Task<EmployeeLoginResponse?> LoginAsync(EmployeeLoginRequest request);
}