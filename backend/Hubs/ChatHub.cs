using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace backend.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // This method will be called by clients to send messages.
            // You can implement logic here to store the message in the database and broadcast it to other clients.
            // For simplicity, we'll just broadcast the message to all connected clients for demonstration purposes.

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            // This method is called when a client connects to the hub.
            // You can implement logic here to manage connected clients, join chat rooms, etc.

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // This method is called when a client disconnects from the hub.
            // You can implement logic here to manage disconnected clients, leave chat rooms, etc.

            await base.OnDisconnectedAsync(exception);
        }
    }
}
