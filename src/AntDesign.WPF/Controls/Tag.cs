using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using AntDesign.WPF.Automation;
using AntDesign.WPF.Colors;

namespace AntDesign.WPF.Controls;

/// <summary>
/// A small labeling component used to categorise or mark items.
/// Supports preset color names, custom color strings, an optional close button,
/// and an optional leading icon.
/// Follows the Ant Design Tag specification.
/// </summary>
public class Tag : ContentControl
{
    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(nameof(Closed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Tag));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(string), typeof(Tag), new PropertyMetadata(null));

    public static readonly DependencyProperty PresetColorProperty =
        DependencyProperty.Register(nameof(PresetColor), typeof(PresetColor?), typeof(Tag), new PropertyMetadata(null));

    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(nameof(Closable), typeof(bool), typeof(Tag), new PropertyMetadata(false));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(Tag), new PropertyMetadata(null));

    private static readonly DependencyPropertyKey IsTagVisiblePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(IsTagVisible), typeof(bool), typeof(Tag), new PropertyMetadata(true));

    public static readonly DependencyProperty IsTagVisibleProperty =
        IsTagVisiblePropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Tag()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Tag), new FrameworkPropertyMetadata(typeof(Tag)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    public string? Color
    {
        get => (string?)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public PresetColor? PresetColor
    {
        get => (PresetColor?)GetValue(PresetColorProperty);
        set => SetValue(PresetColorProperty, value);
    }

    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsTagVisible
    {
        get => (bool)GetValue(IsTagVisibleProperty);
        private set => SetValue(IsTagVisiblePropertyKey, value);
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

        if (GetTemplateChild("PART_CloseButton") is System.Windows.Controls.Button closeButton)
        {
            closeButton.Click -= OnCloseButtonClick;
            closeButton.Click += OnCloseButtonClick;
        }
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Hides the tag with a smooth fade-out animation and raises the <see cref="Closed"/> event.
    /// </summary>
    public void Close()
    {
        var scaleTransform = new ScaleTransform(1, 1);
        RenderTransform = scaleTransform;
        RenderTransformOrigin = new Point(0.5, 0.5);

        var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(150)));
        var scaleX = new DoubleAnimation(1, 0.8, new Duration(TimeSpan.FromMilliseconds(150)));
        var scaleY = new DoubleAnimation(1, 0.8, new Duration(TimeSpan.FromMilliseconds(150)));

        fadeOut.Completed += (_, _) =>
        {
            IsTagVisible = false;
            RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
        };

        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);
        BeginAnimation(OpacityProperty, fadeOut);
    }

    protected override AutomationPeer OnCreateAutomationPeer()
        => new TagAutomationPeer(this);
}
