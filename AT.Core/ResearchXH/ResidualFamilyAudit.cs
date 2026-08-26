namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 310 — Residual Family Audit. Tests whether >1% residuals cluster into identifiable
/// boundary families. D96 only, deterministic, no target values.
///
/// Families reviewed:
///   tau hierarchy
///   top/bottom hierarchy
///   alpha_W
///   alpha_S
///   acoustic l1
/// </summary>
public static class ResidualFamilyAudit
{
    public sealed record ResidualFamily(
        string Name,
        string BoundaryFamily,
        double BoundaryRead,
        double ResidualProxy);

    public static ResidualFamily[] Families() => new[]
    {
        new ResidualFamily("tau hierarchy", "degeneracy boundary", EffectiveAccessCounts.DoubletMultiplicities().Count(m => m == 2), LeptonHierarchyExactLaw.TauMuonRatio()),
        new ResidualFamily("top/bottom hierarchy", "occupancy boundary", ModeAccessOrigin.TopBandFraction(), YukawaOrigin.TopBottomRatio()),
        new ResidualFamily("alpha_W", "octave boundary", FamilyIndexExactOrigin.FourthFamilyThreshold(), GaugeCouplingOrigin.AlphaWeak()),
        new ResidualFamily("alpha_S", "band boundary", ProjectionFamilyAudit.FamilyCount(), GaugeCouplingOrigin.AlphaStrong()),
        new ResidualFamily("acoustic l1", "zero-mode boundary", 0.0, AcousticPeakOrigin.FirstPeak()),
    };

    /// <summary>Do the residuals map to more than one boundary family?</summary>
    public static bool MultipleFamiliesPresent()
        => Families().Select(f => f.BoundaryFamily).Distinct().Count() >= 3;

    /// <summary>How many distinct boundary families are present?</summary>
    public static int FamilyCount()
        => Families().Select(f => f.BoundaryFamily).Distinct().Count();

    public static string Classify()
    {
        int c = FamilyCount();
        if (c <= 1) return "NO FAMILIES";
        if (c == 2) return "PARTIAL FAMILIES";
        return "BOUNDARY FAMILY STRUCTURE";
    }
}
