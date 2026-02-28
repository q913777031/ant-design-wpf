using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AntDesign.WPF.Automation;
using AntDesign.WPF.Helpers;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Modal dialog overlay that renders a semi-transparent mask over the host content.
/// Follows the Ant Design Modal specification.
/// </summary>
/// <remarks>
/// The <see cref="Modal"/> control inherits <see cref="ContentControl"/>; the
/// <see cref="ContentControl.Content"/> property represents the underlying page content
/// while the dialog is surfaced as an overlay controlled by <see cref="IsOpen"/>.
/// </remarks>
[TemplatePart(Name = Modal.PART_Mask,          Type = typeof(FrameworkElement))]
[TemplatePart(Name = Modal.PART_DialogRoot,    Type = typeof(FrameworkElement))]
[TemplatePart(Name = Modal.PART_TitleBar,      Type = typeof(FrameworkElement))]
[TemplatePart(Name = Modal.PART_CloseButton,   Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = Modal.PART_OkButton,      Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = Modal.PART_CancelButton,  Type = typeof(System.Windows.Controls.Button))]
[TemplateVisualState(Name = Modal.STATE_Open,   GroupName = Modal.GROUP_OpenState)]
[TemplateVisualState(Name = Modal.STATE_Closed, GroupName = Modal.GROUP_OpenState)]
public class Modal : ContentControl
{
    // -------------------------------------------------------------------------
    // Template part and visual-state constants
    // -------------------------------------------------------------------------

    /// <summary>Template part name for the semi-transparent background mask.</summary>
    public const string PART_Mask         = "PART_Mask";

    /// <summary>Template part name for the dialog root panel.</summary>
    public const string PART_DialogRoot   = "PART_DialogRoot";

    /// <summary>Template part name for the title-bar region.</summary>
    public const string PART_TitleBar     = "PART_TitleBar";

    /// <summary>Template part name for the close (X) button.</summary>
    public const string PART_CloseButton  = "PART_CloseButton";

    /// <summary>Template part name for the OK confirmation button.</summary>
    public const string PART_OkButton     = "PART_OkButton";

    /// <summary>Template part name for the Cancel button.</summary>
    public const string PART_CancelButton = "PART_CancelButton";

    /// <summary>Visual state group name for open/closed state.</summary>
    public const string GROUP_OpenState = "OpenStates";

    /// <summary>Visual state name when the dialog is visible.</summary>
    public const string STATE_Open   = "Open";

    /// <summary>Visual state name when the dialog is hidden.</summary>
    public const string STATE_Closed = "Closed";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(Modal),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsOpenChanged));

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Modal),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="DialogWidth"/> dependency property.</summary>
    public static readonly DependencyProperty DialogWidthProperty =
        DependencyProperty.Register(
            nameof(DialogWidth),
            typeof(double),
            typeof(Modal),
            new PropertyMetadata(520d));

    /// <summary>Identifies the <see cref="MaskClosable"/> dependency property.</summary>
    public static readonly DependencyProperty MaskClosableProperty =
        DependencyProperty.Register(
            nameof(MaskClosable),
            typeof(bool),
            typeof(Modal),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Closable"/> dependency property.</summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(
            nameof(Closable),
            typeof(bool),
            typeof(Modal),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="OkText"/> dependency property.</summary>
    public static readonly DependencyProperty OkTextProperty =
        DependencyProperty.Register(
            nameof(OkText),
            typeof(string),
            typeof(Modal),
            new PropertyMetadata("OK"));

    /// <summary>Identifies the <see cref="CancelText"/> dependency property.</summary>
    public static readonly DependencyProperty CancelTextProperty =
        DependencyProperty.Register(
            nameof(CancelText),
            typeof(string),
            typeof(Modal),
            new PropertyMetadata("Cancel"));

    /// <summary>Identifies the <see cref="Footer"/> dependency property.</summary>
    public static readonly DependencyProperty FooterProperty =
        DependencyProperty.Register(
            nameof(Footer),
            typeof(object),
            typeof(Modal),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="DialogContent"/> dependency property.</summary>
    public static readonly DependencyProperty DialogContentProperty =
        DependencyProperty.Register(
            nameof(DialogContent),
            typeof(object),
            typeof(Modal),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Attached Property: DialogContent
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <c>Modal.DialogContent</c> attached property.</summary>
    public static readonly DependencyProperty DialogContentAttachedProperty =
        DependencyProperty.RegisterAttached(
            "DialogContent",
            typeof(object),
            typeof(Modal),
            new PropertyMetadata(null));

    /// <summary>Gets the attached dialog content for the target element.</summary>
    public static object? GetDialogContent(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(DialogContentAttachedProperty);
    }

    /// <summary>Sets the attached dialog content on the target element.</summary>
    public static void SetDialogContent(DependencyObject element, object? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(DialogContentAttachedProperty, value);
    }

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Opened"/> routed event.</summary>
    public static readonly RoutedEvent OpenedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Opened),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Modal));

    /// <summary>Raised when the modal dialog becomes visible.</summary>
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
            typeof(Modal));

    /// <summary>Raised when the modal dialog is dismissed.</summary>
    public event RoutedEventHandler Closed
    {
        add    => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    /// <summary>Identifies the <see cref="OkClicked"/> routed event.</summary>
    public static readonly RoutedEvent OkClickedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(OkClicked),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Modal));

    /// <summary>Raised when the OK button is clicked.</summary>
    public event RoutedEventHandler OkClicked
    {
        add    => AddHandler(OkClickedEvent, value);
        remove => RemoveHandler(OkClickedEvent, value);
    }

    /// <summary>Identifies the <see cref="CancelClicked"/> routed event.</summary>
    public static readonly RoutedEvent CancelClickedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(CancelClicked),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Modal));

    /// <summary>Raised when the Cancel button is clicked.</summary>
    public event RoutedEventHandler CancelClicked
    {
        add    => AddHandler(CancelClickedEvent, value);
        remove => RemoveHandler(CancelClickedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Commands
    // -------------------------------------------------------------------------

    /// <summary>Command that opens the dialog (sets <see cref="IsOpen"/> to <see langword="true"/>).</summary>
    public static readonly RoutedCommand OpenCommand = new(nameof(OpenCommand), typeof(Modal));

    /// <summary>Command that closes the dialog (sets <see cref="IsOpen"/> to <see langword="false"/>).</summary>
    public static readonly RoutedCommand CloseCommand = new(nameof(CloseCommand), typeof(Modal));

    // -------------------------------------------------------------------------
    // Static constructor
    // -------------------------------------------------------------------------

    static Modal()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Modal),
            new FrameworkPropertyMetadata(typeof(Modal)));
    }

    // -------------------------------------------------------------------------
    // Constructor
    // -------------------------------------------------------------------------

    private IInputElement? _previousFocus;

    /// <summary>Initializes a new instance of the <see cref="Modal"/> class.</summary>
    public Modal()
    {
        CommandBindings.Add(new CommandBinding(OpenCommand,  ExecuteOpen));
        CommandBindings.Add(new CommandBinding(CloseCommand, ExecuteClose));
        PreviewKeyDown += OnPreviewKeyDown;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _previousFocus = null;
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets a value indicating whether the dialog overlay is visible.</summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>Gets or sets the dialog title text.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>Gets or sets the pixel width of the dialog panel. Defaults to 520.</summary>
    public double DialogWidth
    {
        get => (double)GetValue(DialogWidthProperty);
        set => SetValue(DialogWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether clicking the mask closes the dialog.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool MaskClosable
    {
        get => (bool)GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
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

    /// <summary>Gets or sets the label text of the OK button. Defaults to "OK".</summary>
    public string OkText
    {
        get => (string)GetValue(OkTextProperty);
        set => SetValue(OkTextProperty, value);
    }

    /// <summary>Gets or sets the label text of the Cancel button. Defaults to "Cancel".</summary>
    public string CancelText
    {
        get => (string)GetValue(CancelTextProperty);
        set => SetValue(CancelTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the footer content. When <see langword="null"/> the default
    /// OK / Cancel button row is rendered by the template.
    /// </summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>Gets or sets the content displayed inside the dialog panel.</summary>
    public object? DialogContent
    {
        get => GetValue(DialogContentProperty);
        set => SetValue(DialogContentProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // Mask click
        if (GetTemplateChild(PART_Mask) is FrameworkElement mask)
        {
            mask.MouseLeftButtonDown -= OnMaskMouseLeftButtonDown;
            mask.MouseLeftButtonDown += OnMaskMouseLeftButtonDown;
        }

        // Close (X) button
        if (GetTemplateChild(PART_CloseButton) is System.Windows.Controls.Button closeBtn)
        {
            closeBtn.Click -= OnCloseButtonClick;
            closeBtn.Click += OnCloseButtonClick;
        }

        // OK button
        if (GetTemplateChild(PART_OkButton) is System.Windows.Controls.Button okBtn)
        {
            okBtn.Click -= OnOkButtonClick;
            okBtn.Click += OnOkButtonClick;
        }

        // Cancel button
        if (GetTemplateChild(PART_CancelButton) is System.Windows.Controls.Button cancelBtn)
        {
            cancelBtn.Click -= OnCancelButtonClick;
            cancelBtn.Click += OnCancelButtonClick;
        }

        UpdateVisualState(false);
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private void UpdateVisualState(bool useTransitions)
    {
        VisualStateManager.GoToState(this, IsOpen ? STATE_Open : STATE_Closed, useTransitions);
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var modal = (Modal)d;
        modal.UpdateVisualState(true);

        if ((bool)e.NewValue)
        {
            // Save current focus and trap into dialog
            modal._previousFocus = Keyboard.FocusedElement;
            modal.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, () =>
            {
                var dialogRoot = modal.GetTemplateChild(PART_DialogRoot) as FrameworkElement;
                if (dialogRoot != null)
                    FocusTrapHelper.FocusFirst(dialogRoot);
            });
            modal.RaiseEvent(new RoutedEventArgs(OpenedEvent, modal));
        }
        else
        {
            // Restore previous focus
            modal._previousFocus?.Focus();
            modal._previousFocus = null;
            modal.RaiseEvent(new RoutedEventArgs(ClosedEvent, modal));
        }
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (!IsOpen) return;

        // Escape closes the dialog
        if (e.Key == Key.Escape)
        {
            IsOpen = false;
            e.Handled = true;
            return;
        }

        // Tab focus trap within dialog
        var dialogRoot = GetTemplateChild(PART_DialogRoot) as FrameworkElement;
        if (dialogRoot != null)
            FocusTrapHelper.HandleTabKey(dialogRoot, e);
    }

    private void OnMaskMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (MaskClosable)
            IsOpen = false;
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
    }

    private void OnOkButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(OkClickedEvent, this));
        IsOpen = false;
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(CancelClickedEvent, this));
        IsOpen = false;
    }

    private void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)  => IsOpen = true;
    private void ExecuteClose(object sender, ExecutedRoutedEventArgs e) => IsOpen = false;

    // -------------------------------------------------------------------------
    // Static convenience methods
    // -------------------------------------------------------------------------

    /// <summary>
    /// Programmatically shows a modal dialog on the nearest <see cref="Modal"/> host
    /// found in the logical tree of <paramref name="owner"/>.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="content">Dialog body content.</param>
    /// <param name="owner">
    /// A <see cref="DependencyObject"/> in the visual tree used to locate the
    /// nearest <see cref="Modal"/> ancestor. Pass <see langword="null"/> to
    /// attempt resolution via the active <see cref="Window"/>.
    /// </param>
    public static void Show(string title, object content, DependencyObject? owner = null)
    {
        var modal = ResolveModal(owner);
        if (modal is null) return;

        modal.Title         = title;
        modal.DialogContent = content;
        modal.IsOpen        = true;
    }

    /// <summary>
    /// Shows a confirmation dialog with a plain-text message body.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="content">Plain-text confirmation message.</param>
    /// <param name="owner">Optional owner element for modal resolution.</param>
    public static void Confirm(string title, string content, DependencyObject? owner = null)
    {
        Show(title, content, owner);
    }

    /// <summary>
    /// Walks the logical tree to find the first <see cref="Modal"/> ancestor,
    /// falling back to the active window's visual tree.
    /// </summary>
    private static Modal? ResolveModal(DependencyObject? seed)
    {
        if (seed is not null)
        {
            DependencyObject? current = seed;
            while (current is not null)
            {
                if (current is Modal m) return m;
                current = LogicalTreeHelper.GetParent(current)
                    ?? VisualTreeHelper.GetParent(current);
            }
        }

        // Fallback: scan the active window
        if (Application.Current?.MainWindow is Window win)
            return FindInVisualTree<Modal>(win);

        return null;
    }

    private static T? FindInVisualTree<T>(DependencyObject root) where T : DependencyObject
    {
        if (root is T match) return match;

        int count = VisualTreeHelper.GetChildrenCount(root);
        for (int i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);
            var result = FindInVisualTree<T>(child);
            if (result is not null) return result;
        }

        return null;
    }

    /// <inheritdoc/>
    protected override AutomationPeer OnCreateAutomationPeer()
        => new ModalAutomationPeer(this);
}
