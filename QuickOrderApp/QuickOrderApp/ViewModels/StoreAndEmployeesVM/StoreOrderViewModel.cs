using Library.Models;
using QuickOrderApp.Managers;
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


      
        private MoreManager<Order> MoreManager;
      

        public ICommand MoreCommand => new Command(async() =>
        {

            LoadingManager.OnLoading();

            var storeIdGuid =  Guid.Parse(StoreId);

            var results = await orderDataStore.GetDifferentStoreOrders(MoreManager.GetValues(keyname), storeIdGuid);

            if (results != null)
            {
                var differentValue = MoreManager.InsertDifferentDataValue(results, keyname);

                MoreManager.ModifyDictionary(keyname, differentValue);


                foreach (var item in MoreManager.GetValues(keyname))
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
            MoreManager = new MoreManager<Order>();
            MessagingCenter.Subscribe<EmployeeOrderPresenter>(this, "RemoveEmpOrderPrensenter", (sender) =>
            {
              

                var orderToRemove = StoreOrderPresenters.Where(op => op.DetailOrder.OrderId == sender.OrderId).FirstOrDefault();
                StoreOrderPresenters.Remove(orderToRemove);


            });

        }

        public async Task ExecuteLoadItems(Guid storeId)
        {

            var orderData = await orderDataStore.GetStoreOrders(storeId, App.TokenDto.Token);


            if (!MoreManager.ExistKey(keyname))
            {
                MoreManager.AddKeyAndValues(keyname, orderData);
            }
           


            //var orderssubmited = orderData.Where(o => o.OrderStatus == Status.Submited ).OrderByDescending(date => date.OrderDate);
            if (StoreOrderPresenters.Count() > 0)
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
