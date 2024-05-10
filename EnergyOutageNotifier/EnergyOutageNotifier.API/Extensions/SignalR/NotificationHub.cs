using Microsoft.AspNetCore.SignalR;

namespace EnergyOutageNotifier.API.Extensions.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.Client(this.Context.ConnectionId).SendAsync("ReceiveMessage", message);
        }
    }
}
