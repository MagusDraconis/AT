
namespace AT.Core.ResearchQG;

/// <summary>Minimal log-log scatter/line plotter and bar-chart renderer for the
/// RAR audit (rasterised through AtBitmap; no text labels — the report text
/// describes each plot).</summary>
public static class RARPlotter
{
    const int W = 800, H = 600;
    static readonly AtColor White = new(255, 255, 255);
    static readonly AtColor Black = new(30, 30, 30);
    static readonly AtColor Grey = new(220, 220, 220);

    public sealed record Series(double[] X, double[] Y, AtColor Color, bool Line, int Size);

    public static void PlotLogLog(string path, Series[] series,
        double xmin, double xmax, double ymin, double ymax)
    {
        using var img = new AtBitmap(W, H);
        // White background.
        img.Clear(White);

        int ml = 60, mr = 20, mt = 20, mb = 50;
        double lxmin = Math.Log10(xmin), lxmax = Math.Log10(xmax);
        double lymin = Math.Log10(ymin), lymax = Math.Log10(ymax);

        // Grid lines at decades.
        for (int d = (int)Math.Ceiling(lxmin); d <= (int)Math.Floor(lxmax); d++)
        {
            int px = X(lxmin, lxmax, d, ml, W - mr);
            for (int y = mt; y < H - mb; y++) img.Set(px, y, Grey);
        }
        for (int d = (int)Math.Ceiling(lymin); d <= (int)Math.Floor(lymax); d++)
        {
            int py = Y(lymin, lymax, d, mt, H - mb);
            for (int x = ml; x < W - mr; x++) img.Set(x, py, Grey);
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
                            if (xx >= ml && xx < W - mr && yy >= mt && yy < H - mb) img.Set(xx, yy, s.Color);
                        }
                    }
                }
            }
        }

        // Axes.
        for (int x = ml; x < W - mr; x++) img.Set(x, H - mb, Black);
        for (int y = mt; y < H - mb; y++) img.Set(ml, y, Black);

        img.SavePng(path);
    }

    public static void PlotSemiLogY(string path, Series[] series,
        double xmin, double xmax, double ymin, double ymax)
    {
        using var img = new AtBitmap(W, H);
        img.Clear(White);

        int ml = 60, mr = 20, mt = 20, mb = 50;
        double lymin = Math.Log10(ymin), lymax = Math.Log10(ymax);
        for (int d = (int)Math.Ceiling(lymin); d <= (int)Math.Floor(lymax); d++)
        {
            int py = Y(lymin, lymax, d, mt, H - mb);
            for (int x = ml; x < W - mr; x++) img.Set(x, py, Grey);
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
                            if (xx >= ml && xx < W - mr && yy >= mt && yy < H - mb) img.Set(xx, yy, s.Color);
                        }
                }
            }
        }

        for (int x = ml; x < W - mr; x++) img.Set(x, H - mb, Black);
        for (int y = mt; y < H - mb; y++) img.Set(ml, y, Black);
        img.SavePng(path);
    }

    public static void PlotBars(string path, string[] labels, double[] values, AtColor color)
    {
        using var img = new AtBitmap(W, H);
        img.Clear(White);

        double max = values.Max();
        int n = values.Length;
        int bw = (W - 100) / n;
        for (int i = 0; i < n; i++)
        {
            int hBar = (int)((H - 120) * (values[i] / max));
            int x0 = 50 + i * bw;
            for (int x = x0; x < x0 + bw - 10; x++)
            for (int y = H - 60 - hBar; y < H - 60; y++)
                img.Set(x, y, color);
        }
        // Baseline.
        for (int x = 40; x < W - 20; x++) img.Set(x, H - 60, Black);
        img.SavePng(path);
    }

    public static void PlotLinear(string path, Series[] series,
        double xmin, double xmax, double ymin, double ymax)
    {
        using var img = new AtBitmap(W, H);
        img.Clear(White);

        int ml = 60, mr = 20, mt = 20, mb = 50;
        int Px(double v) => (int)(ml + (v - xmin) / (xmax - xmin) * (W - mr - ml));
        int Py(double v) => (int)(H - mb - (v - ymin) / (ymax - ymin) * (H - mb - mt));

        foreach (var s in series)
        {
            if (s.Line)
            {
                int prevX = -1, prevY = -1;
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] < xmin || s.X[i] > xmax || s.Y[i] < ymin || s.Y[i] > ymax) { prevX = -1; continue; }
                    int px = Px(s.X[i]), py = Py(s.Y[i]);
                    if (prevX >= 0) DrawLine(img, prevX, prevY, px, py, s.Color);
                    prevX = px; prevY = py;
                }
            }
            else
            {
                for (int i = 0; i < s.X.Length; i++)
                {
                    if (s.X[i] < xmin || s.X[i] > xmax || s.Y[i] < ymin || s.Y[i] > ymax) continue;
                    int px = Px(s.X[i]), py = Py(s.Y[i]);
                    int r = s.Size;
                    for (int dy = -r; dy <= r; dy++)
                    for (int dx = -r; dx <= r; dx++)
                        if (dx * dx + dy * dy <= r * r)
                        {
                            int xx = px + dx, yy = py + dy;
                            if (xx >= ml && xx < W - mr && yy >= mt && yy < H - mb) img.Set(xx, yy, s.Color);
                        }
                }
            }
        }

        for (int x = ml; x < W - mr; x++) img.Set(x, H - mb, Black);
        for (int y = mt; y < H - mb; y++) img.Set(ml, y, Black);
        img.SavePng(path);
    }

    private static int X(double lmin, double lmax, double lv, int ml, int mr) =>
        (int)(ml + (lv - lmin) / (lmax - lmin) * (mr - ml));

    private static int Y(double lmin, double lmax, double lv, int mt, int mb) =>
        (int)(mb - (lv - lmin) / (lmax - lmin) * (mb - mt));

    private static void DrawLine(AtBitmap img, int x0, int y0, int x1, int y1, AtColor c)
    {
        int dx = Math.Abs(x1 - x0), dy = Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1, sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;
        while (true)
        {
            if (x0 >= 0 && x0 < W && y0 >= 0 && y0 < H) img.Set(x0, y0, c);
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }
}
