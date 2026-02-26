using System.Windows.Media;
using AntDesign.WPF.Colors;

namespace AntDesign.WPF;

/// <summary>
/// Defines the base visual theme variant for an Ant Design WPF application.
/// </summary>
public enum BaseTheme
{
    /// <summary>Light mode — dark text on light backgrounds.</summary>
    Light,

    /// <summary>Dark mode — light text on dark backgrounds.</summary>
    Dark
}

/// <summary>
/// Contract for an Ant Design WPF theme configuration.
/// Implementations supply the semantic color roles consumed by <see cref="AntDesignTheme"/>
/// and <see cref="ThemeHelper"/>.
/// </summary>
public interface ITheme
{
    /// <summary>Gets or sets the base light/dark variant.</summary>
    BaseTheme BaseTheme { get; set; }

    /// <summary>Gets or sets the primary brand color (used for interactive elements).</summary>
    Color PrimaryColor { get; set; }

    /// <summary>Gets or sets the color used for success states.</summary>
    Color SuccessColor { get; set; }

    /// <summary>Gets or sets the color used for warning states.</summary>
    Color WarningColor { get; set; }

    /// <summary>Gets or sets the color used for error / danger states.</summary>
    Color ErrorColor { get; set; }

    /// <summary>Gets or sets the color used for informational states.</summary>
    Color InfoColor { get; set; }
}

/// <summary>
/// Default concrete implementation of <see cref="ITheme"/>.
/// Initialises to the canonical Ant Design light theme with blue as the primary color.
/// </summary>
public class AntTheme : ITheme
{
    /// <inheritdoc/>
    public BaseTheme BaseTheme { get; set; } = BaseTheme.Light;

    /// <inheritdoc/>
    public Color PrimaryColor { get; set; } = ColorPalette.GetPrimary(PresetColor.Blue);

    /// <inheritdoc/>
    public Color SuccessColor { get; set; } = ColorPalette.GetPrimary(PresetColor.Green);

    /// <inheritdoc/>
    public Color WarningColor { get; set; } = ColorPalette.GetPrimary(PresetColor.Gold);

    /// <inheritdoc/>
    public Color ErrorColor { get; set; } = ColorPalette.GetPrimary(PresetColor.Red);

    /// <inheritdoc/>
    public Color InfoColor { get; set; } = ColorPalette.GetPrimary(PresetColor.Blue);
}
