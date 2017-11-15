using System;
using System.Collections;
using System.Text;

namespace Algorithm
{
    public static class DoubleExtension
    {
        #region private constants

        private const char TrueBit = '1';
        private const char FalseBit = '0';

        private const int Bias = 1023;
        private const int ExponentLength = 11;
        private const int MantissaLength = 52;
        private const int DenormalizedBias = -1022;

        #endregion // !private constants.

        #region public methods

        /// <summary>
        /// Convert <paramref name="number"/> into string which represents it in IEEE 754.
        /// </summary>
        /// <param name="number">double number</param>
        /// <returns>Bit string which represents it in IEEE 754.</returns>
        public static string ToBitStringUsingBitArray(this double number)
        {
            var bitArray = new BitArray(BitConverter.GetBytes(number));
            var result = new StringBuilder(sizeof(double) * 8);

            for (int i = bitArray.Length - 1; i >= 0; i--)
            {
                result.Append(bitArray[i] ? TrueBit : FalseBit);
            }

            return result.ToString();
        }

        /// <summary>
        /// Convert <paramref name="number"/> into string which represents it in IEEE 754.
        /// </summary>
        /// <param name="number">double number</param>
        /// <returns>Bit string which represents it in IEEE 754.</returns>
        public static string ToBitString(this double number)
        {
            var result = new StringBuilder(sizeof(double) * 8);

            char sign = GetSignBinary(number);
            number = sign == TrueBit ? -number : number;
            string exponent = GetExponentBinary(number);
            string mantissa = GetMantissaBinary(number);

            result.Append(sign);
            result.Append(exponent);
            result.Append(mantissa);

            return result.ToString();
        }

        #endregion // !public methods.

        #region private methods

        private static char GetSignBinary(double number)
        {
            bool isNegativeInfinity = double.IsNegativeInfinity(1.0 / number);

            if (number < 0.0 || isNegativeInfinity)
            {
                return TrueBit;
            }

            return FalseBit;
        }

        private static string GetExponentBinary(double number)
        {
            int exponent = GetExponent(number);
            exponent += Bias;
            exponent = exponent < 0 ? 0 : exponent;

            string result = IntegerToBinary(exponent);
            return result.Substring(result.Length - ExponentLength, ExponentLength);
        }

        private static int GetExponent(double number)
        {
            int power = 0;

            double fraction = (number / Math.Pow(2, power)) - 1;

            while ((fraction < 0) || (fraction >= 1))
            {
                power = fraction < 1 ? --power : ++power;
                fraction = (number / Math.Pow(2, power)) - 1;
            }

            return power;
        }

        private static string GetMantissaBinary(double number)
        {
            int exponent = GetExponent(number);
            exponent += Bias;
            exponent = exponent < 0 ? 0 : exponent;
            exponent -= Bias;

            double fraction;

            if (exponent <= -Bias)
            {
                fraction = number / Math.Pow(2, DenormalizedBias);
            }
            else
            {
                fraction = (number / Math.Pow(2, exponent)) - 1;
            }

            return FractionToBinary(fraction, MantissaLength);  
        }

        private static string IntegerToBinary(long number)
        {
            char[] bits = new char[sizeof(long) * 8];

            for (int i = 0; i < bits.Length; i++)
            {
                if ((number & 1) == 1)
                {
                    bits[bits.Length - i - 1] = TrueBit;
                }
                else
                {
                    bits[bits.Length - i - 1] = FalseBit;
                }

                number >>= 1;
            }

            return new string(bits);
        }

        private static string FractionToBinary(double fraction, int eps)
        {
            char[] result = new char[eps];

            for (int i = 0; i < result.Length; i++)
            {
                var bit = FalseBit;
                fraction *= 2;
                if (fraction >= 1)
                {
                    bit = TrueBit;
                    fraction -= 1;
                }

                result[i] = bit;
            }

            return new string(result);
        }

        #endregion // !private methods.
    }
}
