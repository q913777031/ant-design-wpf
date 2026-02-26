using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AntDesign.WPF.Helpers;

/// <summary>
/// Provides keyboard focus trapping within a container element.
/// When active, Tab/Shift+Tab cycle within the container and Escape closes.
/// </summary>
internal static class FocusTrapHelper
{
    /// <summary>
    /// Collects all focusable elements within <paramref name="container"/> in visual-tree order.
    /// </summary>
    public static List<UIElement> GetFocusableElements(DependencyObject container)
    {
        var result = new List<UIElement>();
        CollectFocusable(container, result);
        return result;
    }

    /// <summary>
    /// Focuses the first focusable element within the container.
    /// </summary>
    public static void FocusFirst(DependencyObject container)
    {
        var elements = GetFocusableElements(container);
        if (elements.Count > 0)
            elements[0].Focus();
    }

    /// <summary>
    /// Handles Tab/Shift+Tab to trap focus within the container.
    /// Returns true if the event was handled.
    /// </summary>
    public static bool HandleTabKey(DependencyObject container, KeyEventArgs e)
    {
        if (e.Key != Key.Tab) return false;

        var elements = GetFocusableElements(container);
        if (elements.Count == 0) return false;

        var focused = Keyboard.FocusedElement as UIElement;
        int currentIndex = focused != null ? elements.IndexOf(focused) : -1;

        bool shiftTab = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

        if (shiftTab)
        {
            // Shift+Tab: go backwards
            if (currentIndex <= 0)
            {
                elements[^1].Focus();
                e.Handled = true;
                return true;
            }
        }
        else
        {
            // Tab: go forwards
            if (currentIndex >= elements.Count - 1)
            {
                elements[0].Focus();
                e.Handled = true;
                return true;
            }
        }

        return false;
    }

    private static void CollectFocusable(DependencyObject parent, List<UIElement> result)
    {
        int count = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is UIElement element &&
                element.Visibility == Visibility.Visible &&
                element.Focusable &&
                element.IsEnabled &&
                element is not Panel) // skip panels, they're containers
            {
                result.Add(element);
            }
            CollectFocusable(child, result);
        }
    }
}
