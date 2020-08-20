using Library.Models;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace QuickOrderApp.Utilities.Presenters
{
    public class EmployeeOrderPresenter:BaseViewModel
    {

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

        public ObservableCollection<OrderProductPresenter> OrderProductsPresenter { get; set; }
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

        private Guid storeId;

        public Guid StoreId
        {
            get { return storeId; }
            set { storeId = value;
                OnPropertyChanged();
            }
        }


        public EmployeeOrderPresenter()
        {

        }


        public EmployeeOrderPresenter(Order order)
        {
            OrderId = order.OrderId;
            OrderDate = order.OrderDate;           
            BuyerId = order.BuyerId;
            OrderItems = order.OrderProducts.Count;
            OStatus = order.OrderStatus;
            OrderType = order.OrderType;
            StoreId = order.StoreId;
            OrderProducts = new ObservableCollection<OrderProduct>(order.OrderProducts.ToList());
            OrderProductsPresenter = new ObservableCollection<OrderProductPresenter>();
            foreach (var item in order.OrderProducts)
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



    }
}
