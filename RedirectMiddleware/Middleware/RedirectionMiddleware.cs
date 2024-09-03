using Microsoft.AspNetCore.Http;

namespace RedirectMiddleware.Middleware
{
    internal class RedirectionMiddleware : IMiddleware
    {
        private readonly RedirectManager _redirectManager;

        public RedirectionMiddleware(RedirectManager redirectManager)
        {
            _redirectManager = redirectManager;    
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestUrl = context.Request.Path;
            var redirectResponse = _redirectManager.GetRedirect(requestUrl);

            if (redirectResponse == null)
            {
                await next(context);

                return;
            }

            var permanent = redirectResponse.StatusCode == 301;

            context.Response.Redirect(redirectResponse.RedirectUrl, permanent);
        }
    }
}
