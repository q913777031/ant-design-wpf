using System.Windows.Media;

namespace AntDesign.WPF.Colors;

/// <summary>
/// Ant Design color palette system.
/// Each preset color has 10 levels (1-10), where level 6 is the primary shade.
/// </summary>
public static class ColorPalette
{
    private static readonly Dictionary<PresetColor, Color[]> _palettes = new()
    {
        [PresetColor.Blue] = new[]
        {
            Color.FromRgb(0xE6, 0xF4, 0xFF), // 1
            Color.FromRgb(0xBA, 0xE0, 0xFF), // 2
            Color.FromRgb(0x91, 0xCA, 0xFF), // 3
            Color.FromRgb(0x69, 0xB1, 0xFF), // 4
            Color.FromRgb(0x40, 0x96, 0xFF), // 5
            Color.FromRgb(0x16, 0x77, 0xFF), // 6 (primary)
            Color.FromRgb(0x09, 0x58, 0xD9), // 7
            Color.FromRgb(0x00, 0x3E, 0xB3), // 8
            Color.FromRgb(0x00, 0x2C, 0x8C), // 9
            Color.FromRgb(0x00, 0x1D, 0x66), // 10
        },
        [PresetColor.Green] = new[]
        {
            Color.FromRgb(0xF6, 0xFF, 0xED), // 1
            Color.FromRgb(0xD9, 0xF7, 0xBE), // 2
            Color.FromRgb(0xB7, 0xEB, 0x8F), // 3
            Color.FromRgb(0x95, 0xDE, 0x64), // 4
            Color.FromRgb(0x73, 0xD1, 0x3D), // 5
            Color.FromRgb(0x52, 0xC4, 0x1A), // 6
            Color.FromRgb(0x38, 0x9E, 0x0D), // 7
            Color.FromRgb(0x23, 0x78, 0x04), // 8
            Color.FromRgb(0x13, 0x52, 0x00), // 9
            Color.FromRgb(0x09, 0x2B, 0x00), // 10
        },
        [PresetColor.Red] = new[]
        {
            Color.FromRgb(0xFF, 0xF1, 0xF0), // 1
            Color.FromRgb(0xFF, 0xCC, 0xC7), // 2
            Color.FromRgb(0xFF, 0xA3, 0x9E), // 3
            Color.FromRgb(0xFF, 0x78, 0x75), // 4
            Color.FromRgb(0xFF, 0x4D, 0x4F), // 5
            Color.FromRgb(0xF5, 0x22, 0x2D), // 6
            Color.FromRgb(0xCF, 0x13, 0x22), // 7
            Color.FromRgb(0xA8, 0x07, 0x1A), // 8
            Color.FromRgb(0x82, 0x00, 0x14), // 9
            Color.FromRgb(0x5C, 0x00, 0x11), // 10
        },
        [PresetColor.Gold] = new[]
        {
            Color.FromRgb(0xFF, 0xFB, 0xE6), // 1
            Color.FromRgb(0xFF, 0xF1, 0xB8), // 2
            Color.FromRgb(0xFF, 0xE5, 0x8F), // 3
            Color.FromRgb(0xFF, 0xD6, 0x66), // 4
            Color.FromRgb(0xFF, 0xC5, 0x3D), // 5
            Color.FromRgb(0xFA, 0xAD, 0x14), // 6
            Color.FromRgb(0xD4, 0x88, 0x06), // 7
            Color.FromRgb(0xAD, 0x68, 0x00), // 8
            Color.FromRgb(0x87, 0x4D, 0x00), // 9
            Color.FromRgb(0x61, 0x34, 0x00), // 10
        },
        [PresetColor.Yellow] = new[]
        {
            Color.FromRgb(0xFE, 0xFF, 0xE6), // 1
            Color.FromRgb(0xFF, 0xFF, 0xB8), // 2
            Color.FromRgb(0xFF, 0xFB, 0x8F), // 3
            Color.FromRgb(0xFF, 0xF5, 0x66), // 4
            Color.FromRgb(0xFF, 0xEC, 0x3D), // 5
            Color.FromRgb(0xFA, 0xDB, 0x14), // 6
            Color.FromRgb(0xD4, 0xB1, 0x06), // 7
            Color.FromRgb(0xAD, 0x8B, 0x00), // 8
            Color.FromRgb(0x87, 0x68, 0x00), // 9
            Color.FromRgb(0x61, 0x47, 0x00), // 10
        },
        [PresetColor.Orange] = new[]
        {
            Color.FromRgb(0xFF, 0xF7, 0xE6), // 1
            Color.FromRgb(0xFF, 0xE7, 0xBA), // 2
            Color.FromRgb(0xFF, 0xD5, 0x91), // 3
            Color.FromRgb(0xFF, 0xC0, 0x69), // 4
            Color.FromRgb(0xFF, 0xA9, 0x40), // 5
            Color.FromRgb(0xFA, 0x8C, 0x16), // 6
            Color.FromRgb(0xD4, 0x6B, 0x08), // 7
            Color.FromRgb(0xAD, 0x4E, 0x00), // 8
            Color.FromRgb(0x87, 0x38, 0x00), // 9
            Color.FromRgb(0x61, 0x25, 0x00), // 10
        },
        [PresetColor.Volcano] = new[]
        {
            Color.FromRgb(0xFF, 0xF2, 0xE8), // 1
            Color.FromRgb(0xFF, 0xD8, 0xBF), // 2
            Color.FromRgb(0xFF, 0xBB, 0x96), // 3
            Color.FromRgb(0xFF, 0x9C, 0x6E), // 4
            Color.FromRgb(0xFF, 0x7A, 0x45), // 5
            Color.FromRgb(0xFA, 0x54, 0x1C), // 6
            Color.FromRgb(0xD4, 0x38, 0x0D), // 7
            Color.FromRgb(0xAD, 0x21, 0x02), // 8
            Color.FromRgb(0x87, 0x14, 0x00), // 9
            Color.FromRgb(0x61, 0x0B, 0x00), // 10
        },
        [PresetColor.Lime] = new[]
        {
            Color.FromRgb(0xFC, 0xFF, 0xE6), // 1
            Color.FromRgb(0xF4, 0xFF, 0xB8), // 2
            Color.FromRgb(0xEA, 0xFF, 0x8F), // 3
            Color.FromRgb(0xD3, 0xF2, 0x61), // 4
            Color.FromRgb(0xBA, 0xE6, 0x37), // 5
            Color.FromRgb(0xA0, 0xD9, 0x11), // 6
            Color.FromRgb(0x7C, 0xB3, 0x05), // 7
            Color.FromRgb(0x5B, 0x8C, 0x00), // 8
            Color.FromRgb(0x3F, 0x66, 0x00), // 9
            Color.FromRgb(0x25, 0x40, 0x00), // 10
        },
        [PresetColor.Cyan] = new[]
        {
            Color.FromRgb(0xE6, 0xFF, 0xFB), // 1
            Color.FromRgb(0xB5, 0xF5, 0xEC), // 2
            Color.FromRgb(0x87, 0xE8, 0xDE), // 3
            Color.FromRgb(0x5C, 0xDB, 0xD3), // 4
            Color.FromRgb(0x36, 0xCF, 0xC9), // 5
            Color.FromRgb(0x13, 0xC2, 0xC2), // 6
            Color.FromRgb(0x08, 0x97, 0x9C), // 7
            Color.FromRgb(0x00, 0x6D, 0x75), // 8
            Color.FromRgb(0x00, 0x47, 0x4F), // 9
            Color.FromRgb(0x00, 0x23, 0x29), // 10
        },
        [PresetColor.GeekBlue] = new[]
        {
            Color.FromRgb(0xF0, 0xF5, 0xFF), // 1
            Color.FromRgb(0xD6, 0xE4, 0xFF), // 2
            Color.FromRgb(0xAD, 0xC6, 0xFF), // 3
            Color.FromRgb(0x85, 0xA5, 0xFF), // 4
            Color.FromRgb(0x59, 0x7E, 0xF7), // 5
            Color.FromRgb(0x2F, 0x54, 0xEB), // 6
            Color.FromRgb(0x1D, 0x39, 0xC4), // 7
            Color.FromRgb(0x10, 0x23, 0x9E), // 8
            Color.FromRgb(0x06, 0x11, 0x78), // 9
            Color.FromRgb(0x03, 0x08, 0x52), // 10
        },
        [PresetColor.Purple] = new[]
        {
            Color.FromRgb(0xF9, 0xF0, 0xFF), // 1
            Color.FromRgb(0xEF, 0xDB, 0xFF), // 2
            Color.FromRgb(0xD3, 0xAD, 0xF7), // 3
            Color.FromRgb(0xB3, 0x7F, 0xEB), // 4
            Color.FromRgb(0x92, 0x54, 0xDE), // 5
            Color.FromRgb(0x72, 0x2E, 0xD1), // 6
            Color.FromRgb(0x53, 0x1D, 0xAB), // 7
            Color.FromRgb(0x39, 0x10, 0x85), // 8
            Color.FromRgb(0x22, 0x07, 0x5E), // 9
            Color.FromRgb(0x12, 0x03, 0x38), // 10
        },
        [PresetColor.Magenta] = new[]
        {
            Color.FromRgb(0xFF, 0xF0, 0xF6), // 1
            Color.FromRgb(0xFF, 0xD6, 0xE7), // 2
            Color.FromRgb(0xFF, 0xAD, 0xD2), // 3
            Color.FromRgb(0xFF, 0x85, 0xC2), // 4
            Color.FromRgb(0xF7, 0x59, 0xAB), // 5
            Color.FromRgb(0xEB, 0x2F, 0x96), // 6
            Color.FromRgb(0xC4, 0x1D, 0x7F), // 7
            Color.FromRgb(0x9E, 0x10, 0x68), // 8
            Color.FromRgb(0x78, 0x06, 0x50), // 9
            Color.FromRgb(0x52, 0x03, 0x39), // 10
        },
    };

    /// <summary>
    /// Gets the 10-level color palette for a preset color.
    /// Index 0 = Level 1 (lightest), Index 9 = Level 10 (darkest).
    /// Level 6 (index 5) is the primary shade.
    /// </summary>
    public static Color[] GetPalette(PresetColor color) => _palettes[color];

    /// <summary>
    /// Gets a specific level (1-10) of a preset color.
    /// </summary>
    public static Color GetColor(PresetColor color, int level)
    {
        if (level < 1 || level > 10)
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be between 1 and 10.");
        return _palettes[color][level - 1];
    }

    /// <summary>
    /// Gets the primary shade (level 6) of a preset color.
    /// </summary>
    public static Color GetPrimary(PresetColor color) => _palettes[color][5];

    /// <summary>
    /// Gets the hover shade (level 5) of a preset color.
    /// </summary>
    public static Color GetHover(PresetColor color) => _palettes[color][4];

    /// <summary>
    /// Gets the active/pressed shade (level 7) of a preset color.
    /// </summary>
    public static Color GetActive(PresetColor color) => _palettes[color][6];

    /// <summary>
    /// Gets the background shade (level 1) of a preset color.
    /// </summary>
    public static Color GetBackground(PresetColor color) => _palettes[color][0];

    /// <summary>
    /// Gets the border shade (level 3) of a preset color.
    /// </summary>
    public static Color GetBorder(PresetColor color) => _palettes[color][2];
}
