using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.SolutionUtilities.ValidatorComponents
{
     public static class  ValidatorExtension
    {

        public static bool AllValidatorPassRules(this IList<Validator> propertiesToVerify)
        {
            return propertiesToVerify.All(v => !v.HasError);
        }

        public static void AddPropertyIfNotExits(this IList<Validator> regiterProperties,Validator newValidator)
        {
            if (!regiterProperties.Contains(newValidator))
            {
                regiterProperties.Add(newValidator);
            }
        }
    }
}
