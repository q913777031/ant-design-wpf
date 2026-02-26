using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Controls;

/// <summary>
/// Represents a single segment inside a <see cref="Breadcrumb"/> navigation trail.
/// The <see cref="ContentControl.Content"/> property holds the visible label
/// (text, image, or arbitrary UI element).
/// </summary>
public class BreadcrumbItem : ContentControl
{
    // -------------------------------------------------------------------------
    // Static Constructor
    // -------------------------------------------------------------------------

    static BreadcrumbItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(BreadcrumbItem),
            new FrameworkPropertyMetadata(typeof(BreadcrumbItem)));
    }
}
