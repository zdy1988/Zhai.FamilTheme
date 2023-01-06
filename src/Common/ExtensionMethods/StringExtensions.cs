using System;
using System.Security.Cryptography;
using System.Text;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ToMD5(this string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bytes = Encoding.UTF8.GetBytes(source);

            string result = BitConverter.ToString(md5.ComputeHash(bytes));

            return result.Replace("-", "");
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }
    }
}
