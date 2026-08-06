namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for geometry dependence analysis of the Theta hierarchy.
///
/// TQM-143: Geometry Dependence of the Theta Hierarchy
/// </summary>
public static class QGeometryFamily
{
    /// <summary>
    /// A Q interaction graph geometry specification.
    /// </summary>
    public sealed record GeometrySpec(
        string Name,                        // "1D Chain", "2D Square", etc.
        int Dimension,                      // 1, 2, 3
        int NodeCount,                      // N nodes
        double[,] Adjacency,                // adjacency matrix
        double[,] Laplacian,                // graph Laplacian L_Q
        double MeanDegree,                  // average connections per node
        double ClusteringCoeff,             // graph clustering
        double Diameter,                    // longest shortest path
        string GraphClass);                 // "Regular", "Random", "Small-World", "Scale-Free"

    /// <summary>
    /// Spectral properties for one geometry.
    /// </summary>
    public sealed record GeometrySpectrum(
        string GeometryName,
        int EigenmodeCount,                 // number of distinct modes
        double SpectralGap,                 // λ_2 - λ_1 (gap to first excited mode)
        double SpectralRadius,              // max eigenvalue
        double[] Eigenvalues,               // first 15 eigenvalues
        int PredictedSpeciesCount,          // how many stable modes
        string SpectrumType);               // "Discrete", "Band", "Semicircle", "Power-Law"

    /// <summary>
    /// Comparison of one geometry vs 1D chain baseline across Theta properties.
    /// </summary>
    public sealed record GeometryComparison(
        string GeometryName,
        string VsBaseline,                  // "1D Chain"
        bool TransportSurvives,             // does signal propagation work?
        bool MemorySurvives,                // does persistence work?
        bool SpeciesSurvive,                // do species/exist?
        bool EvolutionSurvives,             // does selection work?
        bool LandscapeFinite,               // is the landscape finite?
        int SpeciesCountDiff,               // difference vs 1D chain
        double SpectralSimilarity,          // cosine similarity of eigenvalue spectra
        string Assessment);                 // "Identical", "Similar", "Different", "Fundamentally Different"

    /// <summary>
    /// Complete geometry dependence report.
    /// </summary>
    public sealed record GeometryComparisonReport(
        List<GeometrySpec> Geometries,
        List<GeometrySpectrum> Spectra,
        List<GeometryComparison> Comparisons,
        int GeometryCount,
        int UniversalProperties,            // how many Theta properties survive all geometries?
        double MeanSpectralSimilarity,      // average similarity to 1D chain
        string[] Invariants,               // which properties are geometric invariants?
        string[] GeometrySpecific,          // which properties are geometry-dependent?
        bool HierarchyIsUniversal,          // does the full hierarchy survive geometry changes?
        string Classification,              // "A: 1D Artifact" ... "D: Universal Graph-Based Information Physics"
        string Verdict);
}
