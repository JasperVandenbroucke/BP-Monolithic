using System.Security.Cryptography;

namespace ECommerceApi.Auth
{
    public class PasswordHasher
    {
        private const int SALTSIZE = 16;
        private const int HASHSIZE = 32;
        private const int ITERATIONS = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SALTSIZE);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, Algorithm, HASHSIZE);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, Algorithm, HASHSIZE);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
