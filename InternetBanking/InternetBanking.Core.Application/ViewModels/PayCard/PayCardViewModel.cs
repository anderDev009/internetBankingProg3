using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.Card;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.PayCard
{
    public class PayCardViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string IdAccountPaid { get; set; }

        public int IdCard { get; set; }

        public BankAccountViewModel? AccountPaid { get; set; }
        public CardViewModel? Card { get; set; }
    }
}
