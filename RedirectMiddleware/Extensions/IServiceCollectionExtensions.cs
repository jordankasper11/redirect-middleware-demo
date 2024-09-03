using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedirectMiddleware.Services;

namespace RedirectMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRedirectMiddleware(this IServiceCollection services, string apiUrl, TimeSpan interval)
        {
            services.AddHostedService<RedirectBackgroundService>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<RedirectBackgroundService>>();

                return new RedirectBackgroundService(apiUrl, interval, logger);
            });

            return services;
        }
    }
}
