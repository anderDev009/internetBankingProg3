
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.BankAccount
{
    public class BankAccountViewModel
    {
        public string Code { get; set; }
        public int IdUser { get; set; }
        public decimal InitialAmmount { get; set; }
        public decimal Balance { get; set; }
        public bool IsMainAccount { get; set; }

        public List<BeneficiaryViewModel>? Beneficiaries { get; set; }
        //pagos realizados a tarjetas
        public List<CardPay>? CardPayments { get; set; }
        //pagos a prestamos
        public List<LoanPay>? LoanPayments { get; set; }
        //pagos que le han hecho a esta cuenta
        public List<PayExpress>? PaymentsMade { get; set; }
        //pagos a otras cuentas
        public List<PayExpress> PaymentsOtherAccount { get; set; }
    }
}
