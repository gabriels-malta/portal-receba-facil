using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.Repository;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.IoC.DependencyInjection
{
    public static class RepositoryInstallers
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IRepositoryContato, RepositoryContato>();
            services.AddScoped<IRepositoryEmpresa, RepositoryEmpresa>();
            services.AddScoped<IRepositoryEncomenda, RepositoryEncomenda>();
            services.AddScoped<IRepositoryEncomendaHistoria, RepositoryEncomendaHistoria>();
            services.AddScoped<IRepositoryEndereco, RepositoryEndereco>();
            services.AddScoped<IRepositoryExpediente, RepositoryExpediente>();
            services.AddScoped<IRepositoryGrupo, RepositoryGrupo>();
            services.AddScoped<IRepositoryPreRegistro, RepositoryPreRegistro>();
            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();
        }
    }
}
