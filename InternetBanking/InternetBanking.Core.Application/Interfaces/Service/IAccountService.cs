using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Users;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task SignOutAsync();
        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm);
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel vm);
        Task ActiveUser(string id);
        Task DesactiveUser(string id);
        Task<UserSearchResponse> SearchUser(UserSearchRequest request);
        Task<List<UserViewModel>> GetAllUserClientAsync();
        Task<List<UserViewModel>> GetAllUserAdminAsync();
        Task<SaveUserViewModel> GetByIdUser(string Id);
    }
}