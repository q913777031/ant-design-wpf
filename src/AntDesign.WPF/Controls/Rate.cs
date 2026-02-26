using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AntDesign.WPF.Automation;

namespace AntDesign.WPF.Controls;

/// <summary>
/// A star-rating input control that allows the user to select a numeric rating value.
/// Supports half-star precision and a clear-on-re-click gesture.
/// Follows the Ant Design Rate specification.
/// </summary>
[TemplatePart(Name = PART_StarsPanel, Type = typeof(StackPanel))]
public class Rate : Control
{
    private const string PART_StarsPanel = "PART_StarsPanel";
    private StackPanel? _starsPanel;

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
            new PropertyMetadata(5, OnCountChanged, CoerceCount));

    /// <summary>Identifies the <see cref="AllowHalf"/> dependency property.</summary>
    public static readonly DependencyProperty AllowHalfProperty =
        DependencyProperty.Register(
            nameof(AllowHalf),
            typeof(bool),
            typeof(Rate),
            new PropertyMetadata(false, OnAllowHalfChanged));

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
            new PropertyMetadata(false, OnDisabledChanged));

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
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _starsPanel = GetTemplateChild(PART_StarsPanel) as StackPanel;
        BuildStars();
    }

    // -------------------------------------------------------------------------
    // Star Building
    // -------------------------------------------------------------------------

    private void BuildStars()
    {
        if (_starsPanel == null) return;
        _starsPanel.Children.Clear();

        for (int i = 0; i < Count; i++)
        {
            var starIndex = i;

            var starContainer = new Border
            {
                Width = 24,
                Height = 24,
                Margin = new Thickness(2, 0, 2, 0),
                Background = Brushes.Transparent,
                Cursor = Disabled ? Cursors.Arrow : Cursors.Hand,
            };

            var starPath = new Path
            {
                Data = Geometry.Parse("M12,2 L14.4,9.2 L22,9.2 L15.8,13.8 L18.2,21 L12,16.4 L5.8,21 L8.2,13.8 L2,9.2 L9.6,9.2 Z"),
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 20,
                Height = 20,
            };

            UpdateStarFill(starPath, starIndex);

            if (!Disabled)
            {
                starContainer.MouseLeftButtonDown += (s, e) =>
                {
                    double newValue = starIndex + 1;
                    if (AllowHalf)
                    {
                        var pos = e.GetPosition(starContainer);
                        if (pos.X < starContainer.ActualWidth / 2)
                            newValue = starIndex + 0.5;
                    }
                    if (AllowClear && Math.Abs(Value - newValue) < 0.01)
                        Value = 0;
                    else
                        Value = newValue;
                    e.Handled = true;
                };

                starContainer.MouseEnter += (s, e) =>
                {
                    if (_starsPanel == null) return;
                    for (int j = 0; j < _starsPanel.Children.Count; j++)
                    {
                        if (_starsPanel.Children[j] is Border b && b.Child is Path p)
                        {
                            var brush = j <= starIndex
                                ? TryFindResource("AntDesign.Brush.Warning") as Brush ?? Brushes.Gold
                                : TryFindResource("AntDesign.Brush.Fill.Tertiary") as Brush ?? Brushes.LightGray;
                            p.Fill = brush;
                        }
                    }
                };

                starContainer.MouseLeave += (s, e) => RefreshAllStars();
            }

            starContainer.Child = starPath;
            _starsPanel.Children.Add(starContainer);
        }
    }

    // -------------------------------------------------------------------------
    // Star Fill Helpers
    // -------------------------------------------------------------------------

    private void UpdateStarFill(Path star, int index)
    {
        var filledBrush = TryFindResource("AntDesign.Brush.Warning") as Brush ?? Brushes.Gold;
        var emptyBrush = TryFindResource("AntDesign.Brush.Fill.Tertiary") as Brush ?? Brushes.LightGray;

        if (index + 1 <= Value)
            star.Fill = filledBrush;
        else if (AllowHalf && index + 0.5 <= Value)
            star.Fill = filledBrush; // simplified - full star for half (proper half-star needs clip)
        else
            star.Fill = emptyBrush;
    }

    private void RefreshAllStars()
    {
        if (_starsPanel == null) return;
        for (int i = 0; i < _starsPanel.Children.Count; i++)
        {
            if (_starsPanel.Children[i] is Border b && b.Child is Path p)
                UpdateStarFill(p, i);
        }
    }

    // -------------------------------------------------------------------------
    // Keyboard Interaction
    // -------------------------------------------------------------------------

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
        rate.RefreshAllStars();

        var args = new RoutedPropertyChangedEventArgs<double>(
            (double)e.OldValue,
            (double)e.NewValue,
            ValueChangedEvent)
        {
            Source = rate
        };
        rate.RaiseEvent(args);
    }

    private static void OnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Rate rate) rate.BuildStars();
    }

    private static void OnAllowHalfChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Rate rate) rate.RefreshAllStars();
    }

    private static void OnDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Rate rate) rate.BuildStars();
    }

    /// <inheritdoc/>
    protected override AutomationPeer OnCreateAutomationPeer()
        => new RateAutomationPeer(this);
}
