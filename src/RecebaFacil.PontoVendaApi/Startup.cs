using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RecebaFacil.WebApi.Configurations;
using System;
using System.Globalization;

namespace RecebaFacil.WebApi
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
            services.AddDependencies(Configuration);

            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.UseMemberCasing();
                setup.SerializerSettings.Culture = new CultureInfo("pt-BR");
                setup.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                setup.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddRouting(o => o.LowercaseUrls = true);

            var swaggerConfig = new SwaggerSettings();
            Configuration.GetSection(SwaggerSettings.Key).Bind(swaggerConfig);
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
                {
                    Version = swaggerConfig.Version,
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    Contact = new OpenApiContact
                    {
                        Email = "",
                        Name = swaggerConfig.ContactName,
                        Url = new Uri(swaggerConfig.ContactUrl)
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Receba Fácil API");
                    setup.RoutePrefix = string.Empty;
                });

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class SwaggerSettings
    {
        public const string Key = "SwaggerSettings";

        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }
    }
}
