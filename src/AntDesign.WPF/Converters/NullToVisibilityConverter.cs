using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts a value to <see cref="Visibility"/> based on whether it is null or empty.
    /// <list type="bullet">
    ///   <item><c>null</c> → <see cref="Visibility.Collapsed"/></item>
    ///   <item>non-null → <see cref="Visibility.Visible"/></item>
    /// </list>
    /// Pass "Inverse" as the converter parameter to reverse the mapping so that
    /// a non-null value becomes <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasValue = value != null;

            bool inverse = string.Equals(parameter as string, "Inverse", StringComparison.OrdinalIgnoreCase);

            if (inverse)
                hasValue = !hasValue;

            return hasValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
