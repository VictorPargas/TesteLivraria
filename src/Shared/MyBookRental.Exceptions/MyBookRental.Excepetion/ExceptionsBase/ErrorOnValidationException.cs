namespace MyBookRental.Excepetion.ExceptionsBase
{
    public class ErrorOnValidationException : MyBookRentalException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errors)
        {
            ErrorMessages = errors;
        }   
    }
}
