
using EnergyOutageNotifier.Models.Context;

namespace EnergyOutageNotifier.API.Extensions
{
    public class NotificationCleanupService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public NotificationCleanupService(IServiceProvider services)
        {
            _services = services;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Her 12 saatte bir çalışacak
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EnerjisaDBContext>();

                var now = DateTime.UtcNow;
                var twelveHoursAgo = now.AddSeconds(-15);

                var outdatedNotifications = dbContext.Notification
                    .Where(n => n.NotificationType == 1 && n.NotificationTime <= twelveHoursAgo)
                    .ToList();

                foreach (var notification in outdatedNotifications)
                {
                    notification.NotificationType = 0; 
                }

                dbContext.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
