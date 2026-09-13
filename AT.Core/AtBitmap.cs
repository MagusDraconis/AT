using SkiaSharp;

namespace AT.Core;

/// <summary>
/// Minimal RGB raster the report renderers draw into, backed by SkiaSharp for PNG encoding.
///
/// SkiaSharp is used **only** here, so the imaging backend stays out of the plotter APIs and off the public
/// surface. Replaced SixLabors.ImageSharp, whose build targets hard-fail a Release build without a commercial
/// licence key (`ContinueOnError` was true only for Debug configurations) — which silently made Release builds
/// impossible. SkiaSharp is MIT and imposes no such check.
/// </summary>
internal sealed class AtBitmap : IDisposable
{
    private readonly SKBitmap _bitmap;

    public AtBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        _bitmap = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque);
    }

    public int Width { get; }
    public int Height { get; }

    public void Set(int x, int y, AtColor c) => _bitmap.SetPixel(x, y, new SKColor(c.R, c.G, c.B));

    /// <summary>Fill the whole raster — one canvas clear rather than width × height pixel writes.</summary>
    public void Clear(AtColor c)
    {
        using var canvas = new SKCanvas(_bitmap);
        canvas.Clear(new SKColor(c.R, c.G, c.B));
    }

    public void SavePng(string path)
    {
        using var image = SKImage.FromBitmap(_bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.Create(path);
        data.SaveTo(stream);
    }

    public void Dispose() => _bitmap.Dispose();
}
