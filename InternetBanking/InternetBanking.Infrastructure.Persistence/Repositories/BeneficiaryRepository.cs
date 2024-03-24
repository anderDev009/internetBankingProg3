

using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiaryRepository : BaseRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly InternetBankingContext _ctx;
        public BeneficiaryRepository(InternetBankingContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Beneficiary>> GetBeneficiaryByIdUser(string IdUser)
        {
            return await _ctx.Set<Beneficiary>().Where(b => b.IdUser == IdUser).ToListAsync();
        }
    }
}
