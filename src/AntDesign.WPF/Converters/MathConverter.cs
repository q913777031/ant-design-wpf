using System;
using System.Globalization;
using System.Windows.Data;

namespace AntDesign.WPF.Converters
{
    /// <summary>
    /// Performs simple arithmetic on a numeric binding value.
    /// The converter parameter specifies the operation and operand in the format
    /// <c>"Operation Operand"</c> (e.g. <c>"Add 10"</c>, <c>"Multiply 0.5"</c>).
    /// </summary>
    /// <remarks>
    /// Supported operations (case-insensitive): Add, Subtract, Multiply, Divide.
    /// When no parameter is supplied the value is returned unchanged.
    /// </remarks>
    /// <example>
    /// XAML usage:
    /// <code>
    /// &lt;TextBlock Width="{Binding ActualWidth, ElementName=parent,
    ///     Converter={StaticResource MathConverter}, ConverterParameter='Subtract 20'}" /&gt;
    /// </code>
    /// </example>
    [ValueConversion(typeof(double), typeof(double))]
    public sealed class MathConverter : IValueConverter
    {
        private static readonly char[] s_parameterSeparators = [' ', '\t'];
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double input))
                return value;

            if (!TryParseParameter(parameter as string ?? string.Empty, out string operation, out double operand))
                return value;

            return operation.ToUpperInvariant() switch
            {
                "ADD"      => input + operand,
                "SUBTRACT" => input - operand,
                "MULTIPLY" => input * operand,
                "DIVIDE"   => operand != 0 ? input / operand : 0.0,
                _          => input
            };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!TryParseDouble(value, out double result))
                return value;

            if (!TryParseParameter(parameter as string ?? string.Empty, out string operation, out double operand))
                return value;

            return operation.ToUpperInvariant() switch
            {
                "ADD"      => result - operand,
                "SUBTRACT" => result + operand,
                "MULTIPLY" => operand != 0 ? result / operand : 0.0,
                "DIVIDE"   => result * operand,
                _          => result
            };
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

        /// <summary>
        /// Parses a parameter string formatted as "Operation Operand" (e.g. "Add 10").
        /// </summary>
        private static bool TryParseParameter(string parameter, out string operation, out double operand)
        {
            operation = string.Empty;
            operand   = 0;

            if (string.IsNullOrWhiteSpace(parameter))
                return false;

            string[] parts = parameter.Trim().Split(s_parameterSeparators, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;

            operation = parts[0];
            return double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out operand);
        }
    }
}
