using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Utilities
{
    public class ComunicationService
    {
        public HubConnection hubConnection;

        public ComunicationService()
        {

            //hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.56.1:5000" + "/comunicationhub").Build();
            hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.1.133:5000" + "/comunicationhub").Build();

            Connect();
        }

        public async Task Connect()
        {
           
            await hubConnection.StartAsync();
               
          

            //task.Wait();
        }
        public async Task Disconnect()
        {
            await hubConnection.StopAsync();
        }

        public async Task SendRequestToUser(string connectionId,UserRequest request)
        {
            try
            {

               var jsonString = JsonSerializer.Serialize(request);

                await hubConnection.InvokeAsync("SenRequestToUser",connectionId, jsonString);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendJobNotification(Store jobRequest,string userdId)
        {
            try
            {
                string message = $"{ jobRequest.StoreName.ToString()} sent a job request.";

                await hubConnection.InvokeAsync("SendJobNotification", message, userdId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendCompletedOrderNotification(Guid OrderId, string userdId)
        {
            try
            {
                string message = $"Order: { OrderId.ToString()}";

                await hubConnection.InvokeAsync("SendCompletedOrderNotification", message, userdId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
