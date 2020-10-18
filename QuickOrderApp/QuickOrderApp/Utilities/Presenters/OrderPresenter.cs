using Library.DTO;
using Library.Models;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class OrderPresenterViewModel : BaseViewModel
    {
        public ICommand DetailCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private Guid orderid;

        public Guid OrderId
        {
            get { return orderid; }
            set
            {
                orderid = value;
                OnPropertyChanged();
            }
        }


        private DateTime orderdate;

        public DateTime OrderDate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }

        private Guid buyerid;

        public Guid BuyerId
        {
            get { return buyerid; }
            set
            {
                buyerid = value;
                OnPropertyChanged();
            }
        }

        private byte[] storeImage;

        public byte[] StoreImage
        {
            get { return storeImage; }
            set
            {
                storeImage = value;
                OnPropertyChanged();
            }
        }

        private string storeName;

        public string StoreName
        {
            get { return storeName; }
            set
            {
                storeName = value;
                OnPropertyChanged();
            }
        }

        private int orderItems;

        public int OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;
                OnPropertyChanged();
            }
        }

        private Status ostatus;

        public Status OStatus
        {
            get { return ostatus; }
            set
            {
                ostatus = value;
                OnPropertyChanged();
            }
        }

        private Library.Models.Type orderType;

        public Library.Models.Type OrderType
        {
            get { return orderType; }
            set
            {
                orderType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderProduct> OrderProducts { get; set; }

        
        public Status OrderStatus { get; set; }


        private bool iscomplete;

        public bool IsComplete
        {
            get { return iscomplete; }
            set
            {
                iscomplete = value;
                OnPropertyChanged();
            }
        }

        private bool isdeleteEnable;

        public bool IsDeleteEnable
        {
            get { return isdeleteEnable; }
            set
            {
                isdeleteEnable = value;
                OnPropertyChanged();
            }
        }

        public double OrderTotal { get; set; }


        public OrderPresenterViewModel(Order order)
        {
            OrderId = order.OrderId;
            OrderDate = order.OrderDate;
            StoreImage = order.StoreOrder.StoreImage;
            BuyerId = order.BuyerId;
            OrderItems = order.OrderProducts.Count;
            OStatus = order.OrderStatus;
            OrderType = order.OrderType;
            StoreName = order.StoreOrder.StoreName;

            if (OStatus == Status.NotSubmited)
            {
                IsDeleteEnable = true;
            }

            OrderProducts = new ObservableCollection<OrderProduct>(order.OrderProducts);

            DetailCommand = new Command(async () =>
            {
                //await Shell.Current.GoToAsync($"DetailOrderRoute?Id={OrderId.ToString()}", animate: true);
                SelectedOrder.CurrentOrder = order;
                await Shell.Current.GoToAsync($"DetailOrderRoute", animate: true);

            });

            DeleteCommand = new Command(async () =>
            {

                var orderDelete = await orderDataStore.DisableOrder(OrderId);

                if (orderDelete)
                {

                    MessagingCenter.Send<OrderPresenterViewModel, OrderPresenterViewModel>(this, "Refresh", this);
                }

                //await Shell.Current.GoToAsync("StoreOrderRoute");

            });
        }
        public OrderPresenterViewModel(OrderDto order)
        {
            OrderId = order.OrderId;
            OrderDate = order.OrderDate;
            StoreImage = order.StoreImage;
            OrderItems = order.ProductQuantity;
            OStatus = order.OrderStatus;
            OrderType = order.OrderType;
            StoreName = order.StoreName;
            OrderTotal = order.OrderTotal;

            if (OStatus == Status.NotSubmited)
            {
                IsDeleteEnable = true;
            }

            //OrderProducts = new ObservableCollection<OrderProduct>(order.OrderProducts);

            DetailCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"DetailOrderRoute?OrderId={OrderId}", animate: true);
                //SelectedOrder.CurrentOrder = order;
                //await Shell.Current.GoToAsync($"DetailOrderRoute", animate: true);

            });

            DeleteCommand = new Command(async () =>
            {
                var orderDelete = await orderDataStore.DisableOrder(OrderId);

                if (orderDelete)
                {
                    MessagingCenter.Send(this, "Refresh", this);
                }

            });
        }
    }
}
