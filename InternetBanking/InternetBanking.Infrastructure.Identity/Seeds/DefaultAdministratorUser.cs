

using InternetBanking.Core.Application.Enums;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Seeds
{
    public class DefaultAdministratorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de usuario Admin
            ApplicationUser defaultUserAdmin = new();
            defaultUserAdmin.UserName = "adminuser";
            defaultUserAdmin.Email = "adminuser@email.com";
            defaultUserAdmin.FirstName = "Juan";
            defaultUserAdmin.LastName = "Box";
            defaultUserAdmin.CardIdentification = "402191111";
            defaultUserAdmin.EmailConfirmed = true;
            defaultUserAdmin.PhoneNumberConfirmed = true;

            //Validar si el default user existe
            if (userManager.Users.All(u => u.Id != defaultUserAdmin.Id))
            {
                //Validar si hay algun user con el mismo correo
                var user = await userManager.FindByEmailAsync(defaultUserAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUserAdmin, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUserAdmin, Roles.Administrator.ToString());
                }
            }
        }
    }
}
