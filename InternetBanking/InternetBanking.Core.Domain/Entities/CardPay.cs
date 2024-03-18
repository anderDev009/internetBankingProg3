

using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class CardPay : BasePay
    {
        public int IdCard { get; set; }
        public Card? Card { get; set; }
    }
}
