namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for nonlinear mode composition: composite modes built from
/// eigenmode combinations, species composition mappings, and coupling matrices.
///
/// AT-141: Nonlinear Mode Composition and Species Emergence
/// </summary>
public static class CompositeMode
{
    /// <summary>
    /// A composite mode formed by combining eigenmodes.
    /// </summary>
    public sealed record CompositeModeInfo(
        string Name,                        // "C1", "C2", etc.
        double[] Pattern,                   // resulting pattern vector
        int[] SourceModes,                  // which eigenmodes are combined
        double[] Coefficients,              // weights for each source mode
        bool HasNonlinearTerm,             // includes v_i · v_j terms
        double Stability,                   // estimated persistence
        double Complexity,                  // zero crossings
        double Energy);                     // pattern norm

    /// <summary>
    /// Mapping of a AT-139 attractor species to its best composite mode.
    /// </summary>
    public sealed record SpeciesComposition(
        string SpeciesName,
        int[] ComposingModes,               // eigenmodes that compose this species
        double Overlap,                     // pattern similarity with best composite
        bool IsPureMode,                    // single eigenmode?
        bool IsLinearPair,                  // linear combination of 2?
        bool IsNonlinearPair,               // nonlinear combination of 2?
        bool IsTriple,                      // combination of 3+?
        string CompositionType);            // "Pure", "Linear-Pair", "Nonlinear-Pair", "Triple"

    /// <summary>
    /// Mode coupling matrix: C_ij = coupling strength between modes i and j.
    /// </summary>
    public sealed record ModeCouplingMatrix(
        int ModeCount,
        double[,] LinearCoupling,           // C_ij for linear combinations
        double[,] NonlinearCoupling,        // C_ij for nonlinear (product) combinations
        (int i, int j)[] StrongestPairs,    // top coupled pairs
        int TotalSignificantCouplings);     // pairs with |C| > threshold

    /// <summary>
    /// Complete nonlinear composition report.
    /// </summary>
    public sealed record NonlinearCompositionReport(
        List<CompositeModeInfo> CompositeModes,
        List<SpeciesComposition> SpeciesMappings,
        ModeCouplingMatrix CouplingMatrix,
        int TotalCompositesGenerated,
        int TotalSpeciesMapped,
        double MeanReconstructionOverlap,   // average overlap with AT-139 species
        int SpeciesCountFromComposites,     // how many unique species from composites
        double SpeciesCoverage,             // fraction of AT-139 species explained
        int MinimumBasisSize,               // how many fundamental modes needed
        bool CompositesExplainExcess,       // do composites explain 13-19 vs 10?
        bool NonlinearEssential,            // are nonlinear terms necessary?
        string Classification,              // "A: Pure Eigenmode Theory" ... "D: Nonlinear Spectral Geometry"
        string Verdict);
}
