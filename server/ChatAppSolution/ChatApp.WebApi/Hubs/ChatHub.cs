using ChatApp.WebApi.DataService;
using ChatApp.WebApi.Models;
using Microsoft.AspNetCore.SignalR;
namespace ChatApp.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SharedDB _sharedDB;
        public ChatHub(SharedDB sharedDB)
        {
            
            _sharedDB = sharedDB;
        }
        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{connection.UserName} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
            _sharedDB.Connections[Context.ConnectionId] = connection;
            await Clients.Group(connection.ChatRoom)
                .SendAsync("JoinSpecificChatRoom", "admin", $"{connection.UserName} has joined {connection.ChatRoom}");
        }

        public async Task SendMessage(string message)
        {
            if (_sharedDB.Connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Groups(connection.ChatRoom) //ClinetProxy
                    .SendAsync("ReceiveSpecificMessage", connection.UserName, message);
            }
        }

    }
}
