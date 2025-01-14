﻿using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Presenters.PresenterModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.HomeVM
{
    public class HomeViewModel : BaseViewModel
    {
        private static int group;

        private string searchText;

        private Store selectedStore;

        public HomeViewModel ()
        {
            StoreCategories = new ObservableCollection<StoreCategory>();

            KeyValues = new Dictionary<string, IEnumerable<Store>>();

            Stores = new ObservableCollection<StorePresenters>();

            LoadingManager = new LoadingManager();

            MoreCommand = new Command(async () =>
            {
                await LoadDifferentItems();
            });

            //Task.Run(async() =>
            //{
            //    await LoadItems();
            //}).Wait();

            SelectedStore = new Store();
        }

        public LoadingManager LoadingManager { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand SearchStoreCommand { get; set; }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;

                OnPropertyChanged();
            }
        }

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<StoreCategory> StoreCategories { get; set; }
        public ObservableCollection<StorePresenters> Stores { get; set; }
        private Dictionary<string, IEnumerable<Store>> KeyValues { get; set; }

        public async Task LoadDifferentItems ()
        {
            LoadingManager.OnLoading();

            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();

                LoadingManager.OffLoading();
            }
            else
            {
                if( !KeyValues.ContainsKey("storeAdded") )
                {
                    await LoadItems();
                }
                else
                {
                    var storeData = await StoreDataStore.GetDifferentStore(KeyValues["storeAdded"]);

                    if( storeData != null )
                    {
                        var tempData = new List<Store>();

                        foreach( var item in KeyValues["storeAdded"] )
                        {
                            if( !tempData.Any(s => s.StoreId == item.StoreId) )
                            {
                                tempData.Add(item);
                            }
                        }

                        foreach( var item in storeData )
                        {
                            if( !tempData.Any(s => s.StoreId == item.StoreId) )
                            {
                                tempData.Add(item);
                            }
                        }

                        KeyValues.Clear();

                        KeyValues.Add("storeAdded", tempData);

                        foreach( var item in KeyValues["storeAdded"] )
                        {
                            if( !Stores.Any(s => s.StoreId == item.StoreId) )
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

        public async Task LoadItems ()
        {
            LoadingManager.OnLoading();

            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();

                LoadingManager.OffLoading();
            }
            else
            {
                var storeData = await StoreDataStore.GetAvailableStore();

                if( !KeyValues.ContainsKey("storeAdded") )
                {
                    KeyValues.Add("storeAdded", storeData);
                }

                if( Stores.Count > 0 )
                {
                    Stores.Clear();
                }

                foreach( var store in storeData )
                {
                    var storepresenter = new StorePresenters(store);

                    Stores.Add(storepresenter);
                }

                LoadingManager.OffLoading();
            }
        }

        public void SetStoreCategories ()
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