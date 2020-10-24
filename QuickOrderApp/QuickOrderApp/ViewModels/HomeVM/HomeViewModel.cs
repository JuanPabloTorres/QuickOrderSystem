using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Configuration;
using System.Linq;
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

        public ICommand MoreCommand { get; set; }

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

        Dictionary<string,IEnumerable<Store>> KeyValues { get; set; }

        private MoreManager<Store> MoreManager;
        private string keyname = "storeAdded";
        static int group;
        public HomeViewModel()
        {
            StoreCategories = new ObservableCollection<StoreCategory>();

            KeyValues = new Dictionary<string, IEnumerable<Store>>();
            Stores = new ObservableCollection<StorePresenters>();
            LoadingManager = new LoadingManager();
            MoreManager = new MoreManager<Store>();

            MoreCommand = new Command(async() =>
            {

               await LoadDifferentItems();
            
            });

          

            SelectedStore = new Store();

        }

       
        public async  Task LoadItems()
        {
            LoadingManager.OnLoading();

            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
            if (tokenExpManger.IsExpired())
            {
               await tokenExpManger.CloseSession();
                LoadingManager.OffLoading();

            }
            else
            {
                
            var storeData = await StoreDataStore.GetAvailableStore();

                //if (!KeyValues.ContainsKey("storeAdded"))
                //{

                //    KeyValues.Add("storeAdded", storeData);
                //}

                MoreManager.AddKeyAndValues(keyname, storeData);


                if (Stores.Count > 0)
            {
                Stores.Clear();
            }

            foreach (var store in storeData)
            {
                var storepresenter = new StorePresenters(store);

                Stores.Add(storepresenter);
            }

                LoadingManager.OffLoading();
            }


        }

        public async Task LoadDifferentItems()
        {
            LoadingManager.OnLoading();

            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
            if (tokenExpManger.IsExpired())
            {
                await tokenExpManger.CloseSession();
                LoadingManager.OffLoading();

            }
            else
            {

                if (!MoreManager.ExistKey(keyname))
                {
                    await LoadItems();
                }

                else
                {
                    var storeData = await StoreDataStore.GetDifferentStore(MoreManager.DataValues[keyname]);

                    if (storeData != null)
                    {


                        var differentValue = MoreManager.InsertDifferentDataValue(storeData, keyname);

                        MoreManager.ModifyDictionary(keyname, differentValue);


                        foreach (var item in MoreManager.DataValues[keyname])
                        {

                            if (!Stores.Any(s => s.StoreId == item.StoreId))
                            {
                                var storepresenter = new StorePresenters(item);


                                Stores.Add(storepresenter);

                            }
                        }


                    }
                }


               


                LoadingManager.OffLoading();
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
