using Library.Helpers;
using Library.Models;
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
    [QueryProperty("SelectedProductType", "type")]
    internal class StoreViewModel : BaseViewModel
    {
        private string selectedproductType;

        private string storedescription;

        private string storeid;

        private byte[] storeimg;

        private string storename;

        private string title;

        private WorkHour workhour;

        public StoreViewModel ()
        {
            PropertiesInitializer();
        }

        public ICommand GoShowCommand { get; set; }

        public ObservableCollection<OrderProduct> OrderProducts { get; set; }

        public ObservableCollection<ProductCategoryPresenter> ProductCategoryPresenters { get; set; }

        public string SelectedProductType
        {
            get { return selectedproductType; }
            set
            {
                selectedproductType = value;

                OnPropertyChanged();

                Title = $"Category: {SelectedProductType}";

                LoadInventory(SelectedProductType);
            }
        }

        public string StoreDescription
        {
            get { return storedescription; }
            set
            {
                storedescription = value;

                OnPropertyChanged();
            }
        }

        public string StoreId
        {
            get { return storeid; }
            set
            {
                storeid = value;

                OnPropertyChanged();

                GetStoreInformation(StoreId);

                //GroupByProductCategory(StoreProducts);
            }
        }

        public byte[] StoreImg
        {
            get { return storeimg; }
            set
            {
                storeimg = value;

                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get { return storename; }
            set
            {
                storename = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductPresenter> StoreProducts { get; set; }

        public ObservableCollection<WorkHour> StoreWorkoutHours { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;

                OnPropertyChanged();
            }
        }

        public WorkHour WorkHour
        {
            get { return workhour; }
            set
            {
                workhour = value;

                OnPropertyChanged();
            }
        }

        public async Task GetStoreInformation (string id)
        {
            Guid StoreId = Guid.Parse(id);

            var store = await StoreDataStore.GetAvailableStoreInformation(StoreId);

            StoreImg = store.StoreImage;

            StoreName = store.StoreName;

            Title = store.StoreName;

            StoreDescription = store.StoreDescription;

            if( StoreProducts.Count > 0 )
            {
                StoreProducts.Clear();
            }

            foreach( var product in store.Products )
            {
                var productPresenter = new ProductPresenter(product);

                if( StoreProducts.Where(p => p.ProductId == productPresenter.ProductId).FirstOrDefault() == null )
                {
                    StoreProducts.Add(productPresenter);
                }
            }

            await GroupByProductCategory(StoreProducts);

            //if (ProductCategoryPresenters.Count() > 0)
            //{
            //    ProductCategoryPresenters.Clear();
            //}

            //var group = StoreProducts.GroupBy(p => p.ProductType);

            //foreach (var item in group)
            //{
            //    var _productCategory = new ProductCategoryPresenter(item.Key.ToString());

            //    ProductCategoryPresenters.Add(_productCategory);
            //}

            if( StoreWorkoutHours.Count() == 0 )
            {
                foreach( var workhour in store.WorkHours )
                {
                    if( workhour.Day == DateTime.Today.DayOfWeek.ToString() )
                    {
                        WorkHour = workhour;
                    }

                    StoreWorkoutHours.Add(workhour);
                }
            }
        }

        public async Task GroupByProductCategory (IList<ProductPresenter> products)
        {
            if( ProductCategoryPresenters.Count() > 0 )
            {
                ProductCategoryPresenters.Clear();
            }

            var group = products.GroupBy(p => p.ProductType);

            foreach( var item in group )
            {
                var _productCategory = new ProductCategoryPresenter(item.Key.ToString());

                ProductCategoryPresenters.Add(_productCategory);
            }
        }

        private async Task LoadInventory (string selectedproductType)
        {
            StoreProducts.Clear();

            ProductType _productType = (ProductType) Enum.Parse(typeof(ProductType), selectedproductType);

            //Guid guidStoreId = Guid.Parse(StoreId);

            var data = await productDataStore.GetSpecificProductTypeFromStore(App.CurrentStore.StoreID, _productType);

            foreach( var item in data )
            {
                var productPresenter = new ProductPresenter(item);

                StoreProducts.Add(productPresenter);
            }
        }

        private void PropertiesInitializer ()
        {
            StoreProducts = new ObservableCollection<ProductPresenter>();

            StoreWorkoutHours = new ObservableCollection<WorkHour>();

            OrderProducts = new ObservableCollection<OrderProduct>();

            ProductCategoryPresenters = new ObservableCollection<ProductCategoryPresenter>();
        }
    }
}