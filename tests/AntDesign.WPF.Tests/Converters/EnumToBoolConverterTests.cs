using System;
using System.Globalization;
using System.Windows.Data;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class EnumToBoolConverterTests
{
    private readonly EnumToBoolConverter _converter = new();

    private enum TestEnum { Alpha, Beta, Gamma }

    [Fact]
    public void Convert_MatchingEnumValue_ReturnsTrue()
    {
        var result = _converter.Convert(TestEnum.Beta, typeof(bool), TestEnum.Beta, CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Fact]
    public void Convert_NonMatchingEnumValue_ReturnsFalse()
    {
        var result = _converter.Convert(TestEnum.Alpha, typeof(bool), TestEnum.Beta, CultureInfo.InvariantCulture);

        result.Should().Be(false);
    }

    [Fact]
    public void Convert_MatchingString_ReturnsTrue()
    {
        var result = _converter.Convert(TestEnum.Beta, typeof(bool), "Beta", CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Fact]
    public void Convert_NonMatchingString_ReturnsFalse()
    {
        var result = _converter.Convert(TestEnum.Alpha, typeof(bool), "Beta", CultureInfo.InvariantCulture);

        result.Should().Be(false);
    }

    [Fact]
    public void Convert_CaseInsensitiveString_ReturnsTrue()
    {
        var result = _converter.Convert(TestEnum.Beta, typeof(bool), "beta", CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Fact]
    public void Convert_InvalidString_ReturnsFalse()
    {
        var result = _converter.Convert(TestEnum.Alpha, typeof(bool), "InvalidValue", CultureInfo.InvariantCulture);

        result.Should().Be(false);
    }

    [Fact]
    public void Convert_NullValue_ReturnsFalse()
    {
        var result = _converter.Convert(null!, typeof(bool), TestEnum.Alpha, CultureInfo.InvariantCulture);

        result.Should().Be(false);
    }

    [Fact]
    public void Convert_NullParameter_ReturnsFalse()
    {
        var result = _converter.Convert(TestEnum.Alpha, typeof(bool), null, CultureInfo.InvariantCulture);

        result.Should().Be(false);
    }

    [Fact]
    public void ConvertBack_TrueWithEnumParam_ReturnsEnumValue()
    {
        var result = _converter.ConvertBack(true, typeof(TestEnum), TestEnum.Gamma, CultureInfo.InvariantCulture);

        result.Should().Be(TestEnum.Gamma);
    }

    [Fact]
    public void ConvertBack_TrueWithStringParam_ReturnsParsedEnum()
    {
        var result = _converter.ConvertBack(true, typeof(TestEnum), "Gamma", CultureInfo.InvariantCulture);

        result.Should().Be(TestEnum.Gamma);
    }

    [Fact]
    public void ConvertBack_False_ReturnsDoNothing()
    {
        var result = _converter.ConvertBack(false, typeof(TestEnum), TestEnum.Alpha, CultureInfo.InvariantCulture);

        result.Should().Be(Binding.DoNothing);
    }

    [Fact]
    public void ConvertBack_TrueWithInvalidString_ReturnsDoNothing()
    {
        var result = _converter.ConvertBack(true, typeof(TestEnum), "InvalidValue", CultureInfo.InvariantCulture);

        result.Should().Be(Binding.DoNothing);
    }
}
