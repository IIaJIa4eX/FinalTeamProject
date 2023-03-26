using System.Security.Cryptography;
using System.Text;

namespace FinalProject.Utils
{
    public class PasswordUtils
    {
        private const string SecretKey = "Fz8wMguwgwegwegq3trqN2DGWiD1ICvRxQ==";
        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            //generate random salt
            byte[] buffer = new byte[16];
            RNGCryptoServiceProvider secureRandom = new RNGCryptoServiceProvider();
            secureRandom.GetBytes(buffer);

            //create hash
            string passwordSalt = Convert.ToBase64String(buffer);
            string passwordHash = GetPasswordHash(password, passwordSalt);

            //done
            return (passwordSalt, passwordHash);
        }
        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            string Hash = GetPasswordHash(password, passwordSalt);
            return Hash == passwordHash;
        }
        public static string GetPasswordHash(string password, string passwordSalt)
        {
            //build password string
            password = $"{password}~{passwordSalt}~{SecretKey}";
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            SHA512 sha512 = new SHA512Managed();
            byte[] passwordHash = sha512.ComputeHash(buffer);
            return Convert.ToBase64String(passwordHash);
        }
    }
}
