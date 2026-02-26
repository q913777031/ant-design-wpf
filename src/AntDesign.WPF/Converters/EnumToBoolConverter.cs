using System;
using System.Globalization;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Converts an enum value to <see cref="bool"/> by comparing the bound value against the
    /// converter parameter. Returns <c>true</c> when the bound value equals the parameter.
    /// Ideal for binding a group of RadioButtons to a single enum property.
    /// </summary>
    /// <example>
    /// XAML usage:
    /// <code>
    /// &lt;RadioButton IsChecked="{Binding Status, Converter={StaticResource EnumToBool}, ConverterParameter={x:Static local:Status.Active}}" /&gt;
    /// </code>
    /// </example>
    [ValueConversion(typeof(Enum), typeof(bool))]
    public sealed class EnumToBoolConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            // Support parameter as string (e.g. ConverterParameter="Active") or as the enum value itself.
            if (parameter is string paramString)
            {
                try
                {
                    object enumValue = Enum.Parse(value.GetType(), paramString, ignoreCase: true);
                    return value.Equals(enumValue);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            return value.Equals(parameter);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter != null)
            {
                if (parameter is string paramString)
                {
                    try
                    {
                        return Enum.Parse(targetType, paramString, ignoreCase: true);
                    }
                    catch (ArgumentException)
                    {
                        return Binding.DoNothing;
                    }
                }

                return parameter;
            }

            return Binding.DoNothing;
        }
    }
}
