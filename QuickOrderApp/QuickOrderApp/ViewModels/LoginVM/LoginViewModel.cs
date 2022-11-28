using Library.DTO;
using Library.Helpers;
using Library.Models;
using Library.SolutionUtilities.ValidatorComponents;
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
        private bool isloading;
        private Validator passwordValidator;
        private string phone;
        private Validator phoneValidator;
        private IPopupNavigation popupNavigation;
        private Validator usernameValidator;

        public void InitialDataTest()
        {
            Email = "est.juanpablotorres@gmail.com";

            Address = "Villalba";

            Phone = "787-111-1111";

            Fullname = "Juan Pablo Torres";

            GenderSelected = Gender.Male.ToString();

            Username = "jp94";

            Password = "1234";

            ConfirmPassword = "1234";
        }

        public LoginViewModel(INavigation _navigation)
        {
            Navigation = _navigation;

            InitialDataTest();

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
                IsLoading = true;

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    //Verifica si el telefono tiene acceso a internet
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        var apiResponse = await userDataStore.LoginCredential(Username, Password);

                        if (apiResponse.IsValid)
                        {
                            if (apiResponse.Status == ResponseStatus.Success)
                            {
                                await App.ComunicationService.Connect();

                                App.TokenDto = apiResponse.Data;

                                var loginresult = App.TokenDto.UserDetail;

                                if (!loginresult.IsValidUser)
                                {
                                    App.LogUser = loginresult;

                                    await popupNavigation.PushAsync(new ValidateEmailCode());

                                    IsLoading = false;
                                }
                                else
                                {
                                    App.LogUser = loginresult;

                                    bool hasPaymentCard = App.LogUser.PaymentCards.Count() > 0 ? true : false;

                                    //Verfico si hay tarjetas registradas con el usuario
                                    if (hasPaymentCard)
                                    {
                                        var data = App.LogUser.PaymentCards;

                                        var card = new List<PaymentCard>(data);

                                        var userCardTokenId = await stripeServiceDS.GetCustomerCardId(App.LogUser.StripeUserId, card[0].StripeCardId);

                                        App.CardPaymentToken.CardTokenId = userCardTokenId;
                                    }

                                    if (App.ComunicationService.hubConnection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
                                    {
                                        App.UsersConnected = new UsersConnected()
                                        {
                                            HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                            UserID = App.LogUser.ID,
                                            IsDisable = false,
                                            ConnecteDate = DateTime.Now
                                        };

                                        var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                                        var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
                                    }

                                    App.Current.MainPage = new AppShell();
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login...!", "OK");
                            }
                        }
                        else
                        {
                            IsLoading = false;

                            await App.Current.MainPage.DisplayAlert("Notification", apiResponse.Message, "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notification", "No internet access.", "OK");
                    }
                }
                else
                {
                    IsLoading = false;

                    await App.Current.MainPage.DisplayAlert("Notification", "Empty Credentials...!", "OK");
                }
            });

            LoginEmployeeCommand = new Command(async () =>
            {
                IsLoading = true;

                //if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                //{
                //    //var loginresult = userDataStore.CheckUserCredential(Username, Password);
                //    App.TokenDto = userDataStore.LoginCredential(Username, Password);

                //    var loginresult = App.TokenDto.UserDetail;

                //    if (loginresult != null)
                //    {
                //        Task.Run(async () =>
                //        {
                //            await App.ComunicationService.Connect();
                //        }).Wait();

                //        var userEmployees = await EmployeeDataStore.GetUserEmployees(loginresult.ID.ToString());

                //        App.LogUser = loginresult;

                //        if (!String.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId))
                //        {
                //            App.UsersConnected = new UsersConnected()
                //            {
                //                HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                //                UserID = App.LogUser.ID,
                //                IsDisable = false,
                //                ConnecteDate = DateTime.Now
                //            };

                //            var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                //            var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
                //        }

                //        //App.UsersConnected = new UsersConnected()
                //        //{
                //        //    HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                //        //    UserID = App.LogUser.UserId,
                //        //    IsDisable = false,
                //        //    ConnecteDate = DateTime.Now
                //        //};
                //        //var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                //        //var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);

                //        App.Current.MainPage = new EmployeeShell();

                //        IsLoading = false;
                //    }
                //    else
                //    {
                //        await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login.", "OK");
                //    }
                //}
                //else
                //{
                //    await App.Current.MainPage.DisplayAlert("Notification", "Empty inputs.", "OK");
                //}
            });

            RegisterCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });

            DoneCommand = new Command(async () =>
            {
                SetValidatorValues();

                Validator.PropertiesToValid.AddPropertyIfNotExits(ConfirmAndPasswordValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(UsernameValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(PasswordValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(PhoneValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(AddressValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(ConfirmPasswordValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(EmailPatternValidator);

                Validator.PropertiesToValid.AddPropertyIfNotExits(GenderValidator);

                bool _formIsValid = Validator.PropertiesToValid.AllValidatorPassRules();

                Validator.PropertiesToValid.Clear();

                //Verficamos que las propiedades esten de con la informacion correcta y llenas.
                if (_formIsValid)
                {
                    //Verificar si usuario tiene el email realizarlo en el api.

                    //Si el username y password existen tendra que reinsertar esa informacion

                    var _apiCredentialRequestReponse = await userDataStore.CheckIfUsernameAndPasswordExist(Username, Password);

                    if (_apiCredentialRequestReponse.IsValid)
                    {
                        if (_apiCredentialRequestReponse.Status == ResponseStatus.Not_Found)
                        {
                            var userlogin = new Credential()
                            {
                                ID = Guid.NewGuid(),
                                IsConnected = false,
                                Password = Password,
                                Username = Username
                            };

                            Gender value;

                            Enum.TryParse(GenderSelected, out value);

                            var newUser = new AppUser()
                            {
                                ID = Guid.NewGuid(),
                                Email = Email,
                                Name = Fullname,
                                LoginId = userlogin.ID,
                                Phone = Phone,
                                Address = Address,
                                Gender = value,
                                UserLogin = userlogin,
                            };

                            userlogin.UserId = newUser.ID;

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

                                if (!string.IsNullOrEmpty(customertokenId))
                                {
                                    await App.ComunicationService.Connect();

                                    newUser.StripeUserId = customertokenId;

                                    var _apiRequestAddUserResponse = await userDataStore.AddItemAsync(newUser);

                                    if (_apiRequestAddUserResponse.IsValid)
                                    {
                                        var _apiCredentialRequestResponse = await userDataStore.LoginCredential(Username, Password);

                                        App.LogUser = App.TokenDto.UserDetail;

                                        if (App.ComunicationService.hubConnection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
                                        {
                                            App.UsersConnected = new UsersConnected()
                                            {
                                                HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                                UserID = App.LogUser.ID,
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
                                        catch (Exception ex)
                                        {
                                            await App.Current.MainPage.DisplayAlert("Notification", ex.Message, "OK");
                                        }

                                        await popupNavigation.PushAsync(new RegisterValidationEmail());
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", e.Message, "OK");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Notification", _apiCredentialRequestReponse.Message, "OK");
                        }
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

        public ICommand ConfirmCodeCommand { get; set; }

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

        public ICommand DoneCommand { get; set; }

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

        public List<string> Genders { get; set; }

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

        public ICommand GoForgotPasswordCommand { get; set; }

        public bool IsLoading
        {
            get { return isloading; }
            set
            {
                isloading = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand LoginEmployeeCommand { get; set; }

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

        public ICommand RegisterCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }

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

        private void SetValidatorValues()
        {
            UsernameValidator = ValidatorRules.EmptyOrNullValueRule(Username);

            FullNameValidator = ValidatorRules.EmptyOrNullValueRule(Fullname);

            PhoneValidator = ValidatorRules.EmptyOrNullValueRule(Phone);

            AddressValidator = ValidatorRules.EmptyOrNullValueRule(Address);

            PasswordValidator = ValidatorRules.EmptyOrNullValueRule(Password);

            ConfirmPasswordValidator = ValidatorRules.EmptyOrNullValueRule(ConfirmPassword);

            EmailValidator = ValidatorRules.EmptyOrNullValueRule(Email);

            if (!EmailValidator.HasError)
            {
                EmailPatternValidator = ValidatorRules.EmptyOrNullValueRule(Email);
            }

            GenderValidator = ValidatorRules.EmptyOrNullValueRule(GenderSelected);

            ConfirmAndPasswordValidator = ValidatorRules.PasswordAndConfirmPasswordEquals(Password, ConfirmPassword);
        }

        private void ValidatorsInitializer()
        {
            UsernameValidator = new Validator();

            PhoneValidator = new Validator();

            EmailValidator = new Validator();

            AddressValidator = new Validator();

            PasswordValidator = new Validator();

            ConfirmPasswordValidator = new Validator();

            GenderValidator = new Validator();

            ConfirmAndPasswordValidator = new Validator();

            EmailPatternValidator = new Validator();
        }
    }
}