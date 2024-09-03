using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectMiddleware
{
    public static class Extensions
    {
        public static IServiceCollection AddRedirectMiddleware(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder app, string apiUrl, TimeSpan interval)
        {
            return app;
        }
    }
}
