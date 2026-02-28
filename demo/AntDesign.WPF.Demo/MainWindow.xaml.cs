namespace AntDesign.WPF.Demo;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AntDesign.WPF;
using AntDesign.WPF.Colors;
using AntDesign.WPF.Demo.Helpers;
using AntDesign.WPF.Demo.Pages;
using AntDesign.WPF.Demo.ViewModels;

#pragma warning disable CA1001 // WPF Window cleanup is done in OnClosed, not IDisposable
public partial class MainWindow : Window
#pragma warning restore CA1001
{
    private const string DefaultPageKey = "Welcome";

    private static readonly Dictionary<string, Func<UserControl>> _pageFactories = new()
    {
        ["Welcome"]    = () => new WelcomePage(),
        ["Button"]     = () => new ButtonPage(),
        ["Typography"] = () => new TypographyPage(),
        ["Input"]      = () => new InputPage(),
        ["CheckBox"]   = () => new CheckBoxPage(),
        ["Select"]     = () => new SelectPage(),
        ["Switch"]     = () => new SwitchPage(),
        ["Rate"]       = () => new RatePage(),
        ["DataEntry"]  = () => new DataEntryPage(),
        ["Card"]       = () => new CardPage(),
        ["Tag"]        = () => new TagPage(),
        ["Badge"]      = () => new BadgePage(),
        ["Table"]      = () => new TablePage(),
        ["Tabs"]       = () => new TabsPage(),
        ["Timeline"]   = () => new TimelinePage(),
        ["Divider"]    = () => new DividerPage(),
        ["Empty"]      = () => new EmptyPage(),
        ["Alert"]      = () => new AlertPage(),
        ["Progress"]   = () => new ProgressPage(),
        ["Spin"]       = () => new SpinPage(),
        ["Modal"]      = () => new ModalPage(),
        ["Drawer"]     = () => new DrawerPage(),
        ["Popconfirm"] = () => new PopconfirmPage(),
        ["Message"]    = () => new MessagePage(),
        ["Result"]     = () => new ResultPage(),
        ["Steps"]      = () => new StepsPage(),
        ["Pagination"] = () => new PaginationPage(),
        ["Segmented"]  = () => new SegmentedPage(),
        ["Theme"]      = () => new ThemePage(),
    };

    private readonly MainViewModel _viewModel;
    private readonly Dictionary<string, UserControl> _pageCache = new();
    private ICollectionView? _groupedView;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;

        RefreshGroupedView();

        NavigateToPage(DefaultPageKey);

        var firstItem = _viewModel.AllItems.FirstOrDefault(i => i.PageKey == DefaultPageKey);
        if (firstItem != null)
        {
            _viewModel.SelectedItem = firstItem;
            NavList.SelectedItem = firstItem;
        }

        LanguageHelper.LanguageChanged += OnLanguageChanged;
    }

    protected override void OnClosed(EventArgs e)
    {
        LanguageHelper.LanguageChanged -= OnLanguageChanged;

        foreach (var page in _pageCache.Values)
        {
            if (page is IDisposable disposable)
                disposable.Dispose();
        }
        _pageCache.Clear();

        _viewModel.Dispose();

        base.OnClosed(e);
    }

    private void OnLanguageChanged()
    {
        _viewModel.RefreshNavigation();
        RefreshGroupedView();

        _pageCache.Clear();

        if (NavList.SelectedItem is NavigationItem item)
        {
            PageTitleText.Text = item.PageKey == DefaultPageKey
                ? LanguageHelper.GetString("Demo.AppTitle")
                : item.Title;
            NavigateToPage(item.PageKey);
        }
    }

    private void NavList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavList.SelectedItem is NavigationItem item)
        {
            PageTitleText.Text = item.PageKey == DefaultPageKey
                ? LanguageHelper.GetString("Demo.AppTitle")
                : item.Title;
            NavigateToPage(item.PageKey);
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.SearchText = SearchBox.Text;
        RefreshGroupedView();
    }

    private void RefreshGroupedView()
    {
        _groupedView = CollectionViewSource.GetDefaultView(_viewModel.FilteredItems);
        _groupedView.GroupDescriptions.Clear();
        _groupedView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        NavList.ItemsSource = _groupedView;
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

    private static UserControl? CreatePage(string key)
    {
        return _pageFactories.TryGetValue(key, out var factory) ? factory() : null;
    }

    private void SetPrimaryColor_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string colorName)
        {
            if (Enum.TryParse<PresetColor>(colorName, out var color))
                ThemeHelper.SetPrimaryColor(color);
        }
    }

    private void ToggleTheme_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.IsDarkTheme = !_viewModel.IsDarkTheme;
        ThemeIcon.Text = _viewModel.IsDarkTheme ? "\u263D" : "\u2600";
    }

    private void ToggleLanguage_Click(object sender, RoutedEventArgs e)
    {
        LanguageHelper.ToggleLanguage();
    }
}
