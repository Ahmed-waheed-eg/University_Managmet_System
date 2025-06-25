using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security
{
    public  class PasswordHasher : IPasswordHasher
    {
        public  string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public  bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            string hashOfInput = HashPassword(enteredPassword);
            return hashOfInput == hashedPassword;
        }
    }
}