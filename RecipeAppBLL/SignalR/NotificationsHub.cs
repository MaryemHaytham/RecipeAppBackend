using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.SignalR
{
    public class NotificationsHub :Hub<INotificationsClient>
    {
        /*public override async Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).RecieveNotification($"Thank you for connecting {Context.User?.Identity?.Name}");

            await base.OnConnectedAsync();
        }*/
        public async Task SendNotificationToUser(string userId, string message)
        {
            await Clients.User(userId).SendNotificationToUser(message);
        }


    }

    public interface INotificationsClient
    {
        Task SendNotificationToUser(string message);
    }
}
