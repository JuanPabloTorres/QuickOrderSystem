using Library.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class RegisterStoreViewModel : BaseViewModel
    {
        private ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();

        private byte[] ImgArray;

        private string licenceCode;

        private Validator licenseCodeValidator;

        private Validator pkeyStripeValidator;

        private Validator selectedTypeValidator;

        private Validator SkStripeValidator;

        private string storeDescription;

        private Validator storeDescriptionValidator;

        private ImageSource storeImage;

        private string storename;

        private Validator storeNameValidator;

        private string storeTypeSelected;

        private string stripepublickey;

        private string stripesecretkey;

        public RegisterStoreViewModel ()
        {
            ValidatorInitializer();

            StoreImage = ImageSource.FromFile("imgPlaceholder.jpg");

            StoreTypes = new List<string>(Enum.GetNames(typeof(StoreType)).ToList());

            #region WorkHourPresenter Initialize

            WorkHourPresenters = new ObservableCollection<WorkHourPresenter>();

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Monday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Tuesday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Wednesday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Thursday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Friday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Saturday.ToString()));

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Sunday.ToString()));

            #endregion WorkHourPresenter Initialize

            PickPhotoCommand = new Command(async () =>
            {
                //Stream stream = await DependencyService.Get<IPickPhotoService>().GetImageStreamAsync();
                //if (stream != null)
                //{
                //    ImgArray = ConvertToByteArray(stream);
                //    StoreImage = ImageSource.FromStream(() => stream);
                //}

                try
                {
                    await CrossMedia.Current.Initialize();

                    files.Clear();

                    if( !CrossMedia.Current.IsPickPhotoSupported )
                    {
                        await Shell.Current.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");

                        return;
                    }

                    var mediOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Full,
                    };

                    var selectedImgFile = await CrossMedia.Current.PickPhotoAsync(mediOptions);

                    if( selectedImgFile == null )
                        return;

                    ImgArray = ConvertToByteArray(selectedImgFile.GetStream());

                    StoreImage = ImageSource.FromStream(() => selectedImgFile.GetStream());
                }
                catch( Exception e )
                {
                    Console.WriteLine(e);
                }
            });

            CompleteRegisterCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if( tokenExpManger.IsExpired() )
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {
                    List<string> valuesToCheck = new List<string>();

                    valuesToCheck.Add(StoreName);

                    valuesToCheck.Add(LicenseCode);

                    valuesToCheck.Add(StripeSecretKey);

                    valuesToCheck.Add(StripePublicKey);

                    StoreNameValidator = ValidatorRules.EmptyOrNullValueRule(StoreName);

                    StoreDescriptionValidator = ValidatorRules.EmptyOrNullValueRule(StoreDescription);

                    LicenseCodeValidator = ValidatorRules.EmptyOrNullValueRule(LicenseCode);

                    SkStripeValidator = ValidatorRules.EmptyOrNullValueRule(StripeSecretKey);

                    PKeyStripeValidator = ValidatorRules.EmptyOrNullValueRule(StripePublicKey);

                    if( !StoreNameValidator.HasError && !StoreDescriptionValidator.HasError && !LicenseCodeValidator.HasError && !SkStripeValidator.HasError && !PKeyStripeValidator.HasError && GlobalValidator.CheckNullOrEmptyImage(StoreImage) )
                    {
                        bool isSKKeyCorrect = StripeSecretKey.StartsWith("sk");

                        bool isPBkeyCorrect = StripePublicKey.StartsWith("pk");

                        if( isSKKeyCorrect || isSKKeyCorrect )
                        {
                            Guid licenseCodeGuid = Guid.Parse(LicenseCode);

                            var licenseResult = storeLicenseDataStore.StoreLicenseExists(licenseCodeGuid);

                            if( licenseResult )
                            {
                                var licenseIsInUsed = await storeLicenseDataStore.IsLicenseInUsed(LicenseCode);

                                if( !licenseIsInUsed )
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
                                        StoreRegisterLicenseId = licenseCodeGuid,
                                        StoreDescription = StoreDescription,
                                        SKKey = StripeSecretKey,
                                        PBKey = StripePublicKey,
                                    };

                                    List<WorkHour> workHours = new List<WorkHour>();

                                    foreach( var item in WorkHourPresenters )
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

                                    if( storeaddedResult )
                                    {
                                        var licenceUpdateResult = await storeLicenseDataStore.UpdateLicenceInCode(licenseCodeGuid);

                                        if( licenceUpdateResult )
                                        {
                                            await Shell.Current.DisplayAlert("Notification", "Store Added...!", "OK");
                                        }
                                    }
                                }
                                else
                                {
                                    await Shell.Current.DisplayAlert("Notification", "This License is in used.", "OK");
                                }
                            }
                            else
                            {
                                await Shell.Current.DisplayAlert("Notification", "License Code Incorrect", "OK");
                            }
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Notification", "Srtipe Secret Key or Public Key are set in incorrect entry check again.", "OK");
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Notification", "Some fields are empty", "OK");
                    }
                }
            });
        }

        public ICommand CompleteRegisterCommand { get; set; }

        public string LicenseCode
        {
            get { return licenceCode; }
            set
            {
                licenceCode = value;

                OnPropertyChanged();
            }
        }

        public Validator LicenseCodeValidator
        {
            get { return licenseCodeValidator; }
            set
            {
                licenseCodeValidator = value;

                OnPropertyChanged();
            }
        }

        public ICommand PickPhotoCommand { get; set; }

        public Validator PKeyStripeValidator
        {
            get { return pkeyStripeValidator; }
            set
            {
                pkeyStripeValidator = value;

                OnPropertyChanged();
            }
        }

        public Validator SelectedTypeValidator
        {
            get { return selectedTypeValidator; }
            set
            {
                selectedTypeValidator = value;

                OnPropertyChanged();
            }
        }

        public Validator SKStripeValidator
        {
            get { return SkStripeValidator; }
            set
            {
                SkStripeValidator = value;

                OnPropertyChanged();
            }
        }

        public string StoreDescription
        {
            get { return storeDescription; }
            set
            {
                storeDescription = value;

                OnPropertyChanged();
            }
        }

        public Validator StoreDescriptionValidator
        {
            get { return storeDescriptionValidator; }
            set
            {
                storeDescriptionValidator = value;

                OnPropertyChanged();
            }
        }

        public ImageSource StoreImage
        {
            get { return storeImage; }
            set
            {
                storeImage = value;

                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get { return storename; }
            set
            {
                if( storename != value )
                {
                    storename = value;

                    OnPropertyChanged();
                }
            }
        }

        public Validator StoreNameValidator
        {
            get { return storeNameValidator; }
            set
            {
                storeNameValidator = value;

                OnPropertyChanged();
            }
        }

        public List<string> StoreTypes { get; set; }

        public string StoreTypeSelected
        {
            get { return storeTypeSelected; }
            set
            {
                storeTypeSelected = value;

                OnPropertyChanged();
            }
        }

        public string StripePublicKey
        {
            get { return stripepublickey; }
            set
            {
                stripepublickey = value;

                OnPropertyChanged();
            }
        }

        public string StripeSecretKey
        {
            get { return stripesecretkey; }
            set
            {
                stripesecretkey = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WorkHourPresenter> WorkHourPresenters { get; set; }

        private byte[] ConvertToByteArray (Stream value)
        {
            byte[] imageArray = null;

            using( MemoryStream memory = new MemoryStream() )
            {
                Stream stream = value;

                stream.CopyTo(memory);

                imageArray = memory.ToArray();
            }

            return imageArray;
        }

        private void ValidatorInitializer ()
        {
            StoreNameValidator = new Validator();

            SelectedTypeValidator = new Validator();

            PKeyStripeValidator = new Validator();

            SKStripeValidator = new Validator();

            LicenseCodeValidator = new Validator();

            StoreDescriptionValidator = new Validator();
        }
    }
}