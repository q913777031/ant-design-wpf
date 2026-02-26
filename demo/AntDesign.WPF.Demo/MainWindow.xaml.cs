namespace AntDesign.WPF.Demo;

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AntDesign.WPF;
using AntDesign.WPF.Colors;
using AntDesign.WPF.Demo.Pages;
using AntDesign.WPF.Demo.ViewModels;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    private readonly Dictionary<string, UserControl> _pageCache = new();

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;

        // Setup grouped navigation using CollectionViewSource
        var view = CollectionViewSource.GetDefaultView(_viewModel.FilteredItems);
        view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        NavList.ItemsSource = view;

        // Navigate to Welcome page by default
        NavigateToPage("Welcome");

        // Select the matching nav item
        var firstItem = _viewModel.AllItems.FirstOrDefault(i => i.PageKey == "Welcome");
        if (firstItem != null)
        {
            _viewModel.SelectedItem = firstItem;
            NavList.SelectedItem = firstItem;
        }
    }

    private void NavList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavList.SelectedItem is NavigationItem item)
        {
            PageTitleText.Text = item.PageKey == "Welcome" ? "Ant Design WPF" : item.Title;
            NavigateToPage(item.PageKey);
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.SearchText = SearchBox.Text;

        // Refresh the grouped view
        var view = CollectionViewSource.GetDefaultView(_viewModel.FilteredItems);
        view.GroupDescriptions.Clear();
        view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        NavList.ItemsSource = view;
    }

    private void NavigateToPage(string pageKey)
    {
        if (!_pageCache.TryGetValue(pageKey, out var page))
        {
            page = CreatePage(pageKey);
            if (page != null)
                _pageCache[pageKey] = page;
        }
        if (page != null)
            ContentPresenter.Content = page;
    }

    private UserControl? CreatePage(string key) => key switch
    {
        "Welcome"    => new WelcomePage(),
        "Button"     => new ButtonPage(),
        "Typography" => new TypographyPage(),
        "Input"      => new InputPage(),
        "CheckBox"   => new CheckBoxPage(),
        "Select"     => new SelectPage(),
        "Switch"     => new SwitchPage(),
        "Rate"       => new RatePage(),
        "DataEntry"  => new DataEntryPage(),
        "Card"       => new CardPage(),
        "Tag"        => new TagPage(),
        "Badge"      => new BadgePage(),
        "Table"      => new TablePage(),
        "Tabs"       => new TabsPage(),
        "Timeline"   => new TimelinePage(),
        "Divider"    => new DividerPage(),
        "Empty"      => new EmptyPage(),
        "Alert"      => new AlertPage(),
        "Progress"   => new ProgressPage(),
        "Spin"       => new SpinPage(),
        "Modal"      => new ModalPage(),
        "Drawer"     => new DrawerPage(),
        "Popconfirm" => new PopconfirmPage(),
        "Message"    => new MessagePage(),
        "Result"     => new ResultPage(),
        "Steps"      => new StepsPage(),
        "Pagination" => new PaginationPage(),
        "Segmented"  => new SegmentedPage(),
        "Theme"      => new ThemePage(),
        _            => null
    };

    private void SetPrimaryColor_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string colorName)
        {
            if (System.Enum.TryParse<PresetColor>(colorName, out var color))
                ThemeHelper.SetPrimaryColor(color);
        }
    }

    private void ToggleTheme_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.IsDarkTheme = !_viewModel.IsDarkTheme;
        ThemeIcon.Text = _viewModel.IsDarkTheme ? "\u263D" : "\u2600";
    }
}
