using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManager()
        {
            return View();
        }
    }
}
