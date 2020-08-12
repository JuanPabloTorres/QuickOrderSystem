using Library.Models;
using Library.Services;
using Library.Services.Interface;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public ICommand LoginCommand { get; set; }
        public ICommand LoginEmployeeCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        public ICommand GoForgotPasswordCommand { get; set; }

        public ICommand SendCodeCommand { get; set; }
        public ICommand ConfirmCodeCommand { get; set; }

        #endregion
        public List<string> Genders { get; set; }

        private bool isloading;

        public bool IsLoading
        {
            get { return isloading; }
            set
            {
                isloading = value;
                OnPropertyChanged();
            }
        }


        public LoginViewModel(INavigation _navigation)
        {
            Navigation = _navigation;

            Username = "Jose1";
            Password = "123";

            #region Instaciando Validadores
            UsernameValidator = new Validator();
            PhoneValidator = new Validator();
            EmailValidator = new Validator();
            AddressValidator = new Validator();
            PasswordValidator = new Validator();
            ConfirmPasswordValidator = new Validator();
            GenderValidator = new Validator();
            ConfirmAndPasswordValidator = new Validator();
            #endregion



            App.ComunicationService = new ComunicationService();


            IsLoading = false;

            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());

            LoginCommand = new Command(async () =>
            {

                IsLoading = true;
                List<string> capturecredentialValues = new List<string>();

                capturecredentialValues.Add(Username);
                capturecredentialValues.Add(Password);

                bool areValid = GlobalValidator.CheckNullOrEmptyPropertiesOfListValues(capturecredentialValues);

                if (areValid)
                {

                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        var loginresult = userDataStore.CheckUserCredential(Username, Password);

                        App.TokenDto = userDataStore.LoginCredential(Username, Password);
                        
                        if (loginresult != null)
                        {
                            App.LogUser = loginresult;



                            App.UsersConnected = new UsersConnected()
                            {
                                HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                                UserID = App.LogUser.UserId
                            };


                           var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);

                            App.Current.MainPage = new AppShell();

                            //await Shell.Current.GoToAsync("HomePageRoute");
                            IsLoading = false;
                            //await App.ComunicationService.SendMessage("Jose", "Hola Jose");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login.", "OK");

                        }

                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Empty inputs.", "OK");

                }

            });
            LoginEmployeeCommand = new Command(async () =>
            {

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {

                    var loginresult = userDataStore.CheckUserCredential(Username, Password);
                    App.TokenDto = userDataStore.LoginCredential(Username, Password);

                    if (loginresult != null)
                    {
                        var userEmployees = await EmployeeDataStore.GetUserEmployees(loginresult.UserId.ToString());
                        App.LogUser = loginresult;
                        App.Current.MainPage = new EmployeeShell();
                        //await Shell.Current.GoToAsync("EmployeeControlPanelRoute");

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
                UsernameValidator = ValidatorRules.EmptyOrNullValueRule(Username);
                FullNameValidator = ValidatorRules.EmptyOrNullValueRule(Fullname);
                PhoneValidator = ValidatorRules.EmptyOrNullValueRule(Phone);
                AddressValidator = ValidatorRules.EmptyOrNullValueRule(Address);
                PasswordValidator = ValidatorRules.EmptyOrNullValueRule(Password);
                ConfirmPasswordValidator = ValidatorRules.EmptyOrNullValueRule(ConfirmPassword);
                EmailValidator = ValidatorRules.EmailPatternRule(Email);
                GenderValidator = ValidatorRules.EmptyOrNullValueRule(GenderSelected);

                if (!UsernameValidator.HasError && !FullNameValidator.HasError && !PhoneValidator.HasError && !AddressValidator.HasError && !PasswordValidator.HasError && !ConfirmPasswordValidator.HasError && !EmailValidator.HasError && !GenderValidator.HasError )
                {

                    ConfirmAndPasswordValidator = ValidatorRules.PasswordAndConfirmPasswordEquals(Password, ConfirmPassword);
                    if (!ConfirmAndPasswordValidator.HasError)
                    {
                        if (!await userDataStore.CheckIfUsernameAndPasswordExist(Username, Password))
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

                            var result = await userDataStore.AddItemAsync(newUser);

                            var getCredential = userDataStore.CheckUserCredential(Username, Password);

                            if (result)
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "Register succsefully", "OK");
                                App.LogUser = getCredential;
                                App.Current.MainPage = new AppShell();
                                //await Shell.Current.GoToAsync("HomePageRoute");

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
                    await App.Current.MainPage.DisplayAlert("Notification", "Some fields are empty.", "OK");
                }
              


            });

            GoForgotPasswordCommand = new Command(async () =>
            {

                await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());

            });


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
               
                    _fullname = value;
                    OnPropertyChanged();
                
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




        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
               
                    _username = value;
                    OnPropertyChanged();
                
            }
        }

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set
            {
                
                    _confirmpassword = value;
                    OnPropertyChanged();
                
            }
        }



        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set { /*SetPropertyValue(ref _isShowCancel, value);*/ }
        }

        private Validator usernameValidator;

        public Validator UsernameValidator
        {
            get { return usernameValidator; }
            set
            {
                usernameValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator emailValidator;

        public Validator EmailValidator
        {
            get { return usernameValidator; }
            set
            {
                usernameValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator fullnameValidator;

        public Validator FullNameValidator
        {
            get { return fullnameValidator; }
            set
            {
                fullnameValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator phoneValidator;

        public Validator PhoneValidator
        {
            get { return phoneValidator; }
            set
            {
                phoneValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator passwordValidator;

        public Validator PasswordValidator
        {
            get { return passwordValidator; }
            set
            {
                passwordValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator confirmpasswordValidator;

        public Validator ConfirmPasswordValidator
        {
            get { return passwordValidator; }
            set
            {
                passwordValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator addressValidator;

        public Validator AddressValidator
        {
            get { return addressValidator; }
            set { addressValidator = value; }
        }

        private Validator genderValidator;

        public Validator GenderValidator
        {
            get { return genderValidator; }
            set { genderValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator confirmAndpasswordValidator;

        public Validator ConfirmAndPasswordValidator
        {
            get { return confirmAndpasswordValidator; }
            set { confirmAndpasswordValidator = value;
                OnPropertyChanged();
            }
        }




        #endregion


        #region Commands

        private ICommand _loginCommand;
        //public ICommand LoginCommand
        //{
        //    get { return _loginCommand = _loginCommand ?? new Command(LoginAction, CanLoginAction); }
        //}

        private ICommand _cancelLoginCommand;
        public ICommand CancelLoginCommand
        {
            get { return _cancelLoginCommand = _cancelLoginCommand ?? new Command(CancelLoginAction); }
        }

        private ICommand _forgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new Command(ForgotPasswordAction); }
        }

        private ICommand _newAccountCommand;
        public ICommand NewAccountCommand
        {
            get { return _newAccountCommand = _newAccountCommand ?? new Command(NewAccountAction); }
        }

        #endregion


        #region Methods

        bool CheckNullOrEmptyProperties(string fullname, string email, string username, string password, string confirmpassword, string phone, string adress, string gender)
        {

            if (!string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(adress) && !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmpassword) && password == confirmpassword)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        bool CheckEmailPatter(string emailValue)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailValue);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CanLoginAction()
        {
            //Perform your "Can Login?" logic here...

            if (string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.Password))
                return false;

            return true;
        }

        async void LoginAction()
        {
            IsBusy = true;

            //TODO - perform your login action + navigate to the next page

            //Simulate an API call to show busy/progress indicator            
            Task.Delay(20000).ContinueWith((t) => IsBusy = false);

            //Show the Cancel button after X seconds
            Task.Delay(5000).ContinueWith((t) => IsShowCancel = true);
        }

        void CancelLoginAction()
        {
            //TODO - perform cancellation logic

            IsBusy = false;
            IsShowCancel = false;
        }

        void ForgotPasswordAction()
        {
            //TODO - navigate to your forgotten password page
            //Navigation.PushAsync(XXX);
        }

        void NewAccountAction()
        {
            //TODO - navigate to your registration page
            //Navigation.PushAsync(XXX);
        }

        #endregion
    }
}
