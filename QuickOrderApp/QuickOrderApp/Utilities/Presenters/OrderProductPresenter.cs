using QuickOrderApp.ViewModels;
using System;

namespace QuickOrderApp.Utilities.Presenters
{
    public class OrderProductPresenter : BaseViewModel
    {

        private Guid productid;

        public Guid ProductId
        {
            get { return productid; }
            set
            {
                productid = value;
                OnPropertyChanged();
            }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged();
            }
        }

        private double productprice;

        public double ProductPrice
        {
            get { return productprice; }
            set
            {
                productprice = value;
                OnPropertyChanged();
            }
        }

        private byte[] img;

        public byte[] ProductImg
        {
            get { return img; }
            set
            {
                img = value;
                OnPropertyChanged();
            }
        }

        private double quantity;

        public double Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

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



    }
}
