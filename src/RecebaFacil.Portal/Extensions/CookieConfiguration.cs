using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecebaFacil.Portal.Extensions
{
    public static class CookieConfiguration
    {
        public static void AddCookies(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name = "_uc";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(16);
                options.LoginPath = new PathString("/");
                options.LogoutPath = new PathString("/");
                options.AccessDeniedPath = new PathString("/");
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "_afk";
                options.FormFieldName = "_rf_afk";
            });
        }
    }
}
