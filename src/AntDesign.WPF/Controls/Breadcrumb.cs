using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// A navigation breadcrumb control that renders a horizontal sequence of
/// <see cref="BreadcrumbItem"/> elements separated by a configurable separator string.
/// Follows the Ant Design Breadcrumb specification.
/// </summary>
public class Breadcrumb : ItemsControl
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    /// <summary>Identifies the <see cref="Separator"/> dependency property.</summary>
    public static readonly DependencyProperty SeparatorProperty =
        DependencyProperty.Register(
            nameof(Separator),
            typeof(string),
            typeof(Breadcrumb),
            new PropertyMetadata("/"));

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static Breadcrumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Breadcrumb),
            new FrameworkPropertyMetadata(typeof(Breadcrumb)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the separator string displayed between breadcrumb items.
    /// Defaults to <c>"/"</c>.
    /// </summary>
    public string Separator
    {
        get => (string)GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }

    // -------------------------------------------------------------------------
    // Item container override
    // -------------------------------------------------------------------------

    /// <inheritdoc/>
    protected override bool IsItemItsOwnContainerOverride(object item) =>
        item is BreadcrumbItem;

    /// <inheritdoc/>
    protected override DependencyObject GetContainerForItemOverride() =>
        new BreadcrumbItem();
}
