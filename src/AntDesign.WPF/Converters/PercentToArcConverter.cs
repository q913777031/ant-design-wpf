using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AntDesign.WPF.Converters;

/// <summary>
/// Converts a percentage (0–100) into a <see cref="PathGeometry"/> representing an arc.
/// Used by the Progress Circle/Dashboard variants.
/// </summary>
/// <remarks>
/// Converter parameters:
/// <list type="bullet">
/// <item><c>values[0]</c> — Percent (double, 0–100)</item>
/// <item><c>values[1]</c> — Radius (double)</item>
/// <item><c>values[2]</c> — StrokeWidth (double)</item>
/// <item><c>parameter</c> — "Circle" or "Dashboard"</item>
/// </list>
/// </remarks>
public sealed class PercentToArcConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3 ||
            values[0] is not double percent ||
            values[1] is not double radius ||
            values[2] is not double strokeWidth)
            return Geometry.Empty;

        string mode = parameter as string ?? "Circle";
        bool isDashboard = mode.Equals("Dashboard", StringComparison.OrdinalIgnoreCase);

        double effectiveRadius = radius - strokeWidth / 2;
        if (effectiveRadius <= 0) return Geometry.Empty;

        double centerX = radius;
        double centerY = radius;

        // Dashboard uses a 270° arc (gap at bottom), Circle uses 360°
        double totalAngleDeg = isDashboard ? 270.0 : 360.0;
        double startAngleDeg = isDashboard ? 135.0 : -90.0; // dashboard starts at 7 o'clock

        double sweepAngleDeg = totalAngleDeg * Math.Clamp(percent, 0, 100) / 100.0;

        if (sweepAngleDeg < 0.01)
            return Geometry.Empty;

        // Clamp to avoid drawing issues at exactly 360
        if (sweepAngleDeg >= 359.99 && !isDashboard)
            sweepAngleDeg = 359.99;

        double startRad = startAngleDeg * Math.PI / 180.0;
        double endRad = (startAngleDeg + sweepAngleDeg) * Math.PI / 180.0;

        double x1 = centerX + effectiveRadius * Math.Cos(startRad);
        double y1 = centerY + effectiveRadius * Math.Sin(startRad);
        double x2 = centerX + effectiveRadius * Math.Cos(endRad);
        double y2 = centerY + effectiveRadius * Math.Sin(endRad);

        bool isLargeArc = sweepAngleDeg > 180;

        var fig = new PathFigure
        {
            StartPoint = new Point(x1, y1),
            IsClosed = false,
            IsFilled = false
        };
        fig.Segments.Add(new ArcSegment
        {
            Point = new Point(x2, y2),
            Size = new Size(effectiveRadius, effectiveRadius),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = isLargeArc
        });

        var geo = new PathGeometry();
        geo.Figures.Add(fig);
        geo.Freeze();
        return geo;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

/// <summary>
/// Generates the background track arc for Circle/Dashboard progress.
/// </summary>
public sealed class TrackArcConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 ||
            values[0] is not double radius ||
            values[1] is not double strokeWidth)
            return Geometry.Empty;

        string mode = parameter as string ?? "Circle";
        bool isDashboard = mode.Equals("Dashboard", StringComparison.OrdinalIgnoreCase);

        double effectiveRadius = radius - strokeWidth / 2;
        if (effectiveRadius <= 0) return Geometry.Empty;

        double centerX = radius;
        double centerY = radius;

        if (!isDashboard)
        {
            // Full circle track
            var geo = new EllipseGeometry(new Point(centerX, centerY), effectiveRadius, effectiveRadius);
            geo.Freeze();
            return geo;
        }

        // Dashboard: 270° arc
        double startAngleDeg = 135.0;
        double sweepAngleDeg = 270.0;

        double startRad = startAngleDeg * Math.PI / 180.0;
        double endRad = (startAngleDeg + sweepAngleDeg) * Math.PI / 180.0;

        double x1 = centerX + effectiveRadius * Math.Cos(startRad);
        double y1 = centerY + effectiveRadius * Math.Sin(startRad);
        double x2 = centerX + effectiveRadius * Math.Cos(endRad);
        double y2 = centerY + effectiveRadius * Math.Sin(endRad);

        var fig = new PathFigure
        {
            StartPoint = new Point(x1, y1),
            IsClosed = false,
            IsFilled = false
        };
        fig.Segments.Add(new ArcSegment
        {
            Point = new Point(x2, y2),
            Size = new Size(effectiveRadius, effectiveRadius),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = true
        });

        var pathGeo = new PathGeometry();
        pathGeo.Figures.Add(fig);
        pathGeo.Freeze();
        return pathGeo;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
