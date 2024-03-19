
using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class LoanService : BaseService<LoanViewModel, SaveLoanViewModel, Loan>, ILoanService
    {
        private readonly IloanRepository _leanRepository;
        private readonly IMapper _mapper;
        public LoanService(IloanRepository leanRepository,IMapper mapper) : base(leanRepository,mapper)
        {
            _leanRepository = leanRepository;
            _mapper = mapper;
        }
        //override para comprobar que el balance no sea 0 
        public override async Task RemoveAsync(int id)
        {
            //comprobamos que el prestamo no sigan pendiente
            bool isPending = await _leanRepository.IsPending(id);
            if(isPending)
            {
                throw new Exception("El usuario esta pendiente a saldar el prestamo.");
            }
            await base.RemoveAsync(id);
        }
    }
}
