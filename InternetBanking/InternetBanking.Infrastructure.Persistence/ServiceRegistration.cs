
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayer(this IServiceCollection services,IConfiguration configuration)
        {

            #region context
            services.AddDbContext<InternetBankingContext>(options => options.UseSqlServer(configuration.GetConnectionString("default"),
                m => m.MigrationsAssembly(typeof(InternetBankingContext).Assembly.FullName)));
            #endregion
            #region Services
            #region Repositorio Cuenta de banco
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            #endregion
            #region Repositorio card
            services.AddTransient<ICardRepository, CardRepository>();
            #endregion
            #region Repositorio de prestamos
            services.AddTransient<IloanRepository, LoanRepository>();
            #endregion
            #endregion

        }
    }
}
