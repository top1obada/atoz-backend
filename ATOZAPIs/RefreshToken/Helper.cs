using System;
using System.Security.Cryptography;
using System.Text;

namespace ATOZAPIs.RefreshToken
{


    public static class clsRefreshTokenHelper
    {
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64]; // 64 بايت = 512 بت
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber); // الناتج ~88 محرف
        }

        public static string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2")); // يحول كل بايت إلى 2 محرف Hex
            return sb.ToString(); // الطول = 64 محرف دائمًا
        }
    }
}
