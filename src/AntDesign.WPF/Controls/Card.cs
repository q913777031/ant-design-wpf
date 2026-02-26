using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of a <see cref="Card"/>.
/// </summary>
public enum CardSize
{
    /// <summary>Default card size with standard padding.</summary>
    Default,

    /// <summary>Small card size with reduced padding.</summary>
    Small
}

/// <summary>
/// A container component that groups related content, optionally with a title and extra actions.
/// Follows the Ant Design Card specification.
/// </summary>
[TemplatePart(Name = Card.PART_Header, Type = typeof(FrameworkElement))]
[TemplatePart(Name = Card.PART_Body, Type = typeof(FrameworkElement))]
public class Card : ContentControl
{
    /// <summary>Template part name for the card header region.</summary>
    public const string PART_Header = "PART_Header";

    /// <summary>Template part name for the card body region.</summary>
    public const string PART_Body = "PART_Body";

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Card),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Extra"/> dependency property.</summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(
            nameof(Extra),
            typeof(object),
            typeof(Card),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Hoverable"/> dependency property.</summary>
    public static readonly DependencyProperty HoverableProperty =
        DependencyProperty.Register(
            nameof(Hoverable),
            typeof(bool),
            typeof(Card),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="Bordered"/> dependency property.</summary>
    public static readonly DependencyProperty BorderedProperty =
        DependencyProperty.Register(
            nameof(Bordered),
            typeof(bool),
            typeof(Card),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(CardSize),
            typeof(Card),
            new PropertyMetadata(CardSize.Default));

    /// <summary>Identifies the <see cref="BodyPadding"/> dependency property.</summary>
    public static readonly DependencyProperty BodyPaddingProperty =
        DependencyProperty.Register(
            nameof(BodyPadding),
            typeof(Thickness),
            typeof(Card),
            new PropertyMetadata(new Thickness(24)));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Card()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Card),
            new FrameworkPropertyMetadata(typeof(Card)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the card header title text.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the extra content rendered in the top-right corner of the card header.
    /// Accepts any object; typically a button, link, or text.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the card lifts on hover via a shadow effect.
    /// </summary>
    public bool Hoverable
    {
        get => (bool)GetValue(HoverableProperty);
        set => SetValue(HoverableProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the card renders a visible border.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool Bordered
    {
        get => (bool)GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>Gets or sets the size variant of the card.</summary>
    public CardSize Size
    {
        get => (CardSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding applied inside the card body region.
    /// Defaults to a uniform thickness of 24.
    /// </summary>
    public Thickness BodyPadding
    {
        get => (Thickness)GetValue(BodyPaddingProperty);
        set => SetValue(BodyPaddingProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        // Named parts are resolved by WPF automatically; override here
        // if additional wiring is required in future iterations.
    }
}
