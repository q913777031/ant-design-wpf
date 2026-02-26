using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the semantic type of an <see cref="Alert"/> message.
/// </summary>
public enum AlertType
{
    /// <summary>Green success alert.</summary>
    Success,

    /// <summary>Blue informational alert.</summary>
    Info,

    /// <summary>Yellow warning alert.</summary>
    Warning,

    /// <summary>Red error alert.</summary>
    Error
}

/// <summary>
/// Displays prominent feedback messages for user actions or system events.
/// Supports a title, optional description, optional icon, and a dismiss button.
/// Follows the Ant Design Alert specification.
/// </summary>
public class Alert : Control
{
    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Identifies the <see cref="Closed"/> routed event.
    /// Raised after the user clicks the close button and the alert is dismissed.
    /// </summary>
    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Closed),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Alert));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Type"/> dependency property.</summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(
            nameof(Type),
            typeof(AlertType),
            typeof(Alert),
            new PropertyMetadata(AlertType.Info));

    /// <summary>Identifies the <see cref="Message"/> dependency property.</summary>
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(
            nameof(Message),
            typeof(string),
            typeof(Alert),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Description"/> dependency property.</summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(Alert),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Closable"/> dependency property.</summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(
            nameof(Closable),
            typeof(bool),
            typeof(Alert),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="ShowIcon"/> dependency property.</summary>
    public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.Register(
            nameof(ShowIcon),
            typeof(bool),
            typeof(Alert),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Banner"/> dependency property.</summary>
    public static readonly DependencyProperty BannerProperty =
        DependencyProperty.Register(
            nameof(Banner),
            typeof(bool),
            typeof(Alert),
            new PropertyMetadata(false));

    private static readonly DependencyPropertyKey IsAlertVisiblePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(IsAlertVisible),
            typeof(bool),
            typeof(Alert),
            new PropertyMetadata(true));

    /// <summary>Identifies the read-only <see cref="IsAlertVisible"/> dependency property.</summary>
    public static readonly DependencyProperty IsAlertVisibleProperty =
        IsAlertVisiblePropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Alert()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Alert),
            new FrameworkPropertyMetadata(typeof(Alert)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the semantic type that determines the alert's color and default icon.</summary>
    public AlertType Type
    {
        get => (AlertType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>Gets or sets the primary alert message text.</summary>
    public string? Message
    {
        get => (string?)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// Gets or sets an optional supplementary description rendered below
    /// <see cref="Message"/> in smaller text.
    /// </summary>
    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the alert renders a close button
    /// the user can click to dismiss the alert.
    /// </summary>
    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a type-appropriate icon is shown
    /// to the left of the message. Defaults to <see langword="true"/>.
    /// </summary>
    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the alert renders in banner style
    /// (no border radius, full-width, top-of-page placement).
    /// </summary>
    public bool Banner
    {
        get => (bool)GetValue(BannerProperty);
        set => SetValue(BannerProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the alert is currently visible.
    /// Becomes <see langword="false"/> after <see cref="Close"/> is called.
    /// </summary>
    public bool IsAlertVisible
    {
        get => (bool)GetValue(IsAlertVisibleProperty);
        private set => SetValue(IsAlertVisiblePropertyKey, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Occurs after the user dismisses the alert via the close button.
    /// </summary>
    public event RoutedEventHandler Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_CloseButton") is UIElement closeBtn)
        {
            closeBtn.MouseLeftButtonUp -= OnCloseButtonClick;
            closeBtn.MouseLeftButtonUp += OnCloseButtonClick;
        }
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void OnCloseButtonClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Hides the alert and raises the <see cref="Closed"/> routed event.
    /// </summary>
    public void Close()
    {
        IsAlertVisible = false;
        RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
    }
}
