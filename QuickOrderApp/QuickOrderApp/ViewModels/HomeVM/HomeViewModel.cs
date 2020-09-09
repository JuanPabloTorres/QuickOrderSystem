using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Dependency;
using QuickOrderApp.Utilities.Dependency.Interface;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Presenters.PresenterModel;
using QuickOrderApp.Views.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.HomeVM
{

    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<StorePresenters> Stores { get; set; }

        public ObservableCollection<StoreCategory> StoreCategories { get; set; }
        public ICommand SearchStoreCommand { get; set; }

        private Store selectedStore;

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;
                OnPropertyChanged();

            }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

       public LoadingManager LoadingManager { get; set; }


        public HomeViewModel()
        {
            StoreCategories = new ObservableCollection<StoreCategory>();
            Stores = new ObservableCollection<StorePresenters>();
            LoadingManager = new LoadingManager();


            Task.Run(async() => 
            {
                LoadingManager.OnLoading();
                await LoadItems();
                LoadingManager.OffLoading();
            }).Wait();
                      

            SelectedStore = new Store();

        }

       
        public async  Task LoadItems()
        {

            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
            if (tokenExpManger.IsExpired())
            {
               await tokenExpManger.CloseSession();
            }
            else
            {

            var storeData = await StoreDataStore.GetAvailableStore();


            if (Stores.Count > 0)
            {

                Stores.Clear();
            }



            foreach (var store in storeData)
            {
                var storepresenter = new StorePresenters(store);

                Stores.Add(storepresenter);
            }

            }


        }

        public void SetStoreCategories()
        {
            //var values = Enum.GetValues(typeof(StoreType));

            StoreCategory barbershopcategory = new StoreCategory(StoreType.BarberShop.ToString(), "barbershop.png");

            StoreCategories.Add(barbershopcategory);

            StoreCategory grocerycategory = new StoreCategory(StoreType.Grocery.ToString(), "supermarket.png");

            StoreCategories.Add(grocerycategory);

            StoreCategory farmingcategory = new StoreCategory(StoreType.Farming.ToString(), "garden.png");


            StoreCategories.Add(farmingcategory);

            StoreCategory restaurantcategory = new StoreCategory(StoreType.Restaurant.ToString(), "food.png");

            StoreCategories.Add(restaurantcategory);

            StoreCategory autopartscategory = new StoreCategory(StoreType.AutorParts.ToString(), "support.png");

            StoreCategories.Add(autopartscategory);


            StoreCategory servicecategory = new StoreCategory(StoreType.Service.ToString(), "partnership.png");

            StoreCategories.Add(servicecategory);

            StoreCategory storecategory = new StoreCategory(StoreType.Store.ToString(), "tag.png");

            StoreCategories.Add(storecategory);

            StoreCategory clothescategory = new StoreCategory(StoreType.ClothingStore.ToString(), "clothes.png");

            StoreCategories.Add(clothescategory);

        }

    }
}
