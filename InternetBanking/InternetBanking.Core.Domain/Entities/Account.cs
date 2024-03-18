using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Account 
    {
        public string Code { get; set; }
        public int IdUser {  get; set; }
        public decimal InitialAmmount { get; set; }
        public decimal Balance {  get; set; }
        public bool IsMainAccount { get; set; }

        public List<Beneficiary>? Beneficiaries { get; set; }
        //pagos realizados a tarjetas
        public List<CardPay>? CardPayments { get; set; }
        //pagos a prestamos
        public List<LoanPay>? LoanPayments{ get; set; }
        //pagos que le han hecho a esta cuenta
        public List<PayExpress>? PaymentsMade { get; set; }
        //pagos a otras cuentas
        public List<PayExpress> PaymentsOtherAccount { get; set; }
        
    }
}
