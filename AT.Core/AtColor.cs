namespace AT.Core;

/// <summary>
/// An 8-bit RGB colour.
///
/// Deliberately independent of any imaging library: the plotter APIs (RARPlotter, ImageMapExporter) are called
/// from ~20 audit classes that have no business depending on a rendering package. Keeping this type neutral
/// means the rendering backend can change without touching a single caller — which is exactly the churn that
/// swapping SixLabors.ImageSharp for SkiaSharp would otherwise have caused twice over.
/// </summary>
public readonly record struct AtColor(byte R, byte G, byte B);
