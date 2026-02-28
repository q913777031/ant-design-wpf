using System.Windows;

namespace AntDesign.WPF.Assists
{
    /// <summary>
    /// Provides attached properties for extended text input functionality including clear button,
    /// prefix/suffix content, and character count display.
    /// </summary>
    public static class TextFieldAssist
    {
        #region HasClearButton

        /// <summary>
        /// Gets or sets whether a clear (X) button is shown inside the text input to reset its value.
        /// </summary>
        public static readonly DependencyProperty HasClearButtonProperty =
            DependencyProperty.RegisterAttached(
                "HasClearButton",
                typeof(bool),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetHasClearButton(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return (bool)element.GetValue(HasClearButtonProperty);
        }

        public static void SetHasClearButton(FrameworkElement element, bool value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(HasClearButtonProperty, value);
        }

        #endregion

        #region Prefix

        /// <summary>
        /// Gets or sets the prefix content (text or icon) rendered at the leading edge of the input.
        /// </summary>
        public static readonly DependencyProperty PrefixProperty =
            DependencyProperty.RegisterAttached(
                "Prefix",
                typeof(object),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetPrefix(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return element.GetValue(PrefixProperty);
        }

        public static void SetPrefix(FrameworkElement element, object value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(PrefixProperty, value);
        }

        #endregion

        #region Suffix

        /// <summary>
        /// Gets or sets the suffix content (text or icon) rendered at the trailing edge of the input.
        /// </summary>
        public static readonly DependencyProperty SuffixProperty =
            DependencyProperty.RegisterAttached(
                "Suffix",
                typeof(object),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetSuffix(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return element.GetValue(SuffixProperty);
        }

        public static void SetSuffix(FrameworkElement element, object value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(SuffixProperty, value);
        }

        #endregion

        #region PrefixIcon

        /// <summary>
        /// Gets or sets a dedicated icon placed at the leading edge of the input, outside the border.
        /// </summary>
        public static readonly DependencyProperty PrefixIconProperty =
            DependencyProperty.RegisterAttached(
                "PrefixIcon",
                typeof(object),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetPrefixIcon(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return element.GetValue(PrefixIconProperty);
        }

        public static void SetPrefixIcon(FrameworkElement element, object value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(PrefixIconProperty, value);
        }

        #endregion

        #region SuffixIcon

        /// <summary>
        /// Gets or sets a dedicated icon placed at the trailing edge of the input, outside the border.
        /// </summary>
        public static readonly DependencyProperty SuffixIconProperty =
            DependencyProperty.RegisterAttached(
                "SuffixIcon",
                typeof(object),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetSuffixIcon(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return element.GetValue(SuffixIconProperty);
        }

        public static void SetSuffixIcon(FrameworkElement element, object value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(SuffixIconProperty, value);
        }

        #endregion

        #region CharacterCount

        /// <summary>
        /// Gets or sets whether a character counter is shown below the input (requires MaxLength to be set).
        /// </summary>
        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.RegisterAttached(
                "CharacterCount",
                typeof(bool),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetCharacterCount(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return (bool)element.GetValue(CharacterCountProperty);
        }

        public static void SetCharacterCount(FrameworkElement element, bool value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(CharacterCountProperty, value);
        }

        #endregion

        #region MaxLength

        /// <summary>
        /// Gets or sets the maximum number of characters allowed in the text input.
        /// Used in conjunction with CharacterCount to display remaining characters.
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.RegisterAttached(
                "MaxLength",
                typeof(int),
                typeof(TextFieldAssist),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.Inherits));

        public static int GetMaxLength(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return (int)element.GetValue(MaxLengthProperty);
        }

        public static void SetMaxLength(FrameworkElement element, int value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(MaxLengthProperty, value);
        }

        #endregion
    }
}
