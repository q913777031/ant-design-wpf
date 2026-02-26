using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="Color"/> value to a <see cref="SolidColorBrush"/>.
    /// Useful when a binding source exposes a raw <see cref="Color"/> struct but the
    /// target property expects a <see cref="Brush"/>.
    /// </summary>
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public sealed class ColorToBrushConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
                return new SolidColorBrush(color);

            return new SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
                return brush.Color;

            return System.Windows.Media.Colors.Transparent;
        }
    }
}
