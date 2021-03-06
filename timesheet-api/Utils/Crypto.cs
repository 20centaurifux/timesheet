using System;
using System.Security.Cryptography;

namespace timesheet_api.Utils
{
    public static class Crypto
    {
        public static string PasswordHash(string text)
        {
            var bytes = System.Text.UTF8Encoding.UTF8.GetBytes(text);
            var sha256 = new SHA256Managed();

            return BitConverter.ToString(sha256.ComputeHash(bytes), 0).Replace("-", String.Empty);
        }
    }
}