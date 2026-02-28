namespace AntDesign.WPF.Demo.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using AntDesign.WPF;
using AntDesign.WPF.Demo.Helpers;

public class NavigationItem : ViewModelBase
{
    private string _title = "";
    private string _category = "";

    public string TitleKey { get; set; } = "";
    public string CategoryKey { get; set; } = "";
    public string PageKey { get; set; } = "";
    public string Icon { get; set; } = "";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public void Refresh()
    {
        Title = LanguageHelper.GetString(TitleKey);
        Category = LanguageHelper.GetString(CategoryKey);
    }
}

public class MainViewModel : ViewModelBase
{
    private NavigationItem? _selectedItem;
    private bool _isDarkTheme;
    private string _searchText = "";
    private readonly ObservableCollection<NavigationItem> _filteredItems = new();

    public ObservableCollection<NavigationItem> AllItems { get; } = new();
    public ObservableCollection<NavigationItem> FilteredItems => _filteredItems;

    public NavigationItem? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
                OnThemeChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
                FilterNavigation();
        }
    }

    public MainViewModel()
    {
        InitializeNavigation();
        FilterNavigation();
    }

    public void RefreshNavigation()
    {
        foreach (var item in AllItems)
            item.Refresh();

        FilterNavigation();
    }

    private void InitializeNavigation()
    {
        // Overview
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Welcome", CategoryKey = "Nav.Cat.Overview", PageKey = "Welcome", Icon = "H" });

        // General
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Button", CategoryKey = "Nav.Cat.General", PageKey = "Button", Icon = "B" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Typography", CategoryKey = "Nav.Cat.General", PageKey = "Typography", Icon = "T" });

        // Data Entry
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Input", CategoryKey = "Nav.Cat.DataEntry", PageKey = "Input", Icon = "I" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.CheckBox", CategoryKey = "Nav.Cat.DataEntry", PageKey = "CheckBox", Icon = "C" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Select", CategoryKey = "Nav.Cat.DataEntry", PageKey = "Select", Icon = "S" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Switch", CategoryKey = "Nav.Cat.DataEntry", PageKey = "Switch", Icon = "W" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Rate", CategoryKey = "Nav.Cat.DataEntry", PageKey = "Rate", Icon = "R" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.DataEntry", CategoryKey = "Nav.Cat.DataEntry", PageKey = "DataEntry", Icon = "D" });

        // Data Display
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Card", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Card", Icon = "C" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Tag", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Tag", Icon = "T" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Badge", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Badge", Icon = "B" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Table", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Table", Icon = "T" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Tabs", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Tabs", Icon = "T" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Timeline", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Timeline", Icon = "L" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Divider", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Divider", Icon = "D" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Empty", CategoryKey = "Nav.Cat.DataDisplay", PageKey = "Empty", Icon = "E" });

        // Feedback
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Alert", CategoryKey = "Nav.Cat.Feedback", PageKey = "Alert", Icon = "A" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Progress", CategoryKey = "Nav.Cat.Feedback", PageKey = "Progress", Icon = "P" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Spin", CategoryKey = "Nav.Cat.Feedback", PageKey = "Spin", Icon = "S" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Modal", CategoryKey = "Nav.Cat.Feedback", PageKey = "Modal", Icon = "M" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Drawer", CategoryKey = "Nav.Cat.Feedback", PageKey = "Drawer", Icon = "D" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Popconfirm", CategoryKey = "Nav.Cat.Feedback", PageKey = "Popconfirm", Icon = "P" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Message", CategoryKey = "Nav.Cat.Feedback", PageKey = "Message", Icon = "M" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Result", CategoryKey = "Nav.Cat.Feedback", PageKey = "Result", Icon = "R" });

        // Navigation
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Steps", CategoryKey = "Nav.Cat.Navigation", PageKey = "Steps", Icon = "S" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Pagination", CategoryKey = "Nav.Cat.Navigation", PageKey = "Pagination", Icon = "P" });
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Segmented", CategoryKey = "Nav.Cat.Navigation", PageKey = "Segmented", Icon = "S" });

        // Settings
        AllItems.Add(new NavigationItem { TitleKey = "Nav.Theme", CategoryKey = "Nav.Cat.Settings", PageKey = "Theme", Icon = "T" });

        // Resolve initial titles
        foreach (var item in AllItems)
            item.Refresh();
    }

    private void FilterNavigation()
    {
        _filteredItems.Clear();

        var items = string.IsNullOrWhiteSpace(SearchText)
            ? AllItems
            : AllItems.Where(i => i.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                  i.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                  i.PageKey.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (var item in items)
            _filteredItems.Add(item);
    }

    private void OnThemeChanged()
    {
        ThemeHelper.SetBaseTheme(IsDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _filteredItems.Clear();
            AllItems.Clear();
        }

        base.Dispose(disposing);
    }
}
