using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using AntDesign.WPF.Automation;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the visual presentation type of a <see cref="Progress"/> indicator.
/// </summary>
public enum ProgressType
{
    /// <summary>A horizontal bar that fills from left to right.</summary>
    Line,

    /// <summary>A circular ring indicator.</summary>
    Circle,

    /// <summary>A dashboard (arc/gauge) style indicator.</summary>
    Dashboard
}

/// <summary>
/// Specifies the semantic status of a <see cref="Progress"/> indicator.
/// </summary>
public enum ProgressStatus
{
    /// <summary>Normal in-progress state (blue).</summary>
    Normal,

    /// <summary>Animated active state indicating ongoing activity.</summary>
    Active,

    /// <summary>Completed successfully (green).</summary>
    Success,

    /// <summary>Completed with an error (red).</summary>
    Exception
}

/// <summary>
/// Specifies the size variant of a <see cref="Progress"/> indicator.
/// </summary>
public enum ProgressSize
{
    /// <summary>Default-sized progress indicator.</summary>
    Default,

    /// <summary>Small, compact progress indicator.</summary>
    Small
}

/// <summary>
/// Displays progress of an operation as a line, circle, or dashboard gauge.
/// Named <c>Progress</c> to align with the Ant Design component name;
/// references from XAML should be qualified when a namespace conflict exists.
/// Follows the Ant Design Progress specification.
/// </summary>
public class Progress : Control
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Percent"/> dependency property.</summary>
    public static readonly DependencyProperty PercentProperty =
        DependencyProperty.Register(
            nameof(Percent),
            typeof(double),
            typeof(Progress),
            new PropertyMetadata(0d, null, CoercePercent));

    /// <summary>Identifies the <see cref="Type"/> dependency property.</summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(
            nameof(Type),
            typeof(ProgressType),
            typeof(Progress),
            new PropertyMetadata(ProgressType.Line));

    /// <summary>Identifies the <see cref="Status"/> dependency property.</summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(ProgressStatus),
            typeof(Progress),
            new PropertyMetadata(ProgressStatus.Normal));

    /// <summary>Identifies the <see cref="StrokeWidth"/> dependency property.</summary>
    public static readonly DependencyProperty StrokeWidthProperty =
        DependencyProperty.Register(
            nameof(StrokeWidth),
            typeof(double),
            typeof(Progress),
            new PropertyMetadata(8d, null, CoerceStrokeWidth));

    /// <summary>Identifies the <see cref="ShowInfo"/> dependency property.</summary>
    public static readonly DependencyProperty ShowInfoProperty =
        DependencyProperty.Register(
            nameof(ShowInfo),
            typeof(bool),
            typeof(Progress),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(ProgressSize),
            typeof(Progress),
            new PropertyMetadata(ProgressSize.Default));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Progress()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Progress),
            new FrameworkPropertyMetadata(typeof(Progress)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the completion percentage.
    /// Clamped to the range [0, 100].
    /// </summary>
    public double Percent
    {
        get => (double)GetValue(PercentProperty);
        set => SetValue(PercentProperty, value);
    }

    /// <summary>Gets or sets the visual presentation type of the progress indicator.</summary>
    public ProgressType Type
    {
        get => (ProgressType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>Gets or sets the semantic status that controls the indicator color and icon.</summary>
    public ProgressStatus Status
    {
        get => (ProgressStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke (bar or ring) width in device-independent pixels.
    /// Defaults to 8. Values less than 1 are coerced to 1.
    /// </summary>
    public double StrokeWidth
    {
        get => (double)GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the percentage text or completion
    /// icon is displayed alongside the progress bar.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool ShowInfo
    {
        get => (bool)GetValue(ShowInfoProperty);
        set => SetValue(ShowInfoProperty, value);
    }

    /// <summary>Gets or sets the size variant of the progress indicator.</summary>
    public ProgressSize Size
    {
        get => (ProgressSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoercePercent(DependencyObject d, object baseValue)
    {
        double value = (double)baseValue;
        if (value < 0d) return 0d;
        if (value > 100d) return 100d;
        return value;
    }

    private static object CoerceStrokeWidth(DependencyObject d, object baseValue)
    {
        double value = (double)baseValue;
        return value < 1d ? 1d : value;
    }

    /// <inheritdoc/>
    protected override AutomationPeer OnCreateAutomationPeer()
        => new ProgressAutomationPeer(this);
}
