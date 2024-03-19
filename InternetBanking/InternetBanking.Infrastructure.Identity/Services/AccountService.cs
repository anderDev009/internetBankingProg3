using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Core.Application.Interfaces.Service;


namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailServices emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
    }
}
