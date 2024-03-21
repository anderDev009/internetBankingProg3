

using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    //clase para manejo de cuentas de banco 
    public class BankAccountService : BaseService<IBankAccountService,SaveBankAccountViewModel,Account>
                                      ,IBankAccountService
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;
        public BankAccountService(IMapper mapper,
                                  IBankAccountRepository bankAccountRepository)
                                    : base(bankAccountRepository,mapper)
        {
            _mapper = mapper;
            _bankAccountRepository = bankAccountRepository;
        }

        //el id user se le debe pasar en el vm debido a que el usuario admin es el que asigna 
        //cuentas de banco
        public override async Task<SaveBankAccountViewModel> SaveAsync(SaveBankAccountViewModel vm)
        {
            //comprobamos que el ID no sea null
            if (vm.IdUser == null)
            {
                return null;
            }
            //comprobamos que en caso de que se marque como cuenta principal el usuario no tenga
            //una existente
            if (vm.IsMainAccount && (await _bankAccountRepository.UserHasMainAccount(vm.IdUser)))
            {
                //va a devolver null en caso de que el usuario contenga una cuenta principal
                return null;
            }
            //creamos un objeto cuenta y le agregamos el monto inicial en balance
            Account accountToAdd = _mapper.Map<Account>(vm);
            //generacion y asignacion de los 9 digitos como codigo unico
            accountToAdd.Code = CodeGenerator.Unique9DigitsGenerator().ToString(); 
            accountToAdd.Balance = accountToAdd.InitialAmmount;
            //lo enviamos al repositorio
            var account = await _bankAccountRepository.SaveAsync(accountToAdd);
            return _mapper.Map<SaveBankAccountViewModel>(account);
        }

        public override async Task RemoveAsync(int id)
        {
            var account = await _bankAccountRepository.GetByIdAsync(id);
            if(account == null)
            {
                //en caso de no encontrarla lanzara una excepcion indicando que la cuenta no existe
                throw new Exception("Esta cuenta no existe");
            }
            if (account.IsMainAccount)
            {
                //en caso de que la cuenta este vinculada como principal no se debe eliminar
                throw new Exception("Esta cuenta esta vinculada como principal, no puede eliminarla.");
            }
            //en caso de que la cuenta tenga dinero se transferira a la cuenta principal
            if(account.Balance > 0)
            {
                //metodo para transferir todo el balance a la cuenta principal
                await _bankAccountRepository.TransferBalanceToMainAccountAsync(account.Code);
            }
            await _bankAccountRepository.RemoveAsync(account);
        }


        //Metodo propio de creacion de objeto para cuando se vaya a crear un usuario con cuenta
        //Este metodo retorna siempre la cuenta cono main
        public SaveBankAccountViewModel CreateNewBank(string IdUser, decimal InitialAmmount)
        {
            SaveBankAccountViewModel bank = new SaveBankAccountViewModel();
            bank.IdUser = IdUser;
            bank.InitialAmmount = InitialAmmount;
            bank.IsMainAccount = true;
            return bank;
        }
        //Metodo para sumar el monto que quieta poner en la cuenta principal
        public async Task UserSumAmmount(string IdUser, decimal Ammount)
        {
            var account = await GetUserMainBank(IdUser);
            account.Balance += Ammount;
            await _bankAccountRepository.UpdateAsync(account, account.Code);
        }

        //metodo para buscar cuenta main de usuario
        public async Task<Account> GetUserMainBank(string IdUser)
        {
            var MainBank = await _bankAccountRepository.GetAllAsync();
            return MainBank.First(a => a.IsMainAccount == true && a.IdUser == IdUser);
        }
    }
}
