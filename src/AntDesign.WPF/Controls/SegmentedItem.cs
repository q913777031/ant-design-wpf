using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Represents a single option inside a <see cref="Segmented"/> control.
/// The <see cref="ContentControl.Content"/> property holds the option label
/// (text, icon, or arbitrary UI element).
/// Inherits from <see cref="ButtonBase"/> to support click interaction.
/// </summary>
public class SegmentedItem : ButtonBase
{
    // -------------------------------------------------------------------------
    // Dependency Properties
    // -------------------------------------------------------------------------

    private static readonly DependencyPropertyKey IsSelectedPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(IsSelected),
            typeof(bool),
            typeof(SegmentedItem),
            new PropertyMetadata(false));

    /// <summary>Identifies the read-only <see cref="IsSelected"/> dependency property.</summary>
    public static readonly DependencyProperty IsSelectedProperty =
        IsSelectedPropertyKey.DependencyProperty;

    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static SegmentedItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedItem),
            new FrameworkPropertyMetadata(typeof(SegmentedItem)));
    }

    // -------------------------------------------------------------------------
    // CLR Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gets a value indicating whether this segment is the currently selected item.
    /// Managed by the parent <see cref="Segmented"/> control via the
    /// <see cref="Selector"/> selection machinery.
    /// </summary>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        internal set => SetValue(IsSelectedPropertyKey, value);
    }
}
