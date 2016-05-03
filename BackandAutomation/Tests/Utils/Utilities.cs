using System;
using Core;

namespace Tests.Utils
{
    public class Utilities
    {
        public static string GenerateString(string str)
        {
            return string.Concat(str, Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
        }

        public static string GenerateEmail()
        {
            var originalEmail = Configuration.Instance.LoginCredentials.Email;
            var userMailNameIndex = originalEmail.IndexOf('@');
            string newEmail =
                $"{originalEmail.Substring(0, userMailNameIndex)}+{Guid.NewGuid().ToString().Substring(0, 5)}{originalEmail.Substring(userMailNameIndex)}";
            return newEmail;
        }
    }
}