using System;

namespace QuickOrderApp.Utilities.Static
{
    public class Validator
    {
        public Validator ()
        {
            HasError = false;

            ErrorMessage = String.Empty;
        }

        public Validator (Validator validator)
        {
            HasError = validator.HasError;

            ErrorMessage = validator.ErrorMessage;
        }

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }
    }
}