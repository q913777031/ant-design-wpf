using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the alignment mode of a <see cref="Timeline"/>.
/// </summary>
public enum TimelineMode
{
    /// <summary>
    /// All content appears to the right of the timeline axis (default).
    /// </summary>
    Left,

    /// <summary>
    /// Content alternates left and right of the timeline axis.
    /// </summary>
    Alternate,

    /// <summary>
    /// All content appears to the left of the timeline axis.
    /// </summary>
    Right
}

/// <summary>
/// A vertical timeline that presents a sequence of events in chronological order.
/// Each child must be a <see cref="TimelineItem"/>.
/// Follows the Ant Design Timeline specification.
/// </summary>
public class Timeline : ItemsControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Mode"/> dependency property.</summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(
            nameof(Mode),
            typeof(TimelineMode),
            typeof(Timeline),
            new PropertyMetadata(TimelineMode.Left));

    /// <summary>Identifies the <see cref="Pending"/> dependency property.</summary>
    public static readonly DependencyProperty PendingProperty =
        DependencyProperty.Register(
            nameof(Pending),
            typeof(bool),
            typeof(Timeline),
            new PropertyMetadata(false));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Timeline()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Timeline),
            new FrameworkPropertyMetadata(typeof(Timeline)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the alignment mode that determines whether content is
    /// placed uniformly to one side or alternates between left and right.
    /// </summary>
    public TimelineMode Mode
    {
        get => (TimelineMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the last timeline item is rendered
    /// as a pending (in-progress) entry with a loading indicator.
    /// </summary>
    public bool Pending
    {
        get => (bool)GetValue(PendingProperty);
        set => SetValue(PendingProperty, value);
    }

    // -------------------------------------------------------------------------
    // Item container override
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override bool IsItemItsOwnContainerOverride(object item) =>
        item is TimelineItem;

    /// <inheritdoc/>
    protected override DependencyObject GetContainerForItemOverride() =>
        new TimelineItem();
}
