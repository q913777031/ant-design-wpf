using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the screen corner in which a <see cref="NotificationContainer"/> renders its items.
/// </summary>
public enum NotificationPlacement
{
    /// <summary>Top-right corner (default).</summary>
    TopRight,

    /// <summary>Top-left corner.</summary>
    TopLeft,

    /// <summary>Bottom-right corner.</summary>
    BottomRight,

    /// <summary>Bottom-left corner.</summary>
    BottomLeft
}

/// <summary>
/// Holds the configuration for a single notification item created via <see cref="NotificationService"/>.
/// </summary>
public sealed class NotificationConfig : DependencyObject
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Message"/> dependency property.</summary>
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(
            nameof(Message),
            typeof(string),
            typeof(NotificationConfig),
            new PropertyMetadata(string.Empty));

    /// <summary>Identifies the <see cref="Description"/> dependency property.</summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(NotificationConfig),
            new PropertyMetadata(string.Empty));

    /// <summary>Identifies the <see cref="Duration"/> dependency property.</summary>
    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register(
            nameof(Duration),
            typeof(double),
            typeof(NotificationConfig),
            new PropertyMetadata(4.5d));

    /// <summary>Identifies the <see cref="Placement"/> dependency property.</summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(
            nameof(Placement),
            typeof(NotificationPlacement),
            typeof(NotificationConfig),
            new PropertyMetadata(NotificationPlacement.TopRight));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(NotificationConfig),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="CloseIcon"/> dependency property.</summary>
    public static readonly DependencyProperty CloseIconProperty =
        DependencyProperty.Register(
            nameof(CloseIcon),
            typeof(object),
            typeof(NotificationConfig),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Type"/> dependency property.</summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(
            nameof(Type),
            typeof(NotificationType),
            typeof(NotificationConfig),
            new PropertyMetadata(NotificationType.None));

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the notification headline text.</summary>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>Gets or sets the optional notification body text.</summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the auto-dismiss duration in seconds. Defaults to 4.5.
    /// Set to 0 or a negative value to disable auto-dismissal.
    /// </summary>
    public double Duration
    {
        get => (double)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    /// <summary>Gets or sets the corner placement. Defaults to <see cref="NotificationPlacement.TopRight"/>.</summary>
    public NotificationPlacement Placement
    {
        get => (NotificationPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>Gets or sets a custom icon element or resource shown alongside the notification.</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>Gets or sets a custom close icon element or resource.</summary>
    public object? CloseIcon
    {
        get => GetValue(CloseIconProperty);
        set => SetValue(CloseIconProperty, value);
    }

    /// <summary>Gets or sets the semantic type used for styling. Defaults to <see cref="NotificationType.None"/>.</summary>
    public NotificationType Type
    {
        get => (NotificationType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}

/// <summary>
/// Semantic type for a notification, used to apply the appropriate icon and color style.
/// </summary>
public enum NotificationType
{
    /// <summary>No specific type — uses a generic style.</summary>
    None,

    /// <summary>Success notification.</summary>
    Success,

    /// <summary>Informational notification.</summary>
    Info,

    /// <summary>Warning notification.</summary>
    Warning,

    /// <summary>Error notification.</summary>
    Error
}

/// <summary>
/// Internal <see cref="ItemsControl"/> that hosts <see cref="NotificationConfig"/> items
/// stacked in a specific screen corner, with auto-dismissal support.
/// </summary>
/// <remarks>
/// Instances are created programmatically by <see cref="NotificationService"/> and
/// attached to the adorner layer of the active window.
/// </remarks>
[TemplatePart(Name = NotificationContainer.PART_ItemsHost, Type = typeof(StackPanel))]
[TemplateVisualState(Name = NotificationContainer.STATE_TopRight,    GroupName = NotificationContainer.GROUP_Placement)]
[TemplateVisualState(Name = NotificationContainer.STATE_TopLeft,     GroupName = NotificationContainer.GROUP_Placement)]
[TemplateVisualState(Name = NotificationContainer.STATE_BottomRight, GroupName = NotificationContainer.GROUP_Placement)]
[TemplateVisualState(Name = NotificationContainer.STATE_BottomLeft,  GroupName = NotificationContainer.GROUP_Placement)]
internal sealed class NotificationContainer : ItemsControl
{
    /// <summary>Template part name for the items host stack panel.</summary>
    public const string PART_ItemsHost = "PART_ItemsHost";

    /// <summary>Visual state group for placement.</summary>
    public const string GROUP_Placement     = "PlacementStates";

    /// <summary>Visual state for top-right placement.</summary>
    public const string STATE_TopRight    = "TopRight";

    /// <summary>Visual state for top-left placement.</summary>
    public const string STATE_TopLeft     = "TopLeft";

    /// <summary>Visual state for bottom-right placement.</summary>
    public const string STATE_BottomRight = "BottomRight";

    /// <summary>Visual state for bottom-left placement.</summary>
    public const string STATE_BottomLeft  = "BottomLeft";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="NotificationItems"/> dependency property.</summary>
    public static readonly DependencyProperty NotificationItemsProperty =
        DependencyProperty.Register(
            nameof(NotificationItems),
            typeof(ObservableCollection<NotificationConfig>),
            typeof(NotificationContainer),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Placement"/> dependency property.</summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(
            nameof(Placement),
            typeof(NotificationPlacement),
            typeof(NotificationContainer),
            new PropertyMetadata(NotificationPlacement.TopRight, OnPlacementChanged));

    // -------------------------------------------------------------------------
    // Static constructor
    // -------------------------------------------------------------------------

    static NotificationContainer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NotificationContainer),
            new FrameworkPropertyMetadata(typeof(NotificationContainer)));
    }

    // -------------------------------------------------------------------------
    // Constructor
    // -------------------------------------------------------------------------

    private readonly List<DispatcherTimer> _activeTimers = new();

    /// <summary>Initializes a new instance of the <see cref="NotificationContainer"/> class.</summary>
    public NotificationContainer(NotificationPlacement placement)
    {
        NotificationItems = new ObservableCollection<NotificationConfig>();
        ItemsSource       = NotificationItems;
        Placement         = placement;
        IsHitTestVisible  = false;

        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment   = VerticalAlignment.Stretch;

        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        foreach (var timer in _activeTimers)
            timer.Stop();
        _activeTimers.Clear();
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets the collection of currently active notification items.</summary>
    public ObservableCollection<NotificationConfig> NotificationItems
    {
        get => (ObservableCollection<NotificationConfig>)GetValue(NotificationItemsProperty);
        private set => SetValue(NotificationItemsProperty, value);
    }

    /// <summary>Gets or sets the corner placement for this container.</summary>
    public NotificationPlacement Placement
    {
        get => (NotificationPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdatePlacementState(false);
    }

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    /// <summary>
    /// Adds a <see cref="NotificationConfig"/> to the container and schedules auto-dismissal.
    /// </summary>
    public void Add(NotificationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        NotificationItems.Add(config);
        IsHitTestVisible = true;

        if (config.Duration > 0)
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher)
            {
                Interval = TimeSpan.FromSeconds(config.Duration)
            };

            _activeTimers.Add(timer);
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                _activeTimers.Remove(timer);
                Remove(config);
            };

            timer.Start();
        }
    }

    /// <summary>Removes the specified <see cref="NotificationConfig"/> from the container.</summary>
    public void Remove(NotificationConfig config)
    {
        NotificationItems.Remove(config);
        if (NotificationItems.Count == 0)
            IsHitTestVisible = false;
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private void UpdatePlacementState(bool useTransitions)
    {
        string state = Placement switch
        {
            NotificationPlacement.TopLeft     => STATE_TopLeft,
            NotificationPlacement.BottomRight => STATE_BottomRight,
            NotificationPlacement.BottomLeft  => STATE_BottomLeft,
            _                                  => STATE_TopRight
        };
        VisualStateManager.GoToState(this, state, useTransitions);
    }

    private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((NotificationContainer)d).UpdatePlacementState(true);
    }

    // -------------------------------------------------------------------------
    // Adorner integration helper
    // -------------------------------------------------------------------------

    /// <summary>
    /// Ensures a <see cref="NotificationContainer"/> for <paramref name="placement"/> exists
    /// in the adorner layer of <paramref name="window"/> and returns it.
    /// </summary>
    internal static NotificationContainer EnsureContainer(Window window, NotificationPlacement placement)
    {
        var contentElement = window.Content as UIElement
            ?? throw new InvalidOperationException("Window.Content must be a UIElement.");

        var adornerLayer = AdornerLayer.GetAdornerLayer(contentElement)
            ?? throw new InvalidOperationException("Could not obtain AdornerLayer from the window content.");

        var existing = adornerLayer.GetAdorners(contentElement);
        if (existing is not null)
        {
            foreach (var adorner in existing)
            {
                if (adorner is NotificationContainerAdorner nca && nca.Placement == placement)
                    return nca.Container;
            }
        }

        var container = new NotificationContainer(placement);
        var newAdorner = new NotificationContainerAdorner(contentElement, container, placement);
        adornerLayer.Add(newAdorner);
        return container;
    }
}

/// <summary>
/// Lightweight <see cref="Adorner"/> that hosts a <see cref="NotificationContainer"/>
/// in the adorner layer.
/// </summary>
internal sealed class NotificationContainerAdorner : Adorner
{
    private readonly NotificationContainer _container;

    /// <summary>Gets the corner placement this adorner represents.</summary>
    public NotificationPlacement Placement { get; }

    /// <summary>Gets the hosted <see cref="NotificationContainer"/>.</summary>
    public NotificationContainer Container => _container;

    /// <summary>Initializes a new instance of <see cref="NotificationContainerAdorner"/>.</summary>
    public NotificationContainerAdorner(
        UIElement adornedElement,
        NotificationContainer container,
        NotificationPlacement placement)
        : base(adornedElement)
    {
        ArgumentNullException.ThrowIfNull(container);
        _container = container;
        Placement  = placement;
        AddVisualChild(_container);
        AddLogicalChild(_container);
        IsHitTestVisible = false;
    }

    /// <inheritdoc/>
    protected override int VisualChildrenCount => 1;

    /// <inheritdoc/>
    protected override Visual GetVisualChild(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(index, 0);
        return _container;
    }

    /// <inheritdoc/>
    protected override Size MeasureOverride(Size constraint)
    {
        _container.Measure(constraint);
        return _container.DesiredSize;
    }

    /// <inheritdoc/>
    protected override Size ArrangeOverride(Size finalSize)
    {
        _container.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
        return finalSize;
    }
}
