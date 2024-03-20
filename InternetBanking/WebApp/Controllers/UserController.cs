using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Users;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

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
                return RedirectToRoute(new { controller = "Home", action = "Index" });
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
            HttpContext.Session.Remove("user_session");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        //metodo activar

        public async Task<IActionResult> Activate(string Id)
        {
            await _userService.ActiveUser(Id);
            return View();
        }

        //metodo desactivar

        public async Task<IActionResult> Desactivated(string Id)
        {
            await _userService.DesactiveUser(Id);
            return View();
        }
    }
}
