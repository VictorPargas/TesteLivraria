using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Security.Cryptography
{
    public interface IPasswordEncripter
    {
        public string Encrypt(string password);
    }
}
