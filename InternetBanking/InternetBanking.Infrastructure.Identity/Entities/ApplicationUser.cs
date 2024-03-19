using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //Entidad de User con campos custom
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Campo custom para la Identificacion de card
        public string CardIdentification { get; set; }
    }
}
