using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.Repository.ContextConfig;

namespace RecebaFacil.CrossCutting.Installers
{
    public static class ContextInstaller
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RFContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("EfDefault"), b => b.EnableRetryOnFailure());
            });
        }
    }
}
