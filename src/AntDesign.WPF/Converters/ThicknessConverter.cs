using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="Thickness"/>.
    /// </summary>
    /// <remarks>
    /// Converter parameter controls which sides receive the value:
    /// <list type="bullet">
    ///   <item><c>null</c> / empty — uniform thickness on all four sides.</item>
    ///   <item><c>"Left"</c> — value on left side only (others 0).</item>
    ///   <item><c>"Top"</c> — value on top side only.</item>
    ///   <item><c>"Right"</c> — value on right side only.</item>
    ///   <item><c>"Bottom"</c> — value on bottom side only.</item>
    ///   <item><c>"Horizontal"</c> — value on left and right sides.</item>
    ///   <item><c>"Vertical"</c> — value on top and bottom sides.</item>
    /// </list>
    /// </remarks>
    [ValueConversion(typeof(double), typeof(Thickness))]
    public sealed class ThicknessConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double thickness))
                return new Thickness(0);

            string side = (parameter as string)?.Trim() ?? string.Empty;

            return side.ToUpperInvariant() switch
            {
                "LEFT"       => new Thickness(thickness, 0, 0, 0),
                "TOP"        => new Thickness(0, thickness, 0, 0),
                "RIGHT"      => new Thickness(0, 0, thickness, 0),
                "BOTTOM"     => new Thickness(0, 0, 0, thickness),
                "HORIZONTAL" => new Thickness(thickness, 0, thickness, 0),
                "VERTICAL"   => new Thickness(0, thickness, 0, thickness),
                _            => new Thickness(thickness)
            };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness t)
                return Math.Max(Math.Max(t.Left, t.Top), Math.Max(t.Right, t.Bottom));

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
