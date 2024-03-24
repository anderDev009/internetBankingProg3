

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Card
{
    public class SaveCardViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }


        [Required(ErrorMessage = "Debe colocar un limite de la tarjeta")]
        [Range(0,double.MaxValue,ErrorMessage = "Debe colocar un valor positivo")]
        public decimal Limit { get; set; }
        public decimal? AmountAvailable { get; set; }
    }
}
