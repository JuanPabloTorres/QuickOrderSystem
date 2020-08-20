using Library.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiQuickOrder.Hubs
{
    public class ComunicationHub : Hub
    {

        public async Task SenRequestToUser(string connectionId,string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public async Task SendOrderToStore(string connectionId, Order message)
        {
            await Clients.Client(connectionId).SendAsync("ReceivedOrder", message);
        }

        public async Task UpdateStoreInventory(IList<Product> message)
        {
            await Clients.All.SendAsync("UpdateStoreInventory", message);
        }

    }
}
