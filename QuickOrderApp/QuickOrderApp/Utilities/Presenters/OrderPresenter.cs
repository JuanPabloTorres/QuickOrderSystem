using Library.Helpers;
using Library.Models;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class OrderPresenter : BaseViewModel
    {
        private Guid buyerid;

        private bool iscomplete;

        private bool isdeleteEnable;

        private DateTime orderdate;

        private Guid orderid;

        private int orderItems;

        private Library.Helpers.Type orderType;

        private Status ostatus;

        private byte[] storeImage;

        private string storeName;

        public OrderPresenter (Order order)
        {
            OrderId = order.ID;

            OrderDate = order.OrderDate;

            StoreImage = order.StoreOrder.StoreImage;

            BuyerId = order.BuyerId;

            OrderItems = order.OrderProducts.Count;

            OStatus = order.OrderStatus;

            OrderType = order.OrderType;

            StoreName = order.StoreOrder.StoreName;

            if( OStatus == Status.NotSubmited )
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

                if( orderDelete )
                {
                    MessagingCenter.Send<OrderPresenter, OrderPresenter>(this, "Refresh", this);
                }

                //await Shell.Current.GoToAsync("StoreOrderRoute");
            });
        }

        public Guid BuyerId
        {
            get { return buyerid; }
            set
            {
                buyerid = value;

                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand DetailCommand { get; set; }

        public bool IsComplete
        {
            get { return iscomplete; }
            set
            {
                iscomplete = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeleteEnable
        {
            get { return isdeleteEnable; }
            set
            {
                isdeleteEnable = value;

                OnPropertyChanged();
            }
        }

        public DateTime OrderDate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }

        public Guid OrderId
        {
            get { return orderid; }
            set
            {
                orderid = value;

                OnPropertyChanged();
            }
        }

        public int OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderProduct> OrderProducts { get; set; }

        public Status OrderStatus { get; set; }

        public Library.Helpers.Type OrderType
        {
            get { return orderType; }
            set
            {
                orderType = value;

                OnPropertyChanged();
            }
        }

        public Status OStatus
        {
            get { return ostatus; }
            set
            {
                ostatus = value;

                OnPropertyChanged();
            }
        }

        public byte[] StoreImage
        {
            get { return storeImage; }
            set
            {
                storeImage = value;

                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get { return storeName; }
            set
            {
                storeName = value;

                OnPropertyChanged();
            }
        }
    }
}