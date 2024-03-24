using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.PayExpress;
using InternetBanking.Core.Application.ViewModels.Card;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Client")]

    public class CashAdvanceController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBankAccountService _bankAccountService;
        private readonly ICardService _cardService;
        private readonly IPayCardService _payCardService;

        public CashAdvanceController(IHttpContextAccessor contextAccessor, IBankAccountService bankAccountService, 
                                    ICardService cardService, IPayCardService payCardService)
        {
            _contextAccessor = contextAccessor;
            _bankAccountService = bankAccountService;
            _cardService = cardService;
            _payCardService = payCardService;
        }
        public async Task<IActionResult> Index()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var cards = await _cardService.GetAllAsync();
            var banks = await _bankAccountService.GetAllAsync();
            ViewBag.cards = cards.FindAll(a => a.IdUser == user.Id);
            ViewBag.banks = banks.FindAll(a => a.IdUser == user.Id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CashAdvanceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Pay", action = "Express" });
            }
            try
            {
                await _payCardService.CashAdvance(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToRoute(new { controller = "Pay", action = "Express" });
            }

            return RedirectToRoute(new { controller = "Home", action = "Home" });

        }
    }
}
