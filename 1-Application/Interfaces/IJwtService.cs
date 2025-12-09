namespace _1_Application.Interfaces;

public interface IJwtService
{
    string GenerateEmployeeToken(string employeeId, string email);
}