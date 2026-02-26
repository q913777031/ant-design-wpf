using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Specifies the size variant of a <see cref="Segmented"/> control.
/// </summary>
public enum SegmentedSize
{
    /// <summary>Small segmented control.</summary>
    Small,

    /// <summary>Default segmented control.</summary>
    Default,

    /// <summary>Large segmented control.</summary>
    Large
}

/// <summary>
/// A segmented control that renders a set of mutually exclusive options as a
/// horizontally grouped button strip.
/// Inherits from <see cref="Selector"/> to gain built-in selected-item tracking.
/// Follows the Ant Design Segmented specification.
/// </summary>
public class Segmented : Selector
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Size"/> dependency property.</summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(
            nameof(Size),
            typeof(SegmentedSize),
            typeof(Segmented),
            new PropertyMetadata(SegmentedSize.Default));

    /// <summary>Identifies the <see cref="Block"/> dependency property.</summary>
    public static readonly DependencyProperty BlockProperty =
        DependencyProperty.Register(
            nameof(Block),
            typeof(bool),
            typeof(Segmented),
            new PropertyMetadata(false));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Segmented()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Segmented),
            new FrameworkPropertyMetadata(typeof(Segmented)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>Gets or sets the size variant of the segmented control.</summary>
    public SegmentedSize Size
    {
        get => (SegmentedSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control stretches to fill its
    /// available width, distributing equal space to each segment.
    /// </summary>
    public bool Block
    {
        get => (bool)GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    // -------------------------------------------------------------------------
    // Item container override
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override bool IsItemItsOwnContainerOverride(object item) =>
        item is SegmentedItem;

    /// <inheritdoc/>
    protected override DependencyObject GetContainerForItemOverride() =>
        new SegmentedItem();

    /// <inheritdoc/>
    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is SegmentedItem segItem)
        {
            segItem.Click -= OnSegmentItemClick;
            segItem.Click += OnSegmentItemClick;
        }
    }

    /// <inheritdoc/>
    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        if (element is SegmentedItem segItem)
        {
            segItem.Click -= OnSegmentItemClick;
        }
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    private void OnSegmentItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is SegmentedItem clickedItem)
        {
            int index = ItemContainerGenerator.IndexFromContainer(clickedItem);
            if (index >= 0)
            {
                SelectedIndex = index;
            }
        }
    }
}
