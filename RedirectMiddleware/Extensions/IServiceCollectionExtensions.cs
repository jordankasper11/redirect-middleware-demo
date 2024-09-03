using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedirectMiddleware.Middleware;
using RedirectMiddleware.Services;

namespace RedirectMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRedirectMiddleware(this IServiceCollection services, string apiUrl, TimeSpan interval)
        {
            services.AddHttpClient();

            services.AddSingleton<IRedirectManager>(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return new RedirectManager(apiUrl, httpClientFactory);
            });

            services.AddTransient<RedirectionMiddleware>();

            services.AddHostedService<RedirectBackgroundService>(serviceProvider =>
            {
                var redirectManager = serviceProvider.GetRequiredService<IRedirectManager>();
                var logger = serviceProvider.GetRequiredService<ILogger<RedirectBackgroundService>>();

                return new RedirectBackgroundService(redirectManager, interval, logger);
            });

            return services;
        }
    }
}
