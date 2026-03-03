using System.Windows;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Attached properties for borderless window customization.
/// </summary>
public static class WindowHelper
{
    /// <summary>
    /// Content to display in the title bar area (e.g., color swatches, theme toggles).
    /// </summary>
    public static readonly DependencyProperty TitleBarContentProperty =
        DependencyProperty.RegisterAttached(
            "TitleBarContent",
            typeof(object),
            typeof(WindowHelper),
            new FrameworkPropertyMetadata(null));

    public static object? GetTitleBarContent(DependencyObject obj) => obj.GetValue(TitleBarContentProperty);
    public static void SetTitleBarContent(DependencyObject obj, object? value) => obj.SetValue(TitleBarContentProperty, value);

    /// <summary>
    /// Height of the title bar. Default is 48.
    /// </summary>
    public static readonly DependencyProperty TitleBarHeightProperty =
        DependencyProperty.RegisterAttached(
            "TitleBarHeight",
            typeof(double),
            typeof(WindowHelper),
            new FrameworkPropertyMetadata(48.0));

    public static double GetTitleBarHeight(DependencyObject obj) => (double)obj.GetValue(TitleBarHeightProperty);
    public static void SetTitleBarHeight(DependencyObject obj, double value) => obj.SetValue(TitleBarHeightProperty, value);
}
