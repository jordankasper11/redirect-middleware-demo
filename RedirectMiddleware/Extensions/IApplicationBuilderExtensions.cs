using Microsoft.AspNetCore.Builder;

namespace RedirectMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
