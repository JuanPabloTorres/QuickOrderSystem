using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZXing;

namespace QuickOrderApp.Utilities.Static
{
    public static class ValidatorRules
    {

        public static Validator EmailPatternRule(string emailValue)
        {
            if (!String.IsNullOrEmpty(emailValue))
            {

                Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                Match match = regex.Match(emailValue);

                if (match.Success)
                {
                    var goodValidator = new Validator()
                    {
                        ErrorMessage = string.Empty,
                        HasError = false
                    };

                    return goodValidator;
                }
                else
                {
                    var emailValidator = new Validator()
                    {
                        ErrorMessage = "Email pattern is incorrect",
                        HasError = true
                    };

                    return emailValidator;
                }
            }
            else
            {
                var emptyAndnullValidator = new Validator()
                {
                    ErrorMessage = "Value is empty.",
                    HasError = true
                };

                return emptyAndnullValidator;
            }
        }

        public static Validator EmptyOrNullValueRule(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var goodValidator = new Validator()
                {
                    ErrorMessage =string.Empty,
                    HasError = false
                };

                return goodValidator;
            }
            else
            {
                var emptyAndnullValidator = new Validator()
                {
                    ErrorMessage = "Value is empty.",
                    HasError = true
                };

                return emptyAndnullValidator;
            }
        }

        public static Validator PasswordAndConfirmPasswordEquals(string password, string confirmpassword)
        {
            if (password == confirmpassword)
            {
                var goodValidator = new Validator()
                {
                    ErrorMessage = string.Empty,
                    HasError = false
                };

                return goodValidator;
            }
            else
            {
                var notEqualValidator = new Validator()
                {
                    ErrorMessage = "Password and confirmpassword diferent.",
                    HasError = true
                };

                return notEqualValidator;
            }
        }


        public static bool ValidationsHaveErrors(IList<Validator> validators)
        {
            bool result = validators.Any(v => v.HasError == true);

            return result;
          
        }

    }
}
