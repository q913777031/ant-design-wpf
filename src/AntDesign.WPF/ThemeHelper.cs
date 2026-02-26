using System.Windows;
using System.Windows.Media;
using AntDesign.WPF.Colors;
using AntDesign.WPF.Tokens;

namespace AntDesign.WPF;

/// <summary>
/// Runtime helper for reading and mutating the active Ant Design WPF theme without reloading
/// the entire resource dictionary.  All public methods operate on
/// <see cref="Application.Current.Resources"/>, so they work from any thread that can
/// access the WPF dispatcher.
///
/// Typical usage:
/// <code>
/// // Switch to dark mode at runtime.
/// ThemeHelper.SetBaseTheme(BaseTheme.Dark);
///
/// // Change the primary brand color.
/// ThemeHelper.SetPrimaryColor(PresetColor.Purple);
///
/// // Apply a full theme object.
/// ThemeHelper.SetTheme(new AntTheme { BaseTheme = BaseTheme.Dark,
///                                     PrimaryColor = ColorPalette.GetPrimary(PresetColor.Cyan) });
/// </code>
/// </summary>
public static class ThemeHelper
{
    // ===================================================================
    // Read
    // ===================================================================

    // Resource key used to persistently store the base theme name.
    private const string BaseThemeKey = "AntDesign.BaseTheme";

    /// <summary>
    /// Reconstructs the logical <see cref="ITheme"/> that is currently reflected in
    /// <see cref="Application.Current.Resources"/>.
    /// Returns a new <see cref="AntTheme"/> populated from the live resource values.
    /// If no theme has been applied yet the defaults from <see cref="AntTheme"/> are returned.
    /// </summary>
    public static ITheme GetTheme()
    {
        var resources = EnsureApplicationResources();
        var theme = new AntTheme();

        // Read base theme from the stored string resource (set by SetTheme / SetBaseTheme).
        if (resources[BaseThemeKey] is string baseThemeStr
            && Enum.TryParse<BaseTheme>(baseThemeStr, out var parsedBaseTheme))
        {
            theme.BaseTheme = parsedBaseTheme;
        }

        // Read primary color.
        if (resources[DesignTokens.ColorPrimary] is Color primary)
            theme.PrimaryColor = primary;

        // Read success / warning / error / info colors.
        if (resources[DesignTokens.ColorSuccess] is Color success)
            theme.SuccessColor = success;

        if (resources[DesignTokens.ColorWarning] is Color warning)
            theme.WarningColor = warning;

        if (resources[DesignTokens.ColorError] is Color error)
            theme.ErrorColor = error;

        if (resources[DesignTokens.ColorInfo] is Color info)
            theme.InfoColor = info;

        return theme;
    }

    // ===================================================================
    // Write — full theme
    // ===================================================================

    /// <summary>
    /// Applies <paramref name="theme"/> by regenerating every Ant Design resource key in
    /// <see cref="Application.Current.Resources"/>.
    ///
    /// If an <see cref="AntDesignTheme"/> instance is already present as a merged dictionary
    /// it is updated in place; otherwise one is created and added.
    /// </summary>
    /// <param name="theme">The theme configuration to apply.</param>
    public static void SetTheme(ITheme theme)
    {
        ArgumentNullException.ThrowIfNull(theme);

        var antTheme = FindOrCreateAntDesignTheme();

        // Use the batch setter to suppress intermediate Initialize() calls.
        antTheme.SetCoreProperties(
            theme.BaseTheme,
            ResolvePresetColorFromColor(theme.PrimaryColor),
            antTheme.BorderRadius);

        // Store the base theme as a named resource for reliable retrieval via GetTheme().
        var resources = EnsureApplicationResources();
        resources[BaseThemeKey] = theme.BaseTheme.ToString();

        // Apply all semantic color overrides from the theme object.
        ApplySemanticColorOverrides(antTheme, theme);
    }

    /// <summary>
    /// Applies success, warning, error, and info color overrides from <paramref name="theme"/>
    /// directly into the live resource dictionary of <paramref name="antTheme"/>.
    /// </summary>
    private static void ApplySemanticColorOverrides(AntDesignTheme antTheme, ITheme theme)
    {
        bool isDark = theme.BaseTheme == BaseTheme.Dark;

        // Success
        var successPreset = ResolvePresetColorFromColor(theme.SuccessColor);
        OverrideColorGroup(antTheme, "Success", ColorPalette.GetPalette(successPreset), isDark);

        // Warning
        var warningPreset = ResolvePresetColorFromColor(theme.WarningColor);
        OverrideColorGroup(antTheme, "Warning", ColorPalette.GetPalette(warningPreset), isDark);

        // Error
        var errorPreset = ResolvePresetColorFromColor(theme.ErrorColor);
        OverrideColorGroup(antTheme, "Error", ColorPalette.GetPalette(errorPreset), isDark);

        // Info
        var infoPreset = ResolvePresetColorFromColor(theme.InfoColor);
        OverrideColorGroup(antTheme, "Info", ColorPalette.GetPalette(infoPreset), isDark);
    }

    /// <summary>
    /// Overwrites the color+brush resources for a single semantic group inside
    /// <paramref name="dict"/> using the supplied <paramref name="palette"/>.
    /// </summary>
    private static void OverrideColorGroup(ResourceDictionary dict, string keyPrefix, Color[] palette, bool isDark)
    {
        Color main        = isDark ? palette[4] : palette[5];
        Color hover       = isDark ? palette[3] : palette[4];
        Color active      = isDark ? palette[5] : palette[6];
        Color bg          = palette[0];
        Color bgHover     = palette[1];
        Color border      = palette[2];
        Color borderHover = palette[3];

        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}",             $"AntDesign.Brush.{keyPrefix}",             main);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.Hover",       $"AntDesign.Brush.{keyPrefix}.Hover",       hover);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.Active",      $"AntDesign.Brush.{keyPrefix}.Active",      active);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.Bg",          $"AntDesign.Brush.{keyPrefix}.Bg",          bg);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.BgHover",     $"AntDesign.Brush.{keyPrefix}.BgHover",     bgHover);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.Border",      $"AntDesign.Brush.{keyPrefix}.Border",      border);
        SetDictColorAndBrush(dict, $"AntDesign.Color.{keyPrefix}.BorderHover", $"AntDesign.Brush.{keyPrefix}.BorderHover", borderHover);
    }

    /// <summary>
    /// Writes a Color and frozen SolidColorBrush entry into <paramref name="dict"/>.
    /// </summary>
    private static void SetDictColorAndBrush(ResourceDictionary dict, string colorKey, string brushKey, Color color)
    {
        dict[colorKey] = color;
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        dict[brushKey] = brush;
    }

    // ===================================================================
    // Write — partial updates
    // ===================================================================

    /// <summary>Switches between light and dark mode without changing any other tokens.</summary>
    /// <param name="baseTheme">The desired <see cref="AntDesign.WPF.BaseTheme"/>.</param>
    public static void SetBaseTheme(BaseTheme baseTheme)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        antTheme.BaseTheme = baseTheme;

        // Persist the base theme for reliable GetTheme() round-trip.
        var resources = EnsureApplicationResources();
        resources[BaseThemeKey] = baseTheme.ToString();
    }

    /// <summary>
    /// Changes the primary brand color at runtime using a <see cref="PresetColor"/> from the
    /// Ant Design palette.  All primary-derived tokens (hover, active, bg, border, link, info)
    /// are recalculated automatically.
    /// </summary>
    /// <param name="color">The new preset color to use as primary.</param>
    public static void SetPrimaryColor(PresetColor color)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        antTheme.PrimaryColor = color;
    }

    /// <summary>
    /// Changes the primary brand color at runtime using a raw <see cref="Color"/> value.
    /// The closest <see cref="PresetColor"/> in the palette is used; if no close match is
    /// found <see cref="PresetColor.Blue"/> is used as a fallback.
    /// </summary>
    /// <param name="color">The desired primary color.</param>
    public static void SetPrimaryColor(Color color)
    {
        SetPrimaryColor(ResolvePresetColorFromColor(color));
    }

    /// <summary>Directly updates the default border radius token (in device-independent pixels).</summary>
    /// <param name="radius">The new border radius value.</param>
    public static void SetBorderRadius(double radius)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        antTheme.BorderRadius = radius;
    }

    /// <summary>
    /// Changes the success semantic color at runtime using a <see cref="PresetColor"/>.
    /// All success-derived tokens (hover, active, bg, border) are recalculated.
    /// </summary>
    /// <param name="color">The preset color to use for success states.</param>
    public static void SetSuccessColor(PresetColor color)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        bool isDark = antTheme.BaseTheme == BaseTheme.Dark;
        OverrideColorGroup(antTheme, "Success", ColorPalette.GetPalette(color), isDark);
    }

    /// <summary>
    /// Changes the warning semantic color at runtime using a <see cref="PresetColor"/>.
    /// All warning-derived tokens (hover, active, bg, border) are recalculated.
    /// </summary>
    /// <param name="color">The preset color to use for warning states.</param>
    public static void SetWarningColor(PresetColor color)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        bool isDark = antTheme.BaseTheme == BaseTheme.Dark;
        OverrideColorGroup(antTheme, "Warning", ColorPalette.GetPalette(color), isDark);
    }

    /// <summary>
    /// Changes the error semantic color at runtime using a <see cref="PresetColor"/>.
    /// All error-derived tokens (hover, active, bg, border) are recalculated.
    /// </summary>
    /// <param name="color">The preset color to use for error/danger states.</param>
    public static void SetErrorColor(PresetColor color)
    {
        var antTheme = FindOrCreateAntDesignTheme();
        bool isDark = antTheme.BaseTheme == BaseTheme.Dark;
        OverrideColorGroup(antTheme, "Error", ColorPalette.GetPalette(color), isDark);
    }

    // ===================================================================
    // Resource convenience accessors
    // ===================================================================

    /// <summary>
    /// Returns the current value of an Ant Design resource from
    /// <see cref="Application.Current.Resources"/>.
    /// Returns <c>null</c> if the key is not present or the application has not started.
    /// </summary>
    /// <param name="key">A <see cref="DesignTokens"/> constant string.</param>
    public static object? GetResource(string key)
    {
        try { return Application.Current?.Resources[key]; }
        catch { return null; }
    }

    /// <summary>
    /// Returns the <see cref="SolidColorBrush"/> for a given brush token key, or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="brushKey">A <c>AntDesign.Brush.*</c> constant from <see cref="DesignTokens"/>.</param>
    public static SolidColorBrush? GetBrush(string brushKey)
        => GetResource(brushKey) as SolidColorBrush;

    /// <summary>
    /// Returns the <see cref="Color"/> for a given color token key, or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="colorKey">A <c>AntDesign.Color.*</c> constant from <see cref="DesignTokens"/>.</param>
    public static Color? GetColor(string colorKey)
        => GetResource(colorKey) is Color c ? c : null;

    // ===================================================================
    // Internal helpers
    // ===================================================================

    /// <summary>
    /// Finds the first <see cref="AntDesignTheme"/> in the application's merged dictionaries,
    /// or creates and adds a default one if none exists.
    /// </summary>
    private static AntDesignTheme FindOrCreateAntDesignTheme()
    {
        var resources = EnsureApplicationResources();

        // Search merged dictionaries (top-level).
        foreach (ResourceDictionary dict in resources.MergedDictionaries)
        {
            if (dict is AntDesignTheme existing)
                return existing;
        }

        // Not found — create a default and insert at the front so controls can override it.
        var newTheme = new AntDesignTheme();
        newTheme.Initialize();
        resources.MergedDictionaries.Insert(0, newTheme);
        return newTheme;
    }

    /// <summary>
    /// Returns <see cref="Application.Current.Resources"/>, throwing if the application is
    /// not available (e.g. in design-time tools).
    /// </summary>
    private static ResourceDictionary EnsureApplicationResources()
    {
        if (Application.Current is null)
            throw new InvalidOperationException(
                "ThemeHelper requires a running WPF Application (Application.Current is null). " +
                "Ensure you call ThemeHelper methods after the WPF Application has been created.");

        return Application.Current.Resources;
    }

    /// <summary>
    /// Finds the <see cref="PresetColor"/> whose level-6 (primary) swatch is closest to the
    /// supplied <paramref name="color"/> using simple Euclidean RGB distance.
    /// Falls back to <see cref="PresetColor.Blue"/> when no good match is found.
    /// </summary>
    private static PresetColor ResolvePresetColorFromColor(Color color)
    {
        PresetColor best = PresetColor.Blue;
        double bestDistance = double.MaxValue;

        foreach (PresetColor preset in Enum.GetValues<PresetColor>())
        {
            Color candidate = ColorPalette.GetPrimary(preset);
            double distance = ColorDistance(color, candidate);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                best = preset;
            }
        }

        return best;
    }

    /// <summary>Euclidean distance in RGB space (ignores alpha).</summary>
    private static double ColorDistance(Color a, Color b)
    {
        double dr = a.R - b.R;
        double dg = a.G - b.G;
        double db = a.B - b.B;
        return Math.Sqrt(dr * dr + dg * dg + db * db);
    }
}
