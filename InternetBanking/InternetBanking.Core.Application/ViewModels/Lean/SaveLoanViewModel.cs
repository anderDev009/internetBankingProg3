

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Lean
{
    public class SaveLoanViewModel
    {
        public int IdUser { get; set; }
        //Prestamo que tomo el usuario
        //cantidad del prestamo
        [Range(0,double.MaxValue, ErrorMessage = "Debe colocar un monto")]
        public decimal LoanUser { get; set; }
        //balance pendiente a pagar
        public decimal? BalanceLoan { get; set; }

    }
}
