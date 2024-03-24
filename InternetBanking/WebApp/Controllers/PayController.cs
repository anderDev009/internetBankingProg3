using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.PayCard;
using InternetBanking.Core.Application.ViewModels.PayExpress;
using InternetBanking.Core.Application.ViewModels.PayLoan;
using InternetBanking.Core.Domain.Entities;
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
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IUserServices _userServices;
        public PayController(IPayExpressService payExpressService, IHttpContextAccessor contextAccessor,
            IBankAccountService bankAccountService, ICardService cardAccountService, IPayCardService payCardService,
            IPayLoanService payLoanServices, ILoanService loanService, IBeneficiaryService beneficiaryService,
            IUserServices userServices)
        {
            _contextAccessor = contextAccessor;
            _payExpressService = payExpressService;
            _payLoanServices = payLoanServices;
            _payCardService = payCardService;
            _bankAccountService = bankAccountService;
            _cardService = cardAccountService;
            _loanService = loanService;
            _beneficiaryService = beneficiaryService;
            _userServices = userServices;
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
            return View("Express");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmExpress(SavePayExpressViewModel vm)
        {
            var accountClientExist = await _bankAccountService.AccountExistsAsync(vm.AccountNumber);
            if (!accountClientExist)
            {
                ViewBag.Error = "Esta cuenta no existe";
                return await Express();
            }
            var account = await _bankAccountService.GetByIdAsync(int.Parse(vm.IdAccountPaid));
            if(account.Balance < vm.Amount)
            {
                ViewBag.Error = "No dispone del balance suficiente";
                return await Express();
            }
            var accountClient = await _bankAccountService.GetByIdAsync(int.Parse(vm.AccountNumber));
            ViewBag.UserToPaid = await _userServices.GetByIdUser(accountClient.IdUser);
            return View("ConfirmExpress",vm);
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
                return await Express();
            }

            return View("Success");

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
            ViewBag.Error = TempData["Error"] as string;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Loan(SavePayLoanViewModel vm)
            {
            if (ModelState["Amount"].Errors.Any() || ModelState["IdAccountPaid"].Errors.Any()
                || ModelState["IdLoan"].Errors.Any())
            {
                var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
                var loans = await _loanService.GetAllAsync();
                var banks = await _bankAccountService.GetAllAsync();
                ViewBag.loans = loans.FindAll(a => a.IdUser == user.Id);
                ViewBag.banks = banks.FindAll(a => a.IdUser == user.Id);
                return View(vm);
            }
            try
            {
                await _payLoanServices.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                if (!(ex is AutoMapper.AutoMapperMappingException))
                {
                    TempData["Error"] = ex.Message;
                }
                return RedirectToRoute(new { controller = "Pay", action = "Loan" });
            }

            return RedirectToRoute(new { controller = "Pay", action = "Loan" });

        }

        //Pago beneficiario
        public async Task<IActionResult> Beneficiary()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var beneficiarys = await _beneficiaryService.GetBeneficiaryByIdUser(user.Id);
            var accounts = await _bankAccountService.GetAccountsByIdUserAsync(user.Id);

            ViewBag.Accounts = accounts;
            ViewBag.beneficiarys = beneficiarys;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Beneficiary(SavePayExpressViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
                var beneficiarys = await _beneficiaryService.GetBeneficiaryByIdUser(user.Id);
                var accounts = await _bankAccountService.GetAccountsByIdUserAsync(user.Id);

                ViewBag.Accounts = accounts;
                ViewBag.beneficiarys = beneficiarys;
                return View("Beneficiary", vm);
            }
            try
            {
                await _payExpressService.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return await Beneficiary() ;
            }

            return RedirectToRoute(new { controller = "Pay", action = "Beneficiary" });

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBeneficiary(SavePayExpressViewModel vm)
        {
            var accountClientExist = await _bankAccountService.AccountExistsAsync(vm.AccountNumber);
            if (!accountClientExist)
            {
                ViewBag.Error = "Esta cuenta no existe";
            }
            var account = await _bankAccountService.GetByIdAsync(int.Parse(vm.IdAccountPaid));
            if (vm.Amount > account.Balance)
            {
                ViewBag.Error = "No dispone del balance suficiente";
            }
            var accountClient = await _bankAccountService.GetByIdAsync(int.Parse(vm.AccountNumber));
            ViewBag.UserToPaid = await _userServices.GetByIdUser(accountClient.IdUser);
            return View("ConfirmBeneficiary", vm);
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
                return await Transfer();
            }
            //comprobamos que no sea a la misma cuenta
            if (vm.AccountNumber == vm.IdAccountPaid)
            {
                ViewBag.Error = "No puedes transferir dinero a la misma cuenta.";
                return await Transfer();
            }
            try
            {
                await _payExpressService.SaveAsync(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return await Transfer();
            }
            return View("Success");
        }

    }
}