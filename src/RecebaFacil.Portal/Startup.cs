using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecebaFacil.Portal.Extensions;
using RecebaFacil.Portal.Services;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Globalization;

namespace RecebaFacil.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache(setup => setup.ExpirationScanFrequency = TimeSpan.FromDays(364));

            services.AddSingleton(new Helpers.SelectListItemHelper());

            services.AddScoped<IHttpContextService, HttpContextService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IIBGEMicrorregioesService, IBGEMicrorregioesService>();

            services.AddDependencies(Configuration);
            services.AddCookies();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews(o => o.EnableEndpointRouting = false);

            services.AddRouting(options => { options.LowercaseUrls = true; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
