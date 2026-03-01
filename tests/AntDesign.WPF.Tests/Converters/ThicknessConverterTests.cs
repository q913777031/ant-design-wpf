using System.Globalization;
using System.Windows;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class ThicknessConverterTests
{
    private readonly AntDesign.WPF.Converters.ThicknessConverter _converter = new();

    [Fact]
    public void Convert_NoParameter_ReturnsUniformThickness()
    {
        var result = _converter.Convert(4.0, typeof(Thickness), null, CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(4));
    }

    [Fact]
    public void Convert_Left_ReturnsLeftOnly()
    {
        var result = _converter.Convert(2.0, typeof(Thickness), "Left", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(2, 0, 0, 0));
    }

    [Fact]
    public void Convert_Top_ReturnsTopOnly()
    {
        var result = _converter.Convert(2.0, typeof(Thickness), "Top", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(0, 2, 0, 0));
    }

    [Fact]
    public void Convert_Right_ReturnsRightOnly()
    {
        var result = _converter.Convert(2.0, typeof(Thickness), "Right", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(0, 0, 2, 0));
    }

    [Fact]
    public void Convert_Bottom_ReturnsBottomOnly()
    {
        var result = _converter.Convert(2.0, typeof(Thickness), "Bottom", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(0, 0, 0, 2));
    }

    [Fact]
    public void Convert_Horizontal_ReturnsLeftAndRight()
    {
        var result = _converter.Convert(3.0, typeof(Thickness), "Horizontal", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(3, 0, 3, 0));
    }

    [Fact]
    public void Convert_Vertical_ReturnsTopAndBottom()
    {
        var result = _converter.Convert(3.0, typeof(Thickness), "Vertical", CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(0, 3, 0, 3));
    }

    [Fact]
    public void Convert_NullValue_ReturnsZeroThickness()
    {
        var result = _converter.Convert(null!, typeof(Thickness), null, CultureInfo.InvariantCulture);

        result.Should().Be(new Thickness(0));
    }

    [Fact]
    public void ConvertBack_Thickness_ReturnsMaxSide()
    {
        var result = _converter.ConvertBack(new Thickness(1, 5, 3, 2), typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(5.0);
    }

    [Fact]
    public void ConvertBack_UniformThickness_ReturnsThatValue()
    {
        var result = _converter.ConvertBack(new Thickness(7), typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(7.0);
    }

    [Fact]
    public void ConvertBack_NonThickness_ReturnsZero()
    {
        var result = _converter.ConvertBack("invalid", typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }
}
