
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.BankAccount
{
    public class SaveBankAccountViewModel
    {
        public string Code { get; set; }

        public string? IdUser { get; set; }

        public decimal? InitialAmmount { get; set; }

        //el balance solo se pasa en caso de edicion a la hora de su creacion no 
        //se tomara en cuenta 
        public decimal? Balance { get; set; }
        
    
        //si el usuario tiene registrado una cuenta principal lanzara una excepcion
        public bool IsMainAccount { get; set; }
    }
}
