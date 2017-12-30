using System;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    internal static class CryptographyHelper
    {
        #region constants

        private const string Alphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";

        #endregion // !constants.

        #region public

        public static string GetSha256Hash(string input)
        {
            var sha256 = SHA256.Create();
            byte[] hashInBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            var result = new StringBuilder();
            foreach (var hashByte in hashInBytes)
            {
                result.Append(hashByte.ToString("x2"));
            }

            return result.ToString();
        }

        public static string Encrypt(string plainText, string key) =>
            CryptographicTransformation(plainText, key, (p, k, n) => (p + k) % n);

        public static string Decrypt(string cipherText, string key) =>
            CryptographicTransformation(cipherText, key, (c, k, n) => (c + n - k) % n);

        //public static string Encrypt(string plainText, string key)
        //{
        //    byte[] result = Encoding.UTF8.GetBytes(plainText);
        //    byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        //    int j = 0;
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        if (keyBytes.Length == j)
        //        {
        //            j = 0;
        //        }

        //        result[i] = (byte)(result[i] ^ keyBytes[j++]);
        //    }

        //    return Encoding.UTF8.GetString(result);
        //}

        //public static string Decrypt(string cipherText, string key) =>
        //    Encrypt(cipherText, key);

        #endregion // !public.

        #region private

        private static string CryptographicTransformation(
            string text, string key, Func<int, int, int, int> cryptographicTransformation)
        {
            var result = new StringBuilder(text.Length);

            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (key.Length == j)
                {
                    j = 0;
                }

                int indexOfTextChar = Alphabet.IndexOf(text[i]);
                int indexOfKeyChar = Alphabet.IndexOf(key[j++]);
                if (indexOfTextChar == -1 || indexOfKeyChar == -1)
                {
                    result.Append(text[i]);
                    continue;
                }

                int indexOfResultChar = cryptographicTransformation(indexOfTextChar, indexOfKeyChar, Alphabet.Length);
                result.Append(Alphabet[indexOfResultChar]);
            }

            return result.ToString();
        }

        #endregion // !private.
    }
}
