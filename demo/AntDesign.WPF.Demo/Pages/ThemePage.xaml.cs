using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF;
using AntDesign.WPF.Colors;

namespace AntDesign.WPF.Demo.Pages;

public partial class ThemePage : UserControl
{
    public ThemePage()
    {
        InitializeComponent();
    }

    private void LightTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeHelper.SetBaseTheme(BaseTheme.Light);
    }

    private void DarkTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeHelper.SetBaseTheme(BaseTheme.Dark);
    }

    private void PrimaryColor_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string colorName)
        {
            PresetColor color = colorName switch
            {
                "Blue"    => PresetColor.Blue,
                "Green"   => PresetColor.Green,
                "Red"     => PresetColor.Red,
                "Purple"  => PresetColor.Purple,
                "Gold"    => PresetColor.Gold,
                "Cyan"    => PresetColor.Cyan,
                "Orange"  => PresetColor.Orange,
                "Magenta" => PresetColor.Magenta,
                _         => PresetColor.Blue
            };
            ThemeHelper.SetPrimaryColor(color);
        }
    }

    private void RadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ThemeHelper.SetBorderRadius(e.NewValue);
    }
}
