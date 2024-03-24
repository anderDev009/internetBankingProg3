using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Application.ViewModels.PayCard;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IPayCardService : IBaseService<PayCardViewModel,SavePayCardViewModel,CardPay>
    {
        Task CashAdvance(CashAdvanceViewModel vm);
    }
}
