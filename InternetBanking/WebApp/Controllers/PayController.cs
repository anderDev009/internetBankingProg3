using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.PayCard;
using InternetBanking.Core.Application.ViewModels.PayExpress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Client")]
    public class PayController : Controller
    {
        private readonly IPayExpressService _payExpressService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBankAccountService _bankAccountService;
        private readonly IPayCardService _payCardService;
        private readonly ICardService _cardService;
        public PayController(IPayExpressService payExpressService, IHttpContextAccessor contextAccessor,
            IBankAccountService bankAccountService, ICardService cardAccountService, IPayCardService payCardService)
        {
            _payExpressService = payExpressService;
            _contextAccessor = contextAccessor;
            _bankAccountService = bankAccountService;
            _cardService = cardAccountService;
            _payCardService = payCardService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //pagos express
        public async Task<IActionResult> Express()
        {
            var user =  _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var accounts = await _bankAccountService.GetAccountsByIdUserAsync(user.Id);
            ViewBag.Accounts = accounts;
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> Express(SavePayExpressViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Pay", action = "Express" });
            }
            try
            {
                await _payExpressService.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToRoute(new { controller = "Pay", action = "Express" });
            }

            return RedirectToRoute(new { controller = "Pay", action = "Express" });

        }

        //pagos card
        public async Task<IActionResult> Card()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var cards = await _cardService.GetAllAsync();
            var banks = await _bankAccountService.GetAllAsync();
            ViewBag.cards = cards.FindAll(a => a.IdUser == user.Id);
            ViewBag.banks = banks.FindAll(a => a.IdUser == user.Id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Card(SavePayCardViewModel vm)
        {
            if (ModelState["Amount"].Errors.Any() || ModelState["IdAccountPaid"].Errors.Any()
                || ModelState["IdAccountPaid"].Errors.Any())
            {
                return RedirectToRoute(new { controller = "Pay", action = "Card" });
            }
            try
            {
                await _payCardService.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToRoute(new { controller = "Pay", action = "Card" });
            }

            return RedirectToRoute(new { controller = "Pay", action = "Card" });

        }

        public async Task<IActionResult> Transfer()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var accounts = await _bankAccountService.GetAccountsByIdUserAsync(user.Id);
            ViewBag.Accounts = accounts;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(SavePayExpressViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = "Campos invalidos";
                return View("Transfer");
            }
            //comprobamos que no sea a la misma cuenta
            if(vm.AccountNumber == vm.IdAccountPaid)
            {
                ViewBag.Error = "No puedes transferir dinero a la misma cuenta.";
                return View("Transfer");
            }
            try
            {
                await _payExpressService.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToRoute(new { controller = "Pay", action = "Transfer" });
            }
            ViewBag.Success = "Transferencia completada";
            return View("Transfer");
        }
            
    }
}
