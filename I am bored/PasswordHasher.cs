using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace I_am_bored
{
    internal class PasswordHasher
    {
        public static string CreateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                100000,
                HashAlgorithmName.SHA256,
                32
            );

            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(
            string password,
            string storedHash,
            string salt)
        {
            string enteredHash = HashPassword(password, salt);

            return enteredHash == storedHash;
        }
    }
}
