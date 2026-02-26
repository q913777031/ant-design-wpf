using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="CornerRadius"/>.
    /// </summary>
    /// <remarks>
    /// Converter parameter controls which corners receive the radius value:
    /// <list type="bullet">
    ///   <item><c>null</c> / empty — uniform radius applied to all four corners.</item>
    ///   <item><c>"TopLeft"</c> — radius on top-left corner only (others 0).</item>
    ///   <item><c>"TopRight"</c> — radius on top-right corner only.</item>
    ///   <item><c>"BottomRight"</c> — radius on bottom-right corner only.</item>
    ///   <item><c>"BottomLeft"</c> — radius on bottom-left corner only.</item>
    ///   <item><c>"Top"</c> — radius on top-left and top-right corners.</item>
    ///   <item><c>"Bottom"</c> — radius on bottom-left and bottom-right corners.</item>
    ///   <item><c>"Left"</c> — radius on top-left and bottom-left corners.</item>
    ///   <item><c>"Right"</c> — radius on top-right and bottom-right corners.</item>
    /// </list>
    /// </remarks>
    [ValueConversion(typeof(double), typeof(CornerRadius))]
    public sealed class CornerRadiusConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double radius))
                return new CornerRadius(0);

            string side = (parameter as string)?.Trim() ?? string.Empty;

            return side.ToUpperInvariant() switch
            {
                "TOPLEFT"     => new CornerRadius(radius, 0, 0, 0),
                "TOPRIGHT"    => new CornerRadius(0, radius, 0, 0),
                "BOTTOMRIGHT" => new CornerRadius(0, 0, radius, 0),
                "BOTTOMLEFT"  => new CornerRadius(0, 0, 0, radius),
                "TOP"         => new CornerRadius(radius, radius, 0, 0),
                "BOTTOM"      => new CornerRadius(0, 0, radius, radius),
                "LEFT"        => new CornerRadius(radius, 0, 0, radius),
                "RIGHT"       => new CornerRadius(0, radius, radius, 0),
                _             => new CornerRadius(radius)
            };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius cr)
                return Math.Max(Math.Max(cr.TopLeft, cr.TopRight), Math.Max(cr.BottomRight, cr.BottomLeft));

            return 0.0;
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
