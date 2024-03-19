

using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public int Id { get; set; } 
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public BankAccountViewModel? accountBeneficiary { get; set; }
    }
}
