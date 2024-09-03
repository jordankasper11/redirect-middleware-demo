using Microsoft.AspNetCore.Mvc;
using RedirectMiddleware;

namespace SampleRedirectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedirectsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RedirectModel> Get()
        {
            return new List<RedirectModel>() {
               new() {
                   RedirectUrl = "/campaignA",
                   TargetUrl = "/campaigns/targetcampaign",
                   RedirectType = 302,
                   UseRelative = false
               },
               new() {
                   RedirectUrl = "/campaignB",
                   TargetUrl = "/campaigns/targetcampaign/channelB",
                   RedirectType = 302,
                   UseRelative = false
               },
               new() {
                   RedirectUrl = "/product-directory",
                   TargetUrl = "/products",
                   RedirectType = 301,
                   UseRelative = true
               }
            };
        }
    }
}
