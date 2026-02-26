namespace AntDesign.WPF.Demo.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AntDesign.WPF;
using AntDesign.WPF.Input;

public class NavigationItem
{
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string PageKey { get; set; } = "";
    public string Icon { get; set; } = "";
}

public class MainViewModel : ViewModelBase
{
    private NavigationItem? _selectedItem;
    private bool _isDarkTheme;
    private string _searchText = "";
    private ObservableCollection<NavigationItem> _filteredItems = new();

    public ObservableCollection<NavigationItem> AllItems { get; } = new();
    public ObservableCollection<NavigationItem> FilteredItems
    {
        get => _filteredItems;
        set => SetProperty(ref _filteredItems, value);
    }

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

    public ICommand ToggleThemeCommand { get; }
    public ICommand SetPrimaryColorCommand { get; }

    public MainViewModel()
    {
        ToggleThemeCommand = new RelayCommand(() => IsDarkTheme = !IsDarkTheme);
        SetPrimaryColorCommand = new RelayCommand<string>(colorName => { /* handled in view */ });

        InitializeNavigation();
        FilteredItems = new ObservableCollection<NavigationItem>(AllItems);
    }

    private void InitializeNavigation()
    {
        // Overview
        AllItems.Add(new NavigationItem { Title = "Welcome", Category = "Overview", PageKey = "Welcome", Icon = "H" });

        // General
        AllItems.Add(new NavigationItem { Title = "Button", Category = "General", PageKey = "Button", Icon = "B" });
        AllItems.Add(new NavigationItem { Title = "Typography", Category = "General", PageKey = "Typography", Icon = "T" });

        // Data Entry
        AllItems.Add(new NavigationItem { Title = "Input", Category = "Data Entry", PageKey = "Input", Icon = "I" });
        AllItems.Add(new NavigationItem { Title = "CheckBox", Category = "Data Entry", PageKey = "CheckBox", Icon = "C" });
        AllItems.Add(new NavigationItem { Title = "Select", Category = "Data Entry", PageKey = "Select", Icon = "S" });
        AllItems.Add(new NavigationItem { Title = "Switch", Category = "Data Entry", PageKey = "Switch", Icon = "W" });
        AllItems.Add(new NavigationItem { Title = "Rate", Category = "Data Entry", PageKey = "Rate", Icon = "R" });
        AllItems.Add(new NavigationItem { Title = "Slider & DatePicker", Category = "Data Entry", PageKey = "DataEntry", Icon = "D" });

        // Data Display
        AllItems.Add(new NavigationItem { Title = "Card", Category = "Data Display", PageKey = "Card", Icon = "C" });
        AllItems.Add(new NavigationItem { Title = "Tag", Category = "Data Display", PageKey = "Tag", Icon = "T" });
        AllItems.Add(new NavigationItem { Title = "Badge", Category = "Data Display", PageKey = "Badge", Icon = "B" });
        AllItems.Add(new NavigationItem { Title = "Table", Category = "Data Display", PageKey = "Table", Icon = "T" });
        AllItems.Add(new NavigationItem { Title = "Tabs", Category = "Data Display", PageKey = "Tabs", Icon = "T" });
        AllItems.Add(new NavigationItem { Title = "Timeline", Category = "Data Display", PageKey = "Timeline", Icon = "L" });
        AllItems.Add(new NavigationItem { Title = "Divider", Category = "Data Display", PageKey = "Divider", Icon = "D" });
        AllItems.Add(new NavigationItem { Title = "Empty", Category = "Data Display", PageKey = "Empty", Icon = "E" });

        // Feedback
        AllItems.Add(new NavigationItem { Title = "Alert", Category = "Feedback", PageKey = "Alert", Icon = "A" });
        AllItems.Add(new NavigationItem { Title = "Progress", Category = "Feedback", PageKey = "Progress", Icon = "P" });
        AllItems.Add(new NavigationItem { Title = "Spin", Category = "Feedback", PageKey = "Spin", Icon = "S" });
        AllItems.Add(new NavigationItem { Title = "Modal", Category = "Feedback", PageKey = "Modal", Icon = "M" });
        AllItems.Add(new NavigationItem { Title = "Drawer", Category = "Feedback", PageKey = "Drawer", Icon = "D" });
        AllItems.Add(new NavigationItem { Title = "Popconfirm", Category = "Feedback", PageKey = "Popconfirm", Icon = "P" });
        AllItems.Add(new NavigationItem { Title = "Message", Category = "Feedback", PageKey = "Message", Icon = "M" });
        AllItems.Add(new NavigationItem { Title = "Result", Category = "Feedback", PageKey = "Result", Icon = "R" });

        // Navigation
        AllItems.Add(new NavigationItem { Title = "Steps", Category = "Navigation", PageKey = "Steps", Icon = "S" });
        AllItems.Add(new NavigationItem { Title = "Pagination", Category = "Navigation", PageKey = "Pagination", Icon = "P" });
        AllItems.Add(new NavigationItem { Title = "Segmented", Category = "Navigation", PageKey = "Segmented", Icon = "S" });

        // Settings
        AllItems.Add(new NavigationItem { Title = "Theme", Category = "Settings", PageKey = "Theme", Icon = "T" });
    }

    private void FilterNavigation()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredItems = new ObservableCollection<NavigationItem>(AllItems);
        }
        else
        {
            var query = SearchText.ToLowerInvariant();
            FilteredItems = new ObservableCollection<NavigationItem>(
                AllItems.Where(i => i.Title.ToLowerInvariant().Contains(query) ||
                                    i.Category.ToLowerInvariant().Contains(query)));
        }
    }

    private void OnThemeChanged()
    {
        ThemeHelper.SetBaseTheme(IsDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
    }
}
