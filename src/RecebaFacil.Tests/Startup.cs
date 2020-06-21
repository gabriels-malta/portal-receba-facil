using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository;
using RecebaFacil.Repository.Interfaces;
using RecebaFacil.Service;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("RecebaFacil.Tests.Startup", "RecebaFacil.Tests")]
namespace RecebaFacil.Tests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink) { }

        protected void ConfiguraServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISecurityService, SecurityService>();

            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();

        }
    }
}
