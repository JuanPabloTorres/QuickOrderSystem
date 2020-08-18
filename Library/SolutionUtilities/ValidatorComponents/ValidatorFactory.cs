using System;
using System.Collections.Generic;
using System.Text;

namespace Library.SolutionUtilities.ValidatorComponents
{
    public class ValidatorFactory
    {

        public static Validator CreateValidator(Validator validator)
        {
            return new Validator(validator);
        }
    }
}
