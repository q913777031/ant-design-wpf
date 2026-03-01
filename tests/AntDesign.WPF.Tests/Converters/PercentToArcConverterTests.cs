using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;
using System.Windows.Media;
using AntDesign.WPF.Converters;
using FluentAssertions;
using Xunit;

namespace AntDesign.WPF.Tests.Converters;

public class PercentToArcConverterTests
{
    private readonly PercentToArcConverter _converter = new();
    private readonly TrackArcConverter _trackConverter = new();

    private static void RunOnSta(Action action)
    {
        Exception? caught = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception ex) { caught = ex; }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
        if (caught != null)
            throw new AggregateException(caught);
    }

    // ── PercentToArcConverter ──────────────────────────────────────────

    [Fact]
    public void Convert_ValidPercent_ReturnsPathGeometry()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 50.0, 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeOfType<PathGeometry>();
        });
    }

    [Fact]
    public void Convert_ZeroPercent_ReturnsEmpty()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 0.0, 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeSameAs(Geometry.Empty);
        });
    }

    [Fact]
    public void Convert_FullPercent_ReturnsPathGeometry()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 100.0, 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeOfType<PathGeometry>();
        });
    }

    [Fact]
    public void Convert_DashboardMode_ReturnsPathGeometry()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 75.0, 60.0, 8.0 }, typeof(Geometry), "Dashboard", CultureInfo.InvariantCulture);

            result.Should().BeOfType<PathGeometry>();
        });
    }

    [Fact]
    public void Convert_TooFewValues_ReturnsEmpty()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 50.0, 60.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeSameAs(Geometry.Empty);
        });
    }

    [Fact]
    public void Convert_InvalidValueTypes_ReturnsEmpty()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { "bad", 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeSameAs(Geometry.Empty);
        });
    }

    [Fact]
    public void Convert_ZeroEffectiveRadius_ReturnsEmpty()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 50.0, 4.0, 10.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeSameAs(Geometry.Empty);
        });
    }

    [Fact]
    public void Convert_ResultIsFrozen()
    {
        RunOnSta(() =>
        {
            var result = _converter.Convert(
                new object[] { 50.0, 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            ((PathGeometry)result).IsFrozen.Should().BeTrue();
        });
    }

    [Fact]
    public void ConvertBack_ReturnsDoNothing()
    {
        var result = _converter.ConvertBack(null!, new[] { typeof(double) }, null, CultureInfo.InvariantCulture);

        result.Should().ContainSingle().Which.Should().Be(Binding.DoNothing);
    }

    // ── TrackArcConverter ──────────────────────────────────────────────

    [Fact]
    public void TrackCircle_ReturnsEllipseGeometry()
    {
        RunOnSta(() =>
        {
            var result = _trackConverter.Convert(
                new object[] { 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeOfType<EllipseGeometry>();
        });
    }

    [Fact]
    public void TrackDashboard_ReturnsPathGeometry()
    {
        RunOnSta(() =>
        {
            var result = _trackConverter.Convert(
                new object[] { 60.0, 8.0 }, typeof(Geometry), "Dashboard", CultureInfo.InvariantCulture);

            result.Should().BeOfType<PathGeometry>();
        });
    }

    [Fact]
    public void TrackCircle_ResultIsFrozen()
    {
        RunOnSta(() =>
        {
            var result = _trackConverter.Convert(
                new object[] { 60.0, 8.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            ((EllipseGeometry)result).IsFrozen.Should().BeTrue();
        });
    }

    [Fact]
    public void Track_TooFewValues_ReturnsEmpty()
    {
        RunOnSta(() =>
        {
            var result = _trackConverter.Convert(
                new object[] { 60.0 }, typeof(Geometry), "Circle", CultureInfo.InvariantCulture);

            result.Should().BeSameAs(Geometry.Empty);
        });
    }

    [Fact]
    public void TrackConvertBack_ReturnsDoNothing()
    {
        var result = _trackConverter.ConvertBack(null!, new[] { typeof(double) }, null, CultureInfo.InvariantCulture);

        result.Should().ContainSingle().Which.Should().Be(Binding.DoNothing);
    }
}
