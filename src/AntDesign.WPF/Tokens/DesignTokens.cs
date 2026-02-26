namespace AntDesign.WPF.Tokens;

/// <summary>
/// String constants for every DynamicResource key used by the Ant Design WPF theme system.
/// Controls bind to these keys via {DynamicResource ...} so that theme switches at runtime
/// automatically propagate without rebinding.
///
/// Naming convention: AntDesign.{Category}.{Name}[.{Variant}]
///   Category  — Brush | Color | CornerRadius | ControlHeight | FontSize | LineHeight | Spacing
///   Name      — semantic role (Primary, Text, Bg, Border, …)
///   Variant   — optional modifier (Hover, Active, Bg, BgHover, Border, BorderHover, Secondary, …)
/// </summary>
public static class DesignTokens
{
    // -----------------------------------------------------------------------
    // Brush — Primary
    // -----------------------------------------------------------------------

    /// <summary>Primary interactive color brush (level 6).</summary>
    public const string BrushPrimary = "AntDesign.Brush.Primary";

    /// <summary>Primary hover state brush (level 5).</summary>
    public const string BrushPrimaryHover = "AntDesign.Brush.Primary.Hover";

    /// <summary>Primary active/pressed state brush (level 7).</summary>
    public const string BrushPrimaryActive = "AntDesign.Brush.Primary.Active";

    /// <summary>Primary subtle background brush (level 1).</summary>
    public const string BrushPrimaryBg = "AntDesign.Brush.Primary.Bg";

    /// <summary>Primary subtle background hover brush (level 2).</summary>
    public const string BrushPrimaryBgHover = "AntDesign.Brush.Primary.BgHover";

    /// <summary>Primary border brush (level 3).</summary>
    public const string BrushPrimaryBorder = "AntDesign.Brush.Primary.Border";

    /// <summary>Primary border hover brush (level 4).</summary>
    public const string BrushPrimaryBorderHover = "AntDesign.Brush.Primary.BorderHover";

    // -----------------------------------------------------------------------
    // Brush — Success
    // -----------------------------------------------------------------------

    /// <summary>Success state brush (level 6).</summary>
    public const string BrushSuccess = "AntDesign.Brush.Success";

    /// <summary>Success hover state brush (level 5).</summary>
    public const string BrushSuccessHover = "AntDesign.Brush.Success.Hover";

    /// <summary>Success active/pressed state brush (level 7).</summary>
    public const string BrushSuccessActive = "AntDesign.Brush.Success.Active";

    /// <summary>Success subtle background brush (level 1).</summary>
    public const string BrushSuccessBg = "AntDesign.Brush.Success.Bg";

    /// <summary>Success subtle background hover brush (level 2).</summary>
    public const string BrushSuccessBgHover = "AntDesign.Brush.Success.BgHover";

    /// <summary>Success border brush (level 3).</summary>
    public const string BrushSuccessBorder = "AntDesign.Brush.Success.Border";

    /// <summary>Success border hover brush (level 4).</summary>
    public const string BrushSuccessBorderHover = "AntDesign.Brush.Success.BorderHover";

    // -----------------------------------------------------------------------
    // Brush — Warning
    // -----------------------------------------------------------------------

    /// <summary>Warning state brush (level 6).</summary>
    public const string BrushWarning = "AntDesign.Brush.Warning";

    /// <summary>Warning hover state brush (level 5).</summary>
    public const string BrushWarningHover = "AntDesign.Brush.Warning.Hover";

    /// <summary>Warning active/pressed state brush (level 7).</summary>
    public const string BrushWarningActive = "AntDesign.Brush.Warning.Active";

    /// <summary>Warning subtle background brush (level 1).</summary>
    public const string BrushWarningBg = "AntDesign.Brush.Warning.Bg";

    /// <summary>Warning subtle background hover brush (level 2).</summary>
    public const string BrushWarningBgHover = "AntDesign.Brush.Warning.BgHover";

    /// <summary>Warning border brush (level 3).</summary>
    public const string BrushWarningBorder = "AntDesign.Brush.Warning.Border";

    /// <summary>Warning border hover brush (level 4).</summary>
    public const string BrushWarningBorderHover = "AntDesign.Brush.Warning.BorderHover";

    // -----------------------------------------------------------------------
    // Brush — Error / Danger
    // -----------------------------------------------------------------------

    /// <summary>Error state brush (level 6).</summary>
    public const string BrushError = "AntDesign.Brush.Error";

    /// <summary>Error hover state brush (level 5).</summary>
    public const string BrushErrorHover = "AntDesign.Brush.Error.Hover";

    /// <summary>Error active/pressed state brush (level 7).</summary>
    public const string BrushErrorActive = "AntDesign.Brush.Error.Active";

    /// <summary>Error subtle background brush (level 1).</summary>
    public const string BrushErrorBg = "AntDesign.Brush.Error.Bg";

    /// <summary>Error subtle background hover brush (level 2).</summary>
    public const string BrushErrorBgHover = "AntDesign.Brush.Error.BgHover";

    /// <summary>Error border brush (level 3).</summary>
    public const string BrushErrorBorder = "AntDesign.Brush.Error.Border";

    /// <summary>Error border hover brush (level 4).</summary>
    public const string BrushErrorBorderHover = "AntDesign.Brush.Error.BorderHover";

    // -----------------------------------------------------------------------
    // Brush — Info
    // -----------------------------------------------------------------------

    /// <summary>Info state brush (level 6).</summary>
    public const string BrushInfo = "AntDesign.Brush.Info";

    /// <summary>Info hover state brush (level 5).</summary>
    public const string BrushInfoHover = "AntDesign.Brush.Info.Hover";

    /// <summary>Info active/pressed state brush (level 7).</summary>
    public const string BrushInfoActive = "AntDesign.Brush.Info.Active";

    /// <summary>Info subtle background brush (level 1).</summary>
    public const string BrushInfoBg = "AntDesign.Brush.Info.Bg";

    /// <summary>Info subtle background hover brush (level 2).</summary>
    public const string BrushInfoBgHover = "AntDesign.Brush.Info.BgHover";

    /// <summary>Info border brush (level 3).</summary>
    public const string BrushInfoBorder = "AntDesign.Brush.Info.Border";

    /// <summary>Info border hover brush (level 4).</summary>
    public const string BrushInfoBorderHover = "AntDesign.Brush.Info.BorderHover";

    // -----------------------------------------------------------------------
    // Brush — Text (colorText family)
    // -----------------------------------------------------------------------

    /// <summary>Primary text — rgba(0,0,0,0.88) light / rgba(255,255,255,0.85) dark.</summary>
    public const string BrushText = "AntDesign.Brush.Text";

    /// <summary>Secondary text — rgba(0,0,0,0.65) light / rgba(255,255,255,0.65) dark.</summary>
    public const string BrushTextSecondary = "AntDesign.Brush.Text.Secondary";

    /// <summary>Tertiary text — rgba(0,0,0,0.45) light / rgba(255,255,255,0.45) dark.</summary>
    public const string BrushTextTertiary = "AntDesign.Brush.Text.Tertiary";

    /// <summary>Quaternary text — rgba(0,0,0,0.25) light / rgba(255,255,255,0.25) dark.</summary>
    public const string BrushTextQuaternary = "AntDesign.Brush.Text.Quaternary";

    /// <summary>Disabled text — rgba(0,0,0,0.25) light / rgba(255,255,255,0.25) dark.</summary>
    public const string BrushTextDisabled = "AntDesign.Brush.Text.Disabled";

    /// <summary>Solid light text used on colored surfaces — #FFFFFF.</summary>
    public const string BrushTextLightSolid = "AntDesign.Brush.Text.LightSolid";

    // -----------------------------------------------------------------------
    // Brush — Background (colorBg family)
    // -----------------------------------------------------------------------

    /// <summary>Container background — #FFFFFF light / #141414 dark.</summary>
    public const string BrushBgContainer = "AntDesign.Brush.Bg.Container";

    /// <summary>Layout / page background — #F5F5F5 light / #000000 dark.</summary>
    public const string BrushBgLayout = "AntDesign.Brush.Bg.Layout";

    /// <summary>Elevated surface background (cards, popups) — #FFFFFF light / #1F1F1F dark.</summary>
    public const string BrushBgElevated = "AntDesign.Brush.Bg.Elevated";

    /// <summary>Spotlight / tooltip background — rgba(0,0,0,0.85) light / rgba(255,255,255,0.85) dark.</summary>
    public const string BrushBgSpotlight = "AntDesign.Brush.Bg.Spotlight";

    /// <summary>Modal mask overlay — rgba(0,0,0,0.45).</summary>
    public const string BrushBgMask = "AntDesign.Brush.Bg.Mask";

    // -----------------------------------------------------------------------
    // Brush — Border (colorBorder family)
    // -----------------------------------------------------------------------

    /// <summary>Default border — #D9D9D9 light / #424242 dark.</summary>
    public const string BrushBorder = "AntDesign.Brush.Border";

    /// <summary>Secondary / subtle border — #F0F0F0 light / #303030 dark.</summary>
    public const string BrushBorderSecondary = "AntDesign.Brush.Border.Secondary";

    // -----------------------------------------------------------------------
    // Brush — Fill (colorFill family)
    // -----------------------------------------------------------------------

    /// <summary>Fill — rgba(0,0,0,0.15) light / rgba(255,255,255,0.18) dark.</summary>
    public const string BrushFill = "AntDesign.Brush.Fill";

    /// <summary>Fill secondary — rgba(0,0,0,0.06) light / rgba(255,255,255,0.12) dark.</summary>
    public const string BrushFillSecondary = "AntDesign.Brush.Fill.Secondary";

    /// <summary>Fill tertiary — rgba(0,0,0,0.04) light / rgba(255,255,255,0.08) dark.</summary>
    public const string BrushFillTertiary = "AntDesign.Brush.Fill.Tertiary";

    /// <summary>Fill quaternary — rgba(0,0,0,0.02) light / rgba(255,255,255,0.04) dark.</summary>
    public const string BrushFillQuaternary = "AntDesign.Brush.Fill.Quaternary";

    // -----------------------------------------------------------------------
    // Brush — Link
    // -----------------------------------------------------------------------

    /// <summary>Hyperlink default color brush — matches primary.</summary>
    public const string BrushLink = "AntDesign.Brush.Link";

    /// <summary>Hyperlink hover color brush.</summary>
    public const string BrushLinkHover = "AntDesign.Brush.Link.Hover";

    /// <summary>Hyperlink active/pressed color brush.</summary>
    public const string BrushLinkActive = "AntDesign.Brush.Link.Active";

    // -----------------------------------------------------------------------
    // Brush — Icon
    // -----------------------------------------------------------------------

    /// <summary>Default icon foreground brush.</summary>
    public const string BrushIcon = "AntDesign.Brush.Icon";

    /// <summary>Icon hover foreground brush.</summary>
    public const string BrushIconHover = "AntDesign.Brush.Icon.Hover";

    // -----------------------------------------------------------------------
    // Color — mirrors of every Brush key using WPF Color values
    // -----------------------------------------------------------------------

    public const string ColorPrimary = "AntDesign.Color.Primary";
    public const string ColorPrimaryHover = "AntDesign.Color.Primary.Hover";
    public const string ColorPrimaryActive = "AntDesign.Color.Primary.Active";
    public const string ColorPrimaryBg = "AntDesign.Color.Primary.Bg";
    public const string ColorPrimaryBgHover = "AntDesign.Color.Primary.BgHover";
    public const string ColorPrimaryBorder = "AntDesign.Color.Primary.Border";
    public const string ColorPrimaryBorderHover = "AntDesign.Color.Primary.BorderHover";

    public const string ColorSuccess = "AntDesign.Color.Success";
    public const string ColorSuccessHover = "AntDesign.Color.Success.Hover";
    public const string ColorSuccessActive = "AntDesign.Color.Success.Active";
    public const string ColorSuccessBg = "AntDesign.Color.Success.Bg";
    public const string ColorSuccessBgHover = "AntDesign.Color.Success.BgHover";
    public const string ColorSuccessBorder = "AntDesign.Color.Success.Border";
    public const string ColorSuccessBorderHover = "AntDesign.Color.Success.BorderHover";

    public const string ColorWarning = "AntDesign.Color.Warning";
    public const string ColorWarningHover = "AntDesign.Color.Warning.Hover";
    public const string ColorWarningActive = "AntDesign.Color.Warning.Active";
    public const string ColorWarningBg = "AntDesign.Color.Warning.Bg";
    public const string ColorWarningBgHover = "AntDesign.Color.Warning.BgHover";
    public const string ColorWarningBorder = "AntDesign.Color.Warning.Border";
    public const string ColorWarningBorderHover = "AntDesign.Color.Warning.BorderHover";

    public const string ColorError = "AntDesign.Color.Error";
    public const string ColorErrorHover = "AntDesign.Color.Error.Hover";
    public const string ColorErrorActive = "AntDesign.Color.Error.Active";
    public const string ColorErrorBg = "AntDesign.Color.Error.Bg";
    public const string ColorErrorBgHover = "AntDesign.Color.Error.BgHover";
    public const string ColorErrorBorder = "AntDesign.Color.Error.Border";
    public const string ColorErrorBorderHover = "AntDesign.Color.Error.BorderHover";

    public const string ColorInfo = "AntDesign.Color.Info";
    public const string ColorInfoHover = "AntDesign.Color.Info.Hover";
    public const string ColorInfoActive = "AntDesign.Color.Info.Active";
    public const string ColorInfoBg = "AntDesign.Color.Info.Bg";
    public const string ColorInfoBgHover = "AntDesign.Color.Info.BgHover";
    public const string ColorInfoBorder = "AntDesign.Color.Info.Border";
    public const string ColorInfoBorderHover = "AntDesign.Color.Info.BorderHover";

    public const string ColorText = "AntDesign.Color.Text";
    public const string ColorTextSecondary = "AntDesign.Color.Text.Secondary";
    public const string ColorTextTertiary = "AntDesign.Color.Text.Tertiary";
    public const string ColorTextQuaternary = "AntDesign.Color.Text.Quaternary";
    public const string ColorTextDisabled = "AntDesign.Color.Text.Disabled";
    public const string ColorTextLightSolid = "AntDesign.Color.Text.LightSolid";

    public const string ColorBgContainer = "AntDesign.Color.Bg.Container";
    public const string ColorBgLayout = "AntDesign.Color.Bg.Layout";
    public const string ColorBgElevated = "AntDesign.Color.Bg.Elevated";
    public const string ColorBgSpotlight = "AntDesign.Color.Bg.Spotlight";
    public const string ColorBgMask = "AntDesign.Color.Bg.Mask";

    public const string ColorBorder = "AntDesign.Color.Border";
    public const string ColorBorderSecondary = "AntDesign.Color.Border.Secondary";

    public const string ColorFill = "AntDesign.Color.Fill";
    public const string ColorFillSecondary = "AntDesign.Color.Fill.Secondary";
    public const string ColorFillTertiary = "AntDesign.Color.Fill.Tertiary";
    public const string ColorFillQuaternary = "AntDesign.Color.Fill.Quaternary";

    public const string ColorLink = "AntDesign.Color.Link";
    public const string ColorLinkHover = "AntDesign.Color.Link.Hover";
    public const string ColorLinkActive = "AntDesign.Color.Link.Active";

    public const string ColorIcon = "AntDesign.Color.Icon";
    public const string ColorIconHover = "AntDesign.Color.Icon.Hover";

    // -----------------------------------------------------------------------
    // CornerRadius
    // -----------------------------------------------------------------------

    /// <summary>Extra-small corner radius — 2 px.</summary>
    public const string CornerRadiusXS = "AntDesign.CornerRadius.XS";

    /// <summary>Small corner radius — 4 px.</summary>
    public const string CornerRadiusSM = "AntDesign.CornerRadius.SM";

    /// <summary>Default corner radius — 6 px.</summary>
    public const string CornerRadius = "AntDesign.CornerRadius";

    /// <summary>Large corner radius — 8 px.</summary>
    public const string CornerRadiusLG = "AntDesign.CornerRadius.LG";

    // -----------------------------------------------------------------------
    // Control heights
    // -----------------------------------------------------------------------

    /// <summary>Small control height — 24 px.</summary>
    public const string ControlHeightSM = "AntDesign.ControlHeight.SM";

    /// <summary>Default control height — 32 px.</summary>
    public const string ControlHeight = "AntDesign.ControlHeight";

    /// <summary>Large control height — 40 px.</summary>
    public const string ControlHeightLG = "AntDesign.ControlHeight.LG";

    // -----------------------------------------------------------------------
    // Typography — font sizes
    // -----------------------------------------------------------------------

    /// <summary>Small font size — 12 px.</summary>
    public const string FontSizeSM = "AntDesign.FontSize.SM";

    /// <summary>Default body font size — 14 px.</summary>
    public const string FontSize = "AntDesign.FontSize";

    /// <summary>Large font size — 16 px.</summary>
    public const string FontSizeLG = "AntDesign.FontSize.LG";

    /// <summary>H5 heading font size — 16 px.</summary>
    public const string FontSizeH5 = "AntDesign.FontSize.H5";

    /// <summary>H4 heading font size — 20 px.</summary>
    public const string FontSizeH4 = "AntDesign.FontSize.H4";

    /// <summary>H3 heading font size — 24 px.</summary>
    public const string FontSizeH3 = "AntDesign.FontSize.H3";

    /// <summary>H2 heading font size — 30 px.</summary>
    public const string FontSizeH2 = "AntDesign.FontSize.H2";

    /// <summary>H1 heading font size — 38 px.</summary>
    public const string FontSizeH1 = "AntDesign.FontSize.H1";

    // -----------------------------------------------------------------------
    // Typography — line height
    // -----------------------------------------------------------------------

    /// <summary>Default line height multiplier (1.5714 ≈ 22/14).</summary>
    public const string LineHeight = "AntDesign.LineHeight";

    // -----------------------------------------------------------------------
    // Spacing (returned as Thickness resources for easy use in Margin/Padding)
    // -----------------------------------------------------------------------

    /// <summary>XXS spacing — 4 px uniform.</summary>
    public const string SpacingXXS = "AntDesign.Spacing.XXS";

    /// <summary>XS spacing — 8 px uniform.</summary>
    public const string SpacingXS = "AntDesign.Spacing.XS";

    /// <summary>SM spacing — 12 px uniform.</summary>
    public const string SpacingSM = "AntDesign.Spacing.SM";

    /// <summary>MD spacing — 16 px uniform.</summary>
    public const string SpacingMD = "AntDesign.Spacing.MD";

    /// <summary>LG spacing — 24 px uniform.</summary>
    public const string SpacingLG = "AntDesign.Spacing.LG";

    /// <summary>XL spacing — 32 px uniform.</summary>
    public const string SpacingXL = "AntDesign.Spacing.XL";
}
