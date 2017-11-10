using System;
using System.Security.Cryptography;

namespace Logic.AccountIdGeneratorService.Implementation
{
    /// <inheritdoc />
    public class GuidAccountIdService : AccountIdService
    {
        #region protected override

        /// <inheritdoc />
        protected override string GetFirstIdPart() =>
            Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

        /// <inheritdoc />
        protected override string GetSecondIdPart(string onwerFirstName) =>
            Math.Abs(this.Shuffle(onwerFirstName).GetHashCode()).ToString();

        /// <inheritdoc />
        protected override string GetThirdIdPart(string onwerSecondName) =>
            this.Shuffle(onwerSecondName);

        /// <inheritdoc />
        protected override string Shuffle(string str)
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

        #endregion // !protected override.

        #region private

        private static void Swap<T>(ref T item1, ref T item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }

        #endregion // !private.
    }
}
