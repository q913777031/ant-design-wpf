using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Represents a single step inside a <see cref="Steps"/> navigator.
/// A <see cref="StepItem"/> can override its status independently from the
/// parent <see cref="Steps"/> computed status.
/// </summary>
public class StepItem : ContentControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(StepItem),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Description"/> dependency property.</summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(StepItem),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(object),
            typeof(StepItem),
            new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="Status"/> dependency property.</summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(StepsStatus?),
            typeof(StepItem),
            new PropertyMetadata(null, OnStatusChanged));

    private static readonly DependencyPropertyKey ComputedStatusPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(ComputedStatus),
            typeof(StepsStatus),
            typeof(StepItem),
            new PropertyMetadata(StepsStatus.Wait));

    /// <summary>Identifies the read-only <see cref="ComputedStatus"/> dependency property.</summary>
    public static readonly DependencyProperty ComputedStatusProperty =
        ComputedStatusPropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static StepItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StepItem),
            new FrameworkPropertyMetadata(typeof(StepItem)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the title label for this step.</summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>Gets or sets an optional description rendered below the step title.</summary>
    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom icon to replace the default step indicator.
    /// Accepts any object; when null the default numeric or status icon is used.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets an explicit status override for this step.
    /// When <see langword="null"/> the status is computed by the parent <see cref="Steps"/>.
    /// </summary>
    public StepsStatus? Status
    {
        get => (StepsStatus?)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets the effective status of this step, combining any explicit <see cref="Status"/>
    /// override with the value computed by the parent <see cref="Steps"/>.
    /// </summary>
    public StepsStatus ComputedStatus
    {
        get => (StepsStatus)GetValue(ComputedStatusProperty);
        internal set => SetValue(ComputedStatusPropertyKey, value);
    }

    /// <summary>
    /// Gets a value indicating whether this item has a locally-set <see cref="Status"/>
    /// that should take precedence over the parent's computed value.
    /// </summary>
    internal bool HasLocalStatus => Status.HasValue;

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var item = (StepItem)d;
        if (item.Status.HasValue)
        {
            item.ComputedStatus = item.Status.Value;
        }
    }
}
