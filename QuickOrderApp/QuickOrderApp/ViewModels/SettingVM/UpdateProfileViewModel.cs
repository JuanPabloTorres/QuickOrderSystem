using Library.DTO;
using Library.Helpers;
using Library.Models;
using Library.SolutionUtilities.ValidatorComponents;
using QuickOrderApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class UpdateProfileViewModel : BaseViewModel
    {
        private string _email;

        private string _fullname;

        private string _password;

        private Validator addressValidator;

        private string adress;

        private Validator emailPatternValidator;

        private Validator emailValidator;

        private Validator fullnameValidator;

        private string genderselected;

        private Validator genderValidator;

        private string phone;

        private Validator phoneValidator;

        public UpdateProfileViewModel ()
        {
            ValidatorInitializer();

            Genders = new List<string>(Enum.GetNames(typeof(Gender)).ToList());

            MessagingCenter.Subscribe<AppUser>(this, "UserInformation", (obj) =>
            {
                Fullname = obj.Name;

                Address = obj.Address;

                Phone = obj.Phone;

                Email = obj.Email;

                GenderSelected = obj.Gender.ToString();
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

        public Validator AddresValidator
        {
            get { return addressValidator; }
            set
            {
                addressValidator = value;

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
                if( _fullname != value )
                {
                    _fullname = value;

                    OnPropertyChanged();
                }
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

        public ICommand UpdateCommand => new Command(async () =>
                                                                                                                         {
                     TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                     if( tokenExpManger.IsExpired() )
                     {
                         await tokenExpManger.CloseSession();
                     }
                     else
                     {
                         SetValidatorValues(Fullname, Email, Phone, Address, GenderSelected);

                         bool isValidInformation = ValidatorChecker(FullNameValidator, PhoneValidator, AddresValidator, EmailValidator, EmailPatternValidator, GenderValidator);

                         if( isValidInformation )
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

                             if( result )
                             {
                                 var userUpdatedDTO = new UserDTO()
                                 {
                                     Name = userUpdated.Name,
                                     Address = userUpdated.Address,
                                     Phone = userUpdated.Phone,
                                     Email = userUpdated.Email,
                                     StripeCustomerId = App.LogUser.StripeUserId
                                 };

                                 var _stripeUpdateResult = await stripeServiceDS.UpdateStripeCustomer(userUpdatedDTO);

                                 if( _stripeUpdateResult )
                                 {
                                     App.LogUser = userUpdated;

                                     await Shell.Current.DisplayAlert("Notification", "User Updated...!", "OK");

                                    //await Shell.Current.GoToAsync(".../UpdateProfileRoute");
                                }
                             }
                         }
                     }
                 });
        private void SetValidatorValues (string _fullname, string _email, string _phone, string _address, string _gender)
        {
            FullNameValidator = ValidatorRules.EmptyOrNullValueRule(_fullname);

            EmailValidator = ValidatorRules.EmptyOrNullValueRule(_email);

            if( !EmailValidator.HasError )
            {
                EmailPatternValidator = ValidatorRules.EmailPatternRule(_email);
            }
            PhoneValidator = ValidatorRules.EmptyOrNullValueRule(_phone);

            AddresValidator = ValidatorRules.EmptyOrNullValueRule(_address);

            GenderValidator = ValidatorRules.EmptyOrNullValueRule(_gender);
        }

        private bool ValidatorChecker (Validator _fullnameValidator, Validator _phoneValidator, Validator _addresValidator, Validator _emailvalidator, Validator _emailPatternValidator, Validator _genderValidator)
        {
            if( !_fullnameValidator.HasError && !_phoneValidator.HasError && !_addresValidator.HasError && !_emailvalidator.HasError && !_emailPatternValidator.HasError && !_genderValidator.HasError )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ValidatorInitializer ()
        {
            FullNameValidator = new Validator();

            AddresValidator = new Validator();

            PhoneValidator = new Validator();

            EmailValidator = new Validator();

            GenderValidator = new Validator();

            EmailPatternValidator = new Validator();
        }
    }
}