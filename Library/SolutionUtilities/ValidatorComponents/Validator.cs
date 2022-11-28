using System;
using System.Collections.Generic;

namespace Library.SolutionUtilities.ValidatorComponents
{
    public class Validator
    {
        public static IList<Validator> PropertiesToValid { get; set; } = new List<Validator>();

        public Validator()
        {
            HasError = false;

            ErrorMessage = String.Empty;
        }

        public Validator(Validator validator)
        {
            HasError = validator.HasError;

            ErrorMessage = validator.ErrorMessage;
        }

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }
    }
}