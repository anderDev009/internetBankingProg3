
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiaryRepository : IBaseRepository<Beneficiary>
    {
        Task<List<Beneficiary>> GetBeneficiaryByIdUser(string IdUser);
    }
}
