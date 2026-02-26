using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Represents a single event entry in a <see cref="Timeline"/>.
/// The <see cref="ContentControl.Content"/> property holds the main event body.
/// </summary>
public class TimelineItem : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Color"/> dependency property.</summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(
            nameof(Color),
            typeof(Brush),
            typeof(TimelineItem),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Dot"/> dependency property.</summary>
    public static readonly DependencyProperty DotProperty =
        DependencyProperty.Register(
            nameof(Dot),
            typeof(object),
            typeof(TimelineItem),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Label"/> dependency property.</summary>
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(TimelineItem),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static TimelineItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(TimelineItem),
            new FrameworkPropertyMetadata(typeof(TimelineItem)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the brush used for the timeline node dot.
    /// When null the template uses the default Ant Design blue.
    /// </summary>
    public Brush? Color
    {
        get => (Brush?)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom element used as the timeline node dot.
    /// Accepts any object; when null a default solid circle is rendered.
    /// </summary>
    public object? Dot
    {
        get => GetValue(DotProperty);
        set => SetValue(DotProperty, value);
    }

    /// <summary>
    /// Gets or sets the optional label displayed on the opposite side of the axis
    /// from the content (used in <see cref="TimelineMode.Alternate"/> or
    /// <see cref="TimelineMode.Right"/> modes).
    /// </summary>
    public string? Label
    {
        get => (string?)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
}
