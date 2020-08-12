using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Utilities
{
    public class ComunicationService
    {
        public HubConnection hubConnection;

        public ComunicationService()
        {

            hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.56.1:5000" + "/comunicationhub").Build();

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

                await hubConnection.InvokeAsync("SenRequestToUser",connectionId, request);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
