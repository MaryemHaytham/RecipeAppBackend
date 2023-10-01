using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.SignalR
{

    
    public class ServerNotifier : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerNotifier> _logger;
        private readonly IHubContext<NotificationsHub,INotificationsClient> _context;

        public ServerNotifier(ILogger<ServerNotifier> logger, IHubContext<NotificationsHub,INotificationsClient> context)
        {
            _logger = logger;
            _context = context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);

            while(!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                var dateTime = DateTime.Now;
                _logger.LogInformation("Executing {Service} {Time} ", nameof(ServerNotifier), dateTime);

                await _context.Clients.All.RecieveNotification($"Server time = {dateTime}");


            }
        }
    }
}
