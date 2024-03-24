using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Domain.Settings;
using InternetBanking.Infrastructure.Shared.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection service, IConfiguration configuracion)
        {
            service.Configure<MailSettings>(configuracion.GetSection("MailSettings"));
            service.AddTransient<IEmailServices, EmailServices>();

        }
    }
}
