namespace AT.Core.FitsAnalysis;

public sealed record HduInfo(
    int Index,
    string TypeName,
    string ExtName,
    string Axes,
    int BitPix,
    string DataType,
    string DataShape,
    string Purpose);

public sealed record HeaderEntry(string Key, string Value, string Comment);

public sealed record FitsReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    HduInfo[] Hdus,
    HeaderEntry[] PrimaryHeader,
    double[] Wavelength,
    double[] Spectrum,
    double Redshift,
    string DetectedLines,
    string Classification);
