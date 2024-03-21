

using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BankAccountRepository : BaseRepository<Account>, IBankAccountRepository
    {
        private readonly InternetBankingContext _ctx;
        public BankAccountRepository(InternetBankingContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AccountExistsAsync(string accountId)
        {
            return await _ctx.Set<Account>().AnyAsync(c => c.Code == accountId);
        }

        public override async Task<Account> GetByIdAsync(int id)
        {
            string code = id.ToString();
            return await _ctx.Set<Account>().Where(a => a.Code == code).FirstOrDefaultAsync();
        }
        public async Task<bool> HasMoney(string codeAccount)
        {
            bool HasMoney = _ctx.Set<Account>().Where(a => a.Code == codeAccount)
                .First().Balance > 0 ? true : false;

            return HasMoney;
        }

        public async Task TransferBalanceToMainAccountAsync(string codeAccount)
        {
            //cuenta de donde saldra el dinero
            Account account = await _ctx.Set<Account>().Where(a => a.Code == codeAccount).FirstAsync();
            //cuenta a la que se va a transferir el dinero.
            Account accountToTransfer = await _ctx.Set<Account>()
                .Where(a => a.IdUser == account.IdUser && a.IsMainAccount == true)
                .FirstAsync();
            if(account != null && accountToTransfer != null)
            {
                accountToTransfer.Balance += account.Balance;
                account.Balance = 0;
                _ctx.Set<Account>().Update(account);
                _ctx.Set<Account>().Update(accountToTransfer);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<bool> UserHasMainAccount(string idUser)
        {
            //Busqueda de la cuenta principal en caso de que exista
            bool HasMainAccount = _ctx.Set<Account>().Any(account => account.IdUser == idUser);
            return HasMainAccount;
        }
    }
}
