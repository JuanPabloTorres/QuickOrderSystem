using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "Id")]
    class StoreViewModel : BaseViewModel
    {
        #region Properties
        private string storeid;

        public string StoreId
        {
            get { return storeid; }
            set
            {
                storeid = value;
                OnPropertyChanged();

                GetStoreInformation(StoreId);


            }
        }

        private string storename;

        public string StoreName
        {
            get { return storename; }
            set
            {
                storename = value;
                OnPropertyChanged();
            }
        }

        private byte[] storeimg;

        public byte[] StoreImg
        {
            get { return storeimg; }
            set
            {
                storeimg = value;
                OnPropertyChanged();
            }
        }


        private string storedescription;

        public string StoreDescription
        {
            get { return storedescription; }
            set
            {
                storedescription = value;
                OnPropertyChanged();
            }
        }





        public ObservableCollection<ProductPresenter> StoreProducts { get; set; }
        public ObservableCollection<WorkHour> StoreWorkoutHours { get; set; }
        public ObservableCollection<OrderProduct> OrderProducts { get; set; }



        private WorkHour workhour;

        public WorkHour WorkHour
        {
            get { return workhour; }
            set
            {
                workhour = value;
                OnPropertyChanged();
            }
        }


        public ICommand GoShowCommand { get; set; }

        #endregion
        public StoreViewModel()
        {

            StoreProducts = new ObservableCollection<ProductPresenter>();
            StoreWorkoutHours = new ObservableCollection<WorkHour>();
            OrderProducts = new ObservableCollection<OrderProduct>();

            GoShowCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"ProductRoute?Id={StoreId.ToString()}", animate: true);
                //await Shell.Current.GoToAsync("ProductRoute");
            });
        }


        // async Task GetStoreProducts(ICollection<Product>)
        //{
        //    var storeproductData = await productDataStore.GetItemsAsync();
        //    StoreProducts = new ObservableCollection<ProductPresenter>(storeproductData);
        //}

        public async Task GetStoreInformation(string id)
        {
            var store = await StoreDataStore.GetItemAsync(StoreId);
            StoreImg = store.StoreImage;
            StoreName = store.StoreName;
            StoreDescription = store.StoreDescription;

            if (StoreProducts.Count > 0)
            {
                StoreProducts.Clear();
            }
            foreach (var product in store.Products)
            {
                var productPresenter = new ProductPresenter(product);
                if (StoreProducts.Where(p => p.ProductId == productPresenter.ProductId).FirstOrDefault() == null)
                {

                    StoreProducts.Add(productPresenter);
                }
            }


            if (StoreWorkoutHours.Count() == 0)
            {

                foreach (var workhour in store.WorkHours)
                {
                    if (workhour.Day == DateTime.Today.DayOfWeek.ToString())
                    {
                        WorkHour = workhour;
                    }

                    StoreWorkoutHours.Add(workhour);
                }
            }



        }



    }
}
