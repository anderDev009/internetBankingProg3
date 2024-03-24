

using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Application.ViewModels.PayLoan;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IPayLoanService : IBaseService<PayLoanViewModel, SavePayLoanViewModel, Loan>
    {
    }
}
