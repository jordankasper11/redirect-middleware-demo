using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectMiddleware
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

        public RedirectModel(string redirectUrl, string targetUrl, int redirectType, bool useRelative)
        {
            this.RedirectUrl = redirectUrl;
            this.TargetUrl = targetUrl;
            this.RedirectType = redirectType;
            this.UseRelative = useRelative;
        }
    }
}
