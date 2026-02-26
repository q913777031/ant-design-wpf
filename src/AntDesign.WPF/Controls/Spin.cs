using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of a <see cref="Spin"/> indicator.
/// </summary>
public enum SpinSize
{
    /// <summary>Small spinner — suitable for inline use.</summary>
    Small,

    /// <summary>Default-sized spinner.</summary>
    Default,

    /// <summary>Large spinner — suitable for page-level loading.</summary>
    Large
}

/// <summary>
/// A loading spinner that can wrap arbitrary content with a translucent overlay
/// while an asynchronous operation is in progress.
/// Follows the Ant Design Spin specification.
/// </summary>
public class Spin : ContentControl
{
    private Storyboard? _spinStoryboard;
    private FrameworkElement? _spinnerRing;

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    public static readonly DependencyProperty IsSpinningProperty =
        DependencyProperty.Register(nameof(IsSpinning), typeof(bool), typeof(Spin),
            new PropertyMetadata(true, OnIsSpinningChanged));

    public static readonly DependencyProperty TipProperty =
        DependencyProperty.Register(nameof(Tip), typeof(string), typeof(Spin),
            new PropertyMetadata(null));

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(SpinSize), typeof(Spin),
            new PropertyMetadata(SpinSize.Default));

    public static readonly DependencyProperty DelayProperty =
        DependencyProperty.Register(nameof(Delay), typeof(int), typeof(Spin),
            new PropertyMetadata(0, null, CoerceDelay));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Spin()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Spin), new FrameworkPropertyMetadata(typeof(Spin)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    public bool IsSpinning
    {
        get => (bool)GetValue(IsSpinningProperty);
        set => SetValue(IsSpinningProperty, value);
    }

    public string? Tip
    {
        get => (string?)GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    public SpinSize Size
    {
        get => (SpinSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public int Delay
    {
        get => (int)GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _spinnerRing = GetTemplateChild("SpinnerRing") as FrameworkElement;
        SetupSpinAnimation();

        if (IsSpinning)
            StartSpinning();
    }

    private void SetupSpinAnimation()
    {
        if (_spinnerRing is null) return;

        // Ensure we have a RotateTransform
        if (_spinnerRing.RenderTransform is not RotateTransform)
        {
            _spinnerRing.RenderTransform = new RotateTransform(0,
                _spinnerRing.Width / 2, _spinnerRing.Height / 2);
        }

        _spinStoryboard = new Storyboard { RepeatBehavior = RepeatBehavior.Forever };
        var rotateAnim = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(1)));
        Storyboard.SetTarget(rotateAnim, _spinnerRing);
        Storyboard.SetTargetProperty(rotateAnim,
            new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
        _spinStoryboard.Children.Add(rotateAnim);
    }

    private void StartSpinning()
    {
        _spinStoryboard?.Begin(this, true);
    }

    private void StopSpinning()
    {
        _spinStoryboard?.Stop(this);
    }

    private static void OnIsSpinningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Spin spin && spin._spinStoryboard is not null)
        {
            if ((bool)e.NewValue)
                spin.StartSpinning();
            else
                spin.StopSpinning();
        }
    }

    private static object CoerceDelay(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 0 ? 0 : value;
    }
}
