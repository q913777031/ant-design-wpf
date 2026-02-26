using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class DrawerPage : UserControl
{
    public DrawerPage()
    {
        InitializeComponent();
    }

    private void OpenRightDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = "Right Drawer";
        DemoDrawer.Placement = DrawerPlacement.Right;
        DemoDrawer.IsOpen = true;
    }

    private void OpenLeftDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = "Left Drawer";
        DemoDrawer.Placement = DrawerPlacement.Left;
        DemoDrawer.IsOpen = true;
    }

    private void OpenTopDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = "Top Drawer";
        DemoDrawer.Placement = DrawerPlacement.Top;
        DemoDrawer.IsOpen = true;
    }

    private void OpenBottomDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = "Bottom Drawer";
        DemoDrawer.Placement = DrawerPlacement.Bottom;
        DemoDrawer.IsOpen = true;
    }
}
