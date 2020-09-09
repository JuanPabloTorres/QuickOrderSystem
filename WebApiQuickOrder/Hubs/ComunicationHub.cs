using Library.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Hubs
{
    public class ComunicationHub : Hub
    {
       readonly QOContext _QOContext;
        public ComunicationHub(QOContext qOContext)
        {
            _QOContext = qOContext;
        }

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

        public async Task SendJobNotification(string notificationMessage, string userReciever)
        {

            var connectionId = _QOContext.usersConnecteds.Where(uc => uc.UserID.ToString() == userReciever && uc.IsDisable == false).FirstOrDefault();

            if (connectionId != null)
            {
                await Clients.Client(connectionId.HubConnectionID).SendAsync("SendJobNotification", notificationMessage);

            }
        }

        public async Task SendCompletedOrderNotification(string notificationMessage, string userReciever)
        {

            var connection = _QOContext.usersConnecteds.Where(uc => uc.UserID.ToString() == userReciever && uc.IsDisable == false).FirstOrDefault();

            if (connection != null)
            {
                await Clients.Client(connection.HubConnectionID).SendAsync("SendCompletedOrderNotification", notificationMessage);

            }
        }




        public async Task OrderPreparer(string message, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("OrderPrepareNotification", message);

        }

      
    }
}
