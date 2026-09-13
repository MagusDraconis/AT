using System.Buffers.Binary;
using AT.Core;
using AT.Core.FitsAnalysis;
using AT.Core.ResearchQG;

namespace AT.Tests.Unit;

/// <summary>
/// Smoke tests for the report rendering backend.
///
/// These exist because every other image-producing test in the suite is data-gated (the FITS-dependent audits
/// skip on a clean checkout) — which is how a *broken* rendering backend went unnoticed: SixLabors.ImageSharp's
/// licence check hard-failed Release builds while every test still passed in Debug. These assert only that the
/// backend produces a valid PNG of the requested size and that drawn colours actually reach the raster, so they
/// stay fast and free of the FITS data requirement.
/// </summary>
public class RenderingBackendTests
{
    static readonly byte[] PngSignature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

    [Fact]
    public void PlotBars_WritesValidPngOfRequestedSize()
    {
        RunWithTempFile(path =>
        {
            RARPlotter.PlotBars(path, new[] { "a", "b", "c" }, new[] { 1.0, 0.5, 0.25 }, new AtColor(200, 30, 60));
            AssertValidPng(path, 800, 600);
        });
    }

    [Fact]
    public void PlotLogLog_WritesValidPngOfRequestedSize()
    {
        RunWithTempFile(path =>
        {
            RARPlotter.PlotLogLog(path,
                new[] { new RARPlotter.Series(new[] { 1.0, 10.0, 100.0 }, new[] { 1.0, 2.0, 3.0 }, new AtColor(30, 100, 220), true, 2) },
                0.1, 1000.0, 0.1, 100.0);
            AssertValidPng(path, 800, 600);
        });
    }

    [Fact]
    public void SaveDiverging_WritesValidPngOfRequestedSize()
    {
        RunWithTempFile(path =>
        {
            var map = Enumerable.Range(0, 40 * 24).Select(i => (i % 40) / 39.0).ToArray();
            map[0] = double.NaN;   // exercises the NaN branch
            ImageMapExporter.SaveDiverging(path, map, 40, 24, 0.0, 1.0);
            AssertValidPng(path, 40, 24);
        });
    }

    /// <summary>
    /// The wiring check: identical data drawn in two different colours must encode to different bytes. Without
    /// this, a backend that silently ignored every pixel write would still emit a valid, correctly sized PNG.
    /// </summary>
    [Fact]
    public void DrawnColour_ReachesTheRaster()
    {
        byte[] Encode(AtColor c)
        {
            string path = Path.Combine(Path.GetTempPath(), $"at_colour_{Guid.NewGuid():N}.png");
            try
            {
                RARPlotter.PlotBars(path, new[] { "a", "b" }, new[] { 1.0, 0.5 }, c);
                return File.ReadAllBytes(path);
            }
            finally { File.Delete(path); }
        }

        Assert.NotEqual(Encode(new AtColor(220, 40, 40)), Encode(new AtColor(40, 160, 60)));
        // …and the same colour twice must be byte-identical, so the renderer stays deterministic.
        Assert.Equal(Encode(new AtColor(30, 100, 220)), Encode(new AtColor(30, 100, 220)));
    }

    private static void RunWithTempFile(Action<string> body)
    {
        string path = Path.Combine(Path.GetTempPath(), $"at_render_{Guid.NewGuid():N}.png");
        try { body(path); }
        finally { if (File.Exists(path)) File.Delete(path); }
    }

    private static void AssertValidPng(string path, int width, int height)
    {
        Assert.True(File.Exists(path), "no PNG was written");
        byte[] bytes = File.ReadAllBytes(path);

        Assert.Equal(PngSignature, bytes[..8]);
        Assert.Equal("IHDR", System.Text.Encoding.ASCII.GetString(bytes, 12, 4));
        int w = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(16));
        int h = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(20));
        Assert.True(width == w && height == h, $"expected {width}x{height}, got {w}x{h}");

        // A well-formed PNG must carry a non-empty IDAT chunk. Byte SIZE is not a valid content test: a smooth
        // 40x24 gradient legitimately deflates to ~150 bytes.
        int idat = bytes.AsSpan().IndexOf("IDAT"u8);
        Assert.True(idat > 0, "PNG has no IDAT chunk");
        Assert.True(BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(idat - 4)) > 0, "PNG has an empty IDAT chunk");
    }
}
