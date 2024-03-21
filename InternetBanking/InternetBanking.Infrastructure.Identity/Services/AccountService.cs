using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Dtos.Email;
using InternetBanking.Core.Application.ViewModels.BankAccount;


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
                if (vm.InitialAmmount < 0)
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
                userVM.HasError = false;
                if (vm.TypeUser == "Administrador")
                {
                    await _userManager.AddToRoleAsync(ApUser, Roles.Administrator.ToString());
                }
                if (vm.TypeUser == "Cliente")
                {
                    //le pone el roll
                    await _userManager.AddToRoleAsync(ApUser, Roles.Client.ToString());

                    //Aqui va logica para crear cliente con Cuenta
                    //crea el objeto con los datos
                    //retorna el objeto con la cuenta como main, los el initialAmmount y UserId
                    SaveBankAccountViewModel bank = _bankAccountService.CreateNewBank(ApUser.Id, vm.InitialAmmount);
                    //Guarda la cuenta
                    await _bankAccountService.SaveAsync(bank);
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
                userVM.HasError = false;
                userVM.Error = "A ocurrido un error, por favor registre el usuario nuevamente";
                return userVM;
            }
            return userVM;
        }


        //metodo para actualizar un user
        public async Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel vm)
        {
            ApplicationUser AppUser = await _userManager.FindByIdAsync(vm.Id);
            SaveUserViewModel userVM = new SaveUserViewModel();

            //valida si el user es cliente que el monto inicial no sea menor que 0
            if (vm.TypeUser == "Cliente")
            {
                if (vm.InitialAmmount != 0)
                {
                    //metodo para sumar el dinero al balance que tiene en la cuenta principal
                    await _bankAccountService.UserSumAmmount(vm.Id, vm.InitialAmmount);
                }
            }

            //valida el username sea cambiado
            if (vm.Username != AppUser.UserName)
            {
                var verifUsername = await _userManager.FindByNameAsync(vm.Username);
                if (verifUsername != null)
                {
                    userVM.HasError = true;
                    userVM.Error = "Este usuario ya esta en uso";
                    return userVM;
                }

            }
            //valida si son iguales las contraseñas
            if (vm.Password != vm.ConfirmPassword)
            {
                userVM.HasError = true;
                userVM.Error = "Las contraseñas nuevas no coinciden";
                return userVM;
            }
            //Si ingresa una contraseña tiene que poner la que tiene actualmente
            if (vm.Password != null)
            {
                if (vm.currentPassword == null)
                {
                    userVM.HasError = true;
                    userVM.Error = "Tiene que escribir la contraseña actual si quiere cambiar la contraseña";
                    return userVM;
                }
            }
            //valida la cedula se diferente que la que ya tiene
            if (vm.CardIdentificantion != AppUser.CardIdentification)
            {
                //valida que la cedula sea unica
                var user = await _userManager.Users.ToListAsync();
                var verifyCedula = user.FirstOrDefault(user => user.CardIdentification == vm.CardIdentificantion);
                if (verifyCedula != null)
                {
                    userVM.HasError = true;
                    userVM.Error = $"Esta cedula ya esta en uso";
                    return userVM;
                }
            }
            
            //valida que el email sea cambiado
            if(vm.Email != AppUser.Email)
            {
                //valida el email
                var verifyEmail = await _userManager.FindByEmailAsync(vm.Email);
                if (verifyEmail != null)
                {
                    userVM.HasError = true;
                    userVM.Error = $"Este email {userVM.Email} ya esta en uso";
                    return userVM;
                }
            }


            AppUser.FirstName = vm.FirstName;
            AppUser.LastName = vm.LastName;
            AppUser.Email = vm.Email;
            AppUser.CardIdentification = vm.CardIdentificantion;
            AppUser.EmailConfirmed = true;
            

            if(vm.Password != null)
            {
                var status = await _userManager.ChangePasswordAsync(AppUser, vm.currentPassword, vm.Password);
                if (status.Succeeded)
                {
                    userVM.HasError = false;
                }
                else
                {
                    //si ocurre un error
                    vm.HasError = false;
                    vm.Error = "A ocurrido un error, por favor edite el usuario nuevamente";
                    return vm;
                }
            }
            await _userManager.UpdateAsync(AppUser);


            return userVM;
        }
        //Metodo para obtener todos los administradores
        public async Task<List<UserViewModel>> GetAllUserAdminAsync()
        {
            var userList = await _userManager.GetUsersInRoleAsync("Administrator");
            List<UserViewModel> vm = new();
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    vm.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CardIdentificantion = user.CardIdentification,
                        Email = user.Email,
                        UserName = user.UserName,
                        IsVerified = user.EmailConfirmed
                    });
                }
            }
            return vm;
        }

        //Metodo para obtener todos los clientes
        public async Task<List<UserViewModel>> GetAllUserClientAsync()
        {
            var userList = await _userManager.GetUsersInRoleAsync("Client");
            List<UserViewModel> vm = new();
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    vm.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CardIdentificantion = user.CardIdentification,
                        Email = user.Email,
                        UserName = user.UserName,
                        IsVerified = user.EmailConfirmed
                    });
                }
            }
            return vm;
        }

        //metodo para obtener user por ID
        public async Task<SaveUserViewModel> GetByIdUser(string Id)
        {
            SaveUserViewModel vm = new();
            var User =  await _userManager.FindByIdAsync(Id);
            var rol = await _userManager.GetRolesAsync(User);
            if (User != null)
            {
                vm = new SaveUserViewModel
                {
                    Id = User.Id,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    CardIdentificantion = User.CardIdentification,
                    Email = User.Email,
                    Username = User.UserName,
                    IsConfirm = User.EmailConfirmed,
                    TypeUser = rol.FirstOrDefault()
                };
                return vm;
            }
            else
            {
                vm.Error = "No se encontro el user";
                vm.HasError = true;
                return vm;
            }
        }

        //metodo para activar usuario
        public async Task ActiveUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
        }

        //metodo para desactivar usuario
        public async Task DesactiveUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.EmailConfirmed = false;
                await _userManager.UpdateAsync(user);
            }
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
