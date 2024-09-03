using Moq;
using RedirectMiddleware.Models;

namespace RedirectMiddleware.Tests
{
    [TestClass]
    public class RedirectManagerTests
    {
        private readonly IRedirectManager _redirectManager;

        public RedirectManagerTests()
        {
            var apiUrl = "http://localhost:5179/api/redirects";
            var httpClientFactory = new Mock<IHttpClientFactory>();

            _redirectManager = new RedirectManager(apiUrl, httpClientFactory.Object);

            var redirects = new List<RedirectModel>()
            {
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

            _redirectManager.Load(redirects);
        }

        [TestMethod]
        [DataRow("/campaignA", "/campaigns/targetcampaign", 302)]
        [DataRow("/campaignB", "/campaigns/targetcampaign/channelb", 302)]
        [DataRow("/campaignAlonger", null, null)]
        [DataRow("/campaignC", null, null)]
        [DataRow("/product-directory", "/products", 301)]
        [DataRow("/product-directory/", "/products", 301)]
        [DataRow("/product-directorylonger", null, null)]
        [DataRow("/product-directory/bits", "/products/bits", 301)]
        [DataRow("/product-directory/bits/masonary/diamond-tip", "/products/bits/masonary/diamond-tip", 301)]
        [DataRow("/product-directory/bits/masonary/diamond-tip?test=true", "/products/bits/masonary/diamond-tip?test=true", 301)]
        public void GetRedirect_ReturnsCorrectResult(string requestUrl, string? expectedRedirectUrl, int? expectedStatusCode)
        {
            var redirect = _redirectManager.GetRedirect(requestUrl);

            Assert.AreEqual(expectedRedirectUrl, redirect?.RedirectUrl, true);
            Assert.AreEqual(expectedStatusCode, redirect?.StatusCode);
        }
    }
}