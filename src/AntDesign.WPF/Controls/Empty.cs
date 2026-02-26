using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Displays an empty-state placeholder with an illustration, description, and
/// optional footer content (e.g. a "Create Now" button).
/// The <see cref="ContentControl.Content"/> property is used as the footer area.
/// Follows the Ant Design Empty specification.
/// </summary>
public class Empty : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Description"/> dependency property.</summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(Empty),
            new PropertyMetadata("No Data"));

    /// <summary>Identifies the <see cref="Image"/> dependency property.</summary>
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register(
            nameof(Image),
            typeof(object),
            typeof(Empty),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Empty()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Empty),
            new FrameworkPropertyMetadata(typeof(Empty)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the description text displayed below the illustration.
    /// Defaults to "No Data".
    /// </summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the image or illustration displayed above the description.
    /// Accepts any object; when null the default SVG illustration is used by the template.
    /// </summary>
    public object? Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
}
