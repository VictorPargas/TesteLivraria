using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Excepetion.ExceptionsBase
{
    public class InvalidLoginException : MyBookRentalException
    {
        public InvalidLoginException() : base(ResourceMessage.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
