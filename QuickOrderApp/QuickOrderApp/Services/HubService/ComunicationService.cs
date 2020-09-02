using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using QuickOrderApp.Utilities.Dependency;
using QuickOrderApp.Utilities.Dependency.Interface;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.Services.HubService
{
    public class ComunicationService
    {
        public  HubConnection hubConnection;
        INotificationManager notificationManager;


        public ComunicationService()
        {
          
            hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.56.1:5000" + "/comunicationhub").Build();

            Task task = Task.Factory.StartNew(() =>
            {
                 Connect();

            });
            //Connect();
             task.Wait();

            notificationManager = DependencyService.Get<INotificationManager>();

            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };

            hubConnection.On<string>("SendCompletedOrderNotification", (message) =>
            {

                notificationManager.ScheduleNotification("Order Completed", message);
            });

        }

       public async Task Connect()
        {
           
                await hubConnection.StartAsync();
            
        }
       public async Task Disconnect()
        {
            await hubConnection.StopAsync();

            if (String.IsNullOrEmpty(hubConnection.ConnectionId))
            {

            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {

            await hubConnection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendOrder(string user, Order message)
        {
            try
            {

                await hubConnection.InvokeAsync("SendOrderToStore", user, message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendRequestToUser(string connectionId, UserRequest request)
        {
            try
            {

                await hubConnection.InvokeAsync("SenRequestToUser", connectionId, request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStoreInventory( Guid storeToUpdate)
        {
            try
            {
                await hubConnection.InvokeAsync("UpdateStoreInventory", storeToUpdate);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendCompletedOrderNotification(Guid OrderId,string userdId)
        {
            try
            {
                string message= $"Order: { OrderId.ToString()}";

                await hubConnection.InvokeAsync("SendCompletedOrderNotification", message, userdId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
