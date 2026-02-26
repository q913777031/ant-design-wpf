using System.Windows;
using System.Windows.Controls;
using AntDesign.WPF;
using AntDesign.WPF.Colors;
using AntDesign.WPF.Demo.Pages;

namespace AntDesign.WPF.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Select the first navigable item (Buttons)
        foreach (ListBoxItem item in NavListBox.Items)
        {
            if (item.Tag != null)
            {
                NavListBox.SelectedItem = item;
                break;
            }
        }
    }

    private void NavListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavListBox.SelectedItem is ListBoxItem selected && selected.Tag is string tag)
        {
            NavigateTo(tag);
        }
    }

    private void NavigateTo(string tag)
    {
        UIElement? page = tag switch
        {
            "Buttons"   => new ButtonPage(),
            "Divider"   => new DividerPage(),
            "Input"     => new InputPage(),
            "CheckBox"  => new CheckBoxPage(),
            "Select"    => new SelectPage(),
            "DataEntry" => new DataEntryPage(),
            "Switch"    => new SwitchPage(),
            "Badge"     => new BadgePage(),
            "Card"      => new CardPage(),
            "Table"     => new TablePage(),
            "Tag"       => new TagPage(),
            "Timeline"  => new TimelinePage(),
            "Empty"     => new EmptyPage(),
            "Alert"     => new AlertPage(),
            "Modal"     => new ModalPage(),
            "Drawer"    => new DrawerPage(),
            "Progress"  => new ProgressPage(),
            "Result"    => new ResultPage(),
            "Steps"     => new StepsPage(),
            "Tabs"      => new TabsPage(),
            "Theme"     => new ThemePage(),
            _           => null
        };

        if (page is not null)
        {
            PageTitleText.Text = tag switch
            {
                "Buttons"   => "Button",
                "Divider"   => "Divider",
                "Input"     => "Input",
                "CheckBox"  => "Checkbox / Radio",
                "Select"    => "Select",
                "DataEntry" => "Slider / DatePicker",
                "Switch"    => "Switch",
                "Badge"     => "Badge",
                "Card"      => "Card",
                "Table"     => "Table",
                "Tag"       => "Tag",
                "Timeline"  => "Timeline",
                "Empty"     => "Empty",
                "Alert"     => "Alert",
                "Modal"     => "Modal",
                "Drawer"    => "Drawer",
                "Progress"  => "Progress",
                "Result"    => "Result",
                "Steps"     => "Steps",
                "Tabs"      => "Tabs",
                "Theme"     => "Theme",
                _           => tag
            };

            ContentFrame.Content = page;
        }
    }

    private void DarkModeToggle_Checked(object sender, RoutedEventArgs e)
    {
        ThemeHelper.SetBaseTheme(BaseTheme.Dark);
    }

    private void DarkModeToggle_Unchecked(object sender, RoutedEventArgs e)
    {
        ThemeHelper.SetBaseTheme(BaseTheme.Light);
    }

    private void ColorButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string colorName)
        {
            PresetColor color = colorName switch
            {
                "Blue"   => PresetColor.Blue,
                "Green"  => PresetColor.Green,
                "Red"    => PresetColor.Red,
                "Purple" => PresetColor.Purple,
                "Orange" => PresetColor.Orange,
                _        => PresetColor.Blue
            };
            ThemeHelper.SetPrimaryColor(color);
        }
    }
}
