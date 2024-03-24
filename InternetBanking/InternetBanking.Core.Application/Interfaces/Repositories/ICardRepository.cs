using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task CashAdvance(int IdCard, decimal amount);
    }
}
