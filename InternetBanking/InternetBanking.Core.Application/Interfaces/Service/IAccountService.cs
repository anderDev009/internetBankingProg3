using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Users;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm);
        Task<UserSearchResponse> SearchUser(UserSearchRequest request);
    }
}