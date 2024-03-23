
namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public interface IReportRepository
    {
        Task<int> GetAllPaysAllTimeAsync();
        Task<int> GetAllPaysThisDayAsync();
        Task<int> QuantityProducts();
        Task<int> GetTransactionsAsync();
        Task<int> GetTransactionsTodayAsync();
    }
}