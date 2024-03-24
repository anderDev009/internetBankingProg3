

using System.Runtime.CompilerServices;

namespace InternetBanking.Core.Application.ViewModels.Card
{
    public class CashAdvanceViewModel
    {
        public int IdCard {  get; set; }
        public decimal Amount { get; set; }
        public string CodeAccount { get; set; }
    }
}
