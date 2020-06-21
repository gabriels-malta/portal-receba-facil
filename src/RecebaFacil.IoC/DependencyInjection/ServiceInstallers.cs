using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.Domain.Services;
using RecebaFacil.Service;

namespace RecebaFacil.IoC.DependencyInjection
{
    public static class ServiceInstallers
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IEnumService, EnumService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IPreRegistroService, PreRegistroService>();
            services.AddScoped<IEnderedecoService, EnderecoService>();
            services.AddScoped<IExpedienteService, ExpedienteService>();
            services.AddScoped<IEncomendaService, EncomendaService>();
        }
    }
}
