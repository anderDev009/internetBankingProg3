using InternetBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Domain.Entities
{
    //prestamo
    public class Loan : BaseEntity
    {
        public int IdUser { get; set; } 
        public decimal LoanUser {  get; set; }
        public decimal BalanceLoan { get; set; }

        public List<LoanPay>? Payments {  get; set; }
    }
}
