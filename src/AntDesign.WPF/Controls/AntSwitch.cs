using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of an <see cref="AntSwitch"/>.
/// </summary>
public enum SwitchSize
{
    /// <summary>Default-sized toggle switch.</summary>
    Default,

    /// <summary>Small toggle switch with reduced dimensions.</summary>
    Small
}

/// <summary>
/// A binary toggle switch that transitions between checked and unchecked states.
/// Named <c>AntSwitch</c> to avoid collision with
/// <see cref="System.Windows.Controls.Primitives"/> internals.
/// Follows the Ant Design Switch specification.
/// </summary>
[TemplatePart(Name = AntSwitch.PART_Track, Type = typeof(FrameworkElement))]
[TemplatePart(Name = AntSwitch.PART_Thumb, Type = typeof(FrameworkElement))]
public class AntSwitch : Control
{
    /// <summary>Template part name for the switch track.</summary>
    public const string PART_Track = "PART_Track";

    /// <summary>Template part name for the sliding thumb.</summary>
    public const string PART_Thumb = "PART_Thumb";

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Identifies the <see cref="Checked"/> routed event.
    /// Raised when <see cref="IsChecked"/> transitions to <see langword="true"/>.
    /// </summary>
    public static readonly RoutedEvent CheckedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Checked),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(AntSwitch));

    /// <summary>
    /// Identifies the <see cref="Unchecked"/> routed event.
    /// Raised when <see cref="IsChecked"/> transitions to <see langword="false"/>.
    /// </summary>
    public static readonly RoutedEvent UncheckedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Unchecked),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(AntSwitch));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="IsChecked"/> dependency property.</summary>
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(
            nameof(IsChecked),
            typeof(bool),
            typeof(AntSwitch),
            new PropertyMetadata(false, OnIsCheckedChanged));

    /// <summary>Identifies the <see cref="CheckedContent"/> dependency property.</summary>
    public static readonly DependencyProperty CheckedContentProperty =
        DependencyProperty.Register(
            nameof(CheckedContent),
            typeof(object),
            typeof(AntSwitch),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="UncheckedContent"/> dependency property.</summary>
    public static readonly DependencyProperty UncheckedContentProperty =
        DependencyProperty.Register(
            nameof(UncheckedContent),
            typeof(object),
            typeof(AntSwitch),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(SwitchSize),
            typeof(AntSwitch),
            new PropertyMetadata(SwitchSize.Default));

    /// <summary>Identifies the <see cref="Loading"/> dependency property.</summary>
    public static readonly DependencyProperty LoadingProperty =
        DependencyProperty.Register(
            nameof(Loading),
            typeof(bool),
            typeof(AntSwitch),
            new PropertyMetadata(false));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static AntSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AntSwitch),
            new FrameworkPropertyMetadata(typeof(AntSwitch)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets a value indicating whether the switch is in the checked (on) state.
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Gets or sets the content displayed inside the switch track when
    /// <see cref="IsChecked"/> is <see langword="true"/>
    /// (e.g. a short string such as "ON" or a checkmark icon).
    /// </summary>
    public object? CheckedContent
    {
        get => GetValue(CheckedContentProperty);
        set => SetValue(CheckedContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content displayed inside the switch track when
    /// <see cref="IsChecked"/> is <see langword="false"/>
    /// (e.g. a short string such as "OFF" or a cross icon).
    /// </summary>
    public object? UncheckedContent
    {
        get => GetValue(UncheckedContentProperty);
        set => SetValue(UncheckedContentProperty, value);
    }

    /// <summary>Gets or sets the size variant of the switch.</summary>
    public SwitchSize Size
    {
        get => (SwitchSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the switch is in a loading state,
    /// rendering a spinner on the thumb and disabling interaction.
    /// </summary>
    public bool Loading
    {
        get => (bool)GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>Occurs when the switch transitions to the checked state.</summary>
    public event RoutedEventHandler Checked
    {
        add => AddHandler(CheckedEvent, value);
        remove => RemoveHandler(CheckedEvent, value);
    }

    /// <summary>Occurs when the switch transitions to the unchecked state.</summary>
    public event RoutedEventHandler Unchecked
    {
        add => AddHandler(UncheckedEvent, value);
        remove => RemoveHandler(UncheckedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Template & Interaction
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Focusable = true;
    }

    /// <inheritdoc/>
    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        if (!Loading && IsEnabled)
        {
            Toggle();
            e.Handled = true;
        }
    }

    /// <inheritdoc/>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!Loading && IsEnabled && (e.Key == Key.Space || e.Key == Key.Enter))
        {
            Toggle();
            e.Handled = true;
        }
    }

    /// <summary>Toggles the <see cref="IsChecked"/> state.</summary>
    public void Toggle()
    {
        IsChecked = !IsChecked;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sw = (AntSwitch)d;
        bool isChecked = (bool)e.NewValue;
        sw.RaiseEvent(new RoutedEventArgs(isChecked ? CheckedEvent : UncheckedEvent, sw));
    }
}
