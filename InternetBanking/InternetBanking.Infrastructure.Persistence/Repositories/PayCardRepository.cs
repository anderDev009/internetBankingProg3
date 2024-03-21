using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PayCardRepository : BaseRepository<CardPay>, IPayCardRepository
    {
        private readonly InternetBankingContext _ctx;
        public PayCardRepository(InternetBankingContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }
    }
}
