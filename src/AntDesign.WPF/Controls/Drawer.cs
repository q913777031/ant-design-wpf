using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the edge from which the <see cref="Drawer"/> slides in.
/// </summary>
public enum DrawerPlacement
{
    /// <summary>The drawer slides in from the left edge.</summary>
    Left,

    /// <summary>The drawer slides in from the right edge (default).</summary>
    Right,

    /// <summary>The drawer slides in from the top edge.</summary>
    Top,

    /// <summary>The drawer slides in from the bottom edge.</summary>
    Bottom
}

/// <summary>
/// A slide-in panel that renders on top of the host content behind a semi-transparent mask.
/// Follows the Ant Design Drawer specification.
/// </summary>
/// <remarks>
/// <see cref="ContentControl.Content"/> represents the underlying page content;
/// the drawer panel itself is the overlay driven by <see cref="IsOpen"/>.
/// </remarks>
[TemplatePart(Name = Drawer.PART_Mask,        Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_DrawerPanel, Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_TitleBar,    Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_CloseButton, Type = typeof(System.Windows.Controls.Button))]
[TemplateVisualState(Name = Drawer.STATE_Open,            GroupName = Drawer.GROUP_OpenState)]
[TemplateVisualState(Name = Drawer.STATE_Closed,          GroupName = Drawer.GROUP_OpenState)]
[TemplateVisualState(Name = Drawer.STATE_PlacementLeft,   GroupName = Drawer.GROUP_Placement)]
[TemplateVisualState(Name = Drawer.STATE_PlacementRight,  GroupName = Drawer.GROUP_Placement)]
[TemplateVisualState(Name = Drawer.STATE_PlacementTop,    GroupName = Drawer.GROUP_Placement)]
[TemplateVisualState(Name = Drawer.STATE_PlacementBottom, GroupName = Drawer.GROUP_Placement)]
public class Drawer : ContentControl
{
    // -------------------------------------------------------------------------
    // Template part and visual-state constants
    // -------------------------------------------------------------------------

    /// <summary>Template part name for the semi-transparent background mask.</summary>
    public const string PART_Mask        = "PART_Mask";

    /// <summary>Template part name for the sliding drawer panel.</summary>
    public const string PART_DrawerPanel = "PART_DrawerPanel";

    /// <summary>Template part name for the title-bar region.</summary>
    public const string PART_TitleBar    = "PART_TitleBar";

    /// <summary>Template part name for the close (X) button.</summary>
    public const string PART_CloseButton = "PART_CloseButton";

    /// <summary>Visual state group name for open/closed state.</summary>
    public const string GROUP_OpenState  = "OpenStates";

    /// <summary>Visual state group name for placement.</summary>
    public const string GROUP_Placement  = "PlacementStates";

    /// <summary>Visual state name when the drawer is visible.</summary>
    public const string STATE_Open            = "Open";

    /// <summary>Visual state name when the drawer is hidden.</summary>
    public const string STATE_Closed          = "Closed";

    /// <summary>Visual state name for left placement.</summary>
    public const string STATE_PlacementLeft   = "PlacementLeft";

    /// <summary>Visual state name for right placement.</summary>
    public const string STATE_PlacementRight  = "PlacementRight";

    /// <summary>Visual state name for top placement.</summary>
    public const string STATE_PlacementTop    = "PlacementTop";

    /// <summary>Visual state name for bottom placement.</summary>
    public const string STATE_PlacementBottom = "PlacementBottom";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(Drawer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsOpenChanged));

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Drawer),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Placement"/> dependency property.</summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(
            nameof(Placement),
            typeof(DrawerPlacement),
            typeof(Drawer),
            new PropertyMetadata(DrawerPlacement.Right, OnPlacementChanged));

    /// <summary>Identifies the <see cref="DrawerWidth"/> dependency property.</summary>
    public static readonly DependencyProperty DrawerWidthProperty =
        DependencyProperty.Register(
            nameof(DrawerWidth),
            typeof(double),
            typeof(Drawer),
            new PropertyMetadata(378d));

    /// <summary>Identifies the <see cref="DrawerHeight"/> dependency property.</summary>
    public static readonly DependencyProperty DrawerHeightProperty =
        DependencyProperty.Register(
            nameof(DrawerHeight),
            typeof(double),
            typeof(Drawer),
            new PropertyMetadata(378d));

    /// <summary>Identifies the <see cref="Closable"/> dependency property.</summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(
            nameof(Closable),
            typeof(bool),
            typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="MaskClosable"/> dependency property.</summary>
    public static readonly DependencyProperty MaskClosableProperty =
        DependencyProperty.Register(
            nameof(MaskClosable),
            typeof(bool),
            typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Mask"/> dependency property.</summary>
    public static readonly DependencyProperty MaskProperty =
        DependencyProperty.Register(
            nameof(Mask),
            typeof(bool),
            typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="DrawerContent"/> dependency property.</summary>
    public static readonly DependencyProperty DrawerContentProperty =
        DependencyProperty.Register(
            nameof(DrawerContent),
            typeof(object),
            typeof(Drawer),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Opened"/> routed event.</summary>
    public static readonly RoutedEvent OpenedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Opened),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Drawer));

    /// <summary>Raised when the drawer becomes visible.</summary>
    public event RoutedEventHandler Opened
    {
        add    => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }

    /// <summary>Identifies the <see cref="Closed"/> routed event.</summary>
    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Closed),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Drawer));

    /// <summary>Raised when the drawer is dismissed.</summary>
    public event RoutedEventHandler Closed
    {
        add    => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Commands
    // -------------------------------------------------------------------------

    /// <summary>Command that opens the drawer.</summary>
    public static readonly RoutedCommand OpenCommand = new(nameof(OpenCommand), typeof(Drawer));

    /// <summary>Command that closes the drawer.</summary>
    public static readonly RoutedCommand CloseCommand = new(nameof(CloseCommand), typeof(Drawer));

    // -------------------------------------------------------------------------
    // Static constructor
    // -------------------------------------------------------------------------

    static Drawer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Drawer),
            new FrameworkPropertyMetadata(typeof(Drawer)));
    }

    // -------------------------------------------------------------------------
    // Constructor
    // -------------------------------------------------------------------------

    /// <summary>Initializes a new instance of the <see cref="Drawer"/> class.</summary>
    public Drawer()
    {
        CommandBindings.Add(new CommandBinding(OpenCommand,  ExecuteOpen));
        CommandBindings.Add(new CommandBinding(CloseCommand, ExecuteClose));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets a value indicating whether the drawer is visible.</summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>Gets or sets the drawer panel title text.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>Gets or sets the edge from which the drawer slides in. Defaults to <see cref="DrawerPlacement.Right"/>.</summary>
    public DrawerPlacement Placement
    {
        get => (DrawerPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>Gets or sets the pixel width of the drawer panel (used for Left/Right placement). Defaults to 378.</summary>
    public double DrawerWidth
    {
        get => (double)GetValue(DrawerWidthProperty);
        set => SetValue(DrawerWidthProperty, value);
    }

    /// <summary>Gets or sets the pixel height of the drawer panel (used for Top/Bottom placement). Defaults to 378.</summary>
    public double DrawerHeight
    {
        get => (double)GetValue(DrawerHeightProperty);
        set => SetValue(DrawerHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the close (X) button is visible.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether clicking the mask closes the drawer.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool MaskClosable
    {
        get => (bool)GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the semi-transparent mask is rendered.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool Mask
    {
        get => (bool)GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }

    /// <summary>Gets or sets the content displayed inside the drawer panel.</summary>
    public object? DrawerContent
    {
        get => GetValue(DrawerContentProperty);
        set => SetValue(DrawerContentProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(PART_Mask) is FrameworkElement mask)
        {
            mask.MouseLeftButtonDown -= OnMaskMouseLeftButtonDown;
            mask.MouseLeftButtonDown += OnMaskMouseLeftButtonDown;
        }

        if (GetTemplateChild(PART_CloseButton) is System.Windows.Controls.Button closeBtn)
        {
            closeBtn.Click -= OnCloseButtonClick;
            closeBtn.Click += OnCloseButtonClick;
        }

        UpdateVisualState(false);
        UpdatePlacementState(false);
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private void UpdateVisualState(bool useTransitions)
    {
        VisualStateManager.GoToState(this, IsOpen ? STATE_Open : STATE_Closed, useTransitions);
    }

    private void UpdatePlacementState(bool useTransitions)
    {
        string state = Placement switch
        {
            DrawerPlacement.Left   => STATE_PlacementLeft,
            DrawerPlacement.Top    => STATE_PlacementTop,
            DrawerPlacement.Bottom => STATE_PlacementBottom,
            _                      => STATE_PlacementRight
        };
        VisualStateManager.GoToState(this, state, useTransitions);
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var drawer = (Drawer)d;
        drawer.UpdateVisualState(true);

        if ((bool)e.NewValue)
            drawer.RaiseEvent(new RoutedEventArgs(OpenedEvent, drawer));
        else
            drawer.RaiseEvent(new RoutedEventArgs(ClosedEvent, drawer));
    }

    private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((Drawer)d).UpdatePlacementState(true);
    }

    private void OnMaskMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (MaskClosable) IsOpen = false;
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
    }

    private void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)  => IsOpen = true;
    private void ExecuteClose(object sender, ExecutedRoutedEventArgs e) => IsOpen = false;
}
