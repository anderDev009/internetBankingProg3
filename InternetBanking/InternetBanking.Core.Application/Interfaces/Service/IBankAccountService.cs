using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IBankAccountService : IBaseService<IBankAccountService,
                                                        SaveBankAccountViewModel
                                                       ,Account>
    {
    }
}
