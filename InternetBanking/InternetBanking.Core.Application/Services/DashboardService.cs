using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Dashboard;
using InternetBanking.Infrastructure.Identity.Services;
using InternetBanking.Infrastructure.Persistence.Repositories;

namespace InternetBanking.Core.Application.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IReportRepository _reporRepository;
        private readonly IAccountService _accountService;
        public DashboardService(IReportRepository reportRepository, IAccountService accountService)
        {
            _reporRepository = reportRepository;
            _accountService = accountService;
        }
        public async Task<DashboardViewModel> GetDashboard()
        {
            DashboardViewModel vm = new();
            vm.Transactions = await _reporRepository.GetTransactionsAsync();
            vm.TransactionsToday = await _reporRepository.GetTransactionsTodayAsync();
            vm.Pays = await _reporRepository.GetAllPaysAllTimeAsync();
            vm.PaysToday = await _reporRepository.GetAllPaysThisDayAsync();
            vm.QuantityProducts = await _reporRepository.QuantityProducts();
            //obteniendo los usuarios para filtrarlos por activos e inactivos
            var users = await _accountService.GetAllUserClientAsync();
            vm.ActiveUsers = users.Where(u => u.IsVerified).Count();
            vm.DisabledUsers = users.Where(u => !u.IsVerified).Count();
            return vm;
        }
    }
}
