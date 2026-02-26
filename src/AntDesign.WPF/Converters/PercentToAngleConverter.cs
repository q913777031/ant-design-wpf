using System;
using System.Globalization;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a percentage value in the range [0, 100] to an angle in degrees [0, 360].
    /// Used by circular progress indicators to map a completion percentage to a sweep angle.
    /// </summary>
    /// <remarks>
    /// Formula: <c>angle = (percent / 100.0) * 360.0</c>
    /// Values outside [0, 100] are clamped before conversion.
    /// An optional converter parameter specifies a start-offset angle added to the result.
    /// </remarks>
    /// <example>
    /// XAML usage:
    /// <code>
    /// &lt;ArcSegment SweepAngle="{Binding Progress,
    ///     Converter={StaticResource PercentToAngleConverter}}" /&gt;
    /// </code>
    /// </example>
    [ValueConversion(typeof(double), typeof(double))]
    public sealed class PercentToAngleConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double percent))
                return 0.0;

            // Clamp to [0, 100].
            percent = Math.Max(0, Math.Min(100, percent));

            double angle = (percent / 100.0) * 360.0;

            // Apply optional start-offset from parameter.
            if (TryParseDouble(parameter, out double offset))
                angle += offset;

            return angle;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double angle))
                return 0.0;

            if (TryParseDouble(parameter, out double offset))
                angle -= offset;

            // Clamp angle to [0, 360] before converting back.
            angle = Math.Max(0, Math.Min(360, angle));

            return (angle / 360.0) * 100.0;
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
