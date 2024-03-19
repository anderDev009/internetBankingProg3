using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Dtos.Email;


namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices _emailService;

        //inyeccion del servicio de bank account 
        private readonly IBankAccountService _bankAccountService;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailServices emailService, IBankAccountService bankAccountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            //inyeccion del servicio de bank account 
            _bankAccountService = bankAccountService;
        }

        //metodo de login
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe una cuenta registrada con el email {request.Email}";
                return response;
            }
            //inicia seccion
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Credenciales incorrectas";
            }
            //valida si el email es confirm
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Su cuenta no está activa, comuniquese con el Admin";
                return response;
            }

            //mapeo de user a response
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            return response;
        }
        //Metodo de LogOut
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm)
        {
            SaveUserViewModel userVM = new SaveUserViewModel();
            //valida si el user es cliente que el monto inicial no sea menor que 0
            if (vm.TypeUser == "cliente")
            {
                if (vm.BankAccount.InitialAmmount < 0)
                {
                    userVM.HasError = true;
                    userVM.Error = "El monto no puede ser negativo";
                    return userVM;
                }
            }
            //valida el user name
            var verifUsername = await _userManager.FindByNameAsync(vm.Username);
            if (verifUsername != null)
            {
                userVM.HasError = true;
                userVM.Error = "Este usuario ya esta en uso";
                return userVM;
            }
            //valida que la cedula sea unica
            var user = await _userManager.Users.ToListAsync();
            var verifyCedula = user.FirstOrDefault(user => user.CardIdentification == vm.CardIdentificantion);
            if (verifyCedula != null)
            {
                userVM.HasError = true;
                userVM.Error = $"Esta cedula ya esta en uso";
                return userVM;
            }
            //valida el email
            var verifyEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (verifyEmail != null)
            {
                userVM.HasError = true;
                userVM.Error = $"Este email {userVM.Email} ya esta en uso";
                return userVM;
            }

            ApplicationUser ApUser = new()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                CardIdentification = vm.CardIdentificantion,
                UserName = vm.Username,
                EmailConfirmed = true
            };

            var status = await _userManager.CreateAsync(ApUser, vm.Password);

            if (status.Succeeded)
            {
                vm.HasError = false;
                if (vm.TypeUser == "Administrador")
                {
                    await _userManager.AddToRoleAsync(ApUser, Roles.Administrator.ToString());
                }
                if (vm.TypeUser == "Cliente")
                {
                    //Aqui va logica para crear cliente con Cuenta
                    await _userManager.AddToRoleAsync(ApUser, Roles.Client.ToString());
                    //
                    vm.BankAccount.IdUser = ApUser.Id;
                    await _bankAccountService.SaveAsync(vm.BankAccount);
                }
                await _emailService.sendAsync(new EmailRequest()
                {
                    To = ApUser.Email,
                    Body = $"<h4> Saludos Usted ha sido registrado en el sistema bancario",
                    Subject = $"<h4>Welcome to system Bank App </h4>"
                });
            }
            else
            {
                //si ocurre un error
                vm.HasError = false;
                vm.Error = "A ocurrido un error, por favor registre el usuario nuevamente";
                return vm;
            }
            return userVM;
        }

        //metodo para buscar un usuario y que te devuelva informacion basica
        public async Task<UserSearchResponse> SearchUser(UserSearchRequest request)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(request.CodeUser);
            if(user == null)
            {
                return new UserSearchResponse { HasError = true };
            }
            //configurar mapping 
            return new UserSearchResponse
            {
                IdUser = user.Id,
                LastName = user.LastName,
                Name = user.FirstName,
                HasError = false
            };
        }
    }
}
