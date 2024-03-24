
using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface ICardService : IBaseService<CardViewModel,SaveCardViewModel,Card>
    {
    }
}
