using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the status variant of a <see cref="Badge"/>.
/// </summary>
public enum BadgeStatus
{
    /// <summary>Default grey status indicator.</summary>
    Default,

    /// <summary>Green success status indicator.</summary>
    Success,

    /// <summary>Blue processing / animated status indicator.</summary>
    Processing,

    /// <summary>Red error status indicator.</summary>
    Error,

    /// <summary>Yellow warning status indicator.</summary>
    Warning
}

/// <summary>
/// A decorator control that overlays a notification count, dot, or status badge
/// on its wrapped content element.
/// Follows the Ant Design Badge specification.
/// </summary>
public class Badge : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Count"/> dependency property.</summary>
    public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register(
            nameof(Count),
            typeof(int),
            typeof(Badge),
            new PropertyMetadata(0, OnCountChanged, CoerceCount));

    /// <summary>Identifies the <see cref="OverflowCount"/> dependency property.</summary>
    public static readonly DependencyProperty OverflowCountProperty =
        DependencyProperty.Register(
            nameof(OverflowCount),
            typeof(int),
            typeof(Badge),
            new PropertyMetadata(99, null, CoerceOverflowCount));

    /// <summary>Identifies the <see cref="Dot"/> dependency property.</summary>
    public static readonly DependencyProperty DotProperty =
        DependencyProperty.Register(
            nameof(Dot),
            typeof(bool),
            typeof(Badge),
            new PropertyMetadata(false, OnDotChanged));

    /// <summary>Identifies the <see cref="Status"/> dependency property.</summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(BadgeStatus),
            typeof(Badge),
            new PropertyMetadata(BadgeStatus.Default));

    /// <summary>Identifies the <see cref="Color"/> dependency property.</summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(
            nameof(Color),
            typeof(Brush),
            typeof(Badge),
            new PropertyMetadata(null));

    // --- Read-only computed properties ---

    private static readonly DependencyPropertyKey DisplayCountPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(DisplayCount),
            typeof(string),
            typeof(Badge),
            new PropertyMetadata(string.Empty));

    /// <summary>Identifies the read-only <see cref="DisplayCount"/> dependency property.</summary>
    public static readonly DependencyProperty DisplayCountProperty =
        DisplayCountPropertyKey.DependencyProperty;

    private static readonly DependencyPropertyKey IsBadgeVisiblePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(IsBadgeVisible),
            typeof(bool),
            typeof(Badge),
            new PropertyMetadata(false));

    /// <summary>Identifies the read-only <see cref="IsBadgeVisible"/> dependency property.</summary>
    public static readonly DependencyProperty IsBadgeVisibleProperty =
        IsBadgeVisiblePropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Badge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Badge),
            new FrameworkPropertyMetadata(typeof(Badge)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the numeric value to display inside the badge.
    /// Must be zero or greater; negative values are coerced to zero.
    /// </summary>
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum count displayed before a "+" suffix is appended.
    /// Values above this threshold render as "<c>{OverflowCount}+</c>".
    /// Defaults to 99. Must be positive; values less than 1 are coerced to 1.
    /// </summary>
    public int OverflowCount
    {
        get => (int)GetValue(OverflowCountProperty);
        set => SetValue(OverflowCountProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the badge renders as a small dot
    /// rather than a numeric count.
    /// </summary>
    public bool Dot
    {
        get => (bool)GetValue(DotProperty);
        set => SetValue(DotProperty, value);
    }

    /// <summary>Gets or sets the semantic status variant of the badge.</summary>
    public BadgeStatus Status
    {
        get => (BadgeStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom brush applied to the badge indicator.
    /// When set, overrides the color implied by <see cref="Status"/>.
    /// </summary>
    public Brush? Color
    {
        get => (Brush?)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets the formatted string shown inside the badge (e.g. "5" or "99+").
    /// Computed automatically from <see cref="Count"/> and <see cref="OverflowCount"/>.
    /// </summary>
    public string DisplayCount
    {
        get => (string)GetValue(DisplayCountProperty);
        private set => SetValue(DisplayCountPropertyKey, value);
    }

    /// <summary>
    /// Gets a value indicating whether the badge indicator should be rendered.
    /// <see langword="true"/> when <see cref="Count"/> is greater than zero or
    /// <see cref="Dot"/> is <see langword="true"/>.
    /// </summary>
    public bool IsBadgeVisible
    {
        get => (bool)GetValue(IsBadgeVisibleProperty);
        private set => SetValue(IsBadgeVisiblePropertyKey, value);
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceCount(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 0 ? 0 : value;
    }

    private static object CoerceOverflowCount(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 1 ? 1 : value;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((Badge)d).UpdateBadgeState();
    }

    private static void OnDotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((Badge)d).UpdateBadgeState();
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void UpdateBadgeState()
    {
        int count = Count;
        int overflow = OverflowCount;

        if (Dot)
        {
            DisplayCount = string.Empty;
            IsBadgeVisible = true;
            return;
        }

        IsBadgeVisible = count > 0;
        DisplayCount = count > overflow
            ? $"{overflow}+"
            : count.ToString();
    }
}
