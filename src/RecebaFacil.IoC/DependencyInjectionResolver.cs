using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Services;
using RecebaFacil.Infrastructure.DataAccess;
using RecebaFacil.Infrastructure.DataAccess.Core;
using RecebaFacil.Infrastructure.Mapper;
using RecebaFacil.Service;

namespace RecebaFacil.IoC
{
    public class DependencyInjectionResolver
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ISecurityService, SecurityService>();

            // Data access
            services.AddScoped<ISqlAccess, SqlAcess>();
            
            // DataServices
            services.AddScoped<IDataServiceUsuario, DataServiceUsuario>();
            services.AddScoped<IDataServiceGrupo, DataServiceGrupo>();
            services.AddScoped<IDataServiceContato, DataServiceContato>();
            services.AddScoped<IDataServiceEmpresa, DataServiceEmpresa>();
            services.AddScoped<IDataServicePreRegistro, DataServicePreRegistro>();
            services.AddScoped<IDataServiceEndereco, DataServiceEndereco>();
            services.AddScoped<IDataServiceExpediente, DataServiceExpediente>();
            
            // Mapper
            services.AddSingleton<FluentMapperCofiguration>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IPreRegistroService, PreRegistroService>();
            services.AddScoped<IEnderedecoService, EnderecoService>();
        }
    }
}
