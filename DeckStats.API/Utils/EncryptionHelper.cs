using System.Security.Cryptography;
using System.Text;

namespace DeckStats.API.Utils;

public class EncryptionHelper
{
    public string GetHashedPasswordAndSalt(string password, string salt)
    {
        using (SHA512 algorithm = SHA512.Create())
        {
            byte[] hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}