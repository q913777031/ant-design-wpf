using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using AntDesign.WPF.Automation;

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

    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(nameof(Closed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Alert));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AlertType), typeof(Alert),
            new PropertyMetadata(AlertType.Info));

    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(Alert),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(Alert),
            new PropertyMetadata(null));

    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(nameof(Closable), typeof(bool), typeof(Alert),
            new PropertyMetadata(false));

    public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(Alert),
            new PropertyMetadata(true));

    public static readonly DependencyProperty BannerProperty =
        DependencyProperty.Register(nameof(Banner), typeof(bool), typeof(Alert),
            new PropertyMetadata(false));

    private static readonly DependencyPropertyKey IsAlertVisiblePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(IsAlertVisible), typeof(bool), typeof(Alert),
            new PropertyMetadata(true));

    public static readonly DependencyProperty IsAlertVisibleProperty =
        IsAlertVisiblePropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Alert()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Alert), new FrameworkPropertyMetadata(typeof(Alert)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    public AlertType Type
    {
        get => (AlertType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public string? Message
    {
        get => (string?)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    public bool Banner
    {
        get => (bool)GetValue(BannerProperty);
        set => SetValue(BannerProperty, value);
    }

    public bool IsAlertVisible
    {
        get => (bool)GetValue(IsAlertVisibleProperty);
        private set => SetValue(IsAlertVisiblePropertyKey, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    public event RoutedEventHandler Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_CloseButton") is System.Windows.Controls.Button closeBtn)
        {
            closeBtn.Click -= OnCloseButtonClick;
            closeBtn.Click += OnCloseButtonClick;
        }
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Hides the alert with a smooth fade-out animation and raises the <see cref="Closed"/> event.
    /// </summary>
    public void Close()
    {
        var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        };
        fadeOut.Completed += (_, _) =>
        {
            IsAlertVisible = false;
            RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
        };
        BeginAnimation(OpacityProperty, fadeOut);
    }

    protected override AutomationPeer OnCreateAutomationPeer()
        => new AlertAutomationPeer(this);
}
