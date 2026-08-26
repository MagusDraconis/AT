namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 201 — Ladder Statistics Audit. Determines whether the 152 GeV excess alignment with the
/// frozen 151.98 GeV ladder rung is statistically significant. Uses ONLY the frozen QG192 ladder values and
/// the published 152 GeV excess central mass (152.0 GeV, arXiv:2503.16245). No new theory, no new ladder
/// values, no fitting. Deterministic.
///
/// FROZEN QG192 LADDER (9 predicted rungs, GeV): 106.39, 136.78, 151.98, 182.38, 197.58, 212.78, 227.97,
/// 243.17, 263.43. The observed excess central mass: 152.0 GeV.
///
/// STATISTICAL FRAMEWORK (null hypothesis): the observed excess mass is drawn UNIFORMLY over the search
/// range [95, 270] GeV (span 175 GeV — the full low/intermediate-mass window covered by ATLAS+CMS γγ
/// searches). Under the null, what is the probability that the observed mass lands within the observed
/// tolerance (±0.0132%, the measured deviation) of ANY of the 9 frozen rungs?
///
///   tolerance τ     = |152.0/151.98 − 1| = 1.316e-4 (0.0132%)
///   window per rung  = 2·τ·E_rung            (masses differ → each rung has its own window)
///   total window     = Σ_rungs 2·τ·E_rung    = 0.4533 GeV
///   p_any            = total_window / span   = 0.002591  → 1 in 386  (LOOK-ELSEWHERE corrected: this
///                     already counts all 9 rungs — no further trial factor needed)
///   p_one            = 2·τ·151.98 / span     = 0.000229  → 1 in 4375 (single-rung, 151.98 only)
///   z(any rung)      = Φ⁻¹(1 − p_any)        = 2.80σ
///   z(151.98 alone)  = Φ⁻¹(1 − p_one)        = 3.50σ
///
/// CLASSIFICATION BANDS (deterministic):
///   COINCIDENCE       p_any &gt; 0.05           (z &lt; 1.6σ)
///   WEAK SUPPORT      0.01 &lt; p_any ≤ 0.05     (1.6σ ≤ z &lt; 2.3σ)
///   MODERATE SUPPORT  0.001 &lt; p_any ≤ 0.01    (2.3σ ≤ z &lt; 3.1σ)
///   STRONG SUPPORT    p_any ≤ 0.001           (z ≥ 3.1σ)
///
/// With p_any = 0.00259 (1 in 386) → z = 2.80σ → MODERATE SUPPORT. The alignment is unlikely by chance
/// (better than 1-in-386 after look-elsewhere over all 9 rungs), but not conclusive at the 5σ level.
/// The single-rung (151.98) coincidence is 1-in-4375 (3.5σ) — strong if the rung had been the ONLY
/// prediction; the 9-rung ladder weakens it to MODERATE.
///
/// NOTE on the stated "0.01%": the exact deviation is 0.0132% (0.020 GeV at 151.98 GeV); 0.01% is the
/// rounded figure used in QG199/QG200 prose. All computations here use the exact 0.0132%.
/// </summary>
public static class LadderStatisticsAudit
{
    // ── Frozen QG192 inputs (only) ──────────────────────────────────────────────

    /// <summary>The 9 frozen predicted rungs (GeV), ascending (QG192).</summary>
    public static readonly double[] FrozenRungs =
        { 106.39, 136.78, 151.98, 182.38, 197.58, 212.78, 227.97, 243.17, 263.43 };

    /// <summary>The observed 152 GeV excess central mass (arXiv:2503.16245).</summary>
    public const double ObservedExcessMass = 152.0;

    /// <summary>Search range over which the excess could have appeared (full low/intermediate-mass window).</summary>
    public const double SearchLow = 95.0;
    public const double SearchHigh = 270.0;

    // ── 1. The observed deviation ────────────────────────────────────────────────

    /// <summary>Tolerance: the measured fractional deviation of the observed mass from the 151.98 rung.</summary>
    public static double Tolerance()
        => Math.Abs(ObservedExcessMass / FrozenRungs[2] - 1.0);   // 0.0132%

    /// <summary>Nearest-rung distance of the observed excess (GeV).</summary>
    public static double NearestRungDistance()
        => FrozenRungs.Min(r => Math.Abs(ObservedExcessMass - r));

    /// <summary>The nearest rung to the observed excess.</summary>
    public static double NearestRung() => FrozenRungs.OrderBy(r => Math.Abs(ObservedExcessMass - r)).First();

    // ── 2. Nearest-rung distances within the ladder ─────────────────────────────

    /// <summary>Nearest-neighbour distance of each frozen rung (GeV).</summary>
    public static double[] NearestNeighbourDistances()
    {
        var d = new double[FrozenRungs.Length];
        for (int i = 0; i < FrozenRungs.Length; i++)
        {
            double min = double.MaxValue;
            for (int j = 0; j < FrozenRungs.Length; j++)
            {
                if (i == j) continue;
                min = Math.Min(min, Math.Abs(FrozenRungs[i] - FrozenRungs[j]));
            }
            d[i] = min;
        }
        return d;
    }

    /// <summary>Mean nearest-neighbour spacing of the 9 rungs (GeV).</summary>
    public static double MeanSpacing()
        => NearestNeighbourDistances().Average();

    // ── 3. Random coincidence rate and look-elsewhere ───────────────────────────

    /// <summary>Total covered window: Σ over rungs of 2·τ·E_rung (GeV).</summary>
    public static double TotalWindowGeV()
        => FrozenRungs.Sum(r => 2.0 * Tolerance() * r);

    /// <summary>Search span (GeV).</summary>
    public static double SearchSpan() => SearchHigh - SearchLow;

    /// <summary>
    /// Probability that a uniformly-drawn excess mass lands within the observed tolerance of ANY of the
    /// 9 rungs. THIS IS ALREADY LOOK-ELSEWHERE CORRECTED (it counts all 9 rungs in the covered window).
    /// </summary>
    public static double ProbabilityAnyRung() => TotalWindowGeV() / SearchSpan();

    /// <summary>Probability of landing within tolerance of the 151.98 rung ALONE (no look-elsewhere).</summary>
    public static double ProbabilitySingleRung151_98() => 2.0 * Tolerance() * FrozenRungs[2] / SearchSpan();

    /// <summary>Effective trial factor (window/span vs single-rung window) ≈ 11.</summary>
    public static double TrialFactor()
        => ProbabilityAnyRung() / ProbabilitySingleRung151_98();

    // ── 4. Normal quantile (Acklam) and significance ────────────────────────────

    /// <summary>Inverse standard normal CDF via the Acklam algorithm (deterministic, 1e-9 accuracy).</summary>
    public static double NormalQuantile(double p)
    {
        if (p <= 0.0) return double.NegativeInfinity;
        if (p >= 1.0) return double.PositiveInfinity;
        double a0 = -3.969683028665376e+1, a1 = 2.209460984245205e+2, a2 = -2.759285104469687e+2;
        double a3 = 1.383577518672690e+2, a4 = -3.066479806614716e+1, a5 = 2.506628277459239e+0;
        double b0 = -5.447609879822406e+1, b1 = 1.615858368580409e+2, b2 = -1.556989798598866e+2;
        double b3 = 6.680131188771972e+1, b4 = -1.328068155288572e+1;
        double c0 = -7.784894002430293e-3, c1 = -3.223964580411365e-1, c2 = -2.400758277161838e+0;
        double c3 = -2.549732539343734e+0, c4 = 4.374664141464968e+0, c5 = 2.938163982698783e+0;
        double d0 = 7.784695709041462e-3, d1 = 3.224671290700398e-1, d2 = 2.445134137142996e+0;
        double d3 = 3.754408661907416e+0;
        const double plow = 0.02425, phigh = 1 - plow;

        double q, r;
        if (p < plow)
        {
            q = Math.Sqrt(-2 * Math.Log(p));
            return (((((c0 * q + c1) * q + c2) * q + c3) * q + c4) * q + c5) /
                   ((((d0 * q + d1) * q + d2) * q + d3) * q + 1);
        }
        if (p <= phigh)
        {
            q = p - 0.5; r = q * q;
            return (((((a0 * r + a1) * r + a2) * r + a3) * r + a4) * r + a5) * q /
                   (((((b0 * r + b1) * r + b2) * r + b3) * r + b4) * r + 1);
        }
        q = Math.Sqrt(-2 * Math.Log(1 - p));
        return -(((((c0 * q + c1) * q + c2) * q + c3) * q + c4) * q + c5) /
               ((((d0 * q + d1) * q + d2) * q + d3) * q + 1);
    }

    /// <summary>Significance of the any-rung coincidence (2.80σ).</summary>
    public static double ZAnyRung() => NormalQuantile(1.0 - ProbabilityAnyRung());

    /// <summary>Significance of the single-rung (151.98) coincidence (3.50σ).</summary>
    public static double ZSingleRung() => NormalQuantile(1.0 - ProbabilitySingleRung151_98());

    // ── Classification ──────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification (bands, deterministic):
    ///   p_any &gt; 0.05      → COINCIDENCE
    ///   0.01 &lt; p_any ≤ 0.05 → WEAK SUPPORT
    ///   0.001 &lt; p_any ≤ 0.01 → MODERATE SUPPORT
    ///   p_any ≤ 0.001       → STRONG SUPPORT
    /// </summary>
    public static string Classify()
    {
        double p = ProbabilityAnyRung();
        if (p > 0.05) return "COINCIDENCE";
        if (p > 0.01) return "WEAK SUPPORT";
        if (p > 0.001) return "MODERATE SUPPORT";
        return "STRONG SUPPORT";
    }

    /// <summary>Score 0..3: +1 deviation &lt; 0.05%; +1 p_any &lt; 1%; +1 z_any ≥ 2σ; +1 single-rung p &lt; 0.001.</summary>
    public static int EvidenceScore()
    {
        int score = 0;
        if (Tolerance() < 0.0005) score++;
        if (ProbabilityAnyRung() < 0.01) score++;
        if (ZAnyRung() >= 2.0) score++;
        if (ProbabilitySingleRung151_98() < 0.001) score++;
        return score;
    }
}
