using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.PayExpress;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    public class PayExpressService : BaseService<PayExpressViewModel, SavePayExpressViewModel, PayExpress>,
                                     IPayExpressService
    {
        private readonly IPayExpressRepository _payExpressRepository;
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        public PayExpressService(IPayExpressRepository payExpressRepository,
                                 IMapper mapper,
                                 IBankAccountService bankAccountService) 
            : base(payExpressRepository, mapper)
        {
            _payExpressRepository = payExpressRepository;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }
        public override async Task<SavePayExpressViewModel> SaveAsync(SavePayExpressViewModel vm)
        {
            //comprobamos el numero de cuenta con un Any
            if(!await _bankAccountService.AccountExistsAsync(vm.AccountNumber))
            {
                throw new Exception("Este numero de cuenta no existe.");
            }
            //obtenemos la cuenta a pagar
            SaveBankAccountViewModel accountToPaid = await _bankAccountService.GetByIdAsync(int.Parse(vm.AccountNumber));
            //obtenemos la cuenta del usuario
            SaveBankAccountViewModel account = await _bankAccountService.GetByIdAsync(int.Parse(vm.IdAccountPaid));
            if(account == null)
            {
                throw new Exception("Cuenta invalida.");
            }
            //comprobamos si contiene el saldo suficiente
            if(account.Balance < vm.Amount)
            {
                throw new Exception("La cuenta no tiene balance suficiente para realizar este pago.");
            }
            //restamos el balance y actualizamos la cuenta del usuario y luego registramos
            //el pago
            account.Balance -= vm.Amount;
            accountToPaid.Balance += vm.Amount;
            //actualizamos ambas cuentas
            await _bankAccountService.UpdateAsync(accountToPaid, int.Parse(accountToPaid.Code));
            await _bankAccountService.UpdateAsync(account, int.Parse(account.Code));
            return await base.SaveAsync(vm);      
        }
    }
}
