namespace AntDesign.WPF.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

public class TransitionPresenter : ContentControl
{
    public static readonly DependencyProperty TransitionTypeProperty =
        DependencyProperty.Register(nameof(TransitionType), typeof(TransitionType), typeof(TransitionPresenter),
            new PropertyMetadata(TransitionType.Fade));

    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(TransitionPresenter),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(250))));

    public TransitionType TransitionType { get => (TransitionType)GetValue(TransitionTypeProperty); set => SetValue(TransitionTypeProperty, value); }
    public Duration Duration { get => (Duration)GetValue(DurationProperty); set => SetValue(DurationProperty, value); }

    private ContentPresenter? _currentPresenter;
    private ContentPresenter? _previousPresenter;
    private Grid? _rootGrid;

    static TransitionPresenter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionPresenter), new FrameworkPropertyMetadata(typeof(TransitionPresenter)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _rootGrid = GetTemplateChild("PART_Root") as Grid;
        _currentPresenter = GetTemplateChild("PART_CurrentContent") as ContentPresenter;
        _previousPresenter = GetTemplateChild("PART_PreviousContent") as ContentPresenter;
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);
        if (_rootGrid == null || _currentPresenter == null || _previousPresenter == null) return;
        if (oldContent == null) return; // First time, no animation

        // Move old content to previous presenter
        _previousPresenter.Content = oldContent;
        _previousPresenter.Opacity = 1;
        _currentPresenter.Content = newContent;
        _currentPresenter.Opacity = 0;

        var duration = Duration;
        var ease = new CubicEase { EasingMode = EasingMode.EaseOut };

        switch (TransitionType)
        {
            case TransitionType.Fade:
                AnimateFade(duration, ease);
                break;
            case TransitionType.SlideLeft:
                AnimateSlide(duration, ease, -1);
                break;
            case TransitionType.SlideRight:
                AnimateSlide(duration, ease, 1);
                break;
            case TransitionType.SlideUp:
                AnimateSlideVertical(duration, ease, -1);
                break;
        }
    }

    private void AnimateFade(Duration duration, IEasingFunction ease)
    {
        var fadeOut = new DoubleAnimation(1, 0, duration) { EasingFunction = ease };
        var fadeIn = new DoubleAnimation(0, 1, duration) { EasingFunction = ease };
        fadeOut.Completed += (s, e) => { _previousPresenter!.Content = null; };
        _previousPresenter!.BeginAnimation(OpacityProperty, fadeOut);
        _currentPresenter!.BeginAnimation(OpacityProperty, fadeIn);
    }

    private void AnimateSlide(Duration duration, IEasingFunction ease, int direction)
    {
        double offset = 40 * direction;
        // Previous slides out
        var slideOut = new DoubleAnimation(0, -offset, duration) { EasingFunction = ease };
        var fadeOut = new DoubleAnimation(1, 0, duration) { EasingFunction = ease };
        // Current slides in
        var slideIn = new DoubleAnimation(offset, 0, duration) { EasingFunction = ease };
        var fadeIn = new DoubleAnimation(0, 1, duration) { EasingFunction = ease };

        _previousPresenter!.RenderTransform = new TranslateTransform();
        _currentPresenter!.RenderTransform = new TranslateTransform();

        fadeOut.Completed += (s, e) => { _previousPresenter.Content = null; };

        _previousPresenter.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideOut);
        _previousPresenter.BeginAnimation(OpacityProperty, fadeOut);
        _currentPresenter.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideIn);
        _currentPresenter.BeginAnimation(OpacityProperty, fadeIn);
    }

    private void AnimateSlideVertical(Duration duration, IEasingFunction ease, int direction)
    {
        double offset = 30 * direction;
        var slideIn = new DoubleAnimation(Math.Abs(offset), 0, duration) { EasingFunction = ease };
        var fadeIn = new DoubleAnimation(0, 1, duration) { EasingFunction = ease };
        var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(100)));

        _previousPresenter!.RenderTransform = new TranslateTransform();
        _currentPresenter!.RenderTransform = new TranslateTransform();

        fadeOut.Completed += (s, e) => { _previousPresenter.Content = null; };
        _previousPresenter.BeginAnimation(OpacityProperty, fadeOut);
        _currentPresenter.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideIn);
        _currentPresenter.BeginAnimation(OpacityProperty, fadeIn);
    }
}

public enum TransitionType
{
    Fade,
    SlideLeft,
    SlideRight,
    SlideUp
}
