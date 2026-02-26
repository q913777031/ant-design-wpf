using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the layout direction of a <see cref="Steps"/> navigator.
/// </summary>
public enum StepsDirection
{
    /// <summary>Steps are laid out horizontally (left to right).</summary>
    Horizontal,

    /// <summary>Steps are laid out vertically (top to bottom).</summary>
    Vertical
}

/// <summary>
/// Specifies the size variant of a <see cref="Steps"/> navigator.
/// </summary>
public enum StepsSize
{
    /// <summary>Default step size.</summary>
    Default,

    /// <summary>Small, compact step size.</summary>
    Small
}

/// <summary>
/// Specifies the overall status of a <see cref="Steps"/> navigator.
/// </summary>
public enum StepsStatus
{
    /// <summary>Step is waiting to be started.</summary>
    Wait,

    /// <summary>Step is currently in progress.</summary>
    Process,

    /// <summary>Step has been completed.</summary>
    Finish,

    /// <summary>Step finished with an error.</summary>
    Error
}

/// <summary>
/// A step-progress navigator that displays a sequence of steps with status indicators.
/// Each child must be a <see cref="StepItem"/>.
/// Follows the Ant Design Steps specification.
/// </summary>
public class Steps : ItemsControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Current"/> dependency property.</summary>
    public static readonly DependencyProperty CurrentProperty =
        DependencyProperty.Register(
            nameof(Current),
            typeof(int),
            typeof(Steps),
            new PropertyMetadata(0, OnCurrentChanged, CoerceCurrent));

    /// <summary>Identifies the <see cref="Direction"/> dependency property.</summary>
    public static readonly DependencyProperty DirectionProperty =
        DependencyProperty.Register(
            nameof(Direction),
            typeof(StepsDirection),
            typeof(Steps),
            new PropertyMetadata(StepsDirection.Horizontal));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(StepsSize),
            typeof(Steps),
            new PropertyMetadata(StepsSize.Default));

    /// <summary>Identifies the <see cref="Status"/> dependency property.</summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(StepsStatus),
            typeof(Steps),
            new PropertyMetadata(StepsStatus.Process, OnStatusChanged));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Steps()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Steps),
            new FrameworkPropertyMetadata(typeof(Steps)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the zero-based index of the currently active step.
    /// Values are coerced to the range [0, Items.Count - 1].
    /// </summary>
    public int Current
    {
        get => (int)GetValue(CurrentProperty);
        set => SetValue(CurrentProperty, value);
    }

    /// <summary>Gets or sets the layout direction of the steps.</summary>
    public StepsDirection Direction
    {
        get => (StepsDirection)GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    /// <summary>Gets or sets the size variant of the steps navigator.</summary>
    public StepsSize Size
    {
        get => (StepsSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the status applied to the current step (index == <see cref="Current"/>).
    /// Steps before Current are always Finish; steps after are always Wait.
    /// </summary>
    public StepsStatus Status
    {
        get => (StepsStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    // -------------------------------------------------------------------------
    // Item container override
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override bool IsItemItsOwnContainerOverride(object item) =>
        item is StepItem;

    /// <inheritdoc/>
    protected override DependencyObject GetContainerForItemOverride() =>
        new StepItem();

    /// <inheritdoc/>
    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);
        UpdateItemStatuses();
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceCurrent(DependencyObject d, object baseValue)
    {
        var steps = (Steps)d;
        int value = (int)baseValue;
        int max = steps.Items.Count > 0 ? steps.Items.Count - 1 : 0;

        if (value < 0) return 0;
        if (value > max) return max;
        return value;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((Steps)d).UpdateItemStatuses();
    }

    private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((Steps)d).UpdateItemStatuses();
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void UpdateItemStatuses()
    {
        int current = Current;

        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is StepItem stepItem
                && !stepItem.HasLocalStatus)
            {
                if (i < current)
                    stepItem.ComputedStatus = StepsStatus.Finish;
                else if (i == current)
                    stepItem.ComputedStatus = Status;
                else
                    stepItem.ComputedStatus = StepsStatus.Wait;
            }
        }
    }
}
