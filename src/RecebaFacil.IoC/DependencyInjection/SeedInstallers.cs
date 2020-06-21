using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.IoC.ContextSeed;

namespace RecebaFacil.IoC.DependencyInjection
{
    public static class SeedInstallers
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<GrupoSeed>();
            services.AddScoped<EmpresaSeed>();
            services.AddScoped<ExpedienteSeed>();
            services.AddHostedService<SeedHostedService>();
        }
    }
}
