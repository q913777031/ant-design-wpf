using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class PopconfirmPage : UserControl
{
    public PopconfirmPage()
    {
        InitializeComponent();
    }

    private void OnDeleteConfirmed(object sender, RoutedEventArgs e)
    {
        StatusText.Text = "Item deleted successfully!";
    }
}
