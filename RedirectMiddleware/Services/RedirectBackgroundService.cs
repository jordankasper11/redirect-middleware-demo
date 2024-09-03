using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectMiddleware.Services
{
    internal class RedirectBackgroundService : IntervalBackgroundService
    {
        private string _apiUrl;
        private ILogger _logger;

        public RedirectBackgroundService(string apiUrl, TimeSpan interval, ILogger<RedirectBackgroundService> logger) : base(interval)
        {
            _apiUrl = apiUrl;
            _logger = logger;
        }

        protected override Task ExecuteIntervalAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Executing");

            return Task.CompletedTask;
        }
    }
}
