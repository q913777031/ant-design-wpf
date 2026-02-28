using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using AntDesign.WPF.Colors;
using AntDesign.WPF.Tokens;

namespace AntDesign.WPF;

/// <summary>
/// A <see cref="ResourceDictionary"/> that generates every Ant Design WPF resource at
/// construction time and can be used as a merged dictionary in XAML, exactly like
/// MaterialDesignThemes' <c>BundledTheme</c>.
///
/// Usage in App.xaml:
/// <code>
/// &lt;Application.Resources&gt;
///   &lt;ResourceDictionary&gt;
///     &lt;ResourceDictionary.MergedDictionaries&gt;
///       &lt;ant:AntDesignTheme BaseTheme="Light"
///                           PrimaryColor="Blue"
///                           BorderRadius="6" /&gt;
///     &lt;/ResourceDictionary.MergedDictionaries&gt;
///   &lt;/ResourceDictionary&gt;
/// &lt;/Application.Resources&gt;
/// </code>
/// </summary>
public class AntDesignTheme : ResourceDictionary, ISupportInitialize
{
    // ------------------------------------------------------------------
    // Backing fields — property setters call Initialize() after all are
    // assigned so that a partial XAML load does not produce incomplete
    // resources.
    // ------------------------------------------------------------------

    private bool _isInitializing;
    private int _initCount;

    private BaseTheme _baseTheme = BaseTheme.Light;
    private PresetColor _primaryColor = PresetColor.Blue;
    private double _borderRadius = 6.0;

    // ===================================================================
    // ISupportInitialize — prevents redundant Initialize() calls when
    // XAML sets multiple properties in sequence.
    // ===================================================================

    /// <inheritdoc/>
    public new void BeginInit()
    {
        base.BeginInit();
        Interlocked.Increment(ref _initCount);
        _isInitializing = true;
    }

    /// <inheritdoc/>
    public new void EndInit()
    {
        if (Interlocked.Decrement(ref _initCount) <= 0)
        {
            _isInitializing = false;
            Interlocked.Exchange(ref _initCount, 0);
            Initialize();
        }
        base.EndInit();
    }

    // ===================================================================
    // Public properties (settable from XAML)
    // ===================================================================

    /// <summary>Gets or sets the base light/dark variant. Triggers resource regeneration.</summary>
    public BaseTheme BaseTheme
    {
        get => _baseTheme;
        set
        {
            _baseTheme = value;
            if (!_isInitializing) Initialize();
        }
    }

    /// <summary>Gets or sets the primary brand color from the Ant Design preset palette.</summary>
    public PresetColor PrimaryColor
    {
        get => _primaryColor;
        set
        {
            _primaryColor = value;
            if (!_isInitializing) Initialize();
        }
    }

    /// <summary>
    /// Gets or sets the default corner radius in device-independent pixels. Default is 6.
    /// Populates CornerRadius, CornerRadius.SM, CornerRadius.XS, and CornerRadius.LG resources.
    /// </summary>
    public double BorderRadius
    {
        get => _borderRadius;
        set
        {
            _borderRadius = value;
            if (!_isInitializing) Initialize();
        }
    }

    // ===================================================================
    // Batch setter (used by ThemeHelper to avoid multiple Initialize() calls)
    // ===================================================================

    /// <summary>
    /// Sets all core theme properties at once and calls <see cref="Initialize"/> exactly once.
    /// This is the preferred path when changing more than one property simultaneously.
    /// </summary>
    internal void SetCoreProperties(BaseTheme baseTheme, PresetColor primaryColor, double borderRadius)
    {
        _baseTheme    = baseTheme;
        _primaryColor = primaryColor;
        _borderRadius = borderRadius;
        Initialize();
    }

    // ===================================================================
    // Initialisation
    // ===================================================================

    /// <summary>
    /// Rebuilds every resource entry in this dictionary from the current property values.
    /// Called automatically when any property changes.
    /// </summary>
    public void Initialize()
    {
        Clear();
        bool isDark = _baseTheme == BaseTheme.Dark;

        PopulateSemanticColors(isDark);
        PopulateCornerRadiusResources();
        PopulateControlHeightResources();
        PopulateTypographyResources();
        PopulateSpacingResources();
    }

    // ===================================================================
    // Section: Semantic color and brush resources
    // ===================================================================

    private void PopulateSemanticColors(bool isDark)
    {
        Color[] primaryPalette = ColorPalette.GetPalette(_primaryColor);

        // Info shares the same palette as Primary by Ant Design spec.
        Color[] infoPalette = primaryPalette;

        // Canonical semantic palettes (always fixed regardless of PrimaryColor).
        Color[] successPalette = ColorPalette.GetPalette(PresetColor.Green);
        Color[] warningPalette = ColorPalette.GetPalette(PresetColor.Gold);
        Color[] errorPalette = ColorPalette.GetPalette(PresetColor.Red);

        // Register semantic color groups.
        RegisterColorGroup(
            keyPrefix: "Primary",
            palette: primaryPalette,
            isDark: isDark);

        RegisterColorGroup(
            keyPrefix: "Success",
            palette: successPalette,
            isDark: isDark);

        RegisterColorGroup(
            keyPrefix: "Warning",
            palette: warningPalette,
            isDark: isDark);

        RegisterColorGroup(
            keyPrefix: "Error",
            palette: errorPalette,
            isDark: isDark);

        RegisterColorGroup(
            keyPrefix: "Info",
            palette: infoPalette,
            isDark: isDark);

        // Link — follows primary color.
        SetColorAndBrush(DesignTokens.ColorLink, DesignTokens.BrushLink,
            isDark ? primaryPalette[4] : primaryPalette[5]);       // level 5 dark / 6 light
        SetColorAndBrush(DesignTokens.ColorLinkHover, DesignTokens.BrushLinkHover,
            primaryPalette[4]);                                    // level 5
        SetColorAndBrush(DesignTokens.ColorLinkActive, DesignTokens.BrushLinkActive,
            primaryPalette[6]);                                    // level 7

        // Text.
        PopulateTextColors(isDark);

        // Backgrounds.
        PopulateBackgroundColors(isDark);

        // Borders.
        PopulateBorderColors(isDark);

        // Fills.
        PopulateFillColors(isDark);

        // Icon — uses text color for default, secondary for hover.
        Color iconDefault = isDark
            ? Color.FromArgb(0xD9, 0xFF, 0xFF, 0xFF)  // rgba(255,255,255,0.85)
            : Color.FromArgb(0xE0, 0x00, 0x00, 0x00); // rgba(0,0,0,0.88)

        Color iconHover = isDark
            ? Color.FromArgb(0xA6, 0xFF, 0xFF, 0xFF)  // rgba(255,255,255,0.65)
            : Color.FromArgb(0xA6, 0x00, 0x00, 0x00); // rgba(0,0,0,0.65)

        SetColorAndBrush(DesignTokens.ColorIcon, DesignTokens.BrushIcon, iconDefault);
        SetColorAndBrush(DesignTokens.ColorIconHover, DesignTokens.BrushIconHover, iconHover);
    }

    /// <summary>
    /// Registers the 7 standard semantic variants for one color group
    /// (Primary, Success, Warning, Error, Info).
    /// Dark-mode palettes invert index mapping per Ant Design 5 spec.
    /// </summary>
    private void RegisterColorGroup(string keyPrefix, Color[] palette, bool isDark)
    {
        // For dark mode Ant Design uses the dark algorithm which maps:
        //   Bg      → level 1 (darkest bg shade in dark palette)
        //   BgHover → level 2
        //   Border  → level 3
        //   BorderHover → level 4
        //   Main    → level 5 (lighter in dark context)
        //   Hover   → level 4
        //   Active  → level 7
        // We use the same palette object and simply pick different indices.

        Color main        = isDark ? palette[4] : palette[5]; // level 5 dark / 6 light
        Color hover       = isDark ? palette[3] : palette[4]; // level 4 dark / 5 light
        Color active      = isDark ? palette[5] : palette[6]; // level 6 dark / 7 light
        Color bg          = isDark ? palette[0] : palette[0]; // level 1
        Color bgHover     = isDark ? palette[1] : palette[1]; // level 2
        Color border      = isDark ? palette[2] : palette[2]; // level 3
        Color borderHover = isDark ? palette[3] : palette[3]; // level 4

        SetColorAndBrush($"AntDesign.Color.{keyPrefix}",            $"AntDesign.Brush.{keyPrefix}",            main);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.Hover",      $"AntDesign.Brush.{keyPrefix}.Hover",      hover);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.Active",     $"AntDesign.Brush.{keyPrefix}.Active",     active);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.Bg",         $"AntDesign.Brush.{keyPrefix}.Bg",         bg);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.BgHover",    $"AntDesign.Brush.{keyPrefix}.BgHover",    bgHover);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.Border",     $"AntDesign.Brush.{keyPrefix}.Border",     border);
        SetColorAndBrush($"AntDesign.Color.{keyPrefix}.BorderHover",$"AntDesign.Brush.{keyPrefix}.BorderHover",borderHover);
    }

    private void PopulateTextColors(bool isDark)
    {
        Color text, textSecondary, textTertiary, textQuaternary, textDisabled, textLightSolid;

        if (isDark)
        {
            text            = Color.FromArgb(0xD9, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.85)
            textSecondary   = Color.FromArgb(0xA6, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.65)
            textTertiary    = Color.FromArgb(0x73, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.45)
            textQuaternary  = Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.25)
            textDisabled    = Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.25)
            textLightSolid  = Color.FromRgb(0xFF, 0xFF, 0xFF);        // #FFFFFF
        }
        else
        {
            text            = Color.FromArgb(0xE0, 0x00, 0x00, 0x00); // rgba(0,0,0,0.88)
            textSecondary   = Color.FromArgb(0xA6, 0x00, 0x00, 0x00); // rgba(0,0,0,0.65)
            textTertiary    = Color.FromArgb(0x73, 0x00, 0x00, 0x00); // rgba(0,0,0,0.45)
            textQuaternary  = Color.FromArgb(0x40, 0x00, 0x00, 0x00); // rgba(0,0,0,0.25)
            textDisabled    = Color.FromArgb(0x40, 0x00, 0x00, 0x00); // rgba(0,0,0,0.25)
            textLightSolid  = Color.FromRgb(0xFF, 0xFF, 0xFF);        // #FFFFFF
        }

        SetColorAndBrush(DesignTokens.ColorText,            DesignTokens.BrushText,            text);
        SetColorAndBrush(DesignTokens.ColorTextSecondary,   DesignTokens.BrushTextSecondary,   textSecondary);
        SetColorAndBrush(DesignTokens.ColorTextTertiary,    DesignTokens.BrushTextTertiary,    textTertiary);
        SetColorAndBrush(DesignTokens.ColorTextQuaternary,  DesignTokens.BrushTextQuaternary,  textQuaternary);
        SetColorAndBrush(DesignTokens.ColorTextDisabled,    DesignTokens.BrushTextDisabled,    textDisabled);
        SetColorAndBrush(DesignTokens.ColorTextLightSolid,  DesignTokens.BrushTextLightSolid,  textLightSolid);
    }

    private void PopulateBackgroundColors(bool isDark)
    {
        Color bgContainer, bgLayout, bgElevated, bgSpotlight, bgMask;

        if (isDark)
        {
            bgContainer = Color.FromRgb(0x14, 0x14, 0x14);           // #141414
            bgLayout    = Color.FromRgb(0x00, 0x00, 0x00);           // #000000
            bgElevated  = Color.FromRgb(0x1F, 0x1F, 0x1F);           // #1F1F1F
            bgSpotlight = Color.FromArgb(0xD9, 0xFF, 0xFF, 0xFF);    // rgba(255,255,255,0.85)
            bgMask      = Color.FromArgb(0x73, 0x00, 0x00, 0x00);    // rgba(0,0,0,0.45)
        }
        else
        {
            bgContainer = Color.FromRgb(0xFF, 0xFF, 0xFF);            // #FFFFFF
            bgLayout    = Color.FromRgb(0xF5, 0xF5, 0xF5);            // #F5F5F5
            bgElevated  = Color.FromRgb(0xFF, 0xFF, 0xFF);            // #FFFFFF
            bgSpotlight = Color.FromArgb(0xD9, 0x00, 0x00, 0x00);    // rgba(0,0,0,0.85)
            bgMask      = Color.FromArgb(0x73, 0x00, 0x00, 0x00);    // rgba(0,0,0,0.45)
        }

        SetColorAndBrush(DesignTokens.ColorBgContainer,  DesignTokens.BrushBgContainer,  bgContainer);
        SetColorAndBrush(DesignTokens.ColorBgLayout,     DesignTokens.BrushBgLayout,     bgLayout);
        SetColorAndBrush(DesignTokens.ColorBgElevated,   DesignTokens.BrushBgElevated,   bgElevated);
        SetColorAndBrush(DesignTokens.ColorBgSpotlight,  DesignTokens.BrushBgSpotlight,  bgSpotlight);
        SetColorAndBrush(DesignTokens.ColorBgMask,       DesignTokens.BrushBgMask,       bgMask);
    }

    private void PopulateBorderColors(bool isDark)
    {
        Color border, borderSecondary;

        if (isDark)
        {
            border          = Color.FromRgb(0x42, 0x42, 0x42); // #424242
            borderSecondary = Color.FromRgb(0x30, 0x30, 0x30); // #303030
        }
        else
        {
            border          = Color.FromRgb(0xD9, 0xD9, 0xD9); // #D9D9D9
            borderSecondary = Color.FromRgb(0xF0, 0xF0, 0xF0); // #F0F0F0
        }

        SetColorAndBrush(DesignTokens.ColorBorder,          DesignTokens.BrushBorder,          border);
        SetColorAndBrush(DesignTokens.ColorBorderSecondary,  DesignTokens.BrushBorderSecondary,  borderSecondary);
    }

    private void PopulateFillColors(bool isDark)
    {
        Color fill, fillSecondary, fillTertiary, fillQuaternary;

        if (isDark)
        {
            fill            = Color.FromArgb(0x2E, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.18)
            fillSecondary   = Color.FromArgb(0x1F, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.12)
            fillTertiary    = Color.FromArgb(0x14, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.08)
            fillQuaternary  = Color.FromArgb(0x0A, 0xFF, 0xFF, 0xFF); // rgba(255,255,255,0.04)
        }
        else
        {
            fill            = Color.FromArgb(0x26, 0x00, 0x00, 0x00); // rgba(0,0,0,0.15)
            fillSecondary   = Color.FromArgb(0x0F, 0x00, 0x00, 0x00); // rgba(0,0,0,0.06)
            fillTertiary    = Color.FromArgb(0x0A, 0x00, 0x00, 0x00); // rgba(0,0,0,0.04)
            fillQuaternary  = Color.FromArgb(0x05, 0x00, 0x00, 0x00); // rgba(0,0,0,0.02)
        }

        SetColorAndBrush(DesignTokens.ColorFill,           DesignTokens.BrushFill,           fill);
        SetColorAndBrush(DesignTokens.ColorFillSecondary,  DesignTokens.BrushFillSecondary,  fillSecondary);
        SetColorAndBrush(DesignTokens.ColorFillTertiary,   DesignTokens.BrushFillTertiary,   fillTertiary);
        SetColorAndBrush(DesignTokens.ColorFillQuaternary, DesignTokens.BrushFillQuaternary, fillQuaternary);
    }

    // ===================================================================
    // Section: CornerRadius resources
    // ===================================================================

    private void PopulateCornerRadiusResources()
    {
        // XS = 2, SM = 4, Default = BorderRadius, LG = BorderRadius + 2.
        // Border radius tokens are plain CornerRadius structs consumed by
        // Border.CornerRadius and similar properties.
        double xs  = 2.0;
        double sm  = 4.0;
        double def = _borderRadius;
        double lg  = _borderRadius + 2.0;

        this[DesignTokens.CornerRadiusXS] = new CornerRadius(xs);
        this[DesignTokens.CornerRadiusSM] = new CornerRadius(sm);
        this[DesignTokens.CornerRadius]   = new CornerRadius(def);
        this[DesignTokens.CornerRadiusLG] = new CornerRadius(lg);

        // Also expose raw doubles for use in templates that can't consume CornerRadius directly.
        this["AntDesign.CornerRadius.XS.Value"]  = xs;
        this["AntDesign.CornerRadius.SM.Value"]  = sm;
        this["AntDesign.CornerRadius.Value"]     = def;
        this["AntDesign.CornerRadius.LG.Value"]  = lg;
    }

    // ===================================================================
    // Section: Control height resources
    // ===================================================================

    private void PopulateControlHeightResources()
    {
        this[DesignTokens.ControlHeightSM] = 24.0;
        this[DesignTokens.ControlHeight]   = 32.0;
        this[DesignTokens.ControlHeightLG] = 40.0;

        // Expose as GridLength too (handy for RowDefinition.Height).
        this["AntDesign.ControlHeight.SM.GL"] = new GridLength(24);
        this["AntDesign.ControlHeight.GL"]    = new GridLength(32);
        this["AntDesign.ControlHeight.LG.GL"] = new GridLength(40);
    }

    // ===================================================================
    // Section: Typography resources
    // ===================================================================

    private void PopulateTypographyResources()
    {
        // Font sizes (doubles — use in FontSize properties).
        this[DesignTokens.FontSizeSM] = 12.0;
        this[DesignTokens.FontSize]   = 14.0;
        this[DesignTokens.FontSizeLG] = 16.0;
        this[DesignTokens.FontSizeH5] = 16.0;
        this[DesignTokens.FontSizeH4] = 20.0;
        this[DesignTokens.FontSizeH3] = 24.0;
        this[DesignTokens.FontSizeH2] = 30.0;
        this[DesignTokens.FontSizeH1] = 38.0;

        // Line height (double multiplier).
        this[DesignTokens.LineHeight] = 1.5714;

        // Heading line heights (doubles, pre-computed px values at 96 dpi).
        this["AntDesign.LineHeight.H1"] = 1.2105;
        this["AntDesign.LineHeight.H2"] = 1.2667;
        this["AntDesign.LineHeight.H3"] = 1.3333;
        this["AntDesign.LineHeight.H4"] = 1.4;
        this["AntDesign.LineHeight.H5"] = 1.5;

        // Font weight tokens.
        this["AntDesign.FontWeight.Normal"]   = 400;
        this["AntDesign.FontWeight.Medium"]   = 500;
        this["AntDesign.FontWeight.Semibold"] = 600;
        this["AntDesign.FontWeight.Bold"]     = 700;
    }

    // ===================================================================
    // Section: Spacing resources (Thickness)
    // ===================================================================

    private void PopulateSpacingResources()
    {
        this[DesignTokens.SpacingXXS] = new Thickness(4);
        this[DesignTokens.SpacingXS]  = new Thickness(8);
        this[DesignTokens.SpacingSM]  = new Thickness(12);
        this[DesignTokens.SpacingMD]  = new Thickness(16);
        this[DesignTokens.SpacingLG]  = new Thickness(24);
        this[DesignTokens.SpacingXL]  = new Thickness(32);

        // Also expose spacing as plain doubles (for use in non-Thickness contexts).
        this["AntDesign.Spacing.XXS.Value"] = 4.0;
        this["AntDesign.Spacing.XS.Value"]  = 8.0;
        this["AntDesign.Spacing.SM.Value"]  = 12.0;
        this["AntDesign.Spacing.MD.Value"]  = 16.0;
        this["AntDesign.Spacing.LG.Value"]  = 24.0;
        this["AntDesign.Spacing.XL.Value"]  = 32.0;

        // Horizontal / vertical variants (left+right / top+bottom only).
        this["AntDesign.Spacing.XS.H"]  = new Thickness(8, 0, 8, 0);
        this["AntDesign.Spacing.XS.V"]  = new Thickness(0, 8, 0, 8);
        this["AntDesign.Spacing.SM.H"]  = new Thickness(12, 0, 12, 0);
        this["AntDesign.Spacing.SM.V"]  = new Thickness(0, 12, 0, 12);
        this["AntDesign.Spacing.MD.H"]  = new Thickness(16, 0, 16, 0);
        this["AntDesign.Spacing.MD.V"]  = new Thickness(0, 16, 0, 16);
    }

    // ===================================================================
    // Helper utilities
    // ===================================================================

    /// <summary>
    /// Creates a frozen <see cref="SolidColorBrush"/> for the given color.
    /// Frozen brushes are thread-safe and improve rendering performance.
    /// </summary>
    private static SolidColorBrush CreateBrush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }

    /// <summary>
    /// Writes both a <see cref="System.Windows.Media.Color"/> resource and the corresponding
    /// frozen <see cref="SolidColorBrush"/> resource under the provided keys.
    /// </summary>
    private void SetColorAndBrush(string colorKey, string brushKey, Color color)
    {
        this[colorKey] = color;
        this[brushKey] = CreateBrush(color);
    }
}
