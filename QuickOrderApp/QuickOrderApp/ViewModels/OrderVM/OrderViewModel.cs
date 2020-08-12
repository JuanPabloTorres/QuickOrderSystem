using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Views.Store;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.OrderVM
{
    [QueryProperty("OrderStatus", "status")]
    public class OrderViewModel : BaseViewModel
    {
        public ObservableCollection<OrderPresenter> UserOrders { get; set; }

        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }

        //public Command LoadItemsCommand { get; set; }

        //public ICommand CheckoutCommand { get; set; }

        public ICommand GetOrdersCommand { get; set; }
        //public ICommand GetOrdersCommand { get; set; }

        private string orderId;

        public string OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                OnPropertyChanged();

            }
        }

        private string orderstatus;

        public string OrderStatus
        {
            get { return orderstatus; }
            set { orderstatus = value;
                OnPropertyChanged();

                SetUserOrderStatus(OrderStatus);
               
            }
        }

        private bool checkoutEnable;

        public bool CheckOutEnable
        {
            get { return checkoutEnable; }
            set
            {
                checkoutEnable = value;
                OnPropertyChanged();
            }
        }

        public OrderViewModel()
        {

            UserOrders = new ObservableCollection<OrderPresenter>();

            MessagingCenter.Subscribe<OrderPresenter, OrderPresenter>(this, "Refresh", (sender, arg) =>
             {

                 UserOrders.Remove(arg);
                //LoadItemsCommand.Execute(null);
            });

            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            GetOrdersCommand = new Command<string>(async (arg) =>
            {

                await Shell.Current.GoToAsync($"{UserOrdersWithStatus.Route}?status={arg}");
              

            });
        }

        async Task SetOrders()
        {
            var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);

            foreach (var item in userOrderData)
            {
                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);
            }
            //UserOrders = new ObservableCollection<Order>(userOrderData);

        }

        async Task SetUserOrderStatus(string value)
        {
            Status _statusvalue = (Status)Enum.Parse(typeof(Status), value);
            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.UserId, _statusvalue);

            UserOrders.Clear();

            foreach (var item in orderData)
            {
                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);
            }
        }

        //public async Task ExecuteLoadItemsCommand()
        //{
        //    IsBusy = true;

              
        //    try
        //    {
        //        UserOrders.Clear();
        //        //var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);
        //        var userorderwithToken = orderDataStore.GetUserOrdersWithToken(App.LogUser.UserId, App.TokenDto.Token);

        //        foreach (var item in userorderwithToken)
        //        {
        //            item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());

        //            var presenter = new OrderPresenter(item);

        //            UserOrders.Add(presenter);
        //        }
        //        //UserOrders = new ObservableCollection<Order>(userOrderData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}


    }
}
