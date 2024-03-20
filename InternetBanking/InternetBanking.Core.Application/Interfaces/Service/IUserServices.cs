using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Users;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IUserServices
    {
        Task ActiveUser(string id);
        Task DesactiveUser(string id);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel svm);
        Task<List<UserViewModel>> GetAllUserClientAsync();
        Task<List<UserViewModel>> GetAllUserAdminAsync();
    }
}