using Azure;
using InternetBanking.Core.Application.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebApp.Controllers;

namespace WebApp.MiddledWares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUser _userValidate;

        public LoginAuthorize(ValidateUser userValidate)
        {
            _userValidate = userValidate;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var u = _userValidate.HasUser();
            //si hay algun usuario en la app
            if (u != null)
            {
                //instancia del controlador
                var controller = (UserController)context.Controller;
                if (u.Roles.Contains(Roles.Administrator.ToString()))
                {
                    context.Result = controller.RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    context.Result = controller.RedirectToRoute(new { controller = "Home", action = "Home" });
                }
            }
            else
            {
                //Si no tiene usuario no hace nada, sigue normal
                await next();
            }
        }
    }
}
