using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly InternetBankingContext _ctx;
        public ReportRepository(InternetBankingContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> GetAllPaysAllTimeAsync()
        {
            return (await _ctx.Set<LoanPay>().CountAsync()) + (await _ctx.Set<CardPay>().CountAsync());
        }
        public async Task<int> GetAllPaysThisDayAsync()
        {
            return (await _ctx.Set<LoanPay>().Where(l => l.Date.Date == DateTime.Today.Date).CountAsync()) +
                (await _ctx.Set<CardPay>().Where(c => c.Date.Date == DateTime.Today.Date).CountAsync());
        }
        public async Task<int> QuantityProducts()
        {
            return (await _ctx.Set<Loan>().CountAsync()) +
                (await _ctx.Set<Card>().CountAsync());
        }
        public async Task<int> GetTransactionsAsync()
        {
            return await _ctx.Set<PayExpress>().CountAsync();
        }

        public async Task<int> GetTransactionsTodayAsync()
        {
            return await _ctx.Set<PayExpress>().Where(p => p.Date == DateTime.Now.Date).CountAsync();
        }
    }
}
