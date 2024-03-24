
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly InternetBankingContext _ctx;
        public CardRepository(InternetBankingContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }


        //metodo para adelantos en efectivo
        public async Task CashAdvance(int IdCard, decimal amount)
        {
            Card card = await _ctx.Set<Card>().FindAsync(IdCard);
            if (card == null)
            {
                throw new Exception("Esta tarjeta no existe");
            }
            card.AmountAvailable -= amount;
            await UpdateAsync(card, card.Id);
        }
    }
}
