using Library.DTO;
using Library.Models;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.Views.Login;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation Navigation;

        #region Commandos

        public ICommand ConfirmCodeCommand { get; set; }
        public ICommand DoneCommand { get; set; }
        public ICommand GoForgotPasswordCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LoginEmployeeCommand { get; set; }

        public ICommand RegisterCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }

        #endregion Commandos

        private bool isloading;

        private IPopupNavigation popupNavigation;

        public LoginViewModel (INavigation _navigation)
        {
            Navigation = _navigation;

            popupNavigation = PopupNavigation.Instance;

            Task.Run(async () =>
            {
                Username = await SecureStorage.GetAsync("username");

                Password = await SecureStorage.GetAsync("password");
            });

            ValidatorsInitializer();

            //App.ComunicationService = new ComunicationService();

            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());

            IsLoading = false;

            LoginCommand = new Command(async () =>
            {
                //var currentuserID = Xamarin.Forms.Application.Current.Properties["loginId "].ToString();
                IsLoading = true;

                if( !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) )
                {
                    //Verifica si el telefono tiene acceso a internet

                    if( Connectivity.NetworkAccess == NetworkAccess.Internet )
                    {
                        //Obtine los credenciales del usuario
                        //var loginresult = userDataStore.CheckUserCredential(Username, Password);
                        //Obtiene el token de acceso
                        App.TokenDto = userDataStore.LoginCredential(Username, Password);

                        var loginresult = App.TokenDto.UserDetail;

                        //Verifica si el resultado del login no es vacio.
                        if( loginresult != null )
                        {
                            if( !loginresult.IsValidUser )
                            {
                                App.LogUser = loginresult;

                                await popupNavigation.PushAsync(new ValidateEmailCode());

                                IsLoading = false;
                            }
                            else
                            {
                                Task.Run(async () =>
                                {
                                    await App.ComunicationService.Connect();
                                }).Wait();

                                App.LogUser = loginresult;

                                bool hasPaymentCard = App.LogUser.PaymentCards.Count() > 0 ? true : false;

                                //Verfico si hay tarjetas registradas con el usuario
                                if( hasPaymentCard )
                                {
                                    var data = App.LogUser.PaymentCards;

                                    var card = new List<PaymentCard>(data);

                                    var userCardTokenId = await stripeServiceDS.GetCustomerCardId(App.LogUser.StripeUserId, card[0].StripeCardId);

                                    App.CardPaymentToken.CardTokenId = userCardTokenId;
                                }

                                if( !String.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId) )
                                {
                                    App.UsersConnected = new UsersConnected()
                                    {
                                        HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                        UserID = App.LogUser.UserId,
                                        IsDisable = false,
                                        ConnecteDate = DateTime.Now
                                    };

                                    var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                                    var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
                                }

                                App.Current.MainPage = new AppShell();

                                IsLoading = false;
                            }
                        }
                        else
                        {
                            IsLoading = false;

                            await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login...!", "OK");
                        }
                    }
                }
                else
                {
                    IsLoading = false;

                    await App.Current.MainPage.DisplayAlert("Notification", "Empty values...!", "OK");
                }
            });

            LoginEmployeeCommand = new Command(async () =>
            {
                IsLoading = true;

                if( !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) )
                {
                    //var loginresult = userDataStore.CheckUserCredential(Username, Password);
                    App.TokenDto = userDataStore.LoginCredential(Username, Password);

                    var loginresult = App.TokenDto.UserDetail;

                    if( loginresult != null )
                    {
                        Task.Run(async () =>
                        {
                            await App.ComunicationService.Connect();
                        }).Wait();

                        var userEmployees = await EmployeeDataStore.GetUserEmployees(loginresult.UserId.ToString());

                        App.LogUser = loginresult;

                        if( !String.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId) )
                        {
                            App.UsersConnected = new UsersConnected()
                            {
                                HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                UserID = App.LogUser.UserId,
                                IsDisable = false,
                                ConnecteDate = DateTime.Now
                            };

                            var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                            var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
                        }

                        //App.UsersConnected = new UsersConnected()
                        //{
                        //    HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                        //    UserID = App.LogUser.UserId,
                        //    IsDisable = false,
                        //    ConnecteDate = DateTime.Now
                        //};
                        //var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                        //var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);

                        App.Current.MainPage = new EmployeeShell();

                        IsLoading = false;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login.", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Empty inputs.", "OK");
                }
            });

            RegisterCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });

            DoneCommand = new Command(async () =>
            {
                SetValidatorValues();

                //Verficamos que las propiedades esten de con la informacion correcta y llenas.
                if( !UsernameValidator.HasError && !FullNameValidator.HasError && !PhoneValidator.HasError && !AddressValidator.HasError && !PasswordValidator.HasError && !ConfirmPasswordValidator.HasError && !EmailValidator.HasError && !GenderValidator.HasError && !EmailPatternValidator.HasError )
                {
                    if( !await userDataStore.EmailExist(Email) )
                    {
                        ConfirmAndPasswordValidator = ValidatorRules.PasswordAndConfirmPasswordEquals(Password, ConfirmPassword);
                        //Verificamos que el password y el confirmpassword matcheen
                        if( !ConfirmAndPasswordValidator.HasError )
                        {
                            //Si el username y password existen tendra que reinsertar esa informacion
                            if( !await userDataStore.CheckIfUsernameAndPasswordExist(Username, Password) )
                            {
                                var userlogin = new Login()
                                {
                                    LoginId = Guid.NewGuid(),
                                    IsConnected = false,
                                    Password = Password,
                                    Username = Username
                                };

                                Gender value;

                                Enum.TryParse(GenderSelected, out value);

                                var newUser = new User()
                                {
                                    UserId = Guid.NewGuid(),
                                    Email = Email,
                                    Name = Fullname,
                                    LoginId = userlogin.LoginId,
                                    Phone = Phone,
                                    Address = Address,
                                    Gender = value,
                                    UserLogin = userlogin,
                                };

                                userlogin.UserId = newUser.UserId;

                                try
                                {
                                    var optionsCustomers = new UserDTO
                                    {
                                        Name = Fullname,
                                        Email = Email,
                                        Phone = Phone,
                                        Address = Address
                                    };

                                    //Create Customer
                                    var customertokenId = await stripeServiceDS.CreateStripeCustomer(optionsCustomers);

                                    if( !string.IsNullOrEmpty(customertokenId) )
                                    {
                                        Task.Run(async () =>
                                        {
                                            await App.ComunicationService.Connect();
                                        }).Wait();

                                        newUser.StripeUserId = customertokenId;

                                        var result = await userDataStore.AddItemAsync(newUser);

                                        //var credentialsResult = userDataStore.CheckUserCredential(Username, Password);
                                        App.TokenDto = userDataStore.LoginCredential(Username, Password);

                                        if( result )
                                        {
                                            App.LogUser = App.TokenDto.UserDetail;

                                            if( !String.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId) )
                                            {
                                                App.UsersConnected = new UsersConnected()
                                                {
                                                    HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                                    UserID = App.LogUser.UserId,
                                                    IsDisable = false,
                                                    ConnecteDate = DateTime.Now
                                                };

                                                //var oldConnectionModify = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                                                var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
                                            }

                                            try
                                            {
                                                await SecureStorage.SetAsync("username", Username);

                                                await SecureStorage.SetAsync("password", Password);
                                            }
                                            catch( Exception ex )
                                            {
                                                // Possible that device doesn't support secure storage on device.
                                            }

                                            await popupNavigation.PushAsync(new RegisterValidationEmail());
                                        }
                                    }
                                }
                                catch( Exception e )
                                {
                                    await App.Current.MainPage.DisplayAlert("Notification", e.Message, "OK");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "Username or password exist try to change to other one.", "OK");
                            }
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notification", "Email is in used.", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Some error ocurred check the information.", "OK");
                }
            });

            GoForgotPasswordCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
            });
        }

        public List<string> Genders { get; set; }

        public bool IsLoading
        {
            get { return isloading; }
            set
            {
                isloading = value;
                OnPropertyChanged();
            }
        }

        private void SetValidatorValues ()
        {
            UsernameValidator = ValidatorRules.EmptyOrNullValueRule(Username);

            FullNameValidator = ValidatorRules.EmptyOrNullValueRule(Fullname);

            PhoneValidator = ValidatorRules.EmptyOrNullValueRule(Phone);

            AddressValidator = ValidatorRules.EmptyOrNullValueRule(Address);

            PasswordValidator = ValidatorRules.EmptyOrNullValueRule(Password);

            ConfirmPasswordValidator = ValidatorRules.EmptyOrNullValueRule(ConfirmPassword);

            EmailValidator = ValidatorRules.EmptyOrNullValueRule(Email);

            if( !EmailValidator.HasError )
            {
                EmailPatternValidator = ValidatorRules.EmptyOrNullValueRule(Email);
            }
            GenderValidator = ValidatorRules.EmptyOrNullValueRule(GenderSelected);
        }

        private void ValidatorsInitializer ()
        {
            #region Instaciando Validadores

            UsernameValidator = new Validator();

            PhoneValidator = new Validator();

            EmailValidator = new Validator();

            AddressValidator = new Validator();

            PasswordValidator = new Validator();

            ConfirmPasswordValidator = new Validator();

            GenderValidator = new Validator();

            ConfirmAndPasswordValidator = new Validator();

            EmailPatternValidator = new Validator();

            #endregion Instaciando Validadores
        }

        #region Properties

        private string _confirmpassword;

        private string _email;

        private string _fullname;

        private string _password;

        private string _username;

        private Validator addressValidator;

        private string adress;

        private Validator confirmAndpasswordValidator;

        private Validator confirmpasswordValidator;

        private Validator emailPatternValidator;

        private Validator emailValidator;

        private Validator fullnameValidator;

        private string genderselected;

        private Validator genderValidator;

        private Validator passwordValidator;

        private string phone;

        private Validator phoneValidator;

        private Validator usernameValidator;

        public string Address
        {
            get { return adress; }
            set
            {
                adress = value;

                OnPropertyChanged();
            }
        }

        public Validator AddressValidator
        {
            get { return addressValidator; }
            set { addressValidator = value; }
        }

        public Validator ConfirmAndPasswordValidator
        {
            get { return confirmAndpasswordValidator; }
            set
            {
                confirmAndpasswordValidator = value;

                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set
            {
                _confirmpassword = value;

                OnPropertyChanged();
            }
        }

        public Validator ConfirmPasswordValidator
        {
            get { return confirmpasswordValidator; }
            set
            {
                confirmpasswordValidator = value;

                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if( _email != value )
                {
                    _email = value;

                    OnPropertyChanged();
                }
            }
        }

        public Validator EmailPatternValidator
        {
            get { return emailPatternValidator; }
            set
            {
                emailPatternValidator = value;

                OnPropertyChanged();
            }
        }

        public Validator EmailValidator
        {
            get { return emailValidator; }
            set
            {
                emailValidator = value;

                OnPropertyChanged();
            }
        }

        public string Fullname
        {
            get { return _fullname; }
            set
            {
                _fullname = value;

                OnPropertyChanged();
            }
        }

        public Validator FullNameValidator
        {
            get { return fullnameValidator; }
            set
            {
                fullnameValidator = value;

                OnPropertyChanged();
            }
        }

        public string GenderSelected
        {
            get { return genderselected; }
            set
            {
                genderselected = value;

                OnPropertyChanged();
            }
        }

        public Validator GenderValidator
        {
            get { return genderValidator; }
            set
            {
                genderValidator = value;

                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if( _password != value )
                {
                    _password = value;

                    OnPropertyChanged();
                }
            }
        }

        public Validator PasswordValidator
        {
            get { return passwordValidator; }
            set
            {
                passwordValidator = value;

                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;

                OnPropertyChanged();
            }
        }

        public Validator PhoneValidator
        {
            get { return phoneValidator; }
            set
            {
                phoneValidator = value;

                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;

                OnPropertyChanged();
            }
        }

        //private bool _isShowCancel;
        //public bool IsShowCancel
        //{
        //    get { return _isShowCancel; }
        //    set { /*SetPropertyValue(ref _isShowCancel, value);*/ }
        //}
        public Validator UsernameValidator
        {
            get { return usernameValidator; }
            set
            {
                usernameValidator = value;

                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}