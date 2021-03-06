﻿using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class StorePresenters : BaseViewModel
    {
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


        private byte[] img;

        public byte[] StoreImg
        {
            get { return img; }
            set
            {
                img = value;
                OnPropertyChanged();
            }
        }

        private Guid storeId;

        public Guid StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;
                OnPropertyChanged();
            }
        }

        private string storeType;

        public string StoreType
        {
            get { return storeType; }
            set
            {
                storeType = value;
                OnPropertyChanged();
            }
        }


        public ICommand GoAdminStoreHomeControl => new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {
                await Shell.Current.GoToAsync($"StoreControlPanelRoute?Id={StoreId.ToString()}", animate: true);

                }

            });
        public ICommand GoHomeStoreCommand { get; set; }

        public ICommand GoSelectedEmployeeStoreCommand => new Command(async() =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await EmployeeShell.Current.GoToAsync($"StoreControlEmployee?EmpStoreId={StoreId.ToString()}", animate: true);
                }
            });

        public StorePresenters(Store store)
        {

            StoreName = store.StoreName;
            StoreId = store.StoreId;
            StoreImg = store.StoreImage;
            StoreDescription = store.StoreDescription;
            StoreType = store.StoreType.ToString();

            GoHomeStoreCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                App.CurrentStore = store;
                await GoStoreHome();
                }

            });
        }

        async Task GoStoreHome()
        {
            await Shell.Current.GoToAsync($"StoreHomeRoute?Id={StoreId.ToString()}", animate: true);
            //await Shell.Current.GoToAsync("StoreHomeRoute");
        }
    }
}
