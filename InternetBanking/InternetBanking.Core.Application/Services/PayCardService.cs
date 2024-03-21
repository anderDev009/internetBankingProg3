
using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Application.ViewModels.PayCard;
using InternetBanking.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.Services
{
    public class PayCardService : BaseService<PayCardViewModel, SavePayCardViewModel, CardPay>, IPayCardService
    {
        private readonly IPayCardRepository _payCardRepository;
        private readonly ICardService _cardService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        public PayCardService(IPayCardRepository payCardRepository, 
                              IMapper mapper,
                              ICardService cardService,
                              IBankAccountService bankAccountService) : base(payCardRepository, mapper)
        {
            _payCardRepository = payCardRepository;
            _mapper = mapper;
            _cardService = cardService;
            _bankAccountService = bankAccountService;

        }

        public async Task CashAdvance(CashAdvanceViewModel vm)
        {
            //cuenta de banco
            SaveBankAccountViewModel account = await _bankAccountService.GetByIdAsync(int.Parse(vm.CodeAccount));
            //tarjeta
            SaveCardViewModel card = await _cardService.GetByIdAsync(vm.IdCard);
            //validando que no pase el limite disponible
            if(vm.Amount > card.AmountAvailable)
            {
                throw new Exception("Este monto supera el limite disponible");
            }
            account.Balance += vm.Amount;
            card.AmountAvailable -= vm.Amount;
            //actualizando la tarjeta y la cuenta
            await _bankAccountService.UpdateAsync(account, int.Parse(account.Code));
            await _cardService.UpdateAsync(card, card.Id);
        }

        public override async Task<SavePayCardViewModel> SaveAsync(SavePayCardViewModel vm)
        {
            //obtenemos la tarjeta 
            SaveCardViewModel card = await _cardService.GetByIdAsync(vm.IdCard);
            if(card == null)
            {
                throw new Exception("Tarjeta invalida");
            }
            //obtenemos la cuenta de banco 
            SaveBankAccountViewModel account = await _bankAccountService.GetByIdAsync(int.Parse(vm.IdAccountPaid));
            if(account == null)
            {
                throw new Exception("Cuenta invalida");
            }
            //comprobamos que la cuenta tenga el saldo disponible para depositar
            if(account.Balance < (vm.Amount + (decimal)((double)vm.Amount * 0.0625)))
            {
                throw new Exception("Saldo insuficiente");
            }
            //logica en caso que el monto sea mayor al que se debe pagar
            var quotePay = card.Limit - card.AmountAvailable;
            if (vm.Amount > quotePay)
            {
                vm.Amount = (decimal)((double)quotePay * 0.0625);
            }
      
            //actualizando la cuenta de banco del usuario
            account.Balance -= vm.Amount;
            await _bankAccountService.UpdateAsync(account, int.Parse(account.Code));
            //actualizamos la tarjeta
            card.AmountAvailable += vm.Amount;
            await _cardService.UpdateAsync(card, card.Id);
            //registrando el pago en la BD
            return await base.SaveAsync(vm);
        }

    }
}
