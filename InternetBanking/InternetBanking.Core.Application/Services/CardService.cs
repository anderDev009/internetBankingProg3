using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class CardService : BaseService<CardViewModel, SaveCardViewModel, Card>,
                               ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        public CardService(ICardRepository cardRepository,IMapper mapper) : base(cardRepository,mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        //hacemos override para comprobar que el idUser no sea nulo
        public override Task<SaveCardViewModel> SaveAsync(SaveCardViewModel vm)
        {
            //comprobamos que el ID no sea null
            if (vm.IdUser == null)
            {
                return null;
            }
            //le asignamos el monto disponible directamente
            vm.AmountAvailable = vm.Limit;
            return base.SaveAsync(vm);
        }
        //override del remove para comprobar de que no tenga balance pendiente
        public override async Task RemoveAsync(int id)
        {
            Card card = await _cardRepository.GetByIdAsync(id);
            //comprobamos si la tarjeta tiene dinero pendiente restando el limite con 
            //el monto disponible esto nos dejara la deuda restante y comprobamos si la deuda es mayor a 0
            decimal pending = card.Limit - card.AmountAvailable;
            if(pending > 0)
            {
                throw new Exception("Tiene dinero pendient en la tarjeta, no puede ser eliminada");
            }
            //removemos la tarjeta del sistema
            await base.RemoveAsync(id);
        }
    }
}
