using System;
using System.Globalization;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class PercentageWidthConverterTests
{
    private readonly PercentageWidthConverter _converter = new();

    [Fact]
    public void Convert_50PercentOf200_Returns100()
    {
        var result = _converter.Convert(
            new object[] { 50.0, 200.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(100.0);
    }

    [Fact]
    public void Convert_100PercentOf300_Returns300()
    {
        var result = _converter.Convert(
            new object[] { 100.0, 300.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(300.0);
    }

    [Fact]
    public void Convert_ZeroPercent_ReturnsZero()
    {
        var result = _converter.Convert(
            new object[] { 0.0, 500.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Convert_NegativePercent_ClampsToZero()
    {
        var result = _converter.Convert(
            new object[] { -10.0, 200.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Convert_PercentAbove100_ClampsTo100()
    {
        var result = _converter.Convert(
            new object[] { 150.0, 200.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(200.0);
    }

    [Fact]
    public void Convert_TooFewValues_ReturnsZero()
    {
        var result = _converter.Convert(
            new object[] { 50.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Convert_InvalidValueTypes_ReturnsZero()
    {
        var result = _converter.Convert(
            new object[] { "bad", 200.0 }, typeof(double), null, CultureInfo.InvariantCulture);

        result.Should().Be(0.0);
    }

    [Fact]
    public void ConvertBack_ThrowsNotSupportedException()
    {
        var act = () => _converter.ConvertBack(100.0, new[] { typeof(double), typeof(double) }, null, CultureInfo.InvariantCulture);

        act.Should().Throw<NotSupportedException>();
    }
}
