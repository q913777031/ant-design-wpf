using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the button style variant for the OK button in a <see cref="Popconfirm"/>.
/// </summary>
public enum PopconfirmOkType
{
    /// <summary>Primary button style (default).</summary>
    Primary,

    /// <summary>Danger / destructive button style.</summary>
    Danger
}

/// <summary>
/// A lightweight confirmation popover that wraps a trigger element and displays an inline
/// confirm/cancel prompt. Follows the Ant Design Popconfirm specification.
/// </summary>
/// <remarks>
/// <para>
/// Place the trigger element (e.g. a <see cref="Button"/>) as the
/// <see cref="ContentControl.Content"/> of the <see cref="Popconfirm"/>. When the trigger
/// is clicked the popover is shown. The user can confirm or dismiss the action.
/// </para>
/// <para>
/// The popover is rendered using an internal <see cref="Popup"/> which is positioned
/// relative to the trigger according to <see cref="Placement"/>.
/// </para>
/// </remarks>
[TemplatePart(Name = Popconfirm.PART_Popup,        Type = typeof(Popup))]
[TemplatePart(Name = Popconfirm.PART_OkButton,     Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = Popconfirm.PART_CancelButton, Type = typeof(System.Windows.Controls.Button))]
[TemplateVisualState(Name = Popconfirm.STATE_Open,   GroupName = Popconfirm.GROUP_OpenState)]
[TemplateVisualState(Name = Popconfirm.STATE_Closed, GroupName = Popconfirm.GROUP_OpenState)]
public class Popconfirm : ContentControl
{
    // -------------------------------------------------------------------------
    // Template part and visual-state constants
    // -------------------------------------------------------------------------

    /// <summary>Template part name for the internal <see cref="Popup"/>.</summary>
    public const string PART_Popup        = "PART_Popup";

    /// <summary>Template part name for the OK confirm button.</summary>
    public const string PART_OkButton     = "PART_OkButton";

    /// <summary>Template part name for the Cancel button.</summary>
    public const string PART_CancelButton = "PART_CancelButton";

    /// <summary>Visual state group name for open/closed state.</summary>
    public const string GROUP_OpenState = "OpenStates";

    /// <summary>Visual state name when the popover is visible.</summary>
    public const string STATE_Open   = "Open";

    /// <summary>Visual state name when the popover is hidden.</summary>
    public const string STATE_Closed = "Closed";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Popconfirm),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="OkText"/> dependency property.</summary>
    public static readonly DependencyProperty OkTextProperty =
        DependencyProperty.Register(
            nameof(OkText),
            typeof(string),
            typeof(Popconfirm),
            new PropertyMetadata("OK"));

    /// <summary>Identifies the <see cref="CancelText"/> dependency property.</summary>
    public static readonly DependencyProperty CancelTextProperty =
        DependencyProperty.Register(
            nameof(CancelText),
            typeof(string),
            typeof(Popconfirm),
            new PropertyMetadata("Cancel"));

    /// <summary>Identifies the <see cref="OkType"/> dependency property.</summary>
    public static readonly DependencyProperty OkTypeProperty =
        DependencyProperty.Register(
            nameof(OkType),
            typeof(PopconfirmOkType),
            typeof(Popconfirm),
            new PropertyMetadata(PopconfirmOkType.Primary));

    /// <summary>Identifies the <see cref="Placement"/> dependency property.</summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(
            nameof(Placement),
            typeof(PlacementMode),
            typeof(Popconfirm),
            new PropertyMetadata(PlacementMode.Top));

    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(Popconfirm),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsOpenChanged));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(Popconfirm),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Confirmed"/> routed event.</summary>
    public static readonly RoutedEvent ConfirmedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Confirmed),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Popconfirm));

    /// <summary>Raised when the user clicks the OK button to confirm the action.</summary>
    public event RoutedEventHandler Confirmed
    {
        add    => AddHandler(ConfirmedEvent, value);
        remove => RemoveHandler(ConfirmedEvent, value);
    }

    /// <summary>Identifies the <see cref="Cancelled"/> routed event.</summary>
    public static readonly RoutedEvent CancelledEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Cancelled),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Popconfirm));

    /// <summary>Raised when the user clicks the Cancel button to dismiss the popover.</summary>
    public event RoutedEventHandler Cancelled
    {
        add    => AddHandler(CancelledEvent, value);
        remove => RemoveHandler(CancelledEvent, value);
    }

    // -------------------------------------------------------------------------
    // Private fields
    // -------------------------------------------------------------------------

    private Popup? _popup;
    private System.Windows.Controls.Button? _okButton;
    private System.Windows.Controls.Button? _cancelButton;

    // -------------------------------------------------------------------------
    // Static constructor
    // -------------------------------------------------------------------------

    static Popconfirm()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Popconfirm),
            new FrameworkPropertyMetadata(typeof(Popconfirm)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the confirmation question text shown in the popover.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
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

    /// <summary>Gets or sets the style variant for the OK button. Defaults to <see cref="PopconfirmOkType.Primary"/>.</summary>
    public PopconfirmOkType OkType
    {
        get => (PopconfirmOkType)GetValue(OkTypeProperty);
        set => SetValue(OkTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the placement of the popover relative to the trigger element.
    /// Defaults to <see cref="PlacementMode.Top"/>.
    /// </summary>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>Gets or sets a value indicating whether the popover is visible.</summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>Gets or sets an optional icon element or resource shown in the popover header.</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // Detach previous handlers
        if (_okButton is not null)     _okButton.Click     -= OnOkButtonClick;
        if (_cancelButton is not null) _cancelButton.Click -= OnCancelButtonClick;
        if (_popup is not null)        _popup.Opened       -= OnPopupOpened;

        _popup        = GetTemplateChild(PART_Popup)        as Popup;
        _okButton     = GetTemplateChild(PART_OkButton)     as System.Windows.Controls.Button;
        _cancelButton = GetTemplateChild(PART_CancelButton) as System.Windows.Controls.Button;

        if (_okButton is not null)     _okButton.Click     += OnOkButtonClick;
        if (_cancelButton is not null) _cancelButton.Click += OnCancelButtonClick;

        if (_popup is not null)
        {
            // Sync popup's PlacementTarget and Placement
            _popup.PlacementTarget = this;
            _popup.Placement       = Placement;
            _popup.IsOpen          = IsOpen;
            _popup.Opened         += OnPopupOpened;
            _popup.StaysOpen       = false;
            _popup.Closed         += OnPopupClosed;
        }

        // Wire the trigger: clicking the content area toggles the popover
        MouseLeftButtonUp -= OnTriggerClick;
        MouseLeftButtonUp += OnTriggerClick;

        Unloaded -= OnUnloaded;
        Unloaded += OnUnloaded;

        UpdateVisualState(false);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_okButton is not null)     _okButton.Click     -= OnOkButtonClick;
        if (_cancelButton is not null) _cancelButton.Click -= OnCancelButtonClick;
        if (_popup is not null)
        {
            _popup.Opened -= OnPopupOpened;
            _popup.Closed -= OnPopupClosed;
        }
        MouseLeftButtonUp -= OnTriggerClick;
    }

    // -------------------------------------------------------------------------
    // Property changed callbacks
    // -------------------------------------------------------------------------

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var pc = (Popconfirm)d;
        pc.UpdateVisualState(true);

        if (pc._popup is not null)
            pc._popup.IsOpen = (bool)e.NewValue;
    }

    // -------------------------------------------------------------------------
    // Event handlers
    // -------------------------------------------------------------------------

    private void OnTriggerClick(object sender, MouseButtonEventArgs e)
    {
        // Only toggle if the click originated from the trigger content, not
        // from within the popup itself.
        if (!IsClickFromPopup(e))
            IsOpen = !IsOpen;
    }

    private void OnPopupOpened(object? sender, EventArgs e)
    {
        if (_popup is not null)
            _popup.Placement = Placement;
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        // Sync back when the Popup closes itself (e.g. StaysOpen = false)
        if (IsOpen)
            IsOpen = false;
    }

    private void OnOkButtonClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
        RaiseEvent(new RoutedEventArgs(ConfirmedEvent, this));
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
        RaiseEvent(new RoutedEventArgs(CancelledEvent, this));
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private void UpdateVisualState(bool useTransitions)
    {
        VisualStateManager.GoToState(this, IsOpen ? STATE_Open : STATE_Closed, useTransitions);
    }

    /// <summary>
    /// Returns <see langword="true"/> if the mouse event originated from inside the popup's
    /// visual tree, preventing the trigger click from immediately closing the popover.
    /// </summary>
    private bool IsClickFromPopup(MouseButtonEventArgs e)
    {
        if (_popup?.Child is null) return false;

        var source = e.OriginalSource as DependencyObject;
        while (source is not null)
        {
            if (source == _popup.Child) return true;
            source = VisualTreeHelper.GetParent(source)
                ?? LogicalTreeHelper.GetParent(source);
        }

        return false;
    }
}
