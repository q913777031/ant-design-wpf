using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AntDesign.WPF.Helpers;

/// <summary>
/// Generates an ant logo icon as a BitmapSource for use as a Window.Icon.
/// Renders the same vector ant used in the sidebar logo onto a colored rounded-square background.
/// </summary>
public static class AntIconHelper
{
    /// <summary>
    /// Creates a 32x32 ant icon as a <see cref="BitmapSource"/>.
    /// </summary>
    /// <param name="background">Background brush for the rounded square. Defaults to Ant Design blue (#1677FF).</param>
    /// <param name="foreground">Foreground brush for the ant body. Defaults to White.</param>
    public static ImageSource CreateAntIcon(Brush? background = null, Brush? foreground = null)
    {
        background ??= new SolidColorBrush(Color.FromRgb(0x16, 0x77, 0xFF));
        foreground ??= Brushes.White;

        if (background is SolidColorBrush scb)
            scb.Freeze();
        if (foreground is SolidColorBrush scf)
            scf.Freeze();

        const double size = 32;
        var visual = new DrawingVisual();

        using (var dc = visual.RenderOpen())
        {
            // Rounded-square background
            var bgRect = new Rect(0, 0, size, size);
            dc.DrawRoundedRectangle(background, null, bgRect, 6, 6);

            // Scale the 24x24 canvas to fit within the 32x32 icon with padding
            dc.PushTransform(new TranslateTransform(4, 3));
            dc.PushTransform(new ScaleTransform(1.0, 1.0));

            var pen = new Pen(foreground, 1.2)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round
            };
            pen.Freeze();

            // Head
            dc.DrawEllipse(foreground, null, new Point(12, 4.5), 4, 3.5);

            // Eyes — use background color for eyes (like the sidebar logo)
            dc.DrawEllipse(background, null, new Point(10.5, 4), 1, 1);
            dc.DrawEllipse(background, null, new Point(13.5, 4), 1, 1);

            // Antennae
            dc.DrawLine(pen, new Point(10, 1.5), new Point(7, 0));
            dc.DrawLine(pen, new Point(14, 1.5), new Point(17, 0));

            // Thorax
            dc.DrawEllipse(foreground, null, new Point(12, 10.5), 3, 2.5);

            // Abdomen
            dc.DrawEllipse(foreground, null, new Point(12, 17.5), 5, 4.5);

            // Legs left
            dc.DrawLine(pen, new Point(9, 10), new Point(4, 7));
            dc.DrawLine(pen, new Point(9, 12), new Point(3, 12));
            dc.DrawLine(pen, new Point(9, 15), new Point(4, 18));

            // Legs right
            dc.DrawLine(pen, new Point(15, 10), new Point(20, 7));
            dc.DrawLine(pen, new Point(15, 12), new Point(21, 12));
            dc.DrawLine(pen, new Point(15, 15), new Point(20, 18));

            dc.Pop(); // ScaleTransform
            dc.Pop(); // TranslateTransform
        }

        var rtb = new RenderTargetBitmap(32, 32, 96, 96, PixelFormats.Pbgra32);
        rtb.Render(visual);
        rtb.Freeze();

        return rtb;
    }
}
