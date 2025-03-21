﻿

using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IloanRepository : IBaseRepository<Loan>
    {
        Task<bool> IsPending(int idLoan);
    }
}
