using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.PayCard;
using InternetBanking.Core.Application.ViewModels.PayExpress;
using InternetBanking.Core.Application.ViewModels.PayLoan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Client")]
    public class PayController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPayExpressService _payExpressService;
        private readonly IPayLoanService _payLoanServices;
        private readonly IPayCardService _payCardService;
        private readonly IBankAccountService _bankAccountService;
        private readonly ICardService _cardService;
        private readonly ILoanService _loanService;
        public PayController(IPayExpressService payExpressService, IHttpContextAccessor contextAccessor,
            IBankAccountService bankAccountService, ICardService cardAccountService, IPayCardService payCardService,
            IPayLoanService payLoanServices, ILoanService loanService)
        {
            _contextAccessor = contextAccessor;
            _payExpressService = payExpressService;
            _payLoanServices = payLoanServices;
            _payCardService = payCardService;
            _bankAccountService = bankAccountService;
            _cardService = cardAccountService;
            _loanService = loanService;
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
                || ModelState["IdCard"].Errors.Any())
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

        //pagos loan
        public async Task<IActionResult> Loan()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var loans = await _loanService.GetAllAsync();
            var banks = await _bankAccountService.GetAllAsync();
            ViewBag.loans = loans.FindAll(a => a.IdUser == user.Id);
            ViewBag.banks = banks.FindAll(a => a.IdUser == user.Id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Loan(SavePayLoanViewModel vm)
        {
            if (ModelState["Amount"].Errors.Any() || ModelState["IdAccountPaid"].Errors.Any()
                || ModelState["IdLoan"].Errors.Any())
            {
                return RedirectToRoute(new { controller = "Pay", action = "Loan" });
            }
            try
            {
                await _payLoanServices.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToRoute(new { controller = "Pay", action = "Loan" });
            }

            return RedirectToRoute(new { controller = "Pay", action = "Loan" });

        }
    }
}
