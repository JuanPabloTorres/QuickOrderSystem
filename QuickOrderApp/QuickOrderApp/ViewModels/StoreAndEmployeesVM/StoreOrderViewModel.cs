using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "Id")]
    public class StoreOrderViewModel : BaseViewModel
    {
        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged();



            }
        }

        private string storeId;

        public string StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;
                OnPropertyChanged();
                Guid id = new Guid(storeId);

                var orderData = orderDataStore.GetStoreOrders(id,App.TokenDto.Token);

                var orderssubmited = orderData.Where(o => o.OrderStatus == Status.Submited).OrderByDescending(date=>date.OrderDate);

                StoreOrderPresenters.Clear();
                foreach (var item in orderssubmited)
                {
                    var detailOrderPresenter = new StoreOrderPresenter(item);
                      StoreOrderPresenters.Add(detailOrderPresenter);

                    //if (item.OrderStatus != Status.NotSubmited || item.OrderStatus != Status.Completed)
                    //{
                    //    var detailOrderPresenter = new StoreOrderPresenter(item);


                    //    StoreOrderPresenters.Add(detailOrderPresenter);

                    //}
                }

            }
        }

        public ObservableCollection<StoreOrderPresenter> StoreOrderPresenters { get; set; }
        public ObservableCollection<Order> Orders { get; set; }

        public StoreOrderViewModel()
        {
            SelectedOrder = new Order();
            StoreOrderPresenters = new ObservableCollection<StoreOrderPresenter>();
            Orders = new ObservableCollection<Order>();

        }
    }
}
