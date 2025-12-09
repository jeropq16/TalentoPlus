using _1_Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace _1_Application.Interfaces;

public interface IAuthService
{
    Task<EmployeeLoginResponse?> LoginAsync(EmployeeLoginRequest request);
}