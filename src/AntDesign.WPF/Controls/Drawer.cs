using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using AntDesign.WPF.Automation;
using AntDesign.WPF.Helpers;

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
[TemplatePart(Name = Drawer.PART_Mask,        Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_DrawerPanel, Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_TitleBar,    Type = typeof(FrameworkElement))]
[TemplatePart(Name = Drawer.PART_CloseButton, Type = typeof(System.Windows.Controls.Button))]
public class Drawer : ContentControl
{
    public const string PART_Mask        = "PART_Mask";
    public const string PART_DrawerPanel = "PART_DrawerPanel";
    public const string PART_TitleBar    = "PART_TitleBar";
    public const string PART_CloseButton = "PART_CloseButton";

    private FrameworkElement? _mask;
    private FrameworkElement? _drawerPanel;
    private FrameworkElement? _overlayRoot;
    private TranslateTransform? _translateTransform;

    private static readonly Duration _slideDuration = new(TimeSpan.FromMilliseconds(250));
    private static readonly Duration _fadeDuration  = new(TimeSpan.FromMilliseconds(200));
    private static readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private static readonly CubicEase _easeIn  = new() { EasingMode = EasingMode.EaseIn };

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Drawer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(Drawer),
            new PropertyMetadata(null));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(DrawerPlacement), typeof(Drawer),
            new PropertyMetadata(DrawerPlacement.Right, OnPlacementChanged));

    public static readonly DependencyProperty DrawerWidthProperty =
        DependencyProperty.Register(nameof(DrawerWidth), typeof(double), typeof(Drawer),
            new PropertyMetadata(378d));

    public static readonly DependencyProperty DrawerHeightProperty =
        DependencyProperty.Register(nameof(DrawerHeight), typeof(double), typeof(Drawer),
            new PropertyMetadata(378d));

    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(nameof(Closable), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    public static readonly DependencyProperty MaskClosableProperty =
        DependencyProperty.Register(nameof(MaskClosable), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    public static readonly DependencyProperty MaskProperty =
        DependencyProperty.Register(nameof(Mask), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    public static readonly DependencyProperty DrawerContentProperty =
        DependencyProperty.Register(nameof(DrawerContent), typeof(object), typeof(Drawer),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    public static readonly RoutedEvent OpenedEvent =
        EventManager.RegisterRoutedEvent(nameof(Opened), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawer));

    public event RoutedEventHandler Opened
    {
        add    => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }

    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(nameof(Closed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawer));

    public event RoutedEventHandler Closed
    {
        add    => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Commands
    // -------------------------------------------------------------------------

    public static readonly RoutedCommand OpenCommand  = new(nameof(OpenCommand),  typeof(Drawer));
    public static readonly RoutedCommand CloseCommand = new(nameof(CloseCommand), typeof(Drawer));

    // -------------------------------------------------------------------------
    // Constructors
    // -------------------------------------------------------------------------

    static Drawer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer), new FrameworkPropertyMetadata(typeof(Drawer)));
    }

    private IInputElement? _previousFocus;

    public Drawer()
    {
        CommandBindings.Add(new CommandBinding(OpenCommand,  ExecuteOpen));
        CommandBindings.Add(new CommandBinding(CloseCommand, ExecuteClose));
        PreviewKeyDown += OnPreviewKeyDown;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _previousFocus = null;
        _translateTransform = null;
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public DrawerPlacement Placement
    {
        get => (DrawerPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public double DrawerWidth
    {
        get => (double)GetValue(DrawerWidthProperty);
        set => SetValue(DrawerWidthProperty, value);
    }

    public double DrawerHeight
    {
        get => (double)GetValue(DrawerHeightProperty);
        set => SetValue(DrawerHeightProperty, value);
    }

    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    public bool MaskClosable
    {
        get => (bool)GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
    }

    public bool Mask
    {
        get => (bool)GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }

    public object? DrawerContent
    {
        get => GetValue(DrawerContentProperty);
        set => SetValue(DrawerContentProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _overlayRoot = GetTemplateChild("OverlayRoot") as FrameworkElement;
        _mask = GetTemplateChild(PART_Mask) as FrameworkElement;
        _drawerPanel = GetTemplateChild(PART_DrawerPanel) as FrameworkElement;

        if (_mask is not null)
        {
            _mask.MouseLeftButtonDown -= OnMaskMouseLeftButtonDown;
            _mask.MouseLeftButtonDown += OnMaskMouseLeftButtonDown;
        }

        if (GetTemplateChild(PART_CloseButton) is System.Windows.Controls.Button closeBtn)
        {
            closeBtn.Click -= OnCloseButtonClick;
            closeBtn.Click += OnCloseButtonClick;
        }

        // Set up the translate transform for animations
        if (_drawerPanel is not null)
        {
            _translateTransform = new TranslateTransform();
            _drawerPanel.RenderTransform = _translateTransform;
            ApplyPlacementLayout();
            SetInitialTranslation();
        }
    }

    // -------------------------------------------------------------------------
    // Animation Logic
    // -------------------------------------------------------------------------

    private void ApplyPlacementLayout()
    {
        if (_drawerPanel is null) return;

        switch (Placement)
        {
            case DrawerPlacement.Left:
                _drawerPanel.HorizontalAlignment = HorizontalAlignment.Left;
                _drawerPanel.VerticalAlignment = VerticalAlignment.Stretch;
                _drawerPanel.Width = DrawerWidth;
                _drawerPanel.ClearValue(HeightProperty);
                break;
            case DrawerPlacement.Right:
                _drawerPanel.HorizontalAlignment = HorizontalAlignment.Right;
                _drawerPanel.VerticalAlignment = VerticalAlignment.Stretch;
                _drawerPanel.Width = DrawerWidth;
                _drawerPanel.ClearValue(HeightProperty);
                break;
            case DrawerPlacement.Top:
                _drawerPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                _drawerPanel.VerticalAlignment = VerticalAlignment.Top;
                _drawerPanel.ClearValue(WidthProperty);
                _drawerPanel.Height = DrawerHeight;
                break;
            case DrawerPlacement.Bottom:
                _drawerPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                _drawerPanel.VerticalAlignment = VerticalAlignment.Bottom;
                _drawerPanel.ClearValue(WidthProperty);
                _drawerPanel.Height = DrawerHeight;
                break;
        }
    }

    private void SetInitialTranslation()
    {
        if (_translateTransform is null) return;

        switch (Placement)
        {
            case DrawerPlacement.Right:
                _translateTransform.X = DrawerWidth + 20;
                _translateTransform.Y = 0;
                break;
            case DrawerPlacement.Left:
                _translateTransform.X = -(DrawerWidth + 20);
                _translateTransform.Y = 0;
                break;
            case DrawerPlacement.Top:
                _translateTransform.X = 0;
                _translateTransform.Y = -(DrawerHeight + 20);
                break;
            case DrawerPlacement.Bottom:
                _translateTransform.X = 0;
                _translateTransform.Y = DrawerHeight + 20;
                break;
        }
    }

    private void AnimateOpen()
    {
        if (_overlayRoot is null || _mask is null || _translateTransform is null) return;

        _overlayRoot.Visibility = Visibility.Visible;

        // Mask fade in
        var maskAnim = new DoubleAnimation(0, 1, _fadeDuration);
        _mask.BeginAnimation(OpacityProperty, maskAnim);

        // Determine slide property and target
        string prop = Placement is DrawerPlacement.Left or DrawerPlacement.Right
            ? nameof(TranslateTransform.X)
            : nameof(TranslateTransform.Y);

        SetInitialTranslation(); // ensure starting position

        var slideAnim = new DoubleAnimation(0, _slideDuration) { EasingFunction = _easeOut };
        _translateTransform.BeginAnimation(
            prop == nameof(TranslateTransform.X) ? TranslateTransform.XProperty : TranslateTransform.YProperty,
            slideAnim);
    }

    private void AnimateClose()
    {
        if (_overlayRoot is null || _mask is null || _translateTransform is null) return;

        // Mask fade out
        var maskAnim = new DoubleAnimation(0, _fadeDuration);
        _mask.BeginAnimation(OpacityProperty, maskAnim);

        // Slide out
        double target = Placement switch
        {
            DrawerPlacement.Right  => DrawerWidth + 20,
            DrawerPlacement.Left   => -(DrawerWidth + 20),
            DrawerPlacement.Top    => -(DrawerHeight + 20),
            DrawerPlacement.Bottom => DrawerHeight + 20,
            _ => DrawerWidth + 20
        };

        string prop = Placement is DrawerPlacement.Left or DrawerPlacement.Right
            ? nameof(TranslateTransform.X)
            : nameof(TranslateTransform.Y);

        var slideAnim = new DoubleAnimation(target, _slideDuration) { EasingFunction = _easeIn };
        slideAnim.Completed += (_, _) =>
        {
            if (!IsOpen && _overlayRoot is not null)
                _overlayRoot.Visibility = Visibility.Collapsed;
        };

        _translateTransform.BeginAnimation(
            prop == nameof(TranslateTransform.X) ? TranslateTransform.XProperty : TranslateTransform.YProperty,
            slideAnim);
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var drawer = (Drawer)d;
        if ((bool)e.NewValue)
        {
            drawer._previousFocus = Keyboard.FocusedElement;
            drawer.AnimateOpen();
            drawer.Dispatcher.BeginInvoke(DispatcherPriority.Input, () =>
            {
                if (drawer._drawerPanel != null)
                    FocusTrapHelper.FocusFirst(drawer._drawerPanel);
            });
            drawer.RaiseEvent(new RoutedEventArgs(OpenedEvent, drawer));
        }
        else
        {
            drawer.AnimateClose();
            drawer._previousFocus?.Focus();
            drawer._previousFocus = null;
            drawer.RaiseEvent(new RoutedEventArgs(ClosedEvent, drawer));
        }
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (!IsOpen) return;

        if (e.Key == Key.Escape)
        {
            IsOpen = false;
            e.Handled = true;
            return;
        }

        if (_drawerPanel != null)
            FocusTrapHelper.HandleTabKey(_drawerPanel, e);
    }

    private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var drawer = (Drawer)d;
        drawer.ApplyPlacementLayout();
        drawer.SetInitialTranslation();
    }

    private void OnMaskMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (MaskClosable) IsOpen = false;
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e) => IsOpen = false;
    private void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)  => IsOpen = true;
    private void ExecuteClose(object sender, ExecutedRoutedEventArgs e) => IsOpen = false;

    protected override AutomationPeer OnCreateAutomationPeer()
        => new DrawerAutomationPeer(this);
}
