
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.PayLoan
{
    public class PayLoanViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string IdAccountPaid { get; set; }
        public BankAccountViewModel? AccountPaid { get; set; }
        public DateTime? Date { get; set; }
        public int IdLoan { get; set; }

        public PayLoanViewModel? Loan { get; set; }
    }
}
