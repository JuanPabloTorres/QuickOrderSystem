namespace Library.SolutionUtilities.ValidatorComponents
{
    public class ValidatorFactory
    {
        public static Validator CreateValidator (Validator validator)
        {
            return new Validator(validator);
        }
    }
}