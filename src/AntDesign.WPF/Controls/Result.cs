using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the semantic status displayed by a <see cref="Result"/> page.
/// </summary>
public enum ResultStatus
{
    /// <summary>Operation completed successfully (green check).</summary>
    Success,

    /// <summary>Operation failed with an error (red cross).</summary>
    Error,

    /// <summary>Informational result (blue info circle).</summary>
    Info,

    /// <summary>Result requires attention (yellow warning triangle).</summary>
    Warning,

    /// <summary>HTTP 403 — Access forbidden.</summary>
    Forbidden403,

    /// <summary>HTTP 404 — Page not found.</summary>
    NotFound404,

    /// <summary>HTTP 500 — Internal server error.</summary>
    ServerError500
}

/// <summary>
/// A full-page feedback component used to communicate the result of an operation.
/// The <see cref="ContentControl.Content"/> property acts as the "extra" action area
/// (e.g. buttons rendered below the subtitle).
/// Follows the Ant Design Result specification.
/// </summary>
public class Result : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Status"/> dependency property.</summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(ResultStatus),
            typeof(Result),
            new PropertyMetadata(ResultStatus.Info));

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Result),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="SubTitle"/> dependency property.</summary>
    public static readonly DependencyProperty SubTitleProperty =
        DependencyProperty.Register(
            nameof(SubTitle),
            typeof(string),
            typeof(Result),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(Result),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Extra"/> dependency property.</summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(
            nameof(Extra),
            typeof(object),
            typeof(Result),
            new PropertyMetadata(null));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Result()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Result),
            new FrameworkPropertyMetadata(typeof(Result)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the semantic status that selects the built-in illustration
    /// and accent color for the result page.
    /// </summary>
    public ResultStatus Status
    {
        get => (ResultStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>Gets or sets the primary result heading text.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets optional supplementary text rendered below <see cref="Title"/>.
    /// </summary>
    public string? SubTitle
    {
        get => (string?)GetValue(SubTitleProperty);
        set => SetValue(SubTitleProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom icon that overrides the default status illustration.
    /// Accepts any object.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the extra action content rendered below <see cref="SubTitle"/>.
    /// Accepts any object; typically a row of <see cref="Button"/> elements.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }
}
