using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MonksInn.Logic
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}
