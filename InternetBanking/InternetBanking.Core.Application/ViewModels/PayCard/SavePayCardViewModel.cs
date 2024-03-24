using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.Card;
using System.ComponentModel.DataAnnotations;


namespace InternetBanking.Core.Application.ViewModels.PayCard
{
    public class SavePayCardViewModel
    {
        public int Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Coloca un monto valido.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Debe colocar un numero de cuenta valido")]
        public string IdAccountPaid { get; set; }
        public DateTime? Date { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe colocar un ID valido.")]
        public int IdCard { get; set; }

      
    }
}
