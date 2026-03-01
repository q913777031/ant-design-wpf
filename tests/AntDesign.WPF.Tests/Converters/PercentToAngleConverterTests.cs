using System.Globalization;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class PercentToAngleConverterTests
{
    private readonly PercentToAngleConverter _converter = new();

    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(50.0, 180.0)]
    [InlineData(100.0, 360.0)]
    [InlineData(25.0, 90.0)]
    public void Convert_Percent_ReturnsAngle(double percent, double expectedAngle)
    {
        var result = _converter.Convert(percent, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(expectedAngle);
    }

    [Fact]
    public void Convert_WithOffset_AddsOffset()
    {
        var result = _converter.Convert(50.0, typeof(double), "90", CultureInfo.InvariantCulture);

        result.Should().Be(270.0);
    }

    [Fact]
    public void Convert_AboveMax_ClampsTo360()
    {
        var result = _converter.Convert(150.0, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(360.0);
    }

    [Fact]
    public void Convert_BelowMin_ClampsToZero()
    {
        var result = _converter.Convert(-10.0, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Convert_NullValue_ReturnsZero()
    {
        var result = _converter.Convert(null!, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(180.0, 50.0)]
    [InlineData(360.0, 100.0)]
    [InlineData(90.0, 25.0)]
    public void ConvertBack_Angle_ReturnsPercent(double angle, double expectedPercent)
    {
        var result = _converter.ConvertBack(angle, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(expectedPercent);
    }

    [Fact]
    public void ConvertBack_WithOffset_SubtractsOffset()
    {
        var result = _converter.ConvertBack(270.0, typeof(double), "90", CultureInfo.InvariantCulture);

        result.Should().Be(50.0);
    }

    [Fact]
    public void ConvertBack_NullValue_ReturnsZero()
    {
        var result = _converter.ConvertBack(null!, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }
}
