using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.IoC;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("RecebaFacil.Tests.Startup", "RecebaFacil.Tests")]
namespace RecebaFacil.Tests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink) { }

        protected void ConfiguraServices(IServiceCollection services)
        {
            DependencyInjectionResolver.Configure(services);
        }
    }
}
