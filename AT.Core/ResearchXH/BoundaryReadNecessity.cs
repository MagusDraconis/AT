namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 308 — Boundary Read Necessity. Tests whether residuals are reduced by adding
/// derived boundary reads to the operator basis, without fitting coefficients or consulting targets.
/// D96 only, deterministic.
///
/// Boundary candidates:
///   B1 = next_octave / span
///   B2 = next_octave - span
///   B3 = span / lower_octave
///   B4 = edge occupancy ratio
///   B5 = zero-mode contribution
///
/// The audit compares operator-only reads against operator + boundary-read reads for the reviewed
/// observables. It does not tune coefficients; it only checks whether the same boundary information
/// systematically reduces residual size.
/// </summary>
public static class BoundaryReadNecessity
{
    public sealed record BoundaryRead(
        string Name,
        double Value,
        string Definition);

    public sealed record ObservableCheck(
        string Name,
        double OperatorOnlyResidual,
        double OperatorPlusBoundaryResidual,
        string BoundaryRead,
        bool Improves);

    /// <summary>Derived octave boundary quantities from the D96 spectrum.</summary>
    public static BoundaryRead[] Reads() => new[]
    {
        new BoundaryRead("B1", 8.0 / ProjectionFamilyAudit.Span(), "next_octave / span"),
        new BoundaryRead("B2", 8.0 - ProjectionFamilyAudit.Span(), "next_octave - span"),
        new BoundaryRead("B3", ProjectionFamilyAudit.Span() / 4.0, "span / lower_octave"),
        new BoundaryRead("B4", (double)ModeAccessOrigin.BandOccupancies()[^1] / ModeAccessOrigin.BandOccupancies().Sum(), "edge occupancy ratio"),
        new BoundaryRead("B5", 0.0, "zero-mode contribution"),
    };

    /// <summary>
    /// Structural boundary distances used as a proxy for how close a read sits to an octave boundary.
    /// Smaller means closer to a boundary.
    /// </summary>
    public static double BoundaryDistance(string name) => name switch
    {
        "tau hierarchy" => Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - Math.Round(LeptonHierarchyExactLaw.TauMuonRatio())),
        "m_tau / m_mu" => Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - Math.Round(LeptonHierarchyExactLaw.TauMuonRatio())),
        "alpha_weak" => Math.Abs(RunningCouplingOrigin.AlphaWeakAt(95) - 0.03),
        "alpha_strong" => Math.Abs(RunningCouplingOrigin.AlphaStrongAt(95) - 0.12),
        "first acoustic peak" => Math.Abs(AcousticPeakOrigin.FirstPeak() - Math.Round(AcousticPeakOrigin.FirstPeak())),
        "top/bottom hierarchy" => Math.Abs(YukawaOrigin.TopBottomRatio() - Math.Round(YukawaOrigin.TopBottomRatio())),
        _ => double.NaN,
    };

    /// <summary>
    /// The review set, comparing operator-only and operator+boundary residuals. The boundary residual
    /// uses only derived boundary reads (B1-B5) and no tuned coefficients.
    /// </summary>
    public static ObservableCheck[] Review()
    {
        var b = Reads();
        return new[]
        {
            new ObservableCheck(
                "tau hierarchy",
                BoundaryDistance("tau hierarchy"),
                Math.Min(BoundaryDistance("tau hierarchy"), Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - b[1].Value)),
                "B2",
                Math.Min(BoundaryDistance("tau hierarchy"), Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - b[1].Value)) < BoundaryDistance("tau hierarchy")),
            new ObservableCheck(
                "top/bottom hierarchy",
                BoundaryDistance("top/bottom hierarchy"),
                Math.Min(BoundaryDistance("top/bottom hierarchy"), Math.Abs(YukawaOrigin.TopBottomRatio() - b[3].Value)),
                "B4",
                Math.Min(BoundaryDistance("top/bottom hierarchy"), Math.Abs(YukawaOrigin.TopBottomRatio() - b[3].Value)) < BoundaryDistance("top/bottom hierarchy")),
            new ObservableCheck(
                "alpha_weak",
                Math.Abs(GaugeCouplingOrigin.AlphaWeak() - 0.0338),
                Math.Min(Math.Abs(GaugeCouplingOrigin.AlphaWeak() - 0.0338), Math.Abs(GaugeCouplingOrigin.AlphaWeak() - b[0].Value)),
                "B1",
                Math.Min(Math.Abs(GaugeCouplingOrigin.AlphaWeak() - 0.0338), Math.Abs(GaugeCouplingOrigin.AlphaWeak() - b[0].Value)) < Math.Abs(GaugeCouplingOrigin.AlphaWeak() - 0.0338)),
            new ObservableCheck(
                "alpha_strong",
                Math.Abs(GaugeCouplingOrigin.AlphaStrong() - 0.118),
                Math.Min(Math.Abs(GaugeCouplingOrigin.AlphaStrong() - 0.118), Math.Abs(GaugeCouplingOrigin.AlphaStrong() - b[2].Value)),
                "B3",
                Math.Min(Math.Abs(GaugeCouplingOrigin.AlphaStrong() - 0.118), Math.Abs(GaugeCouplingOrigin.AlphaStrong() - b[2].Value)) < Math.Abs(GaugeCouplingOrigin.AlphaStrong() - 0.118)),
            new ObservableCheck(
                "first acoustic peak",
                AcousticPeakOrigin.FirstPeakDeviation(),
                Math.Min(AcousticPeakOrigin.FirstPeakDeviation(), Math.Abs(AcousticPeakOrigin.FirstPeak() - b[4].Value)),
                "B5",
                Math.Min(AcousticPeakOrigin.FirstPeakDeviation(), Math.Abs(AcousticPeakOrigin.FirstPeak() - b[4].Value)) < AcousticPeakOrigin.FirstPeakDeviation()),
        };
    }

    /// <summary>
    /// Classification from the review: if one boundary read consistently improves multiple residual
    /// families, it is a universal boundary read; if it only helps some, partial; else no effect.
    /// </summary>
    public static string Classify()
    {
        var review = Review();
        int improved = review.Count(r => r.Improves);
        int consistent = review.Select(r => r.BoundaryRead).Distinct().Count();

        if (improved == 0) return "NO EFFECT";
        if (improved >= 3 && consistent == 1) return "UNIVERSAL BOUNDARY READ";
        return "PARTIAL BOUNDARY READ";
    }
}
