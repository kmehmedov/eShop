using Microsoft.AspNetCore.SignalR;

namespace Notification.SignalR
{   
    // TODO: Implement authorization
    //[Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context?.User?.Identity?.Name is not null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            if (Context?.User?.Identity?.Name is not null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            }

            await base.OnDisconnectedAsync(ex);
        }
    }
}
