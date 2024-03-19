
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IBeneficiaryService : IBaseService<BeneficiaryViewModel,SaveBeneficiaryViewModel,Beneficiary>
    {
    }
}
