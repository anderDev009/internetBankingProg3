
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly InternetBankingContext _ctx;
        public CardRepository(InternetBankingContext ctx) : base(ctx) 
        {
            _ctx = ctx;            
        }
    }
}
