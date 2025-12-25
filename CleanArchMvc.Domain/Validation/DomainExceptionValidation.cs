namespace CleanArchMvc.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error)
        { }


        public static void When(bool hashError, string error)
        {
            if (hashError)
                throw new DomainExceptionValidation(error);

        }
    }
}