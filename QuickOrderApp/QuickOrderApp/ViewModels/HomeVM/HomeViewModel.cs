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

        Dictionary<string, IEnumerable<Store>> KeyValues { get; set; }

        private MoreManager<Store> MoreManager;

        private string keyname = "storeAdded";

        public HomeViewModel()
        {
            StoreCategories = new ObservableCollection<StoreCategory>();

            KeyValues = new Dictionary<string, IEnumerable<Store>>();

            Stores = new ObservableCollection<StorePresenters>();

            LoadingManager = new LoadingManager();

            MoreManager = new MoreManager<Store>();

            MoreCommand = new Command(async () =>
            {

                await LoadDifferentItems();

            });



            SelectedStore = new Store();

        }


        public async Task LoadItems()
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



    }
}
