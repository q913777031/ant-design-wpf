using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class SpinPage : UserControl
{
    public SpinPage()
    {
        InitializeComponent();
    }

    private void ToggleSpin_Click(object sender, RoutedEventArgs e)
    {
        ContentSpin.IsSpinning = !ContentSpin.IsSpinning;
    }
}
