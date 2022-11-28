using Library.Helpers;
using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Views.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.OrderVM
{
    [QueryProperty("OrderStatus", "status")]
    public class OrderViewModel : BaseViewModel
    {
        private bool checkoutEnable;

        private string orderId;

        private string orderstatus;

        public OrderViewModel ()
        {
            UserOrders = new ObservableCollection<OrderPresenter>();

            LoadingManager = new LoadingManager();

            KeyValues = new Dictionary<string, IEnumerable<Order>>();

            MessagingCenter.Subscribe<Order>(this, "RemoveOrderSubtmitedMsg", (sender) =>
            {
                var order = UserOrders.Where(op => op.OrderId == sender.ID).FirstOrDefault();

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

        public bool CheckOutEnable
        {
            get { return checkoutEnable; }
            set
            {
                checkoutEnable = value;

                OnPropertyChanged();
            }
        }

        public ICommand GetOrdersCommand { get; set; }

        public LoadingManager LoadingManager { get; set; }

        public ICommand MoreCommand => new Command(async () =>
          {
              await LoadUserOrderWithStatus(OrderStatus);

             //LoadingManager.OnLoading();
             //Status _statusvalue = (Status)Enum.Parse(typeof(Status), OrderStatus);
             //List<Order> tempData = new List<Order>();

             //var data = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(KeyValues["orderAdded"], _statusvalue,App.LogUser.UserId);

             //if (data != null)
             //{
             //    foreach (var item in KeyValues["orderAdded"])
             //    {
             //        if (!tempData.Any(o => o.OrderId == item.OrderId))
             //        {
             //            tempData.Add(item);
             //        }
             //    }

             //    foreach (var item in data)
             //    {
             //        if (!tempData.Any(s => s.StoreID == item.StoreID))
             //        {
             //            tempData.Add(item);

             //        }

             //    }

             //    KeyValues.Clear();
             //    KeyValues.Add("orderAdded", tempData);

             //    foreach (var item in KeyValues["orderAdded"])
             //    {
             //        if (!UserOrders.Any(s => s.OrderId == item.OrderId))
             //        {
             //            item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());
             //            var presenter = new OrderPresenter(item);

             //            UserOrders.Add(presenter);

             //        }
             //    }
             //}

             //LoadingManager.OffLoading();
         });

        public string OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }

        public string OrderStatus
        {
            get { return orderstatus; }
            set
            {
                orderstatus = value;

                OnPropertyChanged();

                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if( tokenExpManger.IsExpired() )
                {
                    tokenExpManger.CloseSession();
                    //DisplayNotification();
                }
                else
                {
                    Task.Run(async () =>
                    {
                        await LoadOrders(OrderStatus);
                    }).Wait();
                }
            }
        }

        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }

        public ObservableCollection<OrderPresenter> UserOrders { get; set; }

        private Dictionary<string, IEnumerable<Order>> KeyValues { get; set; }

        public async Task SetOrders ()
        {
            var userOrderData = orderDataStore.GetUserOrders(App.LogUser.ID);

            UserOrders.Clear();

            foreach( var item in userOrderData )
            {
                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());

                var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);
            }
        }

        private async Task LoadOrders (string value)
        {
            LoadingManager.OnLoading();

            Status _statusvalue = (Status) Enum.Parse(typeof(Status), value);

            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.ID, _statusvalue, App.TokenDto.Token);

            KeyValues.Clear();

            KeyValues.Add("orderAdded", orderData);

            UserOrders.Clear();

            foreach( var item in orderData )
            {
                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());

                var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);
            }

            LoadingManager.OffLoading();
        }

        private async Task LoadUserOrderWithStatus (string value)
        {
            LoadingManager.OnLoading();

            Status _statusvalue = (Status) Enum.Parse(typeof(Status), value);

            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.ID, _statusvalue, App.TokenDto.Token);

            if( !KeyValues.ContainsKey("orderAdded") )
            {
                KeyValues.Add("orderAdded", orderData);
            }

            switch( _statusvalue )
            {
                case Status.Completed:
                {
                    List<Order> tempData = new List<Order>();

                    var data = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(KeyValues["orderAdded"], _statusvalue, App.LogUser.ID);

                    if( data != null )
                    {
                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !tempData.Any(o => o.ID == item.ID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        foreach( var item in data )
                        {
                            if( !tempData.Any(s => s.StoreID == item.StoreID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        KeyValues.Clear();

                        KeyValues.Add("orderAdded", tempData);

                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !UserOrders.Any(s => s.OrderId == item.ID) )
                            {
                                //Task.Run(async() =>
                                //{
                                //    item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());

                                //}).Wait();

                                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());

                                var presenter = new OrderPresenter(item);

                                UserOrders.Add(presenter);
                            }
                        }
                    }

                    break;
                }

                case Status.NotSubmited:
                {
                    List<Order> tempData = new List<Order>();

                    var data = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(KeyValues["orderAdded"], _statusvalue, App.LogUser.ID);

                    if( data != null )
                    {
                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !tempData.Any(o => o.ID == item.ID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        foreach( var item in data )
                        {
                            if( !tempData.Any(s => s.StoreID == item.StoreID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        KeyValues.Clear();

                        KeyValues.Add("orderAdded", tempData);

                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !UserOrders.Any(s => s.OrderId == item.ID) )
                            {
                                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());
                                var presenter = new OrderPresenter(item);

                                UserOrders.Add(presenter);
                            }
                        }
                    }

                    break;

                    //UserOrders.Clear();

                    //foreach (var item in orderData)
                    //{
                    //    item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());
                    //    var presenter = new OrderPresenter(item);

                    //    UserOrders.Add(presenter);
                    //}
                }
                case Status.Submited:
                {
                    List<Order> tempData = new List<Order>();

                    var data = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(KeyValues["orderAdded"], _statusvalue, App.LogUser.ID);

                    if( data != null )
                    {
                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !tempData.Any(o => o.ID == item.ID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        foreach( var item in data )
                        {
                            if( !tempData.Any(s => s.StoreID == item.StoreID) )
                            {
                                tempData.Add(item);
                            }
                        }

                        KeyValues.Clear();

                        KeyValues.Add("orderAdded", tempData);

                        foreach( var item in KeyValues["orderAdded"] )
                        {
                            if( !UserOrders.Any(s => s.OrderId == item.ID) )
                            {
                                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());

                                var presenter = new OrderPresenter(item);

                                UserOrders.Add(presenter);
                            }
                        }
                    }

                    //UserOrders.Clear();

                    //foreach (var item in orderData)
                    //{
                    //    item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreID.ToString());
                    //    var presenter = new OrderPresenter(item);

                    //    UserOrders.Add(presenter);
                    //}

                    break;
                }
                default:
                    break;
            }

            LoadingManager.OffLoading();
        }
    }
}