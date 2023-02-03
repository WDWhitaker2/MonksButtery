using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    public static class NumberExtensions
    {
        public static string BufferLength (this long value , int totaldigits)
        {
            var valueString = value.ToString();
            if(valueString.Length < totaldigits)
            {
                valueString = valueString.PadLeft(totaldigits, '0');
            }
            return valueString;
        }

        public static string BufferLength(this int value, int totaldigits)
        {
            var valueString = value.ToString();
            if (valueString.Length < totaldigits)
            {
                valueString = valueString.PadLeft(totaldigits, '0');
            }
            return valueString;
        }

        public static string ToTimeFormat(this int value)
        {
            return $"{value.BufferLength(2)}:00";
        }
    }
}
