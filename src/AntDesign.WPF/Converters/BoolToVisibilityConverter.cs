using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="bool"/> value to a <see cref="Visibility"/> value.
    /// Pass "Inverse" as the converter parameter to reverse the mapping
    /// (i.e. <c>true</c> becomes <see cref="Visibility.Collapsed"/>).
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is bool b && b;

            bool inverse = string.Equals(parameter as string, "Inverse", StringComparison.OrdinalIgnoreCase);

            if (inverse)
                boolValue = !boolValue;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility)
                return false;

            bool result = visibility == Visibility.Visible;

            bool inverse = string.Equals(parameter as string, "Inverse", StringComparison.OrdinalIgnoreCase);

            return inverse ? !result : result;
        }
    }
}
