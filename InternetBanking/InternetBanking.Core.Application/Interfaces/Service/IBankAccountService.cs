using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IBankAccountService : IBaseService<BankAccountViewModel,
                                                        SaveBankAccountViewModel
                                                       ,Account>
    {
        SaveBankAccountViewModel CreateNewBank(string IdUser, decimal InitialAmmount);
        Task UserSumAmmount(string IdUser, decimal Ammount);
        Task<Account> GetUserMainBank(string IdUser);
        Task RemoveAsync(string code);

        Task<List<BankAccountViewModel>> GetAccountsByIdUserAsync(string id);
        Task<bool> AccountExistsAsync(string id);
   }
}
