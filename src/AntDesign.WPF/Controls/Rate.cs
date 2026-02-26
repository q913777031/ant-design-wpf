using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AntDesign.WPF.Controls;

/// <summary>
/// A star-rating input control that allows the user to select a numeric rating value.
/// Supports half-star precision and a clear-on-re-click gesture.
/// Follows the Ant Design Rate specification.
/// </summary>
public class Rate : Control
{
    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Identifies the <see cref="ValueChanged"/> routed event.
    /// Raised whenever <see cref="Value"/> is modified by the user or programmatically.
    /// </summary>
    public static readonly RoutedEvent ValueChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(Rate));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Value"/> dependency property.</summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(double),
            typeof(Rate),
            new PropertyMetadata(0d, OnValueChanged, CoerceValue));

    /// <summary>Identifies the <see cref="Count"/> dependency property.</summary>
    public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register(
            nameof(Count),
            typeof(int),
            typeof(Rate),
            new PropertyMetadata(5, null, CoerceCount));

    /// <summary>Identifies the <see cref="AllowHalf"/> dependency property.</summary>
    public static readonly DependencyProperty AllowHalfProperty =
        DependencyProperty.Register(
            nameof(AllowHalf),
            typeof(bool),
            typeof(Rate),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="AllowClear"/> dependency property.</summary>
    public static readonly DependencyProperty AllowClearProperty =
        DependencyProperty.Register(
            nameof(AllowClear),
            typeof(bool),
            typeof(Rate),
            new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Disabled"/> dependency property.</summary>
    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(
            nameof(Disabled),
            typeof(bool),
            typeof(Rate),
            new PropertyMetadata(false));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Rate()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Rate),
            new FrameworkPropertyMetadata(typeof(Rate)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the current rating value.
    /// Must be between 0 and <see cref="Count"/> (inclusive); out-of-range values
    /// are coerced. Supports 0.5 increments when <see cref="AllowHalf"/> is
    /// <see langword="true"/>.
    /// </summary>
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the total number of star elements rendered.
    /// Defaults to 5. Values less than 1 are coerced to 1.
    /// </summary>
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether half-star selection is permitted.
    /// When <see langword="false"/> only whole-star values are selectable.
    /// </summary>
    public bool AllowHalf
    {
        get => (bool)GetValue(AllowHalfProperty);
        set => SetValue(AllowHalfProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether clicking the currently selected
    /// star resets the rating to zero.
    /// </summary>
    public bool AllowClear
    {
        get => (bool)GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the rating control is read-only.
    /// When <see langword="true"/> user interaction is suppressed.
    /// </summary>
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>Occurs when <see cref="Value"/> changes.</summary>
    public event RoutedPropertyChangedEventHandler<double> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
        var rate = (Rate)d;
        double val = (double)baseValue;
        int count = rate.Count;

        if (val < 0d) return 0d;
        if (val > count) return (double)count;
        return val;
    }

    private static object CoerceCount(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 1 ? 1 : value;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var rate = (Rate)d;
        var args = new RoutedPropertyChangedEventArgs<double>(
            (double)e.OldValue,
            (double)e.NewValue,
            ValueChangedEvent)
        {
            Source = rate
        };
        rate.RaiseEvent(args);
    }

    // -------------------------------------------------------------------------
    // Interaction
    // -------------------------------------------------------------------------

    /// <summary>
    /// Sets the rating value to the specified star index.
    /// Handles clear-on-same-value when <see cref="AllowClear"/> is enabled.
    /// </summary>
    /// <param name="starValue">
    /// The new value to assign. Pass a .5 increment for half-star selection.
    /// </param>
    public void SetValue(double starValue)
    {
        if (Disabled) return;

        if (AllowClear && Value == starValue)
        {
            Value = 0d;
        }
        else
        {
            Value = starValue;
        }
    }

    /// <inheritdoc/>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (Disabled) return;

        double step = AllowHalf ? 0.5d : 1d;

        switch (e.Key)
        {
            case Key.Right:
            case Key.Up:
                Value = Math.Min(Count, Value + step);
                e.Handled = true;
                break;

            case Key.Left:
            case Key.Down:
                Value = Math.Max(0d, Value - step);
                e.Handled = true;
                break;
        }
    }
}
