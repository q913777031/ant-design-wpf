using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="string"/> to <see cref="Visibility"/>.
    /// Returns <see cref="Visibility.Visible"/> when the string is non-null and non-empty,
    /// and <see cref="Visibility.Collapsed"/> when the string is <c>null</c>, empty, or whitespace.
    /// Pass "Inverse" as the converter parameter to reverse the mapping.
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public sealed class StringToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasText = !string.IsNullOrWhiteSpace(value as string);

            bool inverse = string.Equals(parameter as string, "Inverse", StringComparison.OrdinalIgnoreCase);

            if (inverse)
                hasText = !hasText;

            return hasText ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(StringToVisibilityConverter)} does not support ConvertBack.");
        }
    }
}
