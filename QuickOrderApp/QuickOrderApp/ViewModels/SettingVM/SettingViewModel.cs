using Library.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class SettingViewModel : BaseViewModel
    {

        //private Store selectedStore;

        //public Store SelectedStore
        //{
        //    get { return selectedStore; }
        //    set
        //    {
        //        selectedStore = value;
        //        OnPropertyChanged();

        //        if (!(SelectedStore.StoreId == Guid.Empty))
        //        {

        //            Shell.Current.GoToAsync($"StoreControlPanelRoute?Id={SelectedStore.StoreId.ToString()}", animate: true);
        //        }
        //    }
        //}

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

        ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();


        #region Properties

        private string storename;

        public string StoreName
        {
            get { return storename; }
            set
            {
                if (storename != value)
                {

                    storename = value;
                    OnPropertyChanged();
                }
            }
        }

        private string licenceCode;

        public string LicenseCode
        {
            get { return licenceCode; }
            set
            {
                licenceCode = value;
                OnPropertyChanged();
            }
        }

        private ImageSource storeImage;

        public ImageSource StoreImage
        {
            get { return storeImage; }
            set
            {
                storeImage = value;
                OnPropertyChanged();
            }
        }


        byte[] ImgArray;



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

        private string storeTypeSelected;

        public string StoreTypeSelected
        {
            get { return storeTypeSelected; }
            set
            {
                storeTypeSelected = value;
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

        public List<string> StoreTypes { get; set; }
        #endregion

        #region Commands

        public ICommand LogOutCommand { get; set; }
        public ICommand GoUserInformationCommand { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
        public ICommand DoneCommand { get; set; }

        public ICommand AlreadyHaveLicenseCommand { get; set; }

        public ICommand RegisterStoreCommand { get; set; }

        public ICommand IPickPhotoCommand { get; set; }

        public ICommand CompleteRegisterCommand { get; set; }

        public ICommand RegisterCardCommand { get; set; }
        public ICommand GoRegisterCardCommand { get; set; }

        public ICommand GoGetLicenseCommand { get; set; }
        public ICommand GoCheckYourStores { get; set; }

        public ICommand TapCommand => new Command<string>(OpenBrowser);

        #endregion

        #region RegisterStoreHour






        public ObservableCollection<WorkHourPresenter> WorkHourPresenters { get; set; }


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
            #region WorkHourPresenter Initialize
            WorkHourPresenters = new ObservableCollection<WorkHourPresenter>();

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Monday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Tuesday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Wednesday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Thursday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Friday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Saturday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Sunday.ToString()));
            #endregion

            StoreImage = ImageSource.FromFile("imgPlaceholder.jpg");
            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());
            StoreTypes = new List<string>(Enum.GetNames(typeof(StoreType)).ToList());
            //SelectedStore = new Store();
            GoUserInformationCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("UserInformationRoute", true);

            });

            UpdateProfileCommand = new Command(async () =>
           {

               await Shell.Current.GoToAsync("UpdateProfileRoute", true);
           });

            DoneCommand = new Command(async () =>
            {

                var userUpdated = App.LogUser;

                userUpdated.Name = Fullname;
                userUpdated.Address = Address;
                userUpdated.Phone = Phone;
                userUpdated.Email = Email;


                Gender value;
                Enum.TryParse(GenderSelected, out value);
                userUpdated.Gender = value;

                var result = await userDataStore.UpdateItemAsync(userUpdated);

                if (result)
                {
                    App.LogUser = userUpdated;
                }

            });

            RegisterStoreCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("GetLicensenStoreRoute", true);
            });

            AlreadyHaveLicenseCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("RegisterStoreRoute", true);
            });

            IPickPhotoCommand = new Command(async () =>
            {
                await CrossMedia.Current.Initialize();
                files.Clear();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Shell.Current.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Full,

                });


                if (file == null)
                    return;

                //files.Add(file);

                ImgArray = ConvertToByteArray(file.GetStream());

                StoreImage = ImageSource.FromStream(() => file.GetStream());
                //storeImage.Source = ImageSource.FromFile(file.Path);

            });

            CompleteRegisterCommand = new Command(async () =>
            {

                List<string> valuesToCheck = new List<string>();

                valuesToCheck.Add(StoreName);
                valuesToCheck.Add(LicenseCode);

                if (GlobalValidator.CheckNullOrEmptyPropertiesOfListValues(valuesToCheck) && GlobalValidator.CheckNullOrEmptyImage(StoreImage))
                {

                    Guid licenseCodeGuid = Guid.Parse(LicenseCode);
                    var licenseResult = storeLicenseDataStore.StoreLicenseExists(licenseCodeGuid);




                    if (licenseResult)
                    {

                        StoreType value;
                        Enum.TryParse(StoreTypeSelected, out value);

                        var newStoreRegister = new Store()
                        {
                            StoreId = Guid.NewGuid(),
                            StoreName = StoreName,
                            UserId = App.LogUser.UserId,
                            StoreImage = ImgArray,
                            StoreType = value,
                            StoreRegisterLicenseId = licenseCodeGuid

                        };

                        List<WorkHour> workHours = new List<WorkHour>();

                        foreach (var item in WorkHourPresenters)
                        {
                            var workHour = new WorkHour()
                            {
                                CloseTime = Convert.ToDateTime(item.Close.ToString()),
                                Day = item.Day,
                                OpenTime = Convert.ToDateTime(item.Open.ToString()),
                                WorkHourId = Guid.NewGuid(),
                                StoreId = newStoreRegister.StoreId
                            };

                            workHours.Add(workHour);
                        }

                        newStoreRegister.WorkHours = workHours;

                        var storeaddedResult = await StoreDataStore.AddItemAsync(newStoreRegister);

                        if (storeaddedResult)
                        {
                            await Shell.Current.DisplayAlert("Notification", "Store Added", "OK");
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Notification", "License Code Incorrect", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Some fields are empty", "OK");
                }

            });

            LogOutCommand = new Command(() =>
            {

                Shell.Current.GoToAsync("../LoginRoute");

            });

            GoRegisterCardCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("RegisterCardRoute");


            });

            GoGetLicenseCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("StoreLicenseRoute");


            });

            GoCheckYourStores = new Command(async () => 
            {
                await Shell.Current.GoToAsync($"{YourStoresPage.Route}");
            });
        }

        void OpenBrowser(string url)
        {
            Device.OpenUri(new Uri(url));
        }

        byte[] ConvertToByteArray(Stream value)
        {

            byte[] imageArray = null;

            using (MemoryStream memory = new MemoryStream())
            {

                Stream stream = value;
                stream.CopyTo(memory);
                imageArray = memory.ToArray();
            }

            return imageArray;
        }



    }
}
