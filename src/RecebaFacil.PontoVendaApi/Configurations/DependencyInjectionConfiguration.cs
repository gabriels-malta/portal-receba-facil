﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecebaFacil.CrossCutting;

namespace RecebaFacil.WebApi.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjectionResolver.Configure(services, configuration);
        }
    }
}
