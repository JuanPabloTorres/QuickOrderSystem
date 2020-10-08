using Library.Models;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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

                Task.Run(async () =>
                {
                    LoadingManager.OnLoading();
                    await ExecuteLoadItems(id);

                    LoadingManager.OffLoading();



                });

               

            }
        }

        public ObservableCollection<StoreOrderPresenter> StoreOrderPresenters { get; set; }
        public ObservableCollection<Order> Orders { get; set; }


        public LoadingManager LoadingManager { get; set; }


        Dictionary<string, IEnumerable<Order>> KeyValues { get; set; }

        public ICommand MoreCommand => new Command(async() =>
        {

            LoadingManager.OnLoading();

            var storeIdGuid =  Guid.Parse(StoreId);

            var results = await orderDataStore.GetDifferentStoreOrders(KeyValues[keyname], storeIdGuid);

            if (results != null)
            {
                List<Order> tempData = new List<Order>();

                foreach (var item in KeyValues[keyname])
                {
                    if (!tempData.Any(s => s.OrderId == item.OrderId))
                    {

                        tempData.Add(item);

                    }
                }
                foreach (var item in results)
                {
                    if (!tempData.Any(s => s.OrderId == item.OrderId))
                    {

                        tempData.Add(item);

                    }
                }

                KeyValues.Clear();
                KeyValues.Add(keyname, tempData);

                foreach (var item in KeyValues[keyname])
                {

                    if (!StoreOrderPresenters.Any(s => s.DetailOrder.OrderId == item.OrderId))
                    {
                        var detailOrderPresenter = new StoreOrderPresenter(item);
                        StoreOrderPresenters.Add(detailOrderPresenter);

                    }
                }


                LoadingManager.OffLoading();
            }

        });

        string keyname = "storeOrdersAdded";

        public StoreOrderViewModel()
        {
            SelectedOrder = new Order();
            LoadingManager = new LoadingManager();
            StoreOrderPresenters = new ObservableCollection<StoreOrderPresenter>();
            Orders = new ObservableCollection<Order>();
            KeyValues = new Dictionary<string, IEnumerable<Order>>();
            MessagingCenter.Subscribe<EmployeeOrderPresenter>(this, "RemoveEmpOrderPrensenter", (sender) =>
            {
              

                var orderToRemove = StoreOrderPresenters.Where(op => op.DetailOrder.OrderId == sender.OrderId).FirstOrDefault();
                StoreOrderPresenters.Remove(orderToRemove);


            });

        }

        public async Task ExecuteLoadItems(Guid storeId)
        {
           
            var orderData =await orderDataStore.GetStoreOrders(storeId, App.TokenDto.Token);


            KeyValues.Add(keyname, orderData);


            //var orderssubmited = orderData.Where(o => o.OrderStatus == Status.Submited ).OrderByDescending(date => date.OrderDate);
            if (StoreOrderPresenters.Count() > 0 )
            {
            StoreOrderPresenters.Clear();

            }

            foreach (var item in orderData)
            {
                var detailOrderPresenter = new StoreOrderPresenter(item);
                StoreOrderPresenters.Add(detailOrderPresenter);

               
            }

           
        }
    }
}
