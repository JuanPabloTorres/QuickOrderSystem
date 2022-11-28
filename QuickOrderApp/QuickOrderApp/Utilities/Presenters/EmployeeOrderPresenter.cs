using Library.Helpers;
using Library.Models;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuickOrderApp.Utilities.Presenters
{
    public class EmployeeOrderPresenter : BaseViewModel
    {
        private Guid buyerid;

        private bool iscomplete;

        private DateTime orderdate;

        private Guid orderid;

        private int orderItems;

        private Library.Helpers.Type orderType;

        private Status ostatus;

        private Guid storeId;

        public EmployeeOrderPresenter ()
        {
        }

        public EmployeeOrderPresenter (Order order)
        {
            OrderId = order.ID;

            OrderDate = order.OrderDate;

            BuyerId = order.BuyerId;

            OrderItems = order.OrderProducts.Count;

            OStatus = order.OrderStatus;

            OrderType = order.OrderType;

            StoreId = order.StoreID;

            OrderProducts = new ObservableCollection<OrderProduct>(order.OrderProducts.ToList());

            OrderProductsPresenter = new ObservableCollection<OrderProductPresenter>();

            foreach( var item in order.OrderProducts )
            {
                var orderProductPresenter = new OrderProductPresenter()
                {
                    IsComplete = false,
                    ProductId = item.OrderProductId,
                    ProductImg = item.ProductImage,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                    Quantity = item.Quantity
                };

                OrderProductsPresenter.Add(orderProductPresenter);
            }
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

        public bool IsComplete
        {
            get { return iscomplete; }
            set
            {
                iscomplete = value;

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

        public ObservableCollection<OrderProductPresenter> OrderProductsPresenter { get; set; }

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

        public Guid StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;

                OnPropertyChanged();
            }
        }
    }
}