using System;

namespace Tests.Utils
{
    public class Utilities
    {
        public static string GenerateString(string str)
        {
            return string.Concat(str, Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
        }
    }
}