using Library.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.Views.Login;
using QuickOrderApp.Views.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class SettingViewModel : BaseViewModel
    {
        private User userinformation;

        public User UserInformation
        {
            get { return userinformation; }
            set
            {
                userinformation = value;
                OnPropertyChanged();
            }
        }

        #region Properties

     

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set
            {
                if (_fullname != value)
                {
                    _fullname = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        private string adress;

        public string Address
        {
            get { return adress; }
            set
            {
                adress = value;
                OnPropertyChanged();
            }
        }

        private string genderselected;

        public string GenderSelected
        {
            get { return genderselected; }
            set
            {
                genderselected = value;
                OnPropertyChanged();
            }
        }

      


        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set { /*SetPropertyValue(ref _isShowCancel, value);*/ }
        }

        public List<string> Genders { get; set; }

      

        #endregion

        #region Commands

        public ICommand LogOutCommand { get; set; }
        public ICommand GoUserInformationCommand { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
     
        public ICommand GoPaymentCardCommand { get; set; }

        public ICommand AlreadyHaveLicenseCommand { get; set; }

        public ICommand RegisterStoreCommand { get; set; }

      

        public ICommand RegisterCardCommand { get; set; }
        public ICommand GoRegisterCardCommand { get; set; }

        public ICommand GoGetLicenseCommand { get; set; }
        public ICommand GoCheckYourStores { get; set; }

        public ICommand TapCommand => new Command<string>(OpenBrowser);

        #endregion

       
        public SettingViewModel()
        {
            #region UserInformaation Initialize
            Fullname = App.LogUser.Name;
            Email = App.LogUser.Email;
            Address = App.LogUser.Address;
            Phone = App.LogUser.Phone;
            GenderSelected = App.LogUser.Gender.ToString();
            UserInformation = App.LogUser;
            #endregion           

            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());
          
            GoUserInformationCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync("UserInformationRoute", true);
                }

            });

            UpdateProfileCommand = new Command(async () =>
           {
               TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
               if (tokenExpManger.IsExpired())
               {
                   await tokenExpManger.CloseSession();
               }
               else
               {

               await Shell.Current.GoToAsync("UpdateProfileRoute", true);
               MessagingCenter.Send<User>(App.LogUser, "UserInformation");
               }
           });

            GoPaymentCardCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync($"{YourCardsPage.Route}", true);
                }

            });


            RegisterStoreCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync("GetLicensenStoreRoute", true);
                }
            });

            AlreadyHaveLicenseCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync("RegisterStoreRoute", true);
                }
            });

           

            LogOutCommand = new Command(async() =>
            {
                await App.ComunicationService.Disconnect();
              

                if (App.UsersConnected != null)
                {
                    App.UsersConnected.IsDisable = true;
                    var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

                    if (result)
                    {
                        App.UsersConnected = null;
                    }

                }
               await Shell.Current.GoToAsync("../LoginRoute");

            });

            GoRegisterCardCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync("RegisterCardRoute");
                }


            });

            GoGetLicenseCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync("StoreLicenseRoute");
                }



            });

            GoCheckYourStores = new Command(async () => 
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                await Shell.Current.GoToAsync($"{YourStoresPage.Route}");
                }
            });
        }

       async  void OpenBrowser(string url)
        {
           await  Launcher.OpenAsync(new Uri(url));
        }

       

    }
}
