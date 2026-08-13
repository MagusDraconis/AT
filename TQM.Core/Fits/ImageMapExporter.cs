using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.FitsAnalysis;

/// <summary>Renders 2D maps to PNG using a simple blue-white-red diverging colormap.</summary>
public static class ImageMapExporter
{
    public static void SaveDiverging(string path, double[] map, int nx, int ny, double vmin, double vmax)
    {
        using var img = new Image<Rgb24>(nx, ny);
        for (int j = 0; j < ny; j++)
        for (int i = 0; i < nx; i++)
        {
            double v = map[j * nx + i];
            if (double.IsNaN(v)) { img[i, j] = new Rgb24(40, 40, 40); continue; }
            double t = vmax > vmin ? (v - vmin) / (vmax - vmin) : 0.5;
            t = Math.Clamp(t, 0.0, 1.0);
            img[i, j] = DivergingColor(t);
        }
        img.Save(path);
    }

    public static void SaveSequential(string path, double[] map, int nx, int ny, double vmin, double vmax)
    {
        using var img = new Image<Rgb24>(nx, ny);
        for (int j = 0; j < ny; j++)
        for (int i = 0; i < nx; i++)
        {
            double v = map[j * nx + i];
            if (double.IsNaN(v)) { img[i, j] = new Rgb24(10, 10, 10); continue; }
            double t = vmax > vmin ? (v - vmin) / (vmax - vmin) : 0.5;
            t = Math.Clamp(t, 0.0, 1.0);
            img[i, j] = ViridisColor(t);
        }
        img.Save(path);
    }

    private static Rgb24 DivergingColor(double t)
    {
        // Blue -> white -> red.
        int r, g, b;
        if (t < 0.5)
        {
            double s = t / 0.5;
            r = (int)(255 * s);
            g = (int)(255 * s);
            b = 255;
        }
        else
        {
            double s = (t - 0.5) / 0.5;
            r = 255;
            g = (int)(255 * (1 - s));
            b = (int)(255 * (1 - s));
        }
        return new Rgb24((byte)r, (byte)g, (byte)b);
    }

    private static Rgb24 ViridisColor(double t)
    {
        // Approximate viridis colormap.
        double r = Math.Clamp(1.0 * (1 - t), 0, 1) * 0.0 + Math.Clamp(1.9 * t - 0.4, 0, 1) * 0.35;
        double g = Math.Clamp(1.8 * t - 0.2, 0, 1) * 0.9;
        double b = Math.Clamp(1.0 - 1.5 * t, 0, 1) * 0.85;
        return new Rgb24((byte)(255 * Math.Clamp(r, 0, 1)), (byte)(255 * Math.Clamp(g, 0, 1)), (byte)(255 * Math.Clamp(b, 0, 1)));
    }
}
