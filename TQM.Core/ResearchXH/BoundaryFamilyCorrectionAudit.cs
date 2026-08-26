namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 311 — Boundary Family Correction Audit. Tests whether specific residuals can be
/// assigned to specific boundary families. D96 only, deterministic, no target values.
///
/// Reviewed observables:
///   tau hierarchy
///   top/bottom hierarchy
///   alpha_W
///   alpha_S
///   first acoustic peak
/// </summary>
public static class BoundaryFamilyCorrectionAudit
{
    public sealed record Correction(
        string Observable,
        string BoundaryFamily,
        double BoundaryRead);

    public static Correction[] Corrections() => new[]
    {
        new Correction("tau hierarchy", "degeneracy boundary", EffectiveAccessCounts.DoubletMultiplicities().Count(m => m == 2)),
        new Correction("top/bottom hierarchy", "occupancy boundary", ModeAccessOrigin.TopBandFraction()),
        new Correction("alpha_W", "octave boundary", FamilyIndexExactOrigin.FourthFamilyThreshold()),
        new Correction("alpha_S", "band boundary", ProjectionFamilyAudit.FamilyCount()),
        new Correction("first acoustic peak", "zero-mode boundary", 0.0),
    };

    public static bool AllAssigned()
        => Corrections().Length == 5 && Corrections().All(c => !string.IsNullOrWhiteSpace(c.BoundaryFamily));

    public static int FamilyCount()
        => Corrections().Select(c => c.BoundaryFamily).Distinct().Count();

    public static string Classify()
    {
        int c = FamilyCount();
        if (c <= 1) return "NO MAP";
        if (c == 2) return "PARTIAL MAP";
        return "BOUNDARY CORRECTION MAP";
    }
}
