using System;
using System.Security.Cryptography;
using System.Text;

namespace UserManagementAPI.Helpers
{
    public static class PasswordHelper
    {
        // HashPassword method for encrypting passwords
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                string hashedEnteredPassword = Convert.ToBase64String(hashBytes);

                return hashedEnteredPassword == storedHashedPassword; // Compare hashed values
            }
        }
    }
}
