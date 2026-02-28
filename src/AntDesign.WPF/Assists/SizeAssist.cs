using System.Windows;

namespace AntDesign.WPF.Assists
{
    /// <summary>
    /// Defines the three standard component size variants used throughout AntDesign.WPF.
    /// These mirror the Ant Design size tokens: small, middle (default), and large.
    /// </summary>
    public enum ControlSize
    {
        /// <summary>Compact size — reduced padding and font size for dense UIs.</summary>
        Small,

        /// <summary>Standard size — the default size used when no size is specified.</summary>
        Default,

        /// <summary>Generous size — increased padding and font size for prominent controls.</summary>
        Large
    }

    /// <summary>
    /// Provides an attached property for controlling the size variant of any FrameworkElement.
    /// Control templates read this value to apply the appropriate sizing tokens.
    /// </summary>
    public static class SizeAssist
    {
        #region Size

        /// <summary>
        /// Gets or sets the size variant applied to the element.
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.RegisterAttached(
                "Size",
                typeof(ControlSize),
                typeof(SizeAssist),
                new FrameworkPropertyMetadata(ControlSize.Default, FrameworkPropertyMetadataOptions.Inherits));

        public static ControlSize GetSize(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return (ControlSize)element.GetValue(SizeProperty);
        }

        public static void SetSize(FrameworkElement element, ControlSize value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(SizeProperty, value);
        }

        #endregion
    }
}
