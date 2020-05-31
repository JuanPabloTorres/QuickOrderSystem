using Library.Models;
using QuickOrderApp.Views.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private readonly INavigation Navigation;

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand DoneCommand { get; set; }
       
        public LoginViewModel(INavigation _navigation)
        {
            Navigation = _navigation;

            Username = "pola94";
            Password = "123";

            LoginCommand = new Command(async() => {

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {

                var loginresult =  userDataStore.CheckUserCredential(Username, Password);

                if (loginresult != null)
                {
                        App.LogUser = loginresult;
                App.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("HomePageRoute");

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

            DoneCommand = new Command(async() => 
            {

                if (CheckNullOrEmptyProperties(Fullname,Email,Username,Password,ConfirmPassword))
                {

                    var userlogin = new Login()
                    {
                        LoginId = Guid.NewGuid(),
                        IsConnected = false,
                        Password = Password,
                        Username = Username
                    };

                    var newUser = new User()
                    {
                        UserId = Guid.NewGuid(),
                        Email = Email,
                        Name = Fullname,                      
                        LoginId = userlogin.LoginId,
                       
                        UserLogin = userlogin,
                    };


                    var result = await userDataStore.AddItemAsync(newUser);

                    if (result)
                    {
                    await App.Current.MainPage.DisplayAlert("Notification", "Register succsefully", "OK");

                        App.Current.MainPage = new AppShell();
                        await Shell.Current.GoToAsync("HomePageRoute");

                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Error", "OK");
                }
            

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
               if(_password != value)
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

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set
            {
                if (_confirmpassword != value)
                {
                    _confirmpassword = value;
                    OnPropertyChanged();
                }
            }
        }



        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set { /*SetPropertyValue(ref _isShowCancel, value);*/ }
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

        bool CheckNullOrEmptyProperties(string fullname, string email, string username,string password, string confirmpassword)
        {

            if (!string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmpassword) && password == confirmpassword)
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
