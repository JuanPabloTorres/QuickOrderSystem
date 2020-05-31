using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels
{
    [QueryProperty("OrderId","Id")]
    public class OrderViewModel:BaseViewModel
    {
        public ObservableCollection<OrderPresenter> UserOrders { get; set; }
             

        public Command LoadItemsCommand { get; set; }

        public ICommand CheckoutCommand { get; set; }

        private string orderId;

        public string OrderId
        {
            get { return orderId; }
            set { orderId = value;
                OnPropertyChanged();

            }
        }

        private Order orderdetail;

        public Order OrderDetail
        {
            get { return orderdetail; }
            set { orderdetail = value;
                OnPropertyChanged();
            }
        }

        private double total;

        public double Total
        {
            get { return total; }
            set { total = value;
                OnPropertyChanged();
            }
        }

        private bool ispickup;

        public bool IspickUp
        {
            get { return ispickup; }
            set { ispickup = value;
                OnPropertyChanged();

                if (IspickUp)
                {
                    IsDelivery = false;
                    OrderTypeFee = 0;
                    Total -= 5;
                }
            }
        }

        private bool isdelivery;

        public bool IsDelivery
        {
            get { return isdelivery; }
            set { isdelivery = value;
                OnPropertyChanged();

                if (IsDelivery)
                {
                    IspickUp = false;

                    OrderTypeFee = 5;
                    Total = Total + 5;

                }
            }
        }


        private double ordertypefee;

        public double OrderTypeFee
        {
            get { return ordertypefee; }
            set { ordertypefee = value;
                OnPropertyChanged();
            }
        }

        private bool checkoutEnable;

        public bool CheckOutEnable
        {
            get { return checkoutEnable; }
            set { checkoutEnable = value;
                OnPropertyChanged();
            }
        }




        public OrderViewModel()
        {
            UserOrders = new ObservableCollection<OrderPresenter>();
                       

            if (SelectedOrder.CurrentOrder != null)
            {
                OrderDetail = SelectedOrder.CurrentOrder;

                Total = OrderDetail.OrderProducts.Sum(op => op.Quantity * op.Price);
            }
            //if (OrderDetail != null)
            //{

            //    if (OrderDetail.OrderStatus == Status.Submited)
            //    {
            //        CheckOutEnable = false;
            //    }
            //}

            //SetOrders();

            MessagingCenter.Subscribe<OrderPresenter,OrderPresenter>(this, "Refresh", (sender,arg) =>
            {

                UserOrders.Remove(arg);
                //LoadItemsCommand.Execute(null);
            });
            CheckoutCommand = new Command(async () => 
            {

                if (OrderDetail.OrderStatus == Status.NotSubmited)
                {
                if (IsDelivery)
                {

                    OrderDetail.OrderType = Library.Models.Type.Delivery;
                }
                if (IspickUp)
                {
                    OrderDetail.OrderType = Library.Models.Type.PickUp;
                }

                OrderDetail.OrderStatus = Status.Submited;

                var orderUpdate = await orderDataStore.UpdateItemAsync(OrderDetail);

                if (orderUpdate)
                {
                    await Shell.Current.Navigation.PopAsync();
                }
                   
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Order was submited.", "OK");
                }

            });
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                UserOrders.Clear();
                var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);
                foreach (var item in userOrderData)
                {
                    item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());

                    var presenter = new OrderPresenter(item);

                    UserOrders.Add(presenter);
                }
                //UserOrders = new ObservableCollection<Order>(userOrderData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
