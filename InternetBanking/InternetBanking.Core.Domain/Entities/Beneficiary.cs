

using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Beneficiary : BaseEntity
    {
        public string Name {  get; set; }
        public string LastName { get; set; }
        public string AccountNumber {  get; set; }
        public Account? accountBeneficiary {  get; set; }

    }
}
