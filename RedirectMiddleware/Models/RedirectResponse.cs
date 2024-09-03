namespace RedirectMiddleware.Models
{
    public class RedirectResponse
    {
        public string RedirectUrl { get; set; }

        public int StatusCode { get; set; }

        public RedirectResponse(string redirectUrl, int statusCode)
        {
            this.RedirectUrl = redirectUrl;
            this.StatusCode = statusCode;
        }
    }
}
