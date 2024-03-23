using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.Card
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public decimal Limit { get; set; }
        public decimal AmountAvailable { get; set; }
        public List<CardPay>? Payments { get; set; }

    }
}
