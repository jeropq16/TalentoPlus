using _1_Application.DTOs;
using _1_Application.DTOs.Dashboard;

namespace _1_Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync();
}