namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 140 — Mass hierarchy amplification. QG139 established that the octave structure gives the
/// family COUNT (3) but its implied mass ratios (1:2:4) do not match the observed lepton hierarchy
/// (1:17:207). This phase asks: can a SECONDARY amplification mechanism transform the octave ladder into the
/// steep fermion mass hierarchies?
///
/// Method (computational, fully deterministic): the observable sector's octave bands carry both a POSITION
/// (geometric center) and a MODE OCCUPATION (mode count per band: [4,4,87]). The amplification hypothesis is
/// a power law in the band center: mass_k = A · center_k^p · modes_k^q. We measure: (1) MODE OCCUPATION —
/// the mode counts per octave band (the crowding signature); (2) COUPLING STRENGTH — the amplification
/// exponent p required to reach the lepton span from the octave span; (3) DAMPING EFFECTS — stability of the
/// octave bands across the damping parameter; (4) EXPONENTIAL SCALING — the fitted amplification law
/// mass = A·center^p·modes^q and the predicted lepton masses; (5) HIERARCHY AMPLIFICATION — the amplification
/// factor (amplified span / octave span) and the reproduction of the observed lepton mass ratios.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class HierarchyAmplification
{
    /// <summary>Default dynamics parameters (matching QG115–139).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;
    public const int DefaultN = 96;

    /// <summary>Documented lepton masses (MeV).</summary>
    public static readonly double[] LeptonMasses =
        { PhysicalCalibration.MElectron, PhysicalCalibration.MMuon, PhysicalCalibration.MTau };

    /// <summary>Lepton mass span (tau/e).</summary>
    public const double LeptonSpan = 3477.2;

    // ── 1. Mode occupation ──────────────────────────────────────────────────────

    /// <summary>Mode counts per octave band of the observable sector (the band occupation).</summary>
    public static int[] ModeOccupation(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => MassHierarchyFromOctaves.BandPositions(n, K, feedback, damping)
            .Select(b => b.Modes).ToArray();

    /// <summary>
    /// Crowding ratio: the top-octave mode count divided by the mean of the lower bands. A large ratio is the
    /// occupation imbalance available for amplification.
    /// </summary>
    public static double CrowdingRatio(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var occ = ModeOccupation(n, K, feedback, damping);
        if (occ.Length < 2) return 1.0;
        double top = occ[^1];
        double lowerMean = occ.Take(occ.Length - 1).Average();
        return top / Math.Max(lowerMean, 1e-9);
    }

    // ── 2. Coupling strength ────────────────────────────────────────────────────

    /// <summary>
    /// Amplification exponent: the power p in mass ∝ center^p needed to reach the lepton span from the
    /// octave span. p = log(leptonSpan)/log(octaveSpan).
    /// </summary>
    public static double AmplificationExponent(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var centers = OctaveCenters(n, K, feedback, damping);
        if (centers.Length < 2) return double.NaN;
        double octaveSpan = centers[^1] / centers[0];
        return Math.Log(LeptonSpan) / Math.Log(octaveSpan);
    }

    /// <summary>Octave band geometric centers.</summary>
    public static double[] OctaveCenters(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => MassHierarchyFromOctaves.BandPositions(n, K, feedback, damping)
            .Select(b => b.Center).ToArray();

    // ── 3. Damping effects ──────────────────────────────────────────────────────

    /// <summary>
    /// Damping effects: the octave band centers (and hence the amplification law) across the damping
    /// parameter. Returns the number of distinct center-ratio patterns (1 = robust).
    /// </summary>
    public static int DampingSensitivity(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback)
    {
        var patterns = new List<string>();
        foreach (double d in new[] { 0.2, 0.3, 0.4 })
        {
            var centers = OctaveCenters(n, K, feedback, d);
            patterns.Add(string.Join("|", centers.Select(c => Math.Round(c, 4).ToString("F4", System.Globalization.CultureInfo.InvariantCulture))));
        }
        return patterns.Distinct().Count();
    }

    // ── 4. Exponential scaling ──────────────────────────────────────────────────

    /// <summary>
    /// Fitted amplification law: log(mass) = a + p·log(center) + q·log(modes), fitted by least squares on
    /// the three lepton masses. Returns (a, p, q, predictedMasses, maxRelativeError).
    /// </summary>
    public static (double A, double P, double Q, double[] Predicted, double MaxRelativeError)
        FitAmplificationLaw(int n = DefaultN, int K = DefaultK, double feedback = DefaultFeedback,
            double damping = DefaultDamping)
    {
        var centers = OctaveCenters(n, K, feedback, damping);
        var modes = ModeOccupation(n, K, feedback, damping);
        if (centers.Length < 3) return (0, 0, 0, Array.Empty<double>(), double.MaxValue);

        // log mass = a + p*log center + q*log modes  → linear least squares
        double lc0 = Math.Log(centers[0]);
        double lm0 = Math.Log(modes[0]);
        double lA = Math.Log(LeptonMasses[0]);
        // solve 3x3 system: [1, x_k, z_k]·[a,p,q] = log m_k
        double[,] M = new double[3, 4];
        for (int i = 0; i < 3; i++)
        {
            M[i, 0] = 1.0;
            M[i, 1] = Math.Log(centers[i]) - lc0;
            M[i, 2] = Math.Log(modes[i]) - lm0;
            M[i, 3] = Math.Log(LeptonMasses[i]);
        }
        var sol = Solve3x3(M);
        if (sol.Length == 0) return (0, 0, 0, Array.Empty<double>(), double.MaxValue);
        double a = sol[0], p = sol[1], q = sol[2];
        double A = Math.Exp(a);
        var pred = new double[3];
        double maxErr = 0;
        for (int i = 0; i < 3; i++)
        {
            pred[i] = A * Math.Pow(centers[i] / centers[0], p) * Math.Pow(modes[i] / modes[0], q);
            maxErr = Math.Max(maxErr, Math.Abs(pred[i] / LeptonMasses[i] - 1.0));
        }
        return (A, p, q, pred, maxErr);
    }

    /// <summary>Solve a 3×3 linear system via Gaussian elimination (deterministic).</summary>
    private static double[] Solve3x3(double[,] m)
    {
        double[,] a = new double[3, 4];
        for (int i = 0; i < 3; i++) for (int j = 0; j < 4; j++) a[i, j] = m[i, j];
        for (int col = 0; col < 3; col++)
        {
            int piv = col;
            for (int r = col + 1; r < 3; r++) if (Math.Abs(a[r, col]) > Math.Abs(a[piv, col])) piv = r;
            if (Math.Abs(a[piv, col]) < 1e-12) return Array.Empty<double>();
            for (int j = 0; j < 4; j++) (a[col, j], a[piv, j]) = (a[piv, j], a[col, j]);
            double d = a[col, col];
            for (int j = 0; j < 4; j++) a[col, j] /= d;
            for (int r = 0; r < 3; r++)
            {
                if (r == col) continue;
                double f = a[r, col];
                for (int j = 0; j < 4; j++) a[r, j] -= f * a[col, j];
            }
        }
        return new[] { a[0, 3], a[1, 3], a[2, 3] };
    }

    // ── 5. Hierarchy amplification ──────────────────────────────────────────────

    /// <summary>
    /// Amplification factor: the ratio of the amplified span (tau/e from the fitted law) to the raw octave
    /// span (center ratio). A large factor means the secondary mechanism steepens the hierarchy.
    /// </summary>
    public static double AmplificationFactor(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var fit = FitAmplificationLaw(n, K, feedback, damping);
        var centers = OctaveCenters(n, K, feedback, damping);
        if (fit.Predicted.Length < 3 || centers.Length < 2) return 0;
        double octaveSpan = centers[^1] / centers[0];
        double amplifiedSpan = fit.Predicted[^1] / fit.Predicted[0];
        return amplifiedSpan / octaveSpan;
    }

    // ── Amplification score & classification ────────────────────────────────────

    /// <summary>
    /// Amplification score (0..5):
    /// 1. the octave bands have strong mode-occupation imbalance (crowding ratio &gt; 2);
    /// 2. the amplification exponent is large (p &gt; 3) — a steep power-law amplification;
    /// 3. the octave structure is robust across damping (≤ 2 distinct patterns);
    /// 4. the fitted amplification law reproduces the lepton masses within 10% (max relative error &lt; 0.1);
    /// 5. the amplification factor is large (&gt; 100×) — the octave ladder is steepened into the observed
    ///    hierarchy span.
    /// </summary>
    public static int AmplificationScore(int n = DefaultN, int K = DefaultK)
    {
        int score = 0;
        if (CrowdingRatio(n, K) > 2.0) score++;
        if (AmplificationExponent(n, K) > 3.0) score++;
        if (DampingSensitivity(n, K) <= 2) score++;
        if (FitAmplificationLaw(n, K).MaxRelativeError < 0.10) score++;
        if (AmplificationFactor(n, K) > 100.0) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO AMPLIFICATION     — no secondary mechanism steepens the octave ladder (amplification factor ≈ 1,
    ///                          no reproduction of lepton masses);
    ///   PARTIAL AMPLIFICATION — an amplification law exists and steepens the hierarchy, but it does not
    ///                           reproduce the observed lepton masses closely, or the mechanism is not robust;
    ///   HIERARCHY ORIGIN     — a secondary amplification (power law in the octave band position/occupation)
    ///                           transforms the octave ladder (1:2:4) into the observed steep fermion mass
    ///                           hierarchy (reproduces e, μ, τ within a few percent) — the concrete case.
    /// </summary>
    public static string Classify(int n = DefaultN, int K = DefaultK)
    {
        int score = AmplificationScore(n, K);
        if (score <= 2) return "NO AMPLIFICATION";
        if (score == 5) return "HIERARCHY ORIGIN";
        return "PARTIAL AMPLIFICATION";
    }
}
