using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of an <see cref="InputNumber"/> control.
/// </summary>
public enum InputNumberSize
{
    /// <summary>Small input with reduced height.</summary>
    Small,

    /// <summary>Default input height.</summary>
    Default,

    /// <summary>Large input with increased height.</summary>
    Large
}

/// <summary>
/// A numeric spin-box control that allows the user to enter or increment/decrement
/// a numeric value within an optional bounded range.
/// Follows the Ant Design InputNumber specification.
/// </summary>
[TemplatePart(Name = InputNumber.PART_TextBox, Type = typeof(TextBox))]
[TemplatePart(Name = InputNumber.PART_IncreaseButton, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
[TemplatePart(Name = InputNumber.PART_DecreaseButton, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
public class InputNumber : Control
{
    /// <summary>Template part name for the text input area.</summary>
    public const string PART_TextBox = "PART_TextBox";

    /// <summary>Template part name for the increment (up) button.</summary>
    public const string PART_IncreaseButton = "PART_IncreaseButton";

    /// <summary>Template part name for the decrement (down) button.</summary>
    public const string PART_DecreaseButton = "PART_DecreaseButton";

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Identifies the <see cref="ValueChanged"/> routed event.
    /// Raised when <see cref="Value"/> changes.
    /// </summary>
    public static readonly RoutedEvent ValueChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double?>),
            typeof(InputNumber));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Value"/> dependency property.</summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(double?),
            typeof(InputNumber),
            new PropertyMetadata(null, OnValueChanged, CoerceValue));

    /// <summary>Identifies the <see cref="Minimum"/> dependency property.</summary>
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(
            nameof(Minimum),
            typeof(double),
            typeof(InputNumber),
            new PropertyMetadata(double.NegativeInfinity));

    /// <summary>Identifies the <see cref="Maximum"/> dependency property.</summary>
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum),
            typeof(double),
            typeof(InputNumber),
            new PropertyMetadata(double.PositiveInfinity));

    /// <summary>Identifies the <see cref="Step"/> dependency property.</summary>
    public static readonly DependencyProperty StepProperty =
        DependencyProperty.Register(
            nameof(Step),
            typeof(double),
            typeof(InputNumber),
            new PropertyMetadata(1d, null, CoerceStep));

    /// <summary>Identifies the <see cref="Precision"/> dependency property.</summary>
    public static readonly DependencyProperty PrecisionProperty =
        DependencyProperty.Register(
            nameof(Precision),
            typeof(int?),
            typeof(InputNumber),
            new PropertyMetadata(null, null, CoercePrecision));

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(InputNumberSize),
            typeof(InputNumber),
            new PropertyMetadata(InputNumberSize.Default));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static InputNumber()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(InputNumber),
            new FrameworkPropertyMetadata(typeof(InputNumber)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the current numeric value.
    /// Null represents an empty / unset state.
    /// Out-of-range values are coerced to <see cref="Minimum"/> or <see cref="Maximum"/>.
    /// </summary>
    public double? Value
    {
        get => (double?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the lower bound of the allowed value range.
    /// Defaults to <see cref="double.NegativeInfinity"/> (no lower bound).
    /// </summary>
    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    /// Gets or sets the upper bound of the allowed value range.
    /// Defaults to <see cref="double.PositiveInfinity"/> (no upper bound).
    /// </summary>
    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    /// <summary>
    /// Gets or sets the increment/decrement amount applied by the stepper buttons
    /// or keyboard Up/Down keys.
    /// Defaults to 1. Values &lt;= 0 are coerced to a minimum of 0.000001.
    /// </summary>
    public double Step
    {
        get => (double)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    /// <summary>
    /// Gets or sets the number of decimal places to which the displayed value is
    /// rounded. When null, no rounding is applied.
    /// Values &lt; 0 are coerced to 0.
    /// </summary>
    public int? Precision
    {
        get => (int?)GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);
    }

    /// <summary>Gets or sets the size variant of the input control.</summary>
    public InputNumberSize Size
    {
        get => (InputNumberSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>Occurs when <see cref="Value"/> changes.</summary>
    public event RoutedPropertyChangedEventHandler<double?> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    private TextBox? _textBox;

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_textBox != null)
        {
            _textBox.LostFocus -= OnTextBoxLostFocus;
            _textBox.KeyDown -= OnTextBoxKeyDown;
        }

        _textBox = GetTemplateChild(PART_TextBox) as TextBox;

        if (_textBox != null)
        {
            _textBox.LostFocus += OnTextBoxLostFocus;
            _textBox.KeyDown += OnTextBoxKeyDown;
            SyncTextBox();
        }

        if (GetTemplateChild(PART_IncreaseButton) is System.Windows.Controls.Primitives.ButtonBase incBtn)
        {
            incBtn.Click -= OnIncreaseClick;
            incBtn.Click += OnIncreaseClick;
        }

        if (GetTemplateChild(PART_DecreaseButton) is System.Windows.Controls.Primitives.ButtonBase decBtn)
        {
            decBtn.Click -= OnDecreaseClick;
            decBtn.Click += OnDecreaseClick;
        }
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
        var ctrl = (InputNumber)d;
        if (baseValue is not double val) return baseValue;

        double min = ctrl.Minimum;
        double max = ctrl.Maximum;

        if (val < min) val = min;
        if (val > max) val = max;

        int? precision = ctrl.Precision;
        if (precision.HasValue)
            val = Math.Round(val, precision.Value, MidpointRounding.AwayFromZero);

        return val;
    }

    private static object CoerceStep(DependencyObject d, object baseValue)
    {
        double value = (double)baseValue;
        return value <= 0d ? 0.000001d : value;
    }

    private static object CoercePrecision(DependencyObject d, object baseValue)
    {
        if (baseValue is int p && p < 0) return 0;
        return baseValue;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (InputNumber)d;
        ctrl.SyncTextBox();
        ctrl.RaiseEvent(new RoutedPropertyChangedEventArgs<double?>(
            (double?)e.OldValue,
            (double?)e.NewValue,
            ValueChangedEvent));
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void SyncTextBox()
    {
        if (_textBox == null) return;

        if (Value is double v)
        {
            _textBox.Text = Precision.HasValue
                ? v.ToString("F" + Precision.Value)
                : v.ToString("G");
        }
        else
        {
            _textBox.Text = string.Empty;
        }
    }

    private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
    {
        CommitText();
    }

    private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Enter:
                CommitText();
                e.Handled = true;
                break;

            case Key.Up:
                Increase();
                e.Handled = true;
                break;

            case Key.Down:
                Decrease();
                e.Handled = true;
                break;
        }
    }

    private void CommitText()
    {
        if (_textBox == null) return;

        string text = _textBox.Text.Trim();

        if (string.IsNullOrEmpty(text))
        {
            Value = null;
        }
        else if (double.TryParse(text, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.CurrentCulture, out double parsed))
        {
            Value = parsed;
        }
        else
        {
            // Restore previous valid text
            SyncTextBox();
        }
    }

    private void OnIncreaseClick(object sender, RoutedEventArgs e) => Increase();

    private void OnDecreaseClick(object sender, RoutedEventArgs e) => Decrease();

    /// <summary>Increments <see cref="Value"/> by <see cref="Step"/>.</summary>
    public void Increase()
    {
        double current = Value ?? 0d;
        Value = current + Step;
    }

    /// <summary>Decrements <see cref="Value"/> by <see cref="Step"/>.</summary>
    public void Decrease()
    {
        double current = Value ?? 0d;
        Value = current - Step;
    }
}
