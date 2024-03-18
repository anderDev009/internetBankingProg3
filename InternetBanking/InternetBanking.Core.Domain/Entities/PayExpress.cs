
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class PayExpress : BasePay
    {
        public string AccountNumber { get; set; }

        //cuenta a la que se le realizo el pago
        public Account? AccountPaymentMade {  get; set; }
    }
}
