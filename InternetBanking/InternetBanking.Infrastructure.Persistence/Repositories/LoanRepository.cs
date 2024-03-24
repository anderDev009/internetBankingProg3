

using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : BaseRepository<Loan>, IloanRepository
    {
        private readonly InternetBankingContext _ctx;
        public LoanRepository(InternetBankingContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }


        //metodo para comprobar si un prestamo ha sido saldado
        public async Task<bool> IsPending(int idLoan)
        {
            //variable para confirmar si queda monto pendiente
            //LoanUser significa el monto que tomo el usuario como prestamo
            //por eso restamos la suma de todos los pagos que realizo el usuario a dicho prestamo menos el 
            //monto total que tomo prestado
            decimal valuePayments = await _ctx.Set<Loan>().Include(l => l.Payments)
                                    .Where(l => l.Id == idLoan )
                                    .Select(p => (p.Payments.Sum(l => l.Amount) - p.LoanUser)).FirstAsync();
            //en caso de que sea mayor que 0 se retorna true para confirmar que sigue pendiente
            if(valuePayments > 0)
            {
                return true;
            }
            return false;
        }
    }
}
