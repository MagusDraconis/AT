namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 312 — Boundary Correction Effect Audit. Tests whether boundary-family reads reduce
/// residuals relative to the baseline operator read. D96 only, deterministic.
///
/// Reviewed observables:
///   tau hierarchy
///   top/bottom hierarchy
///   alpha_W
///   alpha_S
///   first acoustic peak
/// </summary>
public static class BoundaryCorrectionEffectAudit
{
    public sealed record CorrectionEffect(
        string Observable,
        string BoundaryFamily,
        double BaselineResidual,
        double CorrectedResidual,
        string Effect);

    public static CorrectionEffect[] Effects() => new[]
    {
        new CorrectionEffect("tau hierarchy", "degeneracy boundary",
            Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio()),
            Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - BoundaryFamilyCorrectionAudit.Corrections()[0].BoundaryRead),
            Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - BoundaryFamilyCorrectionAudit.Corrections()[0].BoundaryRead) < Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio()) ? "improved" : "unchanged"),
        new CorrectionEffect("top/bottom hierarchy", "occupancy boundary",
            Math.Abs(YukawaOrigin.TopBottomRatio()),
            Math.Abs(YukawaOrigin.TopBottomRatio() - BoundaryFamilyCorrectionAudit.Corrections()[1].BoundaryRead),
            Math.Abs(YukawaOrigin.TopBottomRatio() - BoundaryFamilyCorrectionAudit.Corrections()[1].BoundaryRead) < Math.Abs(YukawaOrigin.TopBottomRatio()) ? "improved" : "unchanged"),
        new CorrectionEffect("alpha_W", "octave boundary",
            Math.Abs(GaugeCouplingOrigin.AlphaWeak()),
            Math.Abs(GaugeCouplingOrigin.AlphaWeak() - BoundaryFamilyCorrectionAudit.Corrections()[2].BoundaryRead),
            Math.Abs(GaugeCouplingOrigin.AlphaWeak() - BoundaryFamilyCorrectionAudit.Corrections()[2].BoundaryRead) < Math.Abs(GaugeCouplingOrigin.AlphaWeak()) ? "improved" : "unchanged"),
        new CorrectionEffect("alpha_S", "band boundary",
            Math.Abs(GaugeCouplingOrigin.AlphaStrong()),
            Math.Abs(GaugeCouplingOrigin.AlphaStrong() - BoundaryFamilyCorrectionAudit.Corrections()[3].BoundaryRead),
            Math.Abs(GaugeCouplingOrigin.AlphaStrong() - BoundaryFamilyCorrectionAudit.Corrections()[3].BoundaryRead) < Math.Abs(GaugeCouplingOrigin.AlphaStrong()) ? "improved" : "unchanged"),
        new CorrectionEffect("first acoustic peak", "zero-mode boundary",
            AcousticPeakOrigin.FirstPeakDeviation(),
            Math.Abs(AcousticPeakOrigin.FirstPeak() - BoundaryFamilyCorrectionAudit.Corrections()[4].BoundaryRead),
            Math.Abs(AcousticPeakOrigin.FirstPeak() - BoundaryFamilyCorrectionAudit.Corrections()[4].BoundaryRead) < AcousticPeakOrigin.FirstPeakDeviation() ? "improved" : "unchanged"),
    };

    public static int ImprovedCount()
        => Effects().Count(e => e.Effect == "improved");

    public static int WorseCount()
        => Effects().Count(e => e.Effect == "worse");

    public static string Classify()
    {
        int i = ImprovedCount();
        int w = WorseCount();
        if (i == 0 && w == 0) return "NO EFFECT";
        if (i >= 1 && w == 0) return "PARTIAL EFFECT";
        return "BOUNDARY CORRECTIONS VALIDATED";
    }
}
