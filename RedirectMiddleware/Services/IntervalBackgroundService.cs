using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectMiddleware.Services
{
    internal abstract class IntervalBackgroundService : BackgroundService
    {
        private TimeSpan _interval;

        protected IntervalBackgroundService(TimeSpan interval)
        {
            _interval = interval;
        }

        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(_interval);

            do
            {
                await ExecuteIntervalAsync(stoppingToken);
            } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
        }

        protected abstract Task ExecuteIntervalAsync(CancellationToken stoppingToken);
    }
}
