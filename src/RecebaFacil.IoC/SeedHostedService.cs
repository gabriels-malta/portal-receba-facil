using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecebaFacil.IoC.ContextSeed;
using RecebaFacil.Repository.ContextConfig;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecebaFacil.IoC
{
    public class SeedHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RFContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var grupoSeed = scope.ServiceProvider.GetRequiredService<GrupoSeed>();
                await grupoSeed.Feed();

                var empresaSeed = scope.ServiceProvider.GetRequiredService<EmpresaSeed>();
                await empresaSeed.Feed();

                var expedienteSeed = scope.ServiceProvider.GetRequiredService<ExpedienteSeed>();
                await expedienteSeed.Feed();
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
