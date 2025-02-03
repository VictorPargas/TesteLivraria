using System.Security.Cryptography;
using System.Text;

namespace MyBookRental.Application.Services.Cryptografy
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;
        public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;
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
