using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Attributes;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Views.Store;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
      
        public ICommand GetOrdersCommand { get; set; }
      

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
            set 
            {
                orderstatus = value;

                OnPropertyChanged();

                  

                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if (tokenExpManger.IsExpired())
                {

                    tokenExpManger.CloseSession();
                    //DisplayNotification();

                }
                else
                {
                    Task.Run(async () =>
                    {

                        await LoadUserOrderWithStatus(OrderStatus);
                    });


                }
             

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

       public LoadingManager LoadingManager { get; set; }


        public OrderViewModel()
        {
            
            UserOrders = new ObservableCollection<OrderPresenter>();
            LoadingManager = new LoadingManager();

            MessagingCenter.Subscribe<Order>(this, "RemoveOrderSubtmitedMsg", (sender) =>
            {


                var order = UserOrders.Where(op => op.OrderId == sender.OrderId).FirstOrDefault();

                UserOrders.Remove(order);

            });

            MessagingCenter.Subscribe<OrderPresenter, OrderPresenter>(this, "Refresh", (sender, arg) =>
             {

                 UserOrders.Remove(arg);
               
            });

            GetOrdersCommand = new Command<string>(async (arg) =>
            {

                await Shell.Current.GoToAsync($"{UserOrdersWithStatus.Route}?status={arg}");
              

            });
        }

        public async Task SetOrders()
        {
            var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);

            UserOrders.Clear();

            foreach (var item in userOrderData)
            {
               
                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);
            }
           
        }

    

        //async void DisplayNotification()
        //{
           
        //}

       
        async Task LoadUserOrderWithStatus(string value)
        {
            LoadingManager.OnLoading();
            Status _statusvalue = (Status)Enum.Parse(typeof(Status), value);

            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.UserId, _statusvalue,App.TokenDto.Token);

            switch (_statusvalue)
            {

                case Status.Completed:
                    {


                        UserOrders.Clear();
                        foreach (var item in orderData)
                        {

                            item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                            var presenter = new OrderPresenter(item);

                            UserOrders.Add(presenter);
                        }
                       

                        break;
                    }

                case Status.NotSubmited:
                    {
                        UserOrders.Clear();

                        foreach (var item in orderData.Where(o => o.IsDisisble == false))
                        {

                            item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                            var presenter = new OrderPresenter(item);

                            UserOrders.Add(presenter);
                        }
                        
                        break;
                    }
                case Status.Submited:
                    {

                        UserOrders.Clear();

                        foreach (var item in orderData)
                        {

                            item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                            var presenter = new OrderPresenter(item);

                            UserOrders.Add(presenter);
                        }

                        break;
                    }
                default:
                    break;
            }

            LoadingManager.OffLoading();


        }


    }
}
