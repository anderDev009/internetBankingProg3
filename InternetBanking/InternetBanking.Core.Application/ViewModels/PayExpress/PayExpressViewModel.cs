
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.PayExpress
{
    public class PayExpressViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string IdAccountPaid { get; set; }
        public BankAccountViewModel? AccountPaid { get; set; }
        public DateTime Date { get; set; }
        public string AccountNumber { get; set; }

        //cuenta a la que se le realizo el pago
        public BankAccountViewModel? AccountPaymentMade { get; set; }
    }
}
