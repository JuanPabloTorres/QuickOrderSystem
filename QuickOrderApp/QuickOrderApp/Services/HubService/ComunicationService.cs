using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using QuickOrderApp.Utilities.Dependency;
using QuickOrderApp.Utilities.Dependency.Interface;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.Services.HubService
{
    public class ComunicationService
    {
        public readonly  HubConnection hubConnection;
        INotificationManager notificationManager;


        public ComunicationService()
        {
          
            //hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.1.144:5000" + "/comunicationhub").Build();

            hubConnection = new HubConnectionBuilder().WithUrl("http://juantorres94-001-site1.htempurl.com" + "/comunicationhub").Build();

            notificationManager = DependencyService.Get<INotificationManager>();

            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };

            CompletedOrderNotificationReciever();
            JobNotificationReciever();

            OrderToPreparerEmployee();
          

        }

        public void CompletedOrderNotificationReciever()
        {
            hubConnection.On<string>("SendCompletedOrderNotification", (message) =>
            {

                notificationManager.ScheduleNotification("Order Completed", message);
            });

        }

        public void JobNotificationReciever()
        {
            hubConnection.On<string>("SendJobNotification", (message) =>
            {

                notificationManager.ScheduleNotification("Job Application", message);
            });
        }

        public void OrderToPreparerEmployee()
        {
            hubConnection.On<string>("OrderPrepareNotification", (message) =>
            {

                notificationManager.ScheduleNotification("Order To Preparer", message);
            });
        }

        public async Task Connect()
        {
                  await hubConnection.StartAsync();

        }
        public async Task Disconnect()
        {
           

            if (!String.IsNullOrEmpty(hubConnection.ConnectionId))
            {
                await hubConnection.StopAsync();
            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {

            await hubConnection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public async Task SendOrder(string user, Order message)
        {
            try
            {

                await hubConnection.InvokeAsync("SendOrderToStore", user, message);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public async Task SendRequestToUser(string connectionId, UserRequest request)
        {
            try
            {

                await hubConnection.InvokeAsync("SenRequestToUser", connectionId, request);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateStoreInventory( Guid storeToUpdate)
        {
            try
            {
                await hubConnection.InvokeAsync("UpdateStoreInventory", storeToUpdate);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public async Task SendCompletedOrderNotification(Guid OrderId,string userdId)
        {
            try
            {
                string message= $"Order: { OrderId.ToString()}";

                await hubConnection.InvokeAsync("SendCompletedOrderNotification", message, userdId);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public async Task OrderToPrepare(Order order)
        {
            try
            {
               
                string Preparemessage = $"Order: { order.OrderId.ToString()}";

              

                await hubConnection.InvokeAsync("OrderToPrepare", Preparemessage, order.StoreId.ToString());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
