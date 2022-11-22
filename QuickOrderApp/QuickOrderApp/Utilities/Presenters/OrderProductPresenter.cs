using QuickOrderApp.ViewModels;
using System;

namespace QuickOrderApp.Utilities.Presenters
{
    public class OrderProductPresenter : BaseViewModel
    {
        private byte[] img;

        private bool iscomplete;

        private Guid productid;

        private string productName;

        private double productprice;

        private double quantity;

        public bool IsComplete
        {
            get { return iscomplete; }
            set
            {
                iscomplete = value;

                OnPropertyChanged();
            }
        }

        public Guid ProductId
        {
            get { return productid; }
            set
            {
                productid = value;

                OnPropertyChanged();
            }
        }

        public byte[] ProductImg
        {
            get { return img; }
            set
            {
                img = value;

                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;

                OnPropertyChanged();
            }
        }

        public double ProductPrice
        {
            get { return productprice; }
            set
            {
                productprice = value;

                OnPropertyChanged();
            }
        }

        public double Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;

                OnPropertyChanged();
            }
        }
    }
}