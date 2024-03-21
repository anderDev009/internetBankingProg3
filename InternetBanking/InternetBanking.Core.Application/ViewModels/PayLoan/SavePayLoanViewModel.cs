
using InternetBanking.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.PayLoan
{
    public class SavePayLoanViewModel
    {
        public int Id { get; set; }

        [Range(0,double.MaxValue,ErrorMessage = "Debe colocar un monto valido.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Coloque un numero de cuenta valido.")]
        public string IdAccountPaid { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public int IdLoan { get; set; }

    }
}
