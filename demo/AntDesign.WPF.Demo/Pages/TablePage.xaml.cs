using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AntDesign.WPF.Demo.Pages;

public partial class TablePage : UserControl
{
    public TablePage()
    {
        InitializeComponent();
        Loaded += TablePage_Loaded;
    }

    private void TablePage_Loaded(object sender, RoutedEventArgs e)
    {
        SampleDataGrid.ItemsSource = new List<PersonRecord>
        {
            new("John Brown",  32, "New York No. 1 Lake Park",       "nice, developer"),
            new("Jim Green",   42, "London No. 1 Bridge Street",     "loser"),
            new("Joe Black",   32, "Sidney No. 1 Lake Park",         "cool, teacher"),
            new("Alice White", 28, "Paris No. 2 Champs-Elysees",     "developer"),
            new("Bob Smith",   35, "Berlin No. 3 Brandenburg Gate",  "designer"),
            new("Carol Jones", 29, "Tokyo No. 4 Shinjuku",           "manager"),
        };
    }

    public record PersonRecord(string Name, int Age, string Address, string Tags);
}
