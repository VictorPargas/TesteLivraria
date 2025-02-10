namespace MyBookRental.Exceptions.ExceptionsBase
{
    public class MyBookRentalException : SystemException
    {
        public MyBookRentalException(string message) : base(message)
        {
        }
    }
}
