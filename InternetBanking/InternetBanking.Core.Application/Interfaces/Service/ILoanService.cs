using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface ILoanService : IBaseService<LoanViewModel,SaveLoanViewModel,Loan>
    {
    }
}
