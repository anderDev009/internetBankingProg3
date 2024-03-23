

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.PayExpress
{
    public class SavePayExpressViewModel
    {
        public int? Id {  get; set; }

        [Range(0,double.MaxValue,ErrorMessage = "Debe colocar un monto valido.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Necesita una cuenta para realizar una transaccion")]
        public string IdAccountPaid { get; set; }
        //cuenta destino
        [Required(ErrorMessage = "Necesita colocar la cuenta destino")]
        public string AccountNumber {  get; set; }
    }
}
