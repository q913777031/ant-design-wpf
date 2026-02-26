using System.Windows;

namespace AntDesign.WPF.Assists
{
    /// <summary>
    /// Defines the shadow/elevation levels for components, following Material-inspired depth semantics.
    /// Dp0 means no shadow; higher values produce larger, softer shadows.
    /// </summary>
    public enum ElevationLevel
    {
        /// <summary>No elevation — no drop shadow.</summary>
        Dp0 = 0,

        /// <summary>Low elevation — subtle shadow suitable for cards and panels.</summary>
        Dp1 = 1,

        /// <summary>Medium elevation — moderate shadow suitable for dialogs and popovers.</summary>
        Dp2 = 2,

        /// <summary>High elevation — prominent shadow suitable for menus and floating elements.</summary>
        Dp3 = 3
    }

    /// <summary>
    /// Provides an attached property to control the visual elevation (drop shadow depth) of any FrameworkElement.
    /// </summary>
    public static class ElevationAssist
    {
        #region Elevation

        /// <summary>
        /// Gets or sets the elevation level that determines the drop shadow applied to the element.
        /// </summary>
        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.RegisterAttached(
                "Elevation",
                typeof(ElevationLevel),
                typeof(ElevationAssist),
                new FrameworkPropertyMetadata(ElevationLevel.Dp0, FrameworkPropertyMetadataOptions.Inherits));

        public static ElevationLevel GetElevation(FrameworkElement element)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            return (ElevationLevel)element.GetValue(ElevationProperty);
        }

        public static void SetElevation(FrameworkElement element, ElevationLevel value)
        {
            if (element is null) throw new System.ArgumentNullException(nameof(element));
            element.SetValue(ElevationProperty, value);
        }

        #endregion
    }
}
