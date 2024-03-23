using InternetBanking.Core.Application.ViewModels.Dashboard;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboard();
    }
}