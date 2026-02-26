using System.Windows;
using System.Windows.Media;

namespace AntDesign.WPF.Assists
{
    /// <summary>
    /// Provides attached properties for floating label and placeholder hint functionality on text inputs.
    /// </summary>
    public static class HintAssist
    {
        #region Hint

        /// <summary>
        /// Gets or sets the placeholder/hint text displayed inside a text input.
        /// </summary>
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.RegisterAttached(
                "Hint",
                typeof(string),
                typeof(HintAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static string GetHint(FrameworkElement element)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            return (string)element.GetValue(HintProperty);
        }

        public static void SetHint(FrameworkElement element, string value)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            element.SetValue(HintProperty, value);
        }

        #endregion

        #region IsFloating

        /// <summary>
        /// Gets or sets whether the hint label floats above the input when the control is focused or has content.
        /// </summary>
        public static readonly DependencyProperty IsFloatingProperty =
            DependencyProperty.RegisterAttached(
                "IsFloating",
                typeof(bool),
                typeof(HintAssist),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetIsFloating(FrameworkElement element)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            return (bool)element.GetValue(IsFloatingProperty);
        }

        public static void SetIsFloating(FrameworkElement element, bool value)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            element.SetValue(IsFloatingProperty, value);
        }

        #endregion

        #region HintForeground

        /// <summary>
        /// Gets or sets the brush used to render the hint/placeholder text.
        /// </summary>
        public static readonly DependencyProperty HintForegroundProperty =
            DependencyProperty.RegisterAttached(
                "HintForeground",
                typeof(Brush),
                typeof(HintAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static Brush GetHintForeground(FrameworkElement element)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            return (Brush)element.GetValue(HintForegroundProperty);
        }

        public static void SetHintForeground(FrameworkElement element, Brush value)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            element.SetValue(HintForegroundProperty, value);
        }

        #endregion

        #region HelperText

        /// <summary>
        /// Gets or sets optional helper text displayed below the input field to provide guidance.
        /// </summary>
        public static readonly DependencyProperty HelperTextProperty =
            DependencyProperty.RegisterAttached(
                "HelperText",
                typeof(string),
                typeof(HintAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static string GetHelperText(FrameworkElement element)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            return (string)element.GetValue(HelperTextProperty);
        }

        public static void SetHelperText(FrameworkElement element, string value)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            element.SetValue(HelperTextProperty, value);
        }

        #endregion
    }
}
