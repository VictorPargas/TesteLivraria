using System.Security.Cryptography;
using System.Text;

namespace MyBookRental.Application.Services.Cryptografy
{
    public class PasswordEncripter
    {
        public string Encrypt(string password)
        {
            var chaveAddicional = "ABC";
            var newPassword = $"{password}{chaveAddicional}";

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
