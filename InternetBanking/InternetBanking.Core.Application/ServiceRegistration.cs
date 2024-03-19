

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service) 
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
