using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// A paging navigation control that allows the user to navigate between pages
/// of a large data set.
/// Follows the Ant Design Pagination specification.
/// </summary>
[TemplatePart(Name = Pagination.PART_PrevButton, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
[TemplatePart(Name = Pagination.PART_NextButton, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
[TemplatePart(Name = Pagination.PART_QuickJumper, Type = typeof(TextBox))]
public class Pagination : Control
{
    /// <summary>Template part name for the previous-page button.</summary>
    public const string PART_PrevButton = "PART_PrevButton";

    /// <summary>Template part name for the next-page button.</summary>
    public const string PART_NextButton = "PART_NextButton";

    /// <summary>Template part name for the quick-jumper text box.</summary>
    public const string PART_QuickJumper = "PART_QuickJumper";

    // -------------------------------------------------------------------------
    // Routed Events
    // -------------------------------------------------------------------------

    /// <summary>
    /// Identifies the <see cref="CurrentChanged"/> routed event.
    /// Raised when the user navigates to a different page.
    /// </summary>
    public static readonly RoutedEvent CurrentChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(CurrentChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<int>),
            typeof(Pagination));

    /// <summary>
    /// Identifies the <see cref="PageSizeChanged"/> routed event.
    /// Raised when the user selects a different page size via the size-changer.
    /// </summary>
    public static readonly RoutedEvent PageSizeChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(PageSizeChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<int>),
            typeof(Pagination));

    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Current"/> dependency property.</summary>
    public static readonly DependencyProperty CurrentProperty =
        DependencyProperty.Register(
            nameof(Current),
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(1, OnCurrentChanged, CoerceCurrent));

    /// <summary>Identifies the <see cref="Total"/> dependency property.</summary>
    public static readonly DependencyProperty TotalProperty =
        DependencyProperty.Register(
            nameof(Total),
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(0, OnTotalChanged, CoerceTotal));

    /// <summary>Identifies the <see cref="PageSize"/> dependency property.</summary>
    public static readonly DependencyProperty PageSizeProperty =
        DependencyProperty.Register(
            nameof(PageSize),
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(10, OnPageSizeChanged, CoercePageSize));

    /// <summary>Identifies the <see cref="ShowSizeChanger"/> dependency property.</summary>
    public static readonly DependencyProperty ShowSizeChangerProperty =
        DependencyProperty.Register(
            nameof(ShowSizeChanger),
            typeof(bool),
            typeof(Pagination),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="ShowQuickJumper"/> dependency property.</summary>
    public static readonly DependencyProperty ShowQuickJumperProperty =
        DependencyProperty.Register(
            nameof(ShowQuickJumper),
            typeof(bool),
            typeof(Pagination),
            new PropertyMetadata(false));

    /// <summary>Identifies the <see cref="Simple"/> dependency property.</summary>
    public static readonly DependencyProperty SimpleProperty =
        DependencyProperty.Register(
            nameof(Simple),
            typeof(bool),
            typeof(Pagination),
            new PropertyMetadata(false));

    private static readonly DependencyPropertyKey PageCountPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(PageCount),
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(0));

    /// <summary>Identifies the read-only <see cref="PageCount"/> dependency property.</summary>
    public static readonly DependencyProperty PageCountProperty =
        PageCountPropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Pagination()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Pagination),
            new FrameworkPropertyMetadata(typeof(Pagination)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the one-based index of the currently displayed page.
    /// Coerced to the range [1, <see cref="PageCount"/>].
    /// </summary>
    public int Current
    {
        get => (int)GetValue(CurrentProperty);
        set => SetValue(CurrentProperty, value);
    }

    /// <summary>
    /// Gets or sets the total number of data items across all pages.
    /// Must be 0 or greater; negative values are coerced to 0.
    /// </summary>
    public int Total
    {
        get => (int)GetValue(TotalProperty);
        set => SetValue(TotalProperty, value);
    }

    /// <summary>
    /// Gets or sets the number of data items per page.
    /// Defaults to 10. Values less than 1 are coerced to 1.
    /// </summary>
    public int PageSize
    {
        get => (int)GetValue(PageSizeProperty);
        set => SetValue(PageSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a page-size selector (e.g. "10 / page")
    /// is rendered beside the navigation controls.
    /// </summary>
    public bool ShowSizeChanger
    {
        get => (bool)GetValue(ShowSizeChangerProperty);
        set => SetValue(ShowSizeChangerProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a quick-jump text box
    /// ("Go to N") is rendered beside the navigation controls.
    /// </summary>
    public bool ShowQuickJumper
    {
        get => (bool)GetValue(ShowQuickJumperProperty);
        set => SetValue(ShowQuickJumperProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control renders in simple mode
    /// (minimal chrome — only prev/next arrows and the current page indicator).
    /// </summary>
    public bool Simple
    {
        get => (bool)GetValue(SimpleProperty);
        set => SetValue(SimpleProperty, value);
    }

    /// <summary>
    /// Gets the total number of pages derived from <see cref="Total"/> and <see cref="PageSize"/>.
    /// </summary>
    public int PageCount
    {
        get => (int)GetValue(PageCountProperty);
        private set => SetValue(PageCountPropertyKey, value);
    }

    // -------------------------------------------------------------------------
    // Events
    // -------------------------------------------------------------------------

    /// <summary>Occurs when the user navigates to a different page.</summary>
    public event RoutedPropertyChangedEventHandler<int> CurrentChanged
    {
        add => AddHandler(CurrentChangedEvent, value);
        remove => RemoveHandler(CurrentChangedEvent, value);
    }

    /// <summary>Occurs when the user selects a different page size.</summary>
    public event RoutedPropertyChangedEventHandler<int> PageSizeChanged
    {
        add => AddHandler(PageSizeChangedEvent, value);
        remove => RemoveHandler(PageSizeChangedEvent, value);
    }

    // -------------------------------------------------------------------------
    // Template
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(PART_PrevButton) is System.Windows.Controls.Primitives.ButtonBase prev)
        {
            prev.Click -= OnPrevClick;
            prev.Click += OnPrevClick;
        }

        if (GetTemplateChild(PART_NextButton) is System.Windows.Controls.Primitives.ButtonBase next)
        {
            next.Click -= OnNextClick;
            next.Click += OnNextClick;
        }

        if (GetTemplateChild(PART_QuickJumper) is TextBox jumper)
        {
            jumper.KeyDown -= OnQuickJumperKeyDown;
            jumper.KeyDown += OnQuickJumperKeyDown;
        }

        UpdatePageCount();
    }

    // -------------------------------------------------------------------------
    // Coercion Callbacks
    // -------------------------------------------------------------------------

    private static object CoerceCurrent(DependencyObject d, object baseValue)
    {
        var pg = (Pagination)d;
        int value = (int)baseValue;
        int pageCount = pg.PageCount;

        if (value < 1) return 1;
        if (pageCount > 0 && value > pageCount) return pageCount;
        return value;
    }

    private static object CoerceTotal(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 0 ? 0 : value;
    }

    private static object CoercePageSize(DependencyObject d, object baseValue)
    {
        int value = (int)baseValue;
        return value < 1 ? 1 : value;
    }

    // -------------------------------------------------------------------------
    // Property Changed Callbacks
    // -------------------------------------------------------------------------

    private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var pg = (Pagination)d;
        pg.RaiseEvent(new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue, CurrentChangedEvent));
    }

    private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var pg = (Pagination)d;
        pg.UpdatePageCount();
        pg.CoerceValue(CurrentProperty);
    }

    private static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var pg = (Pagination)d;
        pg.UpdatePageCount();
        pg.CoerceValue(CurrentProperty);
        pg.RaiseEvent(new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue, PageSizeChangedEvent));
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void UpdatePageCount()
    {
        int total = Total;
        int pageSize = PageSize;
        PageCount = pageSize > 0 ? (int)Math.Ceiling((double)total / pageSize) : 0;
    }

    private void OnPrevClick(object sender, RoutedEventArgs e)
    {
        if (Current > 1)
            Current--;
    }

    private void OnNextClick(object sender, RoutedEventArgs e)
    {
        if (Current < PageCount)
            Current++;
    }

    private void OnQuickJumperKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Enter
            && sender is TextBox tb
            && int.TryParse(tb.Text, out int target))
        {
            Current = target;
            tb.Clear();
        }
    }

    /// <summary>Navigates to the previous page if one exists.</summary>
    public void PreviousPage()
    {
        if (Current > 1) Current--;
    }

    /// <summary>Navigates to the next page if one exists.</summary>
    public void NextPage()
    {
        if (Current < PageCount) Current++;
    }
}
