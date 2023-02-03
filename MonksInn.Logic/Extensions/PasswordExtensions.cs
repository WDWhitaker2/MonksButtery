using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    internal static class PasswordExtensions
    {
        private static Dictionary<int, string> Salts => new Dictionary<int, string>()
        {
            { 1 , "7sEJ7tY15Vm7BEpXNzop" },
            { 2 , "kTdYppvdJydQRSMoKih7" },
            { 3 , "9yUiYwgBvBLiJqblLIkH" },
            { 4 , "cY9z3f8NhswJSILtZZNx" },
            { 5 , "e58xAxpChK6Cf3F0XWcV" },
            { 6 , "L5GsZtq6oWE43u0IQjXs" },
            { 7 , "72snbFQc8xNASjGjoxWY" },
            { 8 , "1Ivg7GIwVSzgOtPdQ5re" },
            { 9 , "UgiLiq1h4c5OGdEtaC7R" },
            { 10 , "y65HUgzw8stR8LdNiBAO" },
        };
        private static int NewSaltStart = 1;

        private static string HashString(string clearText, int saltKey)
        {
            var salt = Salts.GetValueOrDefault(saltKey);
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var clearTextBytes = Encoding.ASCII.GetBytes(clearText);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(clearTextBytes, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return hashPassword;
        }

        internal static string GenerateHashString(string clearText, out int saltKey)
        {

            Random rnd = new Random();
            saltKey = rnd.Next(NewSaltStart, Salts.Count);
            return HashString(clearText, saltKey);
        }

        internal static bool ChallengeString(string HashedString, string clearChallengerString, int saltKey)
        {
            var newHashedPassword = HashString(clearChallengerString, saltKey);
            return HashedString == newHashedPassword;
        }
    }
}
