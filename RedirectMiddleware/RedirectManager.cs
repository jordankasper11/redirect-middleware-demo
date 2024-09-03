using RedirectMiddleware.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("RedirectMiddleware.Tests")]

namespace RedirectMiddleware
{
    internal class RedirectManager
    {
        private readonly string _apiUrl;
        private readonly IHttpClientFactory _httpClientFactory;
        private ReadOnlyDictionary<string, RedirectModel> _redirects;

        public RedirectManager(string apiUrl, IHttpClientFactory httpClientFactory)
        {
            _apiUrl = apiUrl;
            _httpClientFactory = httpClientFactory;
            _redirects = new Dictionary<string, RedirectModel>().AsReadOnly();
        }

        public RedirectResponse? GetRedirect(string requestUrl)
        {
            requestUrl = SanitizeUrl(requestUrl);

            // If there is an exact match, us the specified redirect URL
            if (_redirects.TryGetValue(requestUrl, out var redirect))
            {
                return new RedirectResponse(redirect.TargetUrl, redirect.RedirectType);
            }

            // If there is no exact match, try to find the closest match with UseRelative set to true
            var segments = requestUrl.Split('/');

            for (var i = segments.Length; i > 0; i--)
            {
                var segmentedUrl = String.Join("/", segments.Take(i));

                if (_redirects.TryGetValue(segmentedUrl, out redirect) && redirect.UseRelative)
                {
                    var redirectUrl = $"{redirect.TargetUrl}/{String.Join("/", segments.Skip(i))}";

                    return new RedirectResponse(redirectUrl, redirect.RedirectType);
                }
            }

            return null;
        }

        public async Task Refresh()
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(_apiUrl);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var redirects = JsonSerializer.Deserialize<List<RedirectModel>>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await Load(redirects);
        }

        internal Task Load(IEnumerable<RedirectModel>? redirects)
        {
            if (redirects == null)
            {
                redirects = new List<RedirectModel>();
            }

            _redirects = redirects.ToDictionary(r => SanitizeUrl(r.RedirectUrl), r =>
            {
                r.RedirectUrl = SanitizeUrl(r.RedirectUrl);
                r.TargetUrl = SanitizeUrl(r.TargetUrl);

                return r;
            }).AsReadOnly();

            return Task.CompletedTask;
        }

        private string SanitizeUrl(string url)
        {
            return url.ToLower().TrimEnd('/');
        }
    }
}
