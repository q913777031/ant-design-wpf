using System.Globalization;
using System.Windows;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class CornerRadiusConverterTests
{
    private readonly AntDesign.WPF.Converters.CornerRadiusConverter _converter = new();

    [Fact]
    public void Convert_NoParameter_ReturnsUniformCornerRadius()
    {
        var result = _converter.Convert(8.0, typeof(CornerRadius), null, CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(8));
    }

    [Fact]
    public void Convert_TopLeft_ReturnsSingleCorner()
    {
        var result = _converter.Convert(6.0, typeof(CornerRadius), "TopLeft", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(6, 0, 0, 0));
    }

    [Fact]
    public void Convert_TopRight_ReturnsSingleCorner()
    {
        var result = _converter.Convert(6.0, typeof(CornerRadius), "TopRight", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0, 6, 0, 0));
    }

    [Fact]
    public void Convert_BottomRight_ReturnsSingleCorner()
    {
        var result = _converter.Convert(6.0, typeof(CornerRadius), "BottomRight", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0, 0, 6, 0));
    }

    [Fact]
    public void Convert_BottomLeft_ReturnsSingleCorner()
    {
        var result = _converter.Convert(6.0, typeof(CornerRadius), "BottomLeft", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0, 0, 0, 6));
    }

    [Fact]
    public void Convert_Top_ReturnsTopCorners()
    {
        var result = _converter.Convert(10.0, typeof(CornerRadius), "Top", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(10, 10, 0, 0));
    }

    [Fact]
    public void Convert_Bottom_ReturnsBottomCorners()
    {
        var result = _converter.Convert(10.0, typeof(CornerRadius), "Bottom", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0, 0, 10, 10));
    }

    [Fact]
    public void Convert_Left_ReturnsLeftCorners()
    {
        var result = _converter.Convert(10.0, typeof(CornerRadius), "Left", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(10, 0, 0, 10));
    }

    [Fact]
    public void Convert_Right_ReturnsRightCorners()
    {
        var result = _converter.Convert(10.0, typeof(CornerRadius), "Right", CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0, 10, 10, 0));
    }

    [Fact]
    public void Convert_NullValue_ReturnsZeroCornerRadius()
    {
        var result = _converter.Convert(null!, typeof(CornerRadius), null, CultureInfo.InvariantCulture);

        result.Should().Be(new CornerRadius(0));
    }

    [Fact]
    public void ConvertBack_CornerRadius_ReturnsMaxValue()
    {
        var result = _converter.ConvertBack(new CornerRadius(2, 8, 4, 6), typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(8.0);
    }

    [Fact]
    public void ConvertBack_NonCornerRadius_ReturnsZero()
    {
        var result = _converter.ConvertBack("invalid", typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }
}
