using Microsoft.Extensions.Logging;

namespace RedirectMiddleware.Services
{
    internal class RedirectBackgroundService : IntervalBackgroundService
    {
        private readonly RedirectManager _redirectManager;
        private readonly ILogger _logger;

        public RedirectBackgroundService(RedirectManager redirectManager, TimeSpan interval, ILogger<RedirectBackgroundService> logger) : base(interval)
        {
            _redirectManager = redirectManager;
            _logger = logger;
        }

        protected override async Task ExecuteIntervalAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Refreshing redirect mappings");

                await _redirectManager.Refresh();

                _logger.LogInformation("Completed refreshing redirect mappings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing redirect mappings");
            }
        }
    }
}
