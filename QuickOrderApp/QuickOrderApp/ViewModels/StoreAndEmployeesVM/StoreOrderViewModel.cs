using Library.Models;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "Id")]
    public class StoreOrderViewModel : BaseViewModel
    {
        private Order selectedOrder;

        private string storeId;

        public StoreOrderViewModel ()
        {
            SelectedOrder = new Order();

            LoadingManager = new LoadingManager();

            StoreOrderPresenters = new ObservableCollection<StoreOrderPresenter>();

            Orders = new ObservableCollection<Order>();

            MessagingCenter.Subscribe<EmployeeOrderPresenter>(this, "RemoveEmpOrderPrensenter", (sender) =>
            {
                var orderToRemove = StoreOrderPresenters.Where(op => op.DetailOrder.OrderId == sender.OrderId).FirstOrDefault();

                StoreOrderPresenters.Remove(orderToRemove);
            });
        }

        public LoadingManager LoadingManager { get; set; }

        public ObservableCollection<Order> Orders { get; set; }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;

                OnPropertyChanged();
            }
        }

        public string StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;

                OnPropertyChanged();

                Guid id = new Guid(storeId);

                Task.Run(async () =>
                {
                    LoadingManager.OnLoading();

                    await ExecuteLoadItems(id);

                    LoadingManager.OffLoading();
                });
            }
        }

        public ObservableCollection<StoreOrderPresenter> StoreOrderPresenters { get; set; }

        public async Task ExecuteLoadItems (Guid storeId)
        {
            var orderData = await orderDataStore.GetStoreOrders(storeId, App.TokenDto.Token);

            //var orderssubmited = orderData.Where(o => o.OrderStatus == Status.Submited ).OrderByDescending(date => date.OrderDate);
            if( StoreOrderPresenters.Count() > 0 )
            {
                StoreOrderPresenters.Clear();
            }

            foreach( var item in orderData )
            {
                var detailOrderPresenter = new StoreOrderPresenter(item);

                StoreOrderPresenters.Add(detailOrderPresenter);
            }
        }
    }
}