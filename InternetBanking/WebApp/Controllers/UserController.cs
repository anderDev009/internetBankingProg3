using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Users;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.Enums;
using WebApp.MiddledWares;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserServices _userService;
        private readonly ICardService _cardService;
        private readonly ILoanService _loanService;
        private readonly IBankAccountService _bankAccountService;

        public UserController(IUserServices userService, ICardService cardService, ILoanService loanService, 
            IBankAccountService bankAccountService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _cardService = cardService;
            _loanService = loanService;
            _bankAccountService = bankAccountService;
            _contextAccessor = contextAccessor;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AuthenticationResponse response = await _userService.LoginAsync(login);
            if(response != null && response.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", response);
                if (response.Roles.Contains(Roles.Administrator.ToString()))
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Home", action = "Home" });

            }
            else
            {
                login.HasError = response.HasError;
                login.Error = response.Error;
                return View(login);

            }
        }

        //LogOut metodo
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        //metodo activar
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Activate(string Id)
        {
            await _userService.ActiveUser(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //metodo desactivar
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Desactivated(string Id)
        {
            await _userService.DesactiveUser(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //crear usuario
        [ServiceFilter(typeof(LoginAuthorize))]

        public async Task<IActionResult> Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid && vm.Id != null)
            {
                return View(vm);
            }
            SaveUserViewModel response = await _userService.RegisterAsync(vm);
            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }

        }



        //editar usuario
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(String Id)
        {

            SaveUserViewModel vm = await _userService.GetByIdUser(Id);
            var ListC = await _cardService.GetAllAsync();
            var ListL = await _loanService.GetAllAsync();
            var ListA = await _bankAccountService.GetAllAsync();
            ViewBag.CardList = ListC.FindAll(u => u.IdUser == Id).ToList();
            ViewBag.LoanList = ListL.FindAll(u => u.IdUser == Id).ToList();
            ViewBag.AccountList = ListA.FindAll(a => a.IdUser == Id).ToList();
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Update(SaveUserViewModel vm)
        {
            if (ModelState["Email"].Errors.Any() || ModelState["FirstName"].Errors.Any()
                || ModelState["LastName"].Errors.Any() || ModelState["CardIdentificantion"].Errors.Any()
                || ModelState["Username"].Errors.Any())
            {
                return View(vm);
            }
            SaveUserViewModel response = await _userService.UpdateUserAsync(vm);
            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }

        }
        //crear Cuenta
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateAccount(String Id)
        {
            ViewBag.IdUser = Id;
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(SaveBankAccountViewModel vm)
        {
            if (ModelState["InitialAmmount"].Errors.Any())
            {
                return View(vm);
            }
            await _bankAccountService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //Borrar cuenta de bank
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBank (string Id)
        {
            await _bankAccountService.RemoveAsync(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }


        //crear tarjeta de credito
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateCard(String Id)
        {
            ViewBag.IdUser = Id;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateCard(SaveCardViewModel vm)
        {
            if (ModelState["Limit"].Errors.Any())
            {
                return View(vm);
            }
            await _cardService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }
        //Borrar tarjeta de credito
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCard(int Id)
        {
            await _cardService.RemoveAsync(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //Crear prestamo
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateLoan(String Id)
        {
            ViewBag.IdUser = Id;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateLoan(SaveLoanViewModel vm)
        {
            if (ModelState["LoanUser"].Errors.Any())
            {
                return View(vm);
            }
            vm.BalanceLoan = vm.LoanUser;
            await _loanService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //Borrar prestamo
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteLoan(int Id)
        {
            await _loanService.RemoveAsync(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //denegacion de acceso
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

    }

    
}
