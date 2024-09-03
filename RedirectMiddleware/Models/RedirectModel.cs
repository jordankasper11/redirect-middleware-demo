namespace RedirectMiddleware.Models
{
    public class RedirectModel
    {
        public required string RedirectUrl { get; set; }

        public required string TargetUrl { get; set; }

        public int RedirectType { get; set; }

        public bool UseRelative { get; set; }

        public RedirectModel()
        {
        }
    }
}
