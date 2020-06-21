using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.IoC.DependencyInjection;

namespace RecebaFacil.IoC
{
    public class DependencyInjectionResolver
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            ContextInstaller.Configure(services, configuration);
            RepositoryInstallers.Configure(services);
            ServiceInstallers.Configure(services);
        }
    }
}
