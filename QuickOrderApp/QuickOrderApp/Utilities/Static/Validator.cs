using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOrderApp.Utilities.Static
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
    }
}
