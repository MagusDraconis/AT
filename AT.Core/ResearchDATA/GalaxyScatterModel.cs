namespace AT.Core.ResearchDATA;

/// <summary>
/// RAR scatter statistics for a specific galaxy type.
/// </summary>
public sealed record GalaxyTypeScatter(
    string TypeName,
    int NGalaxies,
    int NPoints,
    double MeanLogGbar,
    double MeanLogGobs,
    double RmsScatter,
    double MedianD,
    double MeanGDagger,
    string Characteristics);

/// <summary>
/// Galaxy-type scatter comparison matrix.
/// </summary>
public sealed record GalaxyScatterMatrix(
    GalaxyTypeScatter[] Types,
    double GlobalScatter,
    double MaxTypeScatter,
    double MinTypeScatter,
    bool ScatterVariesWithType,
    string Summary);
