using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the semantic type of a <see cref="MessageItem"/>.
/// </summary>
public enum MessageType
{
    /// <summary>Informational message.</summary>
    Info,

    /// <summary>Success message.</summary>
    Success,

    /// <summary>Warning message.</summary>
    Warning,

    /// <summary>Error message.</summary>
    Error,

    /// <summary>Loading / in-progress message.</summary>
    Loading
}

/// <summary>
/// Represents a single floating toast message displayed by <see cref="MessageContainer"/>.
/// </summary>
public sealed class MessageItem : DependencyObject
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Content"/> dependency property.</summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(
            nameof(Content),
            typeof(string),
            typeof(MessageItem),
            new PropertyMetadata(string.Empty));

    /// <summary>Identifies the <see cref="Type"/> dependency property.</summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(
            nameof(Type),
            typeof(MessageType),
            typeof(MessageItem),
            new PropertyMetadata(MessageType.Info));

    /// <summary>Identifies the <see cref="Duration"/> dependency property.</summary>
    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register(
            nameof(Duration),
            typeof(double),
            typeof(MessageItem),
            new PropertyMetadata(3d));

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the message text.</summary>
    public string Content
    {
        get => (string)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>Gets or sets the semantic type of the message.</summary>
    public MessageType Type
    {
        get => (MessageType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>Gets or sets how many seconds the message is visible before auto-dismissal. Defaults to 3.</summary>
    public double Duration
    {
        get => (double)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }
}

/// <summary>
/// Internal <see cref="ItemsControl"/> that hosts <see cref="MessageItem"/> instances in a
/// top-centered stack and auto-dismisses each item after its <see cref="MessageItem.Duration"/>.
/// </summary>
/// <remarks>
/// This control is intended to be created programmatically by <see cref="MessageService"/>
/// and inserted into the adorner layer of the active window.
/// </remarks>
[TemplatePart(Name = MessageContainer.PART_ItemsHost, Type = typeof(StackPanel))]
internal sealed class MessageContainer : ItemsControl
{
    /// <summary>Template part name for the items host stack panel.</summary>
    public const string PART_ItemsHost = "PART_ItemsHost";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Items"/> dependency property (observable collection shadow).</summary>
    public static readonly DependencyProperty MessageItemsProperty =
        DependencyProperty.Register(
            nameof(MessageItems),
            typeof(ObservableCollection<MessageItem>),
            typeof(MessageContainer),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Static constructor
    // -------------------------------------------------------------------------

    static MessageContainer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(MessageContainer),
            new FrameworkPropertyMetadata(typeof(MessageContainer)));
    }

    // -------------------------------------------------------------------------
    // Constructor
    // -------------------------------------------------------------------------

    /// <summary>Initializes a new instance of the <see cref="MessageContainer"/> class.</summary>
    public MessageContainer()
    {
        MessageItems = new ObservableCollection<MessageItem>();
        ItemsSource  = MessageItems;

        // Ensure the container is not hit-test visible so it does not block input
        // on the underlying content.
        IsHitTestVisible = false;
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment   = VerticalAlignment.Top;
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets the collection of currently active message items.</summary>
    public ObservableCollection<MessageItem> MessageItems
    {
        get => (ObservableCollection<MessageItem>)GetValue(MessageItemsProperty);
        private set => SetValue(MessageItemsProperty, value);
    }

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    /// <summary>
    /// Adds a <see cref="MessageItem"/> to the container and schedules its removal
    /// after <see cref="MessageItem.Duration"/> seconds.
    /// </summary>
    public void Add(MessageItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        MessageItems.Add(item);

        // Re-enable hit-test only for the items themselves (not the full container)
        IsHitTestVisible = true;

        var timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher)
        {
            Interval = TimeSpan.FromSeconds(item.Duration > 0 ? item.Duration : 3d)
        };

        timer.Tick += (_, _) =>
        {
            timer.Stop();
            Remove(item);
        };

        timer.Start();
    }

    /// <summary>Removes the specified <see cref="MessageItem"/> from the container.</summary>
    public void Remove(MessageItem item)
    {
        MessageItems.Remove(item);
        if (MessageItems.Count == 0)
            IsHitTestVisible = false;
    }

    // -------------------------------------------------------------------------
    // Adorner integration helper
    // -------------------------------------------------------------------------

    /// <summary>
    /// Ensures a <see cref="MessageContainer"/> exists in the adorner layer of
    /// <paramref name="window"/> and returns it.
    /// </summary>
    internal static MessageContainer EnsureContainer(Window window)
    {
        var adornerLayer = AdornerLayer.GetAdornerLayer(window.Content as UIElement
            ?? throw new InvalidOperationException("Window.Content must be a UIElement."));

        if (adornerLayer is null)
            throw new InvalidOperationException("Could not obtain AdornerLayer from the window content.");

        // Check if a container is already attached
        var existing = adornerLayer.GetAdorners(window.Content as UIElement);
        if (existing is not null)
        {
            foreach (var adorner in existing)
            {
                if (adorner is MessageContainerAdorner mca)
                    return mca.Container;
            }
        }

        var container = new MessageContainer();
        var newAdorner = new MessageContainerAdorner((UIElement)window.Content, container);
        adornerLayer.Add(newAdorner);
        return container;
    }
}

/// <summary>
/// Lightweight <see cref="Adorner"/> that hosts a <see cref="MessageContainer"/>
/// in the adorner layer of a window.
/// </summary>
internal sealed class MessageContainerAdorner : Adorner
{
    private readonly MessageContainer _container;

    /// <summary>Gets the hosted <see cref="MessageContainer"/>.</summary>
    public MessageContainer Container => _container;

    /// <summary>Initializes a new instance of <see cref="MessageContainerAdorner"/>.</summary>
    public MessageContainerAdorner(UIElement adornedElement, MessageContainer container)
        : base(adornedElement)
    {
        _container = container ?? throw new ArgumentNullException(nameof(container));
        AddVisualChild(_container);
        AddLogicalChild(_container);
        IsHitTestVisible = false;
    }

    /// <inheritdoc/>
    protected override int VisualChildrenCount => 1;

    /// <inheritdoc/>
    protected override Visual GetVisualChild(int index)
    {
        if (index != 0) throw new ArgumentOutOfRangeException(nameof(index));
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
