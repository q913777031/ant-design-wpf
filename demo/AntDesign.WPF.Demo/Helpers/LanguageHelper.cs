namespace AntDesign.WPF.Demo.Helpers;

using System;
using System.Diagnostics;
using System.Windows;

public static class LanguageHelper
{
    private static readonly object _lock = new();
    private static ResourceDictionary? _currentDictionary;
    private static string _currentLanguage = "en";

    public static event Action? LanguageChanged;

    public static string CurrentLanguage => _currentLanguage;

    public static void Initialize(string language = "en")
    {
        SetLanguage(language);
    }

    public static void SetLanguage(string language)
    {
        lock (_lock)
        {
            var app = Application.Current;
            if (app == null) return;

            var uri = language == "zh"
                ? new Uri("Resources/Strings/Strings.zh.xaml", UriKind.Relative)
                : new Uri("Resources/Strings/Strings.en.xaml", UriKind.Relative);

            var newDict = new ResourceDictionary { Source = uri };
            var appResources = app.Resources;

            if (_currentDictionary != null)
                appResources.MergedDictionaries.Remove(_currentDictionary);

            appResources.MergedDictionaries.Add(newDict);
            _currentDictionary = newDict;
            _currentLanguage = language;
        }

        RaiseLanguageChanged();
    }

    public static void ToggleLanguage()
    {
        SetLanguage(_currentLanguage == "en" ? "zh" : "en");
    }

    public static string GetString(string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;
        var app = Application.Current;
        return app?.TryFindResource(key) as string ?? key;
    }

    private static void RaiseLanguageChanged()
    {
        var handler = LanguageChanged;
        if (handler == null) return;

        foreach (var subscriber in handler.GetInvocationList())
        {
            try
            {
                ((Action)subscriber)();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LanguageChanged subscriber threw: {ex}");
            }
        }
    }
}
