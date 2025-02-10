using MyBookRental.Exceptions;

namespace MyBookRental.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : MyBookRentalException
    {
        public InvalidLoginException() : base(ResourceMessage.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
