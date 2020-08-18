using System;
using System.Collections.Generic;
using System.Text;

namespace Library.SolutionUtilities.ValidatorComponents
{
   public  class Validator
    {

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }

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
    }
}
