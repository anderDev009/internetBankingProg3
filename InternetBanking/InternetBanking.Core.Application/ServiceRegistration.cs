

using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.Services;
using InternetBanking.Infrastructure.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service) 
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region services
            service.AddTransient<IBankAccountService, BankAccountService>();
            service.AddTransient<ILoanService, LoanService>();
            service.AddTransient<IBeneficiaryService, BeneficiaryService>();
            service.AddTransient<ICardService, CardService>();
            service.AddTransient<IUserServices, UserServices>();
            service.AddTransient<IUserServices, UserServices>();
            service.AddTransient<IPayLoanService, PayLoanService>();
            service.AddTransient<IPayExpressService, PayExpressService>();
            service.AddTransient<IPayCardService, PayCardService>();
            service.AddTransient<IUserServices, UserServices>();
            #endregion
        }
    }
}
