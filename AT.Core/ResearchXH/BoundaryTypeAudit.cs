namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 309 — Boundary Type Audit. Tests whether the residual families split by boundary type.
/// D96 only, deterministic, no observables, no target values.
///
/// Boundary types:
///   zero-mode boundary
///   octave boundary
///   band boundary
///   occupancy boundary
///   degeneracy boundary
///
/// The audit checks whether the reviewed residual families associate with different boundary types,
/// using only derived D96 reads.
/// </summary>
public static class BoundaryTypeAudit
{
    public sealed record BoundaryType(
        string Name,
        double Read,
        string Definition);

    public sealed record ResidualFamily(
        string Name,
        string BoundaryType,
        double BoundaryRead,
        double ResidualProxy,
        bool NonZero);

    public static BoundaryType[] Types() => new[]
    {
        new BoundaryType("zero-mode boundary", 0.0, "first mode / vacuum edge"),
        new BoundaryType("octave boundary", FamilyIndexExactOrigin.FourthFamilyThreshold(), "span threshold for the next octave"),
        new BoundaryType("band boundary", ProjectionFamilyAudit.FamilyCount(), "octave band count from span"),
        new BoundaryType("occupancy boundary", ModeAccessOrigin.TopBandFraction(), "dense-band occupancy fraction"),
        new BoundaryType("degeneracy boundary", (double)EffectiveAccessCounts.DoubletMultiplicities().Count(m => m == 2), "doublet multiplicity count"),
    };

    public static ResidualFamily[] ResidualFamilies()
    {
        var types = Types().ToDictionary(t => t.Name, t => t.Read);
        return new[]
        {
            new ResidualFamily("tau residual", "degeneracy boundary", types["degeneracy boundary"], LeptonHierarchyExactLaw.TauMuonRatio(), true),
            new ResidualFamily("top/bottom residual", "occupancy boundary", types["occupancy boundary"], YukawaOrigin.TopBottomRatio(), true),
            new ResidualFamily("alpha_W residual", "octave boundary", types["octave boundary"], GaugeCouplingOrigin.AlphaWeak(), true),
            new ResidualFamily("alpha_S residual", "band boundary", types["band boundary"], GaugeCouplingOrigin.AlphaStrong(), true),
            new ResidualFamily("first-peak normalization", "zero-mode boundary", types["zero-mode boundary"], AcousticPeakOrigin.FirstPeak(), true),
        };
    }

    /// <summary>
    /// Does one boundary type explain more than one residual family? That is evidence for a boundary
    /// hierarchy instead of isolated boundary effects.
    /// </summary>
    public static bool MultipleBoundaryTypesPresent()
        => ResidualFamilies().Select(r => r.BoundaryType).Distinct().Count() >= 3;

    /// <summary>
    /// Boundary-type spread score: counts how many distinct boundary types are implicated.
    /// </summary>
    public static int BoundaryTypeCount()
        => ResidualFamilies().Select(r => r.BoundaryType).Distinct().Count();

    public static string Classify()
    {
        int c = BoundaryTypeCount();
        if (c <= 1) return "NO BOUNDARY STRUCTURE";
        if (c == 2) return "PARTIAL BOUNDARY STRUCTURE";
        return "BOUNDARY HIERARCHY";
    }
}
