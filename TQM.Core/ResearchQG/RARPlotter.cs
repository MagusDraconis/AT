using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>Minimal log-log scatter/line plotter and bar-chart renderer for the
/// RAR audit (uses SixLabors.ImageSharp; no text labels — the report text
/// describes each plot).</summary>
public static class RARPlotter
{
    const int W = 800, H = 600;
    static readonly Rgb24 White = new(255, 255, 255);
    static readonly Rgb24 Black = new(30, 30, 30);
    static readonly Rgb24 Grey = new(220, 220, 220);

    public sealed record Series(double[] X, double[] Y, Rgb24 Color, bool Line, int Size);

    public static void PlotLogLog(string path, Series[] series,
        double xmin, double xmax, double ymin, double ymax)
    {
        using var img = new Image<Rgb24>(W, H);
        // White background.
        for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++) img[x, y] = White;

        int ml = 60, mr = 20, mt = 20, mb = 50;
        double lxmin = Math.Log10(xmin), lxmax = Math.Log10(xmax);
        double lymin = Math.Log10(ymin), lymax = Math.Log10(ymax);

        // Grid lines at decades.
        for (int d = (int)Math.Ceiling(lxmin); d <= (int)Math.Floor(lxmax); d++)
        {
            int px = X(lxmin, lxmax, d, ml, W - mr);
            for (int y = mt; y < H - mb; y++) img[px, y] = Grey;
        }
        for (int d = (int)Math.Ceiling(lymin); d <= (int)Math.Floor(lymax); d++)
        {
            int py = Y(lymin, lymax, d, mt, H - mb);
            for (int x = ml; x < W - mr; x++) img[x, py] = Grey;
        }

        // Series.
        foreach (var s in series)
        {
            if (s.Line)
            {
                int prevX = -1, prevY = -1;
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] <= 0 || s.Y[i] <= 0) { prevX = -1; continue; }
                    int px = X(lxmin, lxmax, Math.Log10(s.X[i]), ml, W - mr);
                    int py = Y(lymin, lymax, Math.Log10(s.Y[i]), mt, H - mb);
                    if (prevX >= 0) DrawLine(img, prevX, prevY, px, py, s.Color);
                    prevX = px; prevY = py;
                }
            }
            else
            {
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] <= 0 || s.Y[i] <= 0) continue;
                    int px = X(lxmin, lxmax, Math.Log10(s.X[i]), ml, W - mr);
                    int py = Y(lymin, lymax, Math.Log10(s.Y[i]), mt, H - mb);
                    int r = s.Size;
                    for (int dy = -r; dy <= r; dy++)
                    for (int dx = -r; dx <= r; dx++)
                    {
                        if (dx * dx + dy * dy <= r * r)
                        {
                            int xx = px + dx, yy = py + dy;
                            if (xx >= ml && xx < W - mr && yy >= mt && yy < H - mb) img[xx, yy] = s.Color;
                        }
                    }
                }
            }
        }

        // Axes.
        for (int x = ml; x < W - mr; x++) img[x, H - mb] = Black;
        for (int y = mt; y < H - mb; y++) img[ml, y] = Black;

        img.Save(path);
    }

    public static void PlotSemiLogY(string path, Series[] series,
        double xmin, double xmax, double ymin, double ymax)
    {
        using var img = new Image<Rgb24>(W, H);
        for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++) img[x, y] = White;

        int ml = 60, mr = 20, mt = 20, mb = 50;
        double lymin = Math.Log10(ymin), lymax = Math.Log10(ymax);
        for (int d = (int)Math.Ceiling(lymin); d <= (int)Math.Floor(lymax); d++)
        {
            int py = Y(lymin, lymax, d, mt, H - mb);
            for (int x = ml; x < W - mr; x++) img[x, py] = Grey;
        }

        foreach (var s in series)
        {
            if (s.Line)
            {
                int prevX = -1, prevY = -1;
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] < xmin || s.X[i] > xmax || s.Y[i] <= 0) { prevX = -1; continue; }
                    int px = (int)(ml + (s.X[i] - xmin) / (xmax - xmin) * (W - mr - ml));
                    int py = Y(lymin, lymax, Math.Log10(s.Y[i]), mt, H - mb);
                    if (prevX >= 0) DrawLine(img, prevX, prevY, px, py, s.Color);
                    prevX = px; prevY = py;
                }
            }
            else
            {
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] < xmin || s.X[i] > xmax || s.Y[i] <= 0) continue;
                    int px = (int)(ml + (s.X[i] - xmin) / (xmax - xmin) * (W - mr - ml));
                    int py = Y(lymin, lymax, Math.Log10(s.Y[i]), mt, H - mb);
                    int r = s.Size;
                    for (int dy = -r; dy <= r; dy++)
                    for (int dx = -r; dx <= r; dx++)
                        if (dx * dx + dy * dy <= r * r)
                        {
                            int xx = px + dx, yy = py + dy;
                            if (xx >= ml && xx < W - mr && yy >= mt && yy < H - mb) img[xx, yy] = s.Color;
                        }
                }
            }
        }

        for (int x = ml; x < W - mr; x++) img[x, H - mb] = Black;
        for (int y = mt; y < H - mb; y++) img[ml, y] = Black;
        img.Save(path);
    }

    public static void PlotBars(string path, string[] labels, double[] values, Rgb24 color)
    {
        using var img = new Image<Rgb24>(W, H);
        for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++) img[x, y] = White;

        double max = values.Max();
        int n = values.Length;
        int bw = (W - 100) / n;
        for (int i = 0; i < n; i++)
        {
            int hBar = (int)((H - 120) * (values[i] / max));
            int x0 = 50 + i * bw;
            for (int x = x0; x < x0 + bw - 10; x++)
            for (int y = H - 60 - hBar; y < H - 60; y++)
                img[x, y] = color;
        }
        // Baseline.
        for (int x = 40; x < W - 20; x++) img[x, H - 60] = Black;
        img.Save(path);
    }

    private static int X(double lmin, double lmax, double lv, int ml, int mr) =>
        (int)(ml + (lv - lmin) / (lmax - lmin) * (mr - ml));

    private static int Y(double lmin, double lmax, double lv, int mt, int mb) =>
        (int)(mb - (lv - lmin) / (lmax - lmin) * (mb - mt));

    private static void DrawLine(Image<Rgb24> img, int x0, int y0, int x1, int y1, Rgb24 c)
    {
        int dx = Math.Abs(x1 - x0), dy = Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1, sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;
        while (true)
        {
            if (x0 >= 0 && x0 < W && y0 >= 0 && y0 < H) img[x0, y0] = c;
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }
}
