using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Infrastructure.Identity.Services;
namespace WebApp.Controllers
{
    [Authorize(Roles = "Client")]

    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor httpContextAccessor, IBankAccountService bankAccountService, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryService = beneficiaryService;
            _bankAccountService = bankAccountService;
            _accountService = accountService;

        }
        public async Task<IActionResult> Index()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var beneficiariesUser = await _beneficiaryService.GetBeneficiaryByIdUser(user.Id);
            return View(beneficiariesUser);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _beneficiaryService.RemoveAsync(Id);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
        }

        public IActionResult Add()
        {
            return View(new SaveBeneficiaryViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(SaveBeneficiaryViewModel vm)
        {
            var account = await _bankAccountService.GetByIdAsync(vm.AccountNumber);
            if(account == null)
            {
                ViewBag.Error = "Cuenta no existe";
                return RedirectToRoute(new { controller = "Beneficiary", action = "Add" });

            }
            var userBeneficiary = await _accountService.GetByIdUser(account.IdUser);
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            SaveBeneficiaryViewModel send = new()
            {
                IdUser = user.Id,
                AccountNumber = vm.AccountNumber,
                Name = userBeneficiary.FirstName,
                LastName = userBeneficiary.LastName
            };
            await _beneficiaryService.SaveAsync(send);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" }); 
        }
    }
}
