using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Hubs
{

    public class ChatHub : Hub
    {
        public async Task SendMessage(int id, string user, string message)
        {
            await Clients.Group(id.ToString()).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroup(int id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
        }
        public async Task Bienvenida(int id, string user, string message)
        {
            await Clients.Group(id.ToString()).SendAsync("ReceiveMessageBienvenida", user, message);

        }

    }
}
