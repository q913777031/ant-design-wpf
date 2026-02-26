using System;
using System.Globalization;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Returns half of the input <see cref="double"/> value.
    /// Useful for computing a radius from a diameter, or centering an element.
    /// An optional numeric converter parameter acts as an additional offset applied
    /// after halving: <c>result = (value / 2) + offset</c>.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public sealed class HalfValueConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double input))
                return 0.0;

            double half = input / 2.0;

            if (TryParseDouble(parameter, out double offset))
                half += offset;

            return half;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double half))
                return 0.0;

            if (TryParseDouble(parameter, out double offset))
                half -= offset;

            return half * 2.0;
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static bool TryParseDouble(object value, out double result)
        {
            result = 0;
            if (value == null)
                return false;
            return double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }
    }
}
