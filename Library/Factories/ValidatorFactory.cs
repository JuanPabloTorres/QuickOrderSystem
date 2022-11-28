using Library.SolutionUtilities.ValidatorComponents;

namespace Library.Factories
{
    public class ValidatorFactory
    {
        public static Validator CreateValidator(Validator validator)
        {
            return new Validator(validator);
        }
    }
}