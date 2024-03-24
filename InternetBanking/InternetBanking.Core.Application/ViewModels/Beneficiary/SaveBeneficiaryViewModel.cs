


using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    //en este ViewModel solo es necesario pasar el numero de cuenta al servicio de Beneficiario
    public class SaveBeneficiaryViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        [Range(0,double.MaxValue,ErrorMessage = "Debe colocar el numero de cuenta")]
        public int AccountNumber { get; set; }

    }
}
