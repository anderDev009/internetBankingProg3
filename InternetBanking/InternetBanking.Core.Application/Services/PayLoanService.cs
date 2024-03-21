using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Application.ViewModels.PayLoan;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    internal class PayLoanService : BaseService<PayLoanViewModel, SavePayLoanViewModel, Loan>, IPayLoanService
    {
        private readonly IPayLoanRepository _loanRepository;
        private readonly IMapper _mapper;
        //servicios 
        private readonly ILoanService _loanService;
        private readonly IBankAccountService _accountService;
        public PayLoanService(IPayLoanRepository loanRepository, IMapper mapper, ILoanService loanService, IBankAccountService accountService) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _loanService = loanService;
            _accountService = accountService;

        }

        public override async Task<SavePayLoanViewModel> SaveAsync(SavePayLoanViewModel vm)
        {
            //obtenemos el prestamo "Loan"
            var loan = await _loanService.GetByIdAsync(vm.IdLoan);
            if (loan == null)
            {
                throw new Exception("Prestamo no existe.");
            }
            //obtenemos la cuenta de banco que pagara el prestamo
            var account = await _accountService.GetByIdAsync(int.Parse(vm.IdAccountPaid));
            if (account == null)
            {
                throw new Exception("Numero de cuenta invalido.");
            }
            //comprobamos si contiene el saldo suficiente
            if (account.Balance < vm.Amount)
            {
                throw new Exception("La cuenta no tiene balance suficiente para realizar este pago.");
            }
            if (vm.Amount > loan.BalanceLoan)
            {
                vm.Amount = (decimal)loan.BalanceLoan;
            }
            //restamos el balance y actualizamos la cuenta del usuario y luego registramos
            //el pago
            account.Balance -= vm.Amount;
            await _accountService.UpdateAsync(account, int.Parse(account.Code));
            //actualizamos el prestamo
            loan.BalanceLoan += vm.Amount;
            await _loanService.UpdateAsync(loan, loan.Id);
            return await base.SaveAsync(vm);
        }
    }
}
