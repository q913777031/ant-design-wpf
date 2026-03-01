using System.Globalization;
using System.Windows.Media;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class ColorToBrushConverterTests
{
    private readonly ColorToBrushConverter _converter = new();

    [Fact]
    public void Convert_Color_ReturnsSolidColorBrush()
    {
        var color = System.Windows.Media.Colors.Red;

        var result = _converter.Convert(color, typeof(SolidColorBrush), null, CultureInfo.InvariantCulture);

        result.Should().BeOfType<SolidColorBrush>();
        ((SolidColorBrush)result).Color.Should().Be(System.Windows.Media.Colors.Red);
    }

    [Fact]
    public void Convert_NonColor_ReturnsTransparentBrush()
    {
        var result = _converter.Convert("not a color", typeof(SolidColorBrush), null, CultureInfo.InvariantCulture);

        result.Should().BeOfType<SolidColorBrush>();
        ((SolidColorBrush)result).Color.Should().Be(System.Windows.Media.Colors.Transparent);
    }

    [Fact]
    public void Convert_Null_ReturnsTransparentBrush()
    {
        var result = _converter.Convert(null!, typeof(SolidColorBrush), null, CultureInfo.InvariantCulture);

        result.Should().BeOfType<SolidColorBrush>();
        ((SolidColorBrush)result).Color.Should().Be(System.Windows.Media.Colors.Transparent);
    }

    [Fact]
    public void ConvertBack_SolidColorBrush_ReturnsColor()
    {
        var brush = new SolidColorBrush(System.Windows.Media.Colors.Blue);

        var result = _converter.ConvertBack(brush, typeof(Color), null, CultureInfo.InvariantCulture);

        result.Should().Be(System.Windows.Media.Colors.Blue);
    }

    [Fact]
    public void ConvertBack_NonBrush_ReturnsTransparent()
    {
        var result = _converter.ConvertBack("not a brush", typeof(Color), null, CultureInfo.InvariantCulture);

        result.Should().Be(System.Windows.Media.Colors.Transparent);
    }

    [Fact]
    public void ConvertBack_Null_ReturnsTransparent()
    {
        var result = _converter.ConvertBack(null!, typeof(Color), null, CultureInfo.InvariantCulture);

        result.Should().Be(System.Windows.Media.Colors.Transparent);
    }
}
