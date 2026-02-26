using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the shape of an <see cref="Avatar"/>.
/// </summary>
public enum AvatarShape
{
    /// <summary>The avatar renders as a circle.</summary>
    Circle,

    /// <summary>The avatar renders as a square with slightly rounded corners.</summary>
    Square
}

/// <summary>
/// Displays a user or entity representative image, icon, or initials in a
/// uniformly sized container.
/// Follows the Ant Design Avatar specification.
/// </summary>
public class Avatar : Control
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Source"/> dependency property.</summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(
            nameof(Source),
            typeof(ImageSource),
            typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Text"/> dependency property.</summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(double),
            typeof(Avatar),
            new PropertyMetadata(32d, null, CoerceSize));

    /// <summary>Identifies the <see cref="Shape"/> dependency property.</summary>
    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.Register(
            nameof(Shape),
            typeof(AvatarShape),
            typeof(Avatar),
            new PropertyMetadata(AvatarShape.Circle));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Avatar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Avatar),
            new FrameworkPropertyMetadata(typeof(Avatar)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the image source displayed in the avatar.
    /// When set, the image takes priority over <see cref="Icon"/> and <see cref="Text"/>.
    /// </summary>
    public ImageSource? Source
    {
        get => (ImageSource?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// Gets or sets an icon displayed in the avatar when <see cref="Source"/> is null.
    /// Accepts any object; typically a geometry path or glyph element.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the text (usually initials) displayed in the avatar when
    /// both <see cref="Source"/> and <see cref="Icon"/> are null.
    /// </summary>
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the uniform size (width and height) of the avatar in device-independent pixels.
    /// Defaults to 32. Values less than 8 are coerced to 8.
    /// </summary>
    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>Gets or sets the shape of the avatar container.</summary>
    public AvatarShape Shape
    {
        get => (AvatarShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceSize(DependencyObject d, object baseValue)
    {
        double value = (double)baseValue;
        return value < 8d ? 8d : value;
    }
}
