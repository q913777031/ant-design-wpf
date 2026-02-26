using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

// Define a convenient XAML namespace URI
[assembly: XmlnsDefinition("https://antdesign.wpf/", "AntDesign.WPF")]
[assembly: XmlnsDefinition("https://antdesign.wpf/", "AntDesign.WPF.Controls")]
[assembly: XmlnsDefinition("https://antdesign.wpf/", "AntDesign.WPF.Assists")]
[assembly: XmlnsDefinition("https://antdesign.wpf/", "AntDesign.WPF.Converters")]
[assembly: XmlnsPrefix("https://antdesign.wpf/", "antd")]
