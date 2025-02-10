using MyBookRental.Exceptions;

namespace MyBookRental.Excepetion.ExceptionsBase
{
    public class InvalidLoginException : MyBookRentalException
    {
        public InvalidLoginException() : base(ResourceMessage.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
