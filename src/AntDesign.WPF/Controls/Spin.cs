using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of a <see cref="Spin"/> indicator.
/// </summary>
public enum SpinSize
{
    /// <summary>Small spinner — suitable for inline use.</summary>
    Small,

    /// <summary>Default-sized spinner.</summary>
    Default,

    /// <summary>Large spinner — suitable for page-level loading.</summary>
    Large
}

/// <summary>
/// A loading spinner that can wrap arbitrary content with a translucent overlay
/// while an asynchronous operation is in progress.
/// The <see cref="ContentControl.Content"/> property holds the content being wrapped.
/// Follows the Ant Design Spin specification.
/// </summary>
public class Spin : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="IsSpinning"/> dependency property.</summary>
    public static readonly DependencyProperty IsSpinningProperty =
        DependencyProperty.Register(
            nameof(IsSpinning),
            typeof(bool),
            typeof(Spin),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Tip"/> dependency property.</summary>
    public static readonly DependencyProperty TipProperty =
        DependencyProperty.Register(
            nameof(Tip),
            typeof(string),
            typeof(Spin),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(SpinSize),
            typeof(Spin),
            new PropertyMetadata(SpinSize.Default));

    /// <summary>Identifies the <see cref="Delay"/> dependency property.</summary>
    public static readonly DependencyProperty DelayProperty =
        DependencyProperty.Register(
            nameof(Delay),
            typeof(int),
            typeof(Spin),
            new PropertyMetadata(0, null, CoerceDelay));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Spin()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Spin),
            new FrameworkPropertyMetadata(typeof(Spin)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets a value indicating whether the spinner overlay is active.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool IsSpinning
    {
        get => (bool)GetValue(IsSpinningProperty);
        set => SetValue(IsSpinningProperty, value);
    }

    /// <summary>
    /// Gets or sets an optional descriptive text rendered below the spinning indicator
    /// while loading is in progress.
    /// </summary>
    public string? Tip
    {
        get => (string?)GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    /// <summary>Gets or sets the size variant of the spinner indicator.</summary>
    public SpinSize Size
    {
        get => (SpinSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the delay in milliseconds before the spinner becomes visible
    /// after <see cref="IsSpinning"/> transitions to <see langword="true"/>.
    /// This prevents flickering for short operations. Defaults to 0 (no delay).
    /// Values less than 0 are coerced to 0.
    /// </summary>
    public int Delay
    {
        get => (int)GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceDelay(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 0 ? 0 : value;
    }
}
