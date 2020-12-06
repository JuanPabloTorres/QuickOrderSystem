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
        public ObservableCollection<OrderPresenterViewModel> UserOrders { get; set; }

        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }

        public ICommand GetOrdersCommand => new Command<string>(async (arg) =>
        {
            await Device.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync($"{UserOrdersWithStatus.Route}?status={arg}"));
        });

        public ICommand MoreCommand => new Command(async () =>
        {
            await LoadUserOrderWithStatus(OrderStatus);
        });

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

                        await LoadOrders(OrderStatus);
                    }).Wait();
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

        Dictionary<string, IEnumerable<Guid>> CurrentOrdersKeys { get; set; }

        private MoreManager<Guid> MoreManager;

        private string keyname = "orderAdded";
        public OrderViewModel()
        {

            PropertiesInitialization();

            MessagingCenter.Subscribe<Order>(this, "RemoveOrderSubtmitedMsg", (sender) =>
            {
                var order = UserOrders.Where(op => op.OrderId == sender.OrderId).FirstOrDefault();

                UserOrders.Remove(order);

            });

            MessagingCenter.Subscribe<OrderPresenterViewModel, OrderPresenterViewModel>(this, "Refresh", (sender, arg) =>
            {
                UserOrders.Remove(arg);
            });

            //GetOrdersCommand = new Command<string>(async (arg) =>
            //{

            //    await Shell.Current.GoToAsync($"{UserOrdersWithStatus.Route}?status={arg}");


            //});
        }


        void PropertiesInitialization()
        {
            UserOrders = new ObservableCollection<OrderPresenterViewModel>();
            LoadingManager = new LoadingManager();
            CurrentOrdersKeys = new Dictionary<string, IEnumerable<Guid>>();
            MoreManager = new MoreManager<Guid>();
        }

        public async Task SetOrders()
        {
            var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);


            UserOrders.Clear();

            foreach (var item in userOrderData)
            {

                item.StoreOrder = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                var presenter = new OrderPresenterViewModel(item);

                UserOrders.Add(presenter);
            }

        }


        async Task LoadOrders(string value)
        {
            LoadingManager.OnLoading();
            //Convertimos el value o su valor enum
            Status _statusvalue = (Status)Enum.Parse(typeof(Status), value);

            //obtenemos las ordenes del usuario del tipo de status solicitado
            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.UserId, _statusvalue, App.TokenDto.Token);

            //Limpiamos el diccionario de valores y llaves
            CurrentOrdersKeys.Clear();



            //Verificamo si la llave existe si no existe la insertamos junto con los valores que son los id de la tienda
            if (!MoreManager.ExistKey(keyname))
            {
                MoreManager.AddKeyAndValues(keyname, orderData.Select(x => x.OrderId).Take(5));
            }

            UserOrders.Clear();

            foreach (var item in orderData)
            {
                var presenter = new OrderPresenterViewModel(item);

                //item.Stor = await StoreDataStore.GetStoreInformation(item.OrderId);
                //var presenter = new OrderPresenter(item);

                UserOrders.Add(presenter);


            }

            LoadingManager.OffLoading();
        }



        async Task LoadUserOrderWithStatus(string value)
        {
            List<Guid> tempData = new List<Guid>();

            LoadingManager.OnLoading();

            Status _statusvalue = (Status)Enum.Parse(typeof(Status), value);

            var orderData = await orderDataStore.GetOrdersOfUserWithSpecificStatus(App.LogUser.UserId, _statusvalue, App.TokenDto.Token);





            if (!MoreManager.ExistKey(keyname))
            {
                MoreManager.AddKeyAndValues(keyname, orderData.Select(o => o.OrderId));
            }


            switch (_statusvalue)
            {

                case Status.Completed:
                    {
                        var datanewValues = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(MoreManager.DataValues[keyname], _statusvalue, App.LogUser.UserId);

                        if (datanewValues != null)
                        {
                            foreach (var item in MoreManager.DataValues[keyname])
                            {
                                if (!tempData.Any(o => o == item))
                                {
                                    tempData.Add(item);
                                }
                            }

                            foreach (var item in datanewValues)
                            {
                                if (!tempData.Any(s => s == item.OrderId))
                                {
                                    tempData.Add(item.OrderId);
                                }

                            }



                            MoreManager.ModifyDictionary(keyname, tempData);



                            foreach (var item in MoreManager.DataValues[keyname])
                            {

                                if (!UserOrders.Any(s => s.OrderId == item))
                                {


                                    //var storeresult = await StoreDataStore.GetStoreSimpleInformationWithOrderId(item);

                                    var order = await orderDataStore.GetItemAsync(item.ToString());


                                    if (order != null)
                                    {
                                        order.StoreOrder = await StoreDataStore.GetItemAsync(order.StoreId.ToString());

                                        var presenter = new OrderPresenterViewModel(order);
                                        UserOrders.Add(presenter);

                                    }





                                }
                            }
                        }
                        break;
                    }

                case Status.NotSubmited:
                    {
                        var datanewValues = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(MoreManager.DataValues[keyname], _statusvalue, App.LogUser.UserId);

                        if (datanewValues != null)
                        {
                            foreach (var item in MoreManager.DataValues[keyname])
                            {
                                if (!tempData.Any(o => o == item))
                                {
                                    tempData.Add(item);
                                }
                            }

                            foreach (var item in datanewValues)
                            {
                                if (!tempData.Any(s => s == item.OrderId))
                                {
                                    tempData.Add(item.OrderId);
                                }

                            }



                            MoreManager.ModifyDictionary(keyname, tempData);



                            foreach (var item in MoreManager.DataValues[keyname])
                            {

                                if (!UserOrders.Any(s => s.OrderId == item))
                                {


                                    //var storeresult = await StoreDataStore.GetStoreSimpleInformationWithOrderId(item);

                                    var order = await orderDataStore.GetItemAsync(item.ToString());


                                    if (order != null)
                                    {
                                        order.StoreOrder = await StoreDataStore.GetItemAsync(order.StoreId.ToString());

                                        var presenter = new OrderPresenterViewModel(order);
                                        UserOrders.Add(presenter);

                                    }





                                }
                            }
                        }
                        break;



                    }
                case Status.Submited:
                    {
                        var datanewValues = await orderDataStore.GetOrdersOfUserWithSpecificStatusDifferent(MoreManager.DataValues[keyname], _statusvalue, App.LogUser.UserId);

                        if (datanewValues != null)
                        {
                            foreach (var item in MoreManager.DataValues[keyname])
                            {
                                if (!tempData.Any(o => o == item))
                                {
                                    tempData.Add(item);
                                }
                            }

                            foreach (var item in datanewValues)
                            {
                                if (!tempData.Any(s => s == item.OrderId))
                                {
                                    tempData.Add(item.OrderId);
                                }

                            }



                            MoreManager.ModifyDictionary(keyname, tempData);



                            foreach (var item in MoreManager.DataValues[keyname])
                            {

                                if (!UserOrders.Any(s => s.OrderId == item))
                                {


                                    //var storeresult = await StoreDataStore.GetStoreSimpleInformationWithOrderId(item);

                                    var order = await orderDataStore.GetItemAsync(item.ToString());


                                    if (order != null)
                                    {
                                        order.StoreOrder = await StoreDataStore.GetItemAsync(order.StoreId.ToString());

                                        var presenter = new OrderPresenterViewModel(order);
                                        UserOrders.Add(presenter);

                                    }





                                }
                            }
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
