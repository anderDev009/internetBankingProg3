
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<InternetBankingContext>(options => options.UseSqlServer(configuration.GetConnectionString("default"),
                m => m.MigrationsAssembly(typeof(InternetBankingContext).Assembly.FullName)));
        }
    }
}
