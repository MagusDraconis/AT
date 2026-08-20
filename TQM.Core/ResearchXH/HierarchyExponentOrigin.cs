namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 141 — Origin of hierarchy exponents. QG140 reproduced the lepton mass hierarchy via a FITTED
/// amplification law mass = A·center^p·modes^q (p≈7.69, q≈−0.82). This phase asks: can these exponents EMERGE
/// from spectral or actualization dynamics rather than fitting?
///
/// Method (computational, fully deterministic): (1) SPECTRAL SCALING LAWS — the Weyl-like scaling of the
/// observable sector's intra-sector spectrum: the cumulative mode count N(ω) ~ ω^δ; the mode density
/// g(ω) = dN/dω ~ ω^(δ−1). (2) OCTAVE OCCUPANCY — the mode count per octave band as a function of the band
/// center: modes_k ~ center_k^δ (the octave occupation law). (3) MODE-DENSITY EFFECTS — verify that the
/// per-octave occupation follows the spectral density law, giving a DERIVED occupation exponent δ.
/// (4) ACTUALIZATION STATISTICS — the final activity distribution of the observable sector (does it carry a
/// hierarchy? it saturates at the ceiling). (5) EXPONENT DERIVATION — combine the derived spectral density
/// exponent δ with the required net mass-span exponent (p_net = log(leptonSpan)/log(octaveSpan)) to derive
/// the amplification exponents, and compare the derived occupation exponent δ_derived (implied by the QG140
/// fit) with the measured spectral density exponent δ_measured.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class HierarchyExponentOrigin
{
    /// <summary>Default dynamics parameters (matching QG115–140).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;
    public const int DefaultN = 96;

    /// <summary>Lepton mass span (tau/e).</summary>
    public const double LeptonSpan = 3477.2;

    // ── 1. Spectral scaling laws ────────────────────────────────────────────────

    /// <summary>
    /// Weyl-like spectral scaling exponent δ: the cumulative mode count N(ω) ~ ω^δ of the observable
    /// sector's intra-sector spectrum. Fitted on log N vs log ω over the populated range.
    /// </summary>
    public static double WeylExponent(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K, feedback, damping);
        if (w.Length < 4) return double.NaN;
        var ws = w.OrderBy(x => x).ToArray();
        var logW = new List<double>();
        var logN = new List<double>();
        for (int i = 0; i < ws.Length; i++)
        {
            if (ws[i] <= 0) continue;
            logW.Add(Math.Log(ws[i]));
            logN.Add(Math.Log(i + 1));
        }
        return LinearFitSlope(logW.ToArray(), logN.ToArray());
    }

    /// <summary>Mode density exponent: g(ω) ~ ω^(δ−1).</summary>
    public static double ModeDensityExponent(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        double d = WeylExponent(n, K, feedback, damping);
        return double.IsNaN(d) ? double.NaN : d - 1.0;
    }

    private static double LinearFitSlope(double[] x, double[] y)
    {
        int m = x.Length;
        if (m < 2) return double.NaN;
        double mx = x.Average(), my = y.Average();
        double num = 0, den = 0;
        for (int i = 0; i < m; i++)
        {
            num += (x[i] - mx) * (y[i] - my);
            den += (x[i] - mx) * (x[i] - mx);
        }
        return den < 1e-12 ? double.NaN : num / den;
    }

    // ── 2. Octave occupancy ─────────────────────────────────────────────────────

    /// <summary>
    /// Octave occupancy law: (octaveIndex, center, modeCount) for each octave band, testing whether
    /// modes_k ~ center_k^δ.
    /// </summary>
    public static (int Octave, double Center, int Modes)[] OctaveOccupancy(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => MassHierarchyFromOctaves.BandPositions(n, K, feedback, damping)
            .Select(b => (b.Band, b.Center, b.Modes)).ToArray();

    /// <summary>
    /// Fitted octave-occupation exponent: log(modes_k) vs log(center_k) over the octave bands. This is the
    /// measured spectral-density-controlled occupation exponent.
    /// </summary>
    public static double OccupationExponentFromOccupancy(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var occ = OctaveOccupancy(n, K, feedback, damping);
        if (occ.Length < 2) return double.NaN;
        return LinearFitSlope(occ.Select(o => Math.Log(o.Center)).ToArray(),
            occ.Select(o => Math.Log(o.Modes)).ToArray());
    }

    // ── 3. Mode-density effects ─────────────────────────────────────────────────

    /// <summary>
    /// Consistency: the octave-occupation exponent should approximately equal the Weyl-mode-density
    /// exponent δ (mode count per octave ~ center^δ). Returns the absolute difference.
    /// </summary>
    public static double DensityOccupationConsistency(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        double weyl = WeylExponent(n, K, feedback, damping);
        double occ = OccupationExponentFromOccupancy(n, K, feedback, damping);
        if (double.IsNaN(weyl) || double.IsNaN(occ)) return double.NaN;
        return Math.Abs(weyl - occ);
    }

    // ── 4. Actualization statistics ─────────────────────────────────────────────

    /// <summary>
    /// Actualization statistics: the final activity distribution of the observable sector. If ALL nodes
    /// saturate at the ceiling (a_i = 1.0), the activity carries NO hierarchy — the mass hierarchy cannot
    /// come from the raw actualization-rate values.
    /// </summary>
    public static (double Min, double Max, int DistinctValues) ActualizationStatistics(int n = DefaultN,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var (a, _) = HighEnergySectorStability.ObservableSector(n, K, feedback, damping);
        var distinct = a.Select(x => Math.Round(x, 6)).Distinct().Count();
        return (a.Min(), a.Max(), distinct);
    }

    /// <summary>Does the final activity carry a hierarchy (more than 1 distinct level, non-saturated)?</summary>
    public static bool ActivityCarriesHierarchy(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => ActualizationStatistics(n, K, feedback, damping).DistinctValues > 1;

    // ── 5. Exponent derivation ──────────────────────────────────────────────────

    /// <summary>
    /// Net mass-span exponent: p_net = log(leptonSpan)/log(octaveSpan), the exponent of mass ∝ center^p_net
    /// that must hold to reproduce the lepton span.
    /// </summary>
    public static double NetMassExponent(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var centers = HierarchyAmplification.OctaveCenters(n, K, feedback, damping);
        if (centers.Length < 2) return double.NaN;
        return Math.Log(LeptonSpan) / Math.Log(centers[^1] / centers[0]);
    }

    /// <summary>
    /// Derived occupation exponent: the δ implied by the QG140 fit. Since mass = A·center^p·modes^q and
    /// modes ~ center^δ, the net exponent is p + q·δ = p_net, so δ_derived = (p_net − p)/q.
    /// </summary>
    public static double DerivedOccupationExponent(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var fit = HierarchyAmplification.FitAmplificationLaw(n, K, feedback, damping);
        double pNet = NetMassExponent(n, K, feedback, damping);
        if (Math.Abs(fit.Q) < 1e-9) return double.NaN;
        return (pNet - fit.P) / fit.Q;
    }

    /// <summary>
    /// Exponent derivation check: the derived occupation exponent δ_derived (from the QG140 fit) should
    /// match the measured spectral density exponent δ_measured (Weyl exponent). Returns the relative
    /// deviation |δ_derived/δ_measured − 1|.
    /// </summary>
    public static double ExponentDerivationDeviation(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        double derived = DerivedOccupationExponent(n, K, feedback, damping);
        double measured = WeylExponent(n, K, feedback, damping);
        if (double.IsNaN(derived) || double.IsNaN(measured) || Math.Abs(measured) < 1e-9) return double.NaN;
        return Math.Abs(derived / measured - 1.0);
    }

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Exponent-origin score (0..5):
    /// 1. the spectrum has a well-defined Weyl-like scaling exponent (finite, 1–4);
    /// 2. the octave occupancy follows a power law (occupation exponent finite);
    /// 3. the raw activity distribution is saturated (no hierarchy in activity — the exponents must come
    ///    from the spectrum);
    /// 4. the derived occupation exponent matches the measured Weyl exponent within 40% (partial
    ///    derivation);
    /// 5. the derived occupation exponent matches the measured Weyl exponent within 15% (tight derivation).
    /// </summary>
    public static int OriginScore(int n = DefaultN, int K = DefaultK)
    {
        int score = 0;
        double weyl = WeylExponent(n, K);
        if (!double.IsNaN(weyl) && weyl > 1.0 && weyl < 4.0) score++;
        double occ = OccupationExponentFromOccupancy(n, K);
        if (!double.IsNaN(occ)) score++;
        if (!ActivityCarriesHierarchy(n, K)) score++;
        double dev = ExponentDerivationDeviation(n, K);
        if (!double.IsNaN(dev) && dev < 0.40) score++;
        if (!double.IsNaN(dev) && dev < 0.15) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FIT ONLY         — the amplification exponents are purely fitted; the spectrum/activity carry no
    ///                      derivable exponent structure;
    ///   PARTIAL ORIGIN   — a spectral scaling structure exists and partially predicts the exponents, but
    ///                      the derived exponent does not tightly match the measured spectral density
    ///                      exponent;
    ///   DERIVED EXPONENTS — the amplification exponents follow from the spectral (Weyl/mode-density)
    ///                      scaling of the observable sector — derived, not fitted.
    /// </summary>
    public static string Classify(int n = DefaultN, int K = DefaultK)
    {
        int score = OriginScore(n, K);
        if (score <= 2) return "FIT ONLY";
        if (score == 5) return "DERIVED EXPONENTS";
        return "PARTIAL ORIGIN";
    }
}
