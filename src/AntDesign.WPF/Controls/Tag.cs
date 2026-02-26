using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

    /// <summary>
    /// Identifies the <see cref="Closed"/> routed event.
    /// Raised after the user clicks the close button and the tag begins its
    /// close sequence.
    /// </summary>
    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Closed),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Tag));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Color"/> dependency property.</summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(
            nameof(Color),
            typeof(string),
            typeof(Tag),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="PresetColor"/> dependency property.</summary>
    public static readonly DependencyProperty PresetColorProperty =
        DependencyProperty.Register(
            nameof(PresetColor),
            typeof(PresetColor?),
            typeof(Tag),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Closable"/> dependency property.</summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(
            nameof(Closable),
            typeof(bool),
            typeof(Tag),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(Tag),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="IsVisible"/> dependency property (read-only key).</summary>
    private static readonly DependencyPropertyKey IsTagVisiblePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(IsTagVisible),
            typeof(bool),
            typeof(Tag),
            new PropertyMetadata(true));

    /// <summary>Identifies the read-only <see cref="IsTagVisible"/> dependency property.</summary>
    public static readonly DependencyProperty IsTagVisibleProperty =
        IsTagVisiblePropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Tag()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Tag),
            new FrameworkPropertyMetadata(typeof(Tag)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets a CSS-style color string (e.g. <c>"#f50"</c>, <c>"magenta"</c>)
    /// applied to the tag background. Takes priority over <see cref="PresetColor"/>
    /// when both are set.
    /// </summary>
    public string? Color
    {
        get => (string?)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a preset Ant Design palette color applied to the tag.
    /// Ignored when <see cref="Color"/> is non-null.
    /// </summary>
    public PresetColor? PresetColor
    {
        get => (PresetColor?)GetValue(PresetColorProperty);
        set => SetValue(PresetColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tag renders a close button
    /// that the user can click to dismiss the tag.
    /// </summary>
    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets an optional icon placed before the tag content.
    /// Accepts any object; typically an image, path geometry, or glyph.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the tag is currently visible.
    /// Set to <see langword="false"/> after <see cref="Close"/> is invoked.
    /// </summary>
    public bool IsTagVisible
    {
        get => (bool)GetValue(IsTagVisibleProperty);
        private set => SetValue(IsTagVisiblePropertyKey, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Occurs after the user dismisses the tag via the close button.
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

        if (GetTemplateChild("PART_CloseButton") is UIElement closeButton)
        {
            closeButton.MouseLeftButtonUp -= OnCloseButtonClick;
            closeButton.MouseLeftButtonUp += OnCloseButtonClick;
        }
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void OnCloseButtonClick(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Hides the tag and raises the <see cref="Closed"/> routed event.
    /// </summary>
    public void Close()
    {
        IsTagVisible = false;
        RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
    }
}
