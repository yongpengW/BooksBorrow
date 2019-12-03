using BooksBorrow.Common.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BooksBorrow.Common.Extensions
{
    public static class DecimalExtension
    {
        public static string ToCurrencyString(this decimal source, CultureInfo culture = null)
        {
            var usedCulture = culture;

            if (usedCulture == null)
            {
                usedCulture = SystemDefaultSettings.DefaultCulture;
            }

            return source.ToString("C", usedCulture);
        }

        public static string ToCurrencyString(this decimal? source, CultureInfo culture = null)
        {
            if (source.HasValue)
            {
                return source.Value.ToCurrencyString(culture);
            }

            return String.Empty;
        }
    }
}
