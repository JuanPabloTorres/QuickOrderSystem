using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.OrderVM
{
    [QueryProperty("OrderId", "Id")]
    public class OrderViewModel : BaseViewModel
    {
        public ObservableCollection<OrderPresenter> UserOrders { get; set; }

        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ICommand CheckoutCommand { get; set; }

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

            MessagingCenter.Subscribe<ProductPresenter, ProductPresenter>(this, "RemoveOrderProduct", (sender, arg) =>
            {


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
                //var userOrderData = orderDataStore.GetUserOrders(App.LogUser.UserId);
                var userorderwithToken = orderDataStore.GetUserOrdersWithToken(App.LogUser.UserId, App.TokenDto.Token);

                foreach (var item in userorderwithToken)
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
