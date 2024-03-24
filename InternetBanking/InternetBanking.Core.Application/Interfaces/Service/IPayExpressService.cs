
using InternetBanking.Core.Application.ViewModels.PayExpress;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IPayExpressService : IBaseService<PayExpressViewModel,SavePayExpressViewModel,PayExpress>
    {
    }
}
