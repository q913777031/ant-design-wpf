using System.Windows.Media;

namespace AntDesign.WPF.Colors;

/// <summary>
/// Ant Design neutral color palette (13 levels).
/// </summary>
public static class NeutralColors
{
    public static Color Gray1 { get; } = Color.FromRgb(0xFF, 0xFF, 0xFF);  // #FFFFFF
    public static Color Gray2 { get; } = Color.FromRgb(0xFA, 0xFA, 0xFA);  // #FAFAFA
    public static Color Gray3 { get; } = Color.FromRgb(0xF5, 0xF5, 0xF5);  // #F5F5F5
    public static Color Gray4 { get; } = Color.FromRgb(0xF0, 0xF0, 0xF0);  // #F0F0F0
    public static Color Gray5 { get; } = Color.FromRgb(0xD9, 0xD9, 0xD9);  // #D9D9D9
    public static Color Gray6 { get; } = Color.FromRgb(0xBF, 0xBF, 0xBF);  // #BFBFBF
    public static Color Gray7 { get; } = Color.FromRgb(0x8C, 0x8C, 0x8C);  // #8C8C8C
    public static Color Gray8 { get; } = Color.FromRgb(0x59, 0x59, 0x59);  // #595959
    public static Color Gray9 { get; } = Color.FromRgb(0x43, 0x43, 0x43);  // #434343
    public static Color Gray10 { get; } = Color.FromRgb(0x26, 0x26, 0x26); // #262626
    public static Color Gray11 { get; } = Color.FromRgb(0x1F, 0x1F, 0x1F); // #1F1F1F
    public static Color Gray12 { get; } = Color.FromRgb(0x14, 0x14, 0x14); // #141414
    public static Color Gray13 { get; } = Color.FromRgb(0x00, 0x00, 0x00); // #000000

    public static Color[] All { get; } =
    {
        Gray1, Gray2, Gray3, Gray4, Gray5, Gray6, Gray7,
        Gray8, Gray9, Gray10, Gray11, Gray12, Gray13
    };
}
