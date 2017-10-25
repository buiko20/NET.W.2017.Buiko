using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class DoubleExtension
    {
        private const int BitCount = sizeof(double) * 8;

        #region public methods
        /// <summary>
        /// Convert double into string which represents it in IEEE 754.
        /// </summary>
        /// <param name="number">double number</param>
        /// <returns>bit string which represents it in IEEE 754</returns>
        public static string ToBitStringUsingBitArray(this double number)
        {
            var bitArray = new BitArray(BitConverter.GetBytes(number));
            var result = new StringBuilder(BitCount);

            for (int i = bitArray.Length - 1; i >= 0; i--)
                result.Append(bitArray[i] ? '1' : '0');

            return result.ToString();
        }

        /// <summary>
        /// Convert double into string which represents it in IEEE 754.
        /// </summary>
        /// <param name="number">double number</param>
        /// <returns>bit string which represents it in IEEE 754</returns>
        public static string ToBitString(this double number)
        {
            // Алгоритм не доделан, работает не всегда правильно, много костылей.  

            if ((number == 0.0) && (double.IsNegativeInfinity(1.0 / number)))
            {
                string res = new string('0', 64);
                res = res.Insert(0, "1");
                res = res.Remove(res.Length - 1);
                return res;
            }

            if ((number == 0.0) && (!double.IsNegativeInfinity(1.0 / number)))
                return new string('0', 64);

            if (number == double.Epsilon)
            {
                string res = new string('0', 64);
                res = res.Insert(res.Length - 1, "1");
                res = res.Remove(res.Length - 1);
                return res;
            }

            char sign = number > 0 ? '0' : '1';
            long integerPart = (long)Math.Truncate(number);
            double fraction = number - integerPart;
            fraction = Math.Abs(fraction);

            var result = new StringBuilder(BitCount);
            string intPart = IntToBitString(integerPart);
            string fracPart = FractionToString(fraction);

            int exp = GetExponent(intPart);

            string temp = IntToBitString(exp + 1023);

            result.Append(sign);
            int j = temp.Length - 11;
            int delta1 = 0, delta2 = 0;
            if (number == double.MinValue || number == double.MaxValue || double.IsNaN(number) ||
                number == double.NegativeInfinity || number == Double.PositiveInfinity)
            {
                j = temp.Length - 1;
                for (int i = 0; i < 11; i++)
                    result.Append(temp[j--]);
                if (double.IsNaN(number))
                {
                    result.Remove(result.Length - 1, 1);
                    result.Append("11");
                    delta1 = -1;
                }
                if (number == double.NegativeInfinity || number == Double.PositiveInfinity)
                {
                    delta2 = 63;
                    result.Remove(result.Length - 1, 1);
                    result.Append("1");
                }
                    
            }
            else
            {
                for (int i = 0; i < 11; i++)
                    result.Append(temp[j++]);
            }

            temp = intPart + fracPart;
            string mantissa = "";
            j = intPart.Length - exp;
            for (int i = j - delta2; i < 52 + delta1 + j - delta2; i++)
                mantissa += temp[i];

            result.Append(mantissa);

            return result.ToString();
        }

        #endregion

        #region private methods
        private static int GetExponent(string bits)
        {
            int i = 2;
            while ((bits[i - 1] == '0') && (i < bits.Length)) i++;
            return bits.Length - i;
        }

        private static string IntToBitString(long number)
        {
            if (number == long.MaxValue || number == long.MinValue)
                return Convert.ToString(number, 2);
            if (number < 0)
                number = ~number + 1;
            int i;          
            char[] result = new char[sizeof(long) * 8];
            for (i = 0; i < result.Length; i++)
                result[i] = '0';

            i = 0;
            long temp = Math.Abs(number);
            while (temp >= 2)
            {
                char bit = (temp % 2).ToString()[0];
                result[i++] = bit;
                temp /= 2;
            }
            result[i] = temp.ToString()[0];

          //  if (number < 0) Not(result);
                
            Reverse(result);

            return new string(result);
        }

        private static string FractionToString(double fraction)
        {
            char[] result = new char[sizeof(double) * 8];

            for (int i = 0; i < result.Length; i++)
            {
                char bit = '0';
                if (fraction * 2 - 1 >= 0)
                {
                    bit = '1';
                    fraction = fraction * 2 - 1;
                }
                else
                {
                    fraction *= 2;
                }
                result[i] = bit;
            }

            return new string(result);
        }

        private static void Not(char[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '0') array[i] = '1';
                else array[i] = '0';
            }
        }

        private static void Reverse(char[] array)
        {
            int limit = array.Length / 2;
            int j = array.Length - 1;
            for (int i = 0; i < limit; i++)
            {
                var temp = array[i];
                array[i] = array[j];
                array[j--] = temp;
            }
        }
        #endregion
    }
}
