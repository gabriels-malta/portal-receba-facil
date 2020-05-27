using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.IoC;

namespace RecebaFacil.Portal.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            DependencyInjectionResolver.Configure(services);
        }
    }
}
