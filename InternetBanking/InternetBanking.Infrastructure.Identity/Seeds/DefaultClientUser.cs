using InternetBanking.Core.Application.Enums;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;


namespace InternetBanking.Infrastructure.Identity.Seeds
{
    public class DefaultClientUser
    {

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de usuario Cliente
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "defaultUser";
            defaultUser.Email = "default_user@email.com";
            defaultUser.FirstName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.CardIdentification = "40211711";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            //Validar si el default user existe
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                //Validar si hay algun user con el mismo correo
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Password$");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }
            }
        }

    }
}
