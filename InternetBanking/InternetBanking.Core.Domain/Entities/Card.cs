
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Card : BaseEntity
    {
        public string IdUser {  get; set; }
        public decimal Limit { get; set; }
        public decimal AmountAvailable { get; set; }
        
        public List<CardPay>? Payments {  get; set; }
    }
}
