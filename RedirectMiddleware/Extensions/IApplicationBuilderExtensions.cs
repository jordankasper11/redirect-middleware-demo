using Microsoft.AspNetCore.Builder;
using RedirectMiddleware.Middleware;

namespace RedirectMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectionMiddleware>();

            return app;
        }
    }
}
