using InternetBanking.Core.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserServices _userService;

        public AdminController(IUserServices userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserManager()
        {
            ViewBag.ListClient = await _userService.GetAllUserClientAsync();
            ViewBag.ListAdmin = await _userService.GetAllUserAdminAsync();
            return View();
        }
    }
}
