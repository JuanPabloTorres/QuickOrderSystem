﻿using FFImageLoading.Concurrency;
using Library.DTO;
using Library.Models;
using Library.Services;
using Library.Services.Interface;
using QuickOrderApp.LoginBuilder;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Static;
using QuickOrderApp.Views.Login;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Stripe;
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



        IPopupNavigation popupNavigation;
        public LoginViewModel()
        {
            popupNavigation = PopupNavigation.Instance;

            Task.Run(async () =>
            {
                Username = await SecureStorage.GetAsync("username");
                Password = await SecureStorage.GetAsync("password");
            });

            ValidatorsInitializer();


            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());



            LoginCommand = new Command(async () =>
            {
                IsLoading = true;

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    //Verifica si el telefono tiene acceso a internet




                    LoginTokenDirector LoginDirector = new LoginTokenDirector();

                    ConcreteLoginTokenBuilder userlog = new ConcreteLoginTokenBuilder();

                    var result = LoginDirector.MakeLogin(userlog, Username, Password);

                    if (result != null)
                    {
                        App.TokenDto = result;

                        userlog.GoQuickOrderHome();
                    }

                    IsLoading = false;


                }
                else
                {
                    IsLoading = false;

                    await App.Current.MainPage.DisplayAlert("Notification", "Empty values...!", "OK");

                }

            });




            LoginEmployeeCommand = new Command(async () =>
            {
                //var currentuserID = Xamarin.Forms.Application.Current.Properties["loginId "].ToString();
                IsLoading = true;

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    //Verifica si el telefono tiene acceso a internet




                    LoginTokenDirector LoginDirector = new LoginTokenDirector();

                    ConcreteLoginTokenBuilder userlog = new ConcreteLoginTokenBuilder();

                    var result = LoginDirector.MakeLogin(userlog, Username, Password);

                    if (result != null)
                    {
                        App.TokenDto = result;

                        userlog.GoEmployeeHome();
                    }

                    IsLoading = false;


                }
                else
                {
                    IsLoading = false;
                    await App.Current.MainPage.DisplayAlert("Notification", "Empty values...!", "OK");

                }

            });

            RegisterCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"//{LoginPage.Route}/{ RegisterPage.Route}");

                //await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());

            });



            DoneCommand = new Command(async () =>
            {

                SetValidatorValues();

                //Verficamos que las propiedades esten de con la informacion correcta y llenas.
                if (!UsernameValidator.HasError && !FullNameValidator.HasError && !PhoneValidator.HasError && !AddressValidator.HasError && !PasswordValidator.HasError && !ConfirmPasswordValidator.HasError && !EmailValidator.HasError && !GenderValidator.HasError && !EmailPatternValidator.HasError)
                {

                    if (!await userDataStore.EmailExist(Email))
                    {
                        ConfirmAndPasswordValidator = ValidatorRules.PasswordAndConfirmPasswordEquals(Password, ConfirmPassword);

                        //Verificamos que el password y el confirmpassword matcheen
                        if (!ConfirmAndPasswordValidator.HasError)
                        {
                            //Si el username y password existen tendra que reinsertar esa informacion
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


                                    if (!string.IsNullOrEmpty(customertokenId))
                                    {
                                        Task.Run(async () =>
                                        {

                                            await App.ComunicationService.Connect();

                                        }).Wait();

                                        newUser.StripeUserId = customertokenId;
                                        var result = await userDataStore.AddItemAsync(newUser);

                                        //var credentialsResult = userDataStore.CheckUserCredential(Username, Password);
                                        App.TokenDto = userDataStore.LoginCredential(Username, Password);

                                        if (result)
                                        {


                                            App.LogUser = App.TokenDto.UserDetail;



                                            try
                                            {
                                                await SecureStorage.SetAsync("username", Username);
                                                await SecureStorage.SetAsync("password", Password);
                                            }
                                            catch (Exception ex)
                                            {
                                                // Possible that device doesn't support secure storage on device.
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
                await Shell.Current.GoToAsync($"//{LoginPage.Route}/{ ForgotPasswordPage.Route}");
                //await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());

            });


        }

        void ValidatorsInitializer()
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
            #endregion
        }

        void SetValidatorValues()
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
            get { return emailValidator; }
            set
            {
                emailValidator = value;
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
            get { return confirmpasswordValidator; }
            set
            {
                confirmpasswordValidator = value;
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
            set
            {
                genderValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator confirmAndpasswordValidator;

        public Validator ConfirmAndPasswordValidator
        {
            get { return confirmAndpasswordValidator; }
            set
            {
                confirmAndpasswordValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator emailPatternValidator;

        public Validator EmailPatternValidator
        {
            get { return emailPatternValidator; }
            set
            {
                emailPatternValidator = value;
                OnPropertyChanged();
            }
        }



        #endregion







    }
}
