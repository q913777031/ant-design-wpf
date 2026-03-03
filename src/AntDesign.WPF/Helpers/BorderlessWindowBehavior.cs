using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Shell;

namespace AntDesign.WPF.Helpers;

/// <summary>
/// Attached behavior that enables borderless window support.
/// Hooks WM_GETMINMAXINFO to prevent maximized window from covering the taskbar,
/// and wires PART_MinimizeButton/PART_MaximizeButton/PART_CloseButton to SystemCommands.
/// </summary>
public static class BorderlessWindowBehavior
{
    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(BorderlessWindowBehavior),
            new PropertyMetadata(false, OnEnableChanged));

    public static bool GetEnable(DependencyObject obj) => (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) => obj.SetValue(EnableProperty, value);

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window window) return;
        if ((bool)e.NewValue)
        {
            window.SourceInitialized += OnSourceInitialized;
            window.Loaded += OnLoaded;
            window.StateChanged += OnStateChanged;
        }
        else
        {
            window.SourceInitialized -= OnSourceInitialized;
            window.Loaded -= OnLoaded;
            window.StateChanged -= OnStateChanged;
        }
    }

    private static void OnSourceInitialized(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;
        var hwnd = new WindowInteropHelper(window).Handle;
        HwndSource.FromHwnd(hwnd)?.AddHook(WindowProc);
    }

    private static void OnStateChanged(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;

        // Update maximize/restore icon via visual state
        if (window.Template?.FindName("PART_MaximizeButton", window) is Button maxBtn)
        {
            // Find the Path elements within the button and toggle visibility
            if (maxBtn.Template?.FindName("MaximizeIcon", maxBtn) is UIElement maxIcon)
                maxIcon.Visibility = window.WindowState == WindowState.Maximized
                    ? Visibility.Collapsed : Visibility.Visible;
            if (maxBtn.Template?.FindName("RestoreIcon", maxBtn) is UIElement restoreIcon)
                restoreIcon.Visibility = window.WindowState == WindowState.Maximized
                    ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    private static void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (sender is not Window window) return;

        if (window.Template?.FindName("PART_MinimizeButton", window) is Button minBtn)
        {
            minBtn.Click += (_, _) => SystemCommands.MinimizeWindow(window);
        }

        if (window.Template?.FindName("PART_MaximizeButton", window) is Button maxBtn)
        {
            maxBtn.Click += (_, _) =>
            {
                if (window.WindowState == WindowState.Maximized)
                    SystemCommands.RestoreWindow(window);
                else
                    SystemCommands.MaximizeWindow(window);
            };
        }

        if (window.Template?.FindName("PART_CloseButton", window) is Button closeBtn)
        {
            closeBtn.Click += (_, _) => SystemCommands.CloseWindow(window);
        }

        // Initial state sync
        OnStateChanged(window, EventArgs.Empty);
    }

    private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        // WM_GETMINMAXINFO — prevent maximized window from covering the taskbar
        if (msg == 0x0024) // WM_GETMINMAXINFO
        {
            GetMinMaxInfo(hwnd, lParam);
            handled = true;
        }
        return IntPtr.Zero;
    }

    private static void GetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
    {
        var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

        var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
        if (monitor != IntPtr.Zero)
        {
            var monitorInfo = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
            if (GetMonitorInfo(monitor, ref monitorInfo))
            {
                var rcWork = monitorInfo.rcWork;
                var rcMonitor = monitorInfo.rcMonitor;

                mmi.ptMaxPosition.X = Math.Abs(rcWork.Left - rcMonitor.Left);
                mmi.ptMaxPosition.Y = Math.Abs(rcWork.Top - rcMonitor.Top);
                mmi.ptMaxSize.X = Math.Abs(rcWork.Right - rcWork.Left);
                mmi.ptMaxSize.Y = Math.Abs(rcWork.Bottom - rcWork.Top);
            }
        }

        Marshal.StructureToPtr(mmi, lParam, true);
    }

    private const int MONITOR_DEFAULTTONEAREST = 0x00000002;

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMaxTrackSize;
        public POINT ptMinTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public int dwFlags;
    }
}
