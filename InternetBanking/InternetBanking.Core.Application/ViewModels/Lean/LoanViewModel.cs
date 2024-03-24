

using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.Lean
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        //Prestamo que tomo el usuario
        public decimal LoanUser { get; set; }
        //balance pendiente
        public decimal BalanceLoan { get; set; }

        public List<LoanPay>? Payments { get; set; }
    }
}
