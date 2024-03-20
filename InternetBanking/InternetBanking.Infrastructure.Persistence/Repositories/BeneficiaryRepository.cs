

using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiaryRepository : BaseRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly InternetBankingContext _ctx;
        public BeneficiaryRepository(InternetBankingContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
