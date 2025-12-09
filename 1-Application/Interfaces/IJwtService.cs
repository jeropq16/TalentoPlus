namespace _1_Application.Interfaces;

public interface IJwtService
{
    string GenerateEmployeeToken(int employeeId, string email);
}