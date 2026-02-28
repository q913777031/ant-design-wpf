using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Controls;
using AntDesign.WPF.Demo.Helpers;

namespace AntDesign.WPF.Demo.Pages;

public partial class DrawerPage : UserControl
{
    public DrawerPage()
    {
        InitializeComponent();
    }

    private void OpenRightDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = LanguageHelper.GetString("Drawer.Right");
        DemoDrawer.Placement = DrawerPlacement.Right;
        DemoDrawer.IsOpen = true;
    }

    private void OpenLeftDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = LanguageHelper.GetString("Drawer.Left");
        DemoDrawer.Placement = DrawerPlacement.Left;
        DemoDrawer.IsOpen = true;
    }

    private void OpenTopDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = LanguageHelper.GetString("Drawer.Top");
        DemoDrawer.Placement = DrawerPlacement.Top;
        DemoDrawer.IsOpen = true;
    }

    private void OpenBottomDrawer_Click(object sender, RoutedEventArgs e)
    {
        DemoDrawer.Title = LanguageHelper.GetString("Drawer.Bottom");
        DemoDrawer.Placement = DrawerPlacement.Bottom;
        DemoDrawer.IsOpen = true;
    }
}
