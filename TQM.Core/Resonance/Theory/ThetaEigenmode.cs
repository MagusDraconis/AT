namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for the spectral origin of the Theta information landscape.
/// Eigenmodes, spectral families, and species-mode mappings.
///
/// TQM-140: Spectral Origin of the Information Landscape
/// </summary>
public static class ThetaEigenmode
{
    /// <summary>
    /// A single eigenmode of the discrete Theta field operator.
    /// </summary>
    public sealed record Eigenmode(
        int ModeIndex,                      // k = 0, 1, 2, ... (mode number)
        double Eigenvalue,                  // λ_k
        double[] Eigenvector,               // normalized mode shape
        double Frequency,                   // oscillation frequency (|Im(λ)|)
        double DampingRate,                 // decay rate (|Re(λ)|)
        double Stability,                   // 1 / damping rate
        int NodalCount,                     // number of zero crossings
        string ModeFamily,                  // "Uniform", "Fundamental", "Harmonic-2", etc.
        int Degeneracy,                     // number of modes in same family
        bool IsStable);                     // damping rate below threshold?

    /// <summary>
    /// A spectral family: group of modes with the same frequency.
    /// </summary>
    public sealed record SpectralFamily(
        string FamilyName,                  // "k=0 Uniform", "k=1 Fundamental", etc.
        int ModeCount,                      // number of modes in family
        double CentralFrequency,            // characteristic frequency
        double MeanStability,               // average stability
        int[] ModeIndices,                  // which mode indices belong
        bool CorrespondsToGraphComponent,   // maps to a TQM-139 graph component?
        string MappedComponentName);        // which component

    /// <summary>
    /// Mapping between a TQM-139 attractor and a Theta eigenmode.
    /// </summary>
    public sealed record SpeciesModeMap(
        string SpeciesName,                 // TQM-139 attractor name
        int MappedModeIndex,               // closest eigenmode
        double PatternOverlap,              // cosine similarity
        double EigenvalueMatch,             // how well eigenvalues align
        string ModeFamily,                  // spectral family
        bool IsHubMode,                     // is this mode a hub in the spectral graph?
        bool IsBottleneck);                 // is this mode a bottleneck?

    /// <summary>
    /// Complete spectral landscape report.
    /// </summary>
    public sealed record SpectralLandscapeReport(
        List<Eigenmode> Eigenmodes,
        List<SpectralFamily> Families,
        List<SpeciesModeMap> SpeciesMappings,
        int TotalEigenmodes,
        int TotalFamilies,
        int MappedSpecies,                  // how many TQM-139 species were mapped
        double MeanMappingOverlap,          // average pattern similarity
        int PredictedAttractorCount,        // spectral prediction of species count
        bool SpectralOriginConfirmed,       // do eigenmodes explain the landscape?
        bool FamiliesMatchComponents,       // do spectral families = graph components?
        bool HubsMatchLowOrderModes,        // are hubs = low-k modes?
        bool BottlenecksMatchHighOrder,     // are bottlenecks = high-k modes?
        string Classification,              // "A: Attractors Only" ... "D: Fundamental Spectral Landscape"
        string Verdict);
}
