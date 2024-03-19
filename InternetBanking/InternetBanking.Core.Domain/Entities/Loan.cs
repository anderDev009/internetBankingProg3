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
        public int Id { get; set; }
        public string IdUser { get; set; } 
        //Prestamo que tomo el usuario
        public decimal LoanUser {  get; set; }
        //balance pendiente
        public decimal BalanceLoan { get; set; }

        public List<LoanPay>? Payments {  get; set; }
    }
}
