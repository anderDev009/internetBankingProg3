using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService _dashboardService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserServices _userService;
        private readonly ICardService _cardService;
        private readonly ILoanService _loanService;
        private readonly IBankAccountService _bankAccountService;

        public HomeController(ILogger<HomeController> logger, IDashboardService dashboardService, 
            IHttpContextAccessor contextAccessor, IUserServices userService, ICardService cardService, 
            ILoanService loanService, IBankAccountService bankAccountService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _cardService = cardService;
            _loanService = loanService;
            _bankAccountService = bankAccountService;
        }


        [Authorize(Roles = "Administrator")]
        //dashboard user
        public async Task<IActionResult> Index()
        {
            //
            var vm = await _dashboardService.GetDashboard();
            //---
            return View(vm);
        }
        //Home de user
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Home()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var ListC = await _cardService.GetAllAsync();
            var ListL = await _loanService.GetAllAsync();
            var ListA = await _bankAccountService.GetAllAsync();
            ViewBag.CardList = ListC.FindAll(u => u.IdUser == user.Id).ToList();
            ViewBag.LoanList = ListL.FindAll(u => u.IdUser == user.Id).ToList();
            ViewBag.AccountList = ListA.FindAll(a => a.IdUser == user.Id).ToList();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
