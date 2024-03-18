

using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Domain.Common
{
    public class BasePay : BaseEntity
    {
        public decimal Amount {  get; set; }
        public string IdAccountPaid { get; set; }
        public Account? AccountPaid { get; set; }
        public DateTime Date {  get; set; }

    }
}
