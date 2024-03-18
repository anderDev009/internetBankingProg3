
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    //pago de prestamo
    public class LoanPay : BasePay
    {
        public int IdLoan {  get; set; }

        public Loan? Loan { get; set; }
    }
}
