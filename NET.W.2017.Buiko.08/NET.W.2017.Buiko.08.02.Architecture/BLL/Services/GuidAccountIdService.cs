﻿using System;
using System.Security.Cryptography;
using System.Text;
using BLL.Interface.AccountIdService;

namespace BLL.Services
{
    public class GuidAccountIdService : IAccountIdService
    {
        #region public

        /// <summary>
        /// Account id max length.
        /// </summary>
        public static readonly int IdLength = 20;

        /// <inheritdoc />
        public string GenerateAccountId(string onwerFirstName, string onwerSecondName)
        {
            try
            {
                var accountId = new StringBuilder(GetFirstIdPart());

                accountId.Append(GetSecondIdPart(onwerFirstName));

                accountId.Append(GetThirdIdPart(onwerSecondName));

                return Shuffle(accountId.ToString()).Trim().Substring(0, IdLength);
            }
            catch (Exception e)
            {
                throw new AccountIdServiceException("Id generation error", e);
            }
        }

        #endregion // !public.

        #region private

        private static string GetFirstIdPart() =>
            Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

        private static string GetSecondIdPart(string onwerFirstName) =>
            Math.Abs(Shuffle(onwerFirstName).GetHashCode()).ToString();

        private static string GetThirdIdPart(string onwerSecondName) =>
            Shuffle(onwerSecondName);

        private static string Shuffle(string str)
        {
            var provider = new RNGCryptoServiceProvider();
            var stringChars = str.ToCharArray();
            int n = stringChars.Length;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = box[0] % n--;

                Swap(ref stringChars[k], ref stringChars[n]);
            }

            return new string(stringChars);
        }

        private static void Swap<T>(ref T item1, ref T item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }

        #endregion // !private.
    }
}
