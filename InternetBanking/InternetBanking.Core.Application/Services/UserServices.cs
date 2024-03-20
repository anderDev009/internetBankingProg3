using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Application.ViewModels.Users;
using InternetBanking.Infrastructure.Identity.Services;

namespace InternetBanking.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserServices(IAccountService accountService, IMapper mapper)
        {

            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm)
        {
            return await _accountService.RegisterAsync(vm);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel svm)
        {
            return await _accountService.UpdateUserAsync(svm);
        }

        public async Task DesactiveUser(string id)
        {
            await _accountService.DesactiveUser(id);
        }
        public async Task ActiveUser(string id)
        {
            await _accountService.ActiveUser(id);
        }

        public async Task<List<UserViewModel>> GetAllUserClientAsync()
        {
            return await _accountService.GetAllUserClientAsync();
        }

        public async Task<List<UserViewModel>> GetAllUserAdminAsync()
        {
            return await _accountService.GetAllUserAdminAsync();
        }
    }
}
