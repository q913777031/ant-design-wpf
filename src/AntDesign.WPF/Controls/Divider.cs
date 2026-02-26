using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the horizontal alignment of text inside a <see cref="Divider"/>.
/// </summary>
public enum DividerTextAlignment
{
    /// <summary>Text is positioned toward the left end of the divider.</summary>
    Left,

    /// <summary>Text is centered along the divider.</summary>
    Center,

    /// <summary>Text is positioned toward the right end of the divider.</summary>
    Right
}

/// <summary>
/// A horizontal or vertical line that visually separates content sections.
/// Optionally renders a text label over the line.
/// Follows the Ant Design Divider specification.
/// </summary>
public class Divider : Control
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Orientation"/> dependency property.</summary>
    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(Divider),
            new PropertyMetadata(Orientation.Horizontal));

    /// <summary>Identifies the <see cref="DashedLine"/> dependency property.</summary>
    public static readonly DependencyProperty DashedLineProperty =
        DependencyProperty.Register(
            nameof(DashedLine),
            typeof(bool),
            typeof(Divider),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="Text"/> dependency property.</summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(Divider),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="TextAlignment"/> dependency property.</summary>
    public static readonly DependencyProperty TextAlignmentProperty =
        DependencyProperty.Register(
            nameof(TextAlignment),
            typeof(DividerTextAlignment),
            typeof(Divider),
            new PropertyMetadata(DividerTextAlignment.Center));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Divider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Divider),
            new FrameworkPropertyMetadata(typeof(Divider)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the layout axis of the divider line.
    /// <see cref="Orientation.Horizontal"/> renders a full-width horizontal rule;
    /// <see cref="Orientation.Vertical"/> renders an inline vertical separator.
    /// </summary>
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the divider line is rendered
    /// as a dashed stroke rather than a solid line.
    /// </summary>
    public bool DashedLine
    {
        get => (bool)GetValue(DashedLineProperty);
        set => SetValue(DashedLineProperty, value);
    }

    /// <summary>
    /// Gets or sets an optional text label rendered over the center (or aligned
    /// position) of the divider. Only applicable when <see cref="Orientation"/>
    /// is <see cref="Orientation.Horizontal"/>.
    /// </summary>
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of <see cref="Text"/> relative to
    /// the divider line. Defaults to <see cref="DividerTextAlignment.Center"/>.
    /// </summary>
    public DividerTextAlignment TextAlignment
    {
        get => (DividerTextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }
}
