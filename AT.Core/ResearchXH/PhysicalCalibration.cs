namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 129 — Physical calibration of the sector ladder. QG128 established that sector transitions
/// generate a PREDICTIVE discrete spectrum (unit quantum Δradius=1 dominant, plus one 1.333 line; 8 discrete
/// energy thresholds; 12-rung decay ladder). This phase asks: can the ladder be CALIBRATED to known particle
/// masses or collider energy scales?
///
/// Method (computational, fully deterministic): take the network ladder from QG128 (12 rungs, radii 6..17.333,
/// dominant unit quantum, thresholds) and compare its CHARACTERISTIC RATIOS against the documented Standard
/// Model mass ratios. We measure: (1) MASS-SPECTRUM MATCHING — does any network characteristic ratio
/// (top quantum 1.333, ladder span 2.889, rung spacing 1.0) reproduce a known SM mass ratio (t/H, H/Z, Z/W,
/// t/W, tau/mu, mu/e, tau/e)? (2) RESONANCE SPACING — is the ladder spacing uniform (harmonic-like resonance
/// ladder)? (3) THRESHOLD ENERGIES — the discrete dimensionless thresholds and their span; (4) COLLIDER
/// ACCESSIBILITY — the energy range needed to reach the highest sector, relative to collider reach; (5)
/// SCALING LAWS — does the ladder follow a scaling law (arithmetic in radius) consistent with the observed
/// mass hierarchy?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here. SM masses are
/// treated as documented empirical constants.
/// </summary>
public static class PhysicalCalibration
{
    // ── Documented SM masses (MeV; GeV noted) — empirical constants ──────────────

    /// <summary>Electron mass (MeV).</summary>
    public const double MElectron = 0.511;

    /// <summary>Muon mass (MeV).</summary>
    public const double MMuon = 105.66;

    /// <summary>Tau mass (MeV).</summary>
    public const double MTau = 1776.86;

    /// <summary>W boson mass (GeV).</summary>
    public const double MWGeV = 80.38;

    /// <summary>Z boson mass (GeV).</summary>
    public const double MZGeV = 91.19;

    /// <summary>Higgs mass (GeV).</summary>
    public const double MHGeV = 125.10;

    /// <summary>Top quark mass (GeV).</summary>
    public const double MTopGeV = 173.0;

    /// <summary>Approximate ratio of highest to lowest collider energy scales (LHC 14 TeV / LEP 0.2 TeV).</summary>
    public const double ColliderScaleSpan = 65.0;

    // ── Network ladder constants (QG128) ─────────────────────────────────────────

    /// <summary>The 12-rung decay ladder radii (QG128).</summary>
    public static readonly double[] LadderRadii =
        { 17.333, 16.0, 15.0, 14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0 };

    /// <summary>Dominant unit quantum (Δradius = 1).</summary>
    public const double UnitQuantum = 1.0;

    /// <summary>Top (first) transition quantum (17.333 → 16.0).</summary>
    public const double TopQuantum = 1.333;

    /// <summary>Network characteristic ratios to test against SM ratios.</summary>
    public static (string Name, double Ratio)[] NetworkCharacteristicRatios()
        => new[]
        {
            ("unit_quantum", UnitQuantum),
            ("top_quantum", TopQuantum),
            ("ladder_span", LadderRadii[0] / LadderRadii[^1]),
        };

    /// <summary>Documented SM mass ratios to test against.</summary>
    public static (string Name, double Ratio)[] SmMassRatios()
        => new[]
        {
            ("t/H", MTopGeV / MHGeV),
            ("H/Z", MHGeV / MZGeV),
            ("Z/W", MZGeV / MWGeV),
            ("t/W", MTopGeV / MWGeV),
            ("tau/mu", MTau / MMuon),
            ("mu/e", MMuon / MElectron),
            ("tau/e", MTau / MElectron),
        };

    // ── 1. Mass-spectrum matching ────────────────────────────────────────────────

    /// <summary>
    /// Best SM-ratio match for a given network ratio: (smName, smRatio, relativeDeviation). Deviation is
    /// |sm/net - 1|.
    /// </summary>
    public static (string SmName, double SmRatio, double Deviation) BestMassMatch(double networkRatio)
    {
        (string, double, double) best = ("", 0, double.MaxValue);
        foreach (var sm in SmMassRatios())
        {
            double dev = Math.Abs(sm.Ratio / networkRatio - 1.0);
            if (dev < best.Item3) best = (sm.Name, sm.Ratio, dev);
        }
        return best;
    }

    /// <summary>
    /// Number of network characteristic ratios that reproduce a known SM mass ratio within the tolerance
    /// (relative deviation &lt; tolerance).
    /// </summary>
    public static int MassMatchCount(double tolerance = 0.10)
    {
        int count = 0;
        foreach (var (_, r) in NetworkCharacteristicRatios())
            if (BestMassMatch(r).Deviation < tolerance) count++;
        return count;
    }

    /// <summary>Best overall SM-ratio match across all network ratios (lowest deviation).</summary>
    public static (string NetName, double NetRatio, string SmName, double SmRatio, double Deviation)
        BestOverallMatch()
    {
        (string, double, string, double, double) best = ("", 0, "", 0, double.MaxValue);
        foreach (var (n, r) in NetworkCharacteristicRatios())
        {
            var m = BestMassMatch(r);
            if (m.Deviation < best.Item5) best = (n, r, m.SmName, m.SmRatio, m.Deviation);
        }
        return best;
    }

    // ── 2. Resonance spacing ─────────────────────────────────────────────────────

    /// <summary>Ladder spacings (radius drops between consecutive rungs).</summary>
    public static double[] LadderSpacings()
    {
        var spacings = new double[LadderRadii.Length - 1];
        for (int i = 0; i < spacings.Length; i++)
            spacings[i] = Math.Abs(LadderRadii[i + 1] - LadderRadii[i]);
        return spacings;
    }

    /// <summary>
    /// Uniformity of the resonance spacing: relative standard deviation of the spacings (0 = perfectly
    /// harmonic/equal spacing). Uniform spacing = harmonic-like resonance ladder.
    /// </summary>
    public static double SpacingUniformity()
    {
        var s = LadderSpacings();
        double mean = s.Average();
        if (mean <= 0) return 1.0;
        double variance = s.Average(x => (x - mean) * (x - mean));
        return Math.Sqrt(variance) / mean;
    }

    /// <summary>Is the resonance spacing uniform (relative std &lt; 0.3)?</summary>
    public static bool UniformResonanceSpacing() => SpacingUniformity() < 0.3;

    // ── 3. Threshold energies ────────────────────────────────────────────────────

    /// <summary>Discrete energy thresholds (dimensionless ceiling units, QG127/128).</summary>
    public static double[] ThresholdEnergies()
        => HighEnergySectorSignatures.EnergyThresholds().Thresholds;

    /// <summary>Threshold span (max threshold / min threshold).</summary>
    public static double ThresholdSpan()
    {
        var t = ThresholdEnergies();
        return t.Length == 0 ? 1.0 : t[^1] / t[0];
    }

    // ── 4. Collider accessibility ────────────────────────────────────────────────

    /// <summary>
    /// Collider accessibility: ratio of the energy range needed to reach the highest network sector
    /// (highest ceiling / baseline ceiling = 8.0/1.0) to the approximate collider scale span. A value &lt; 1
    /// means all network sectors lie within a NARROW energy window (reachable at modest collider reach).
    /// </summary>
    public static double AccessibilityRatio()
        => HighEnergySectorStability.HighCeiling / HighEnergySectorStability.BaselineCeiling
            / ColliderScaleSpan;

    /// <summary>Are all network sectors within a narrow collider window (ratio &lt; 0.5)?</summary>
    public static bool NarrowColliderWindow() => AccessibilityRatio() < 0.5;

    // ── 5. Scaling laws ──────────────────────────────────────────────────────────

    /// <summary>
    /// Scaling law of the ladder: the radius ladder is ARITHMETIC (constant spacing 1.0). If log(mass) ∝
    /// radius, masses would be GEOMETRIC. Test whether the 12-rung ladder can host the lepton hierarchy:
    /// the muon/electron ratio (207) would need log-spacing ln(207)≈5.33 between two rungs — the FULL ladder
    /// only spans ln(2.889)≈1.06 in log-radius. Returns the maximum lepton ratio the ladder could host.
    /// </summary>
    public static double HostableLeptonRatio()
        => LadderRadii[0] / LadderRadii[^1];   // linear calibration: max mass ratio = radius span

    /// <summary>
    /// Can the ladder host the full lepton hierarchy? False if mu/e (207) exceeds the hostable ratio
    /// (2.889) — a linear calibration cannot reach the observed lepton hierarchy.
    /// </summary>
    public static bool CanHostLeptonHierarchy() => HostableLeptonRatio() >= MMuon / MElectron;

    // ── Calibration score & classification ───────────────────────────────────────

    /// <summary>
    /// Calibration score (0..5):
    /// 1. a network ratio reproduces an SM mass ratio within 10% (mass-spectrum matching);
    /// 2. the resonance spacing is uniform (harmonic-like);
    /// 3. at least 3 discrete energy thresholds exist;
    /// 4. all sectors are within a narrow collider window;
    /// 5. the ladder can host the full lepton hierarchy (mu/e).
    /// </summary>
    public static int CalibrationScore()
    {
        int score = 0;
        if (MassMatchCount(0.10) >= 1) score++;
        if (UniformResonanceSpacing()) score++;
        if (ThresholdEnergies().Length >= 3) score++;
        if (NarrowColliderWindow()) score++;
        if (CanHostLeptonHierarchy()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO MAPPING           — the ladder carries no structure corresponding to known masses/scales (no
    ///                          ratio match, no thresholds, no accessibility);
    ///   PARTIAL MAPPING      — some correspondences hold (one ratio match, uniform spacing, thresholds,
    ///                          accessibility) but the ladder CANNOT reproduce the full mass hierarchy (the
    ///                          lepton hierarchy exceeds the hostable span) — a calibration exists for the
    ///                          electroweak ratios but not the generation hierarchy;
    ///   PHYSICAL CALIBRATION — the ladder reproduces known masses/scales across the hierarchy (ratio
    ///                          matches AND the ladder can host the lepton hierarchy).
    /// </summary>
    public static string Classify()
    {
        int score = CalibrationScore();
        if (score <= 2) return "NO MAPPING";
        if (score == 5) return "PHYSICAL CALIBRATION";
        return "PARTIAL MAPPING";
    }
}
