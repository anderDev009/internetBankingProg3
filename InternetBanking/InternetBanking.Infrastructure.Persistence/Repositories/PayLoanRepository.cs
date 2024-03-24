
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using System.Reflection.Metadata;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PayLoanRepository : BaseRepository<Loan>, IPayLoanRepository
    {
        private readonly InternetBankingContext _ctx;
        public PayLoanRepository(InternetBankingContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }
    }
}
