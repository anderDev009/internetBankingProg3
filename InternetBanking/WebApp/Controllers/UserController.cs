﻿using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Users;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //metodo desactivar

        public async Task<IActionResult> Desactivated(string Id)
        {
            await _userService.DesactiveUser(Id);
            return RedirectToRoute(new { controller = "Admin", action = "UserManager" });
        }

        //crear usuario
        public async Task<IActionResult> Register()
        {
            return View(new SaveUserViewModel());
        }

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
        public async Task<IActionResult> Update(String Id)
        {

            return View(await _userService.GetByIdUser(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid && vm.Id != null)
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
    }
}
