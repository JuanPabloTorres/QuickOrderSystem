using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.SolutionUtilities.ValidatorComponents
{
    public static class ValidatorRules
    {

        public static Validator EmailPatternRule(string emailValue)
        {
            if (!String.IsNullOrEmpty(emailValue))
            {

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
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

        public static Validator CreditCardCheckerRule(PaymentCard paymentCard)
        {
            if (!string.IsNullOrEmpty(paymentCard.HolderName)&& !string.IsNullOrEmpty(paymentCard.CardNumber) && !string.IsNullOrEmpty(paymentCard.Month) && !string.IsNullOrEmpty(paymentCard.Year) && !string.IsNullOrEmpty(paymentCard.Cvc))
            {

                if (paymentCard.CardNumber.Length >= 15 && paymentCard.CardNumber.Length <= 16)
                {
                    if ((paymentCard.CardNumber.Length == 16 && paymentCard.Cvc.Length == 3) || (paymentCard.CardNumber.Length == 15 && paymentCard.Cvc.Length == 4))
                    {
                        return new Validator();
                       

                    }
                    else
                    {
                        return new Validator() { HasError = true, ErrorMessage = "Card Number is in incorrect format check the card digits or the CVC , to continue." };
                    }

                }
                else
                {
                    return new Validator() { HasError = true, ErrorMessage = "Card number are invalid. Check again." };
                }

            }
            else
            {
                return new Validator() { HasError = true, ErrorMessage = "Values are empty." };
            }
        }

    }
}
