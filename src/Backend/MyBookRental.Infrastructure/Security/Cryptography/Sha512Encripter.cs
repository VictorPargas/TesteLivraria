using System.Security.Cryptography;
using System.Text;
using MyBookRental.Domain.Security.Cryptography;

namespace MyBookRental.Infrastructure.Security.Cryptography
{
    public class Sha512Encripter : IPasswordEncripter
    {
        private readonly string _additionalKey;
        public Sha512Encripter(string additionalKey) => _additionalKey = additionalKey;
        public string Encrypt(string password)
        {

            var newPassword = $"{password}{_additionalKey}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hashByter = SHA512.HashData(bytes);
            return ByteArrayToString(hashByter);
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            var result = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("X2");
                result.Append(hex);
            }
            return result.ToString();
        }
    }
}
