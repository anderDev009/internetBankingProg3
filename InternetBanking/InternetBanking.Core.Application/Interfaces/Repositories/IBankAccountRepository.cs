using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IBankAccountRepository : IBaseRepository<Account>
    {
        //metodo para saber si ya un usuario tiene asignado una cuenta principal
        Task<bool> UserHasMainAccount(string idUser);
        Task<bool> HasMoney(string codeAccount);
        Task TransferBalanceToMainAccountAsync(string codeAccount);
        Task<Account> UpdateAsync(Account entity, string id);
    }
}
