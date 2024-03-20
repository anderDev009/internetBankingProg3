using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
namespace WebApp.MiddledWares
{
    public class ValidateUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationResponse HasUser()
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("User");

            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }
    }
}
