using Library.Helpers;
using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("SelectedProductType", "type")]
    public class StoreProductViewModel : BaseViewModel
    {
        private string selectedproductType;

        public StoreProductViewModel ()
        {
            StoreProducts = new ObservableCollection<ProductPresenter>();
        }

        public string SelectedProductType
        {
            get { return selectedproductType; }
            set
            {
                selectedproductType = value;

                OnPropertyChanged();

                LoadInventory(SelectedProductType);
            }
        }

        public ObservableCollection<ProductPresenter> StoreProducts { get; set; }

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

        //public async Task GetStoreInformation(string id)
        //{
        //    var store = await StoreDataStore.GetItemAsync(StoreId);
        //    StoreImg = store.StoreImage;
        //    StoreName = store.StoreName;
        //    StoreDescription = store.StoreDescription;

        //    if (StoreProducts.Count > 0)
        //    {
        //        StoreProducts.Clear();
        //    }
        //    foreach (var product in store.Products)
        //    {
        //        var productPresenter = new ProductPresenter(product);
        //        if (StoreProducts.Where(p => p.ProductId == productPresenter.ProductId).FirstOrDefault() == null)
        //        {
        //            StoreProducts.Add(productPresenter);
        //        }
        //    }

        //    if (StoreWorkoutHours.Count() == 0)
        //    {
        //        foreach (var workhour in store.WorkHours)
        //        {
        //            if (workhour.Day == DateTime.Today.DayOfWeek.ToString())
        //            {
        //                WorkHour = workhour;
        //            }

        //            StoreWorkoutHours.Add(workhour);
        //        }
        //    }

        //}
    }
}