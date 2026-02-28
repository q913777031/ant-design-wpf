using System.Windows;

namespace AntDesign.WPF.Assists
{
    /// <summary>
    /// Defines where an icon is placed relative to the content of a control.
    /// </summary>
    public enum IconPlacement
    {
        /// <summary>Icon is placed to the left of (before) the content.</summary>
        Left,

        /// <summary>Icon is placed to the right of (after) the content.</summary>
        Right
    }

    /// <summary>
    /// Provides attached properties to associate an icon and its placement with any FrameworkElement.
    /// The Icon value can be a string (glyph/path data), an ImageSource, a Geometry, or any UIElement.
    /// </summary>
    public static class IconAssist
    {
        #region Icon

        /// <summary>
        /// Gets or sets the icon associated with the element. Accepts strings, ImageSources,
        /// Geometry instances, or any UIElement that the control template can render.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(object),
                typeof(IconAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetIcon(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return element.GetValue(IconProperty);
        }

        public static void SetIcon(FrameworkElement element, object value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(IconProperty, value);
        }

        #endregion

        #region IconPlacement

        /// <summary>
        /// Gets or sets whether the icon is rendered to the left or right of the control's content.
        /// Defaults to <see cref="IconPlacement.Left"/>.
        /// </summary>
        public static readonly DependencyProperty IconPlacementProperty =
            DependencyProperty.RegisterAttached(
                "IconPlacement",
                typeof(IconPlacement),
                typeof(IconAssist),
                new FrameworkPropertyMetadata(IconPlacement.Left, FrameworkPropertyMetadataOptions.Inherits));

        public static IconPlacement GetIconPlacement(FrameworkElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            return (IconPlacement)element.GetValue(IconPlacementProperty);
        }

        public static void SetIconPlacement(FrameworkElement element, IconPlacement value)
        {
            ArgumentNullException.ThrowIfNull(element);
            element.SetValue(IconPlacementProperty, value);
        }

        #endregion
    }
}
