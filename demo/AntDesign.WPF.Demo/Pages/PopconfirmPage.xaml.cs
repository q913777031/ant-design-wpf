using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF.Demo.Helpers;

namespace AntDesign.WPF.Demo.Pages;

public partial class PopconfirmPage : UserControl
{
    public PopconfirmPage()
    {
        InitializeComponent();
    }

    private void OnDeleteConfirmed(object sender, RoutedEventArgs e)
    {
        StatusText.Text = LanguageHelper.GetString("Popconfirm.DeletedSuccess");
    }
}
