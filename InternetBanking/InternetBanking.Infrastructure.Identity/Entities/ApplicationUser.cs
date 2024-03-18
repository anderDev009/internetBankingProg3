using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string cedula {  get; set; }
        public decimal? InitialAmount { get; set; }
    }
}
