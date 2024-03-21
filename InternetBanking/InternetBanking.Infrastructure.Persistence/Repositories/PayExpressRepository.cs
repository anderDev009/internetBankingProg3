

using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PayExpressRepository : BaseRepository<PayExpress>, IPayExpressRepository
    {
        private readonly InternetBankingContext _ctx;
        public PayExpressRepository(InternetBankingContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

    }
}
