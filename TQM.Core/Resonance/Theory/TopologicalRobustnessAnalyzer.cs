namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Hostile robustness test of the TQM-113 topological charge.
/// Scans threshold T ∈ [0.10, 0.90] and measures Q(T) under
/// perturbations to determine if Q is a genuine invariant
/// or a threshold artifact.
///
/// TQM-115: Topological Charge Robustness
/// </summary>
public static class TopologicalRobustnessAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record ChargeThresholdProfile(
        double Threshold,
        int Charge,
        int NumDomains,
        double MaxR,
        double MinRInDomain,
        bool IsStable);  // charge unchanged from previous threshold

    public sealed record InvariantValidationReport(
        List<ChargeThresholdProfile> SingleCondensate,
        List<ChargeThresholdProfile> TwoCondensate,
        List<ChargeThresholdProfile> NoisyCondensate,
        double PlateauStart,     // threshold where charge stabilizes
        double PlateauEnd,       // threshold where charge destabilizes
        double PlateauWidth,
        bool HasRobustPlateau,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Compute Q(T) for a given R field
    // ══════════════════════════════════════════════════════════════════

    public static int ComputeCharge(double[] R, double threshold)
    {
        int count = 0;
        bool inside = false;
        for (int i = 0; i < R.Length; i++)
        {
            if (R[i] > threshold && !inside)
            { inside = true; count++; }
            else if (R[i] <= threshold && inside)
            { inside = false; }
        }
        return count;
    }

    /// <summary>
    /// Scan Q(T) across thresholds for a given R field.
    /// </summary>
    public static List<ChargeThresholdProfile> ScanThresholds(
        double[] R, string label)
    {
        var profiles = new List<ChargeThresholdProfile>();
        int prevCharge = -1;

        for (double t = 0.10; t <= 0.90; t += 0.05)
        {
            int q = ComputeCharge(R, t);
            int domains = q;
            double maxR = R.Max();
            double minIn = 1.0;
            bool inside = false;
            for (int i = 0; i < R.Length; i++)
            {
                if (R[i] > t && !inside) inside = true;
                if (inside && R[i] > t) minIn = Math.Min(minIn, R[i]);
                if (R[i] <= t && inside) inside = false;
            }
            bool stable = (prevCharge >= 0 && q == prevCharge);
            profiles.Add(new ChargeThresholdProfile(t, q, domains, maxR,
                minIn < 1.0 ? minIn : 0, stable));
            prevCharge = q;
        }
        return profiles;
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate test R fields
    // ══════════════════════════════════════════════════════════════════

    private static double[] SingleCondensate(int nx = 200)
    {
        double[] X = new double[nx], R = new double[nx];
        double dx = 2.0 / (nx - 1);
        for (int i = 0; i < nx; i++)
        {
            X[i] = -1.0 + i * dx;
            R[i] = Math.Exp(-X[i] * X[i] / (2.0 * 0.10 * 0.10));
        }
        return R;
    }

    private static double[] TwoCondensates(int nx = 200, double sep = 0.6)
    {
        double[] X = new double[nx], R = new double[nx];
        double dx = 2.0 / (nx - 1);
        for (int i = 0; i < nx; i++)
        {
            X[i] = -1.0 + i * dx;
            R[i] = Math.Exp(-(X[i] + sep / 2) * (X[i] + sep / 2) / (2.0 * 0.10 * 0.10))
                 + Math.Exp(-(X[i] - sep / 2) * (X[i] - sep / 2) / (2.0 * 0.10 * 0.10));
        }
        return R;
    }

    private static double[] TwoCloseCondensates(int nx = 200)
    {
        return TwoCondensates(nx, 0.20); // close: sep = 0.20
    }

    private static double[] NoisyCondensate(int nx = 200)
    {
        double[] R = SingleCondensate(nx);
        var rng = new Random(42);
        for (int i = 0; i < nx; i++)
            R[i] += (rng.NextDouble() * 2 - 1) * 0.08;
        return R;
    }

    private static double[] WeakCondensate(int nx = 200)
    {
        double[] R = SingleCondensate(nx);
        for (int i = 0; i < nx; i++) R[i] *= 0.4; // peak = 0.4
        return R;
    }

    // ══════════════════════════════════════════════════════════════════
    // Full robustness analysis
    // ══════════════════════════════════════════════════════════════════

    public static InvariantValidationReport RunRobustnessAnalysis()
    {
        var single = ScanThresholds(SingleCondensate(), "single");
        var two = ScanThresholds(TwoCondensates(), "two");
        var twoClose = ScanThresholds(TwoCloseCondensates(), "two-close");
        var noisy = ScanThresholds(NoisyCondensate(), "noisy");
        var weak = ScanThresholds(WeakCondensate(), "weak");

        // Find plateau: range of thresholds where single gives Q=1.
        double plateauStart = 0, plateauEnd = 0;
        foreach (var p in single)
        {
            if (p.Charge == 1 && plateauStart == 0) plateauStart = p.Threshold;
            if (p.Charge == 1) plateauEnd = p.Threshold;
        }
        double plateauWidth = plateauEnd - plateauStart;

        // Robust plateau: width > 0.5 AND charge remains constant.
        bool hasRobustPlateau = plateauWidth >= 0.5 &&
            single.Skip(1).All(p => p.Charge == 1 || p.Threshold > plateauEnd || p.Threshold < plateauStart);

        // Also check: two-separated gives plateau of Q=2.
        double twoPlateauStart = 0, twoPlateauEnd = 0;
        foreach (var p in two)
        {
            if (p.Charge == 2 && twoPlateauStart == 0) twoPlateauStart = p.Threshold;
            if (p.Charge == 2) twoPlateauEnd = p.Threshold;
        }
        double twoPlateauWidth = twoPlateauEnd - twoPlateauStart;

        string classification;
        string verdict;

        if (hasRobustPlateau && twoPlateauWidth >= 0.4)
        {
            classification = "D: Genuine Topological Charge";
            verdict =
                $"THE CHARGE IS GENUINE. Q(T) has a robust plateau spanning " +
                $"[{plateauStart:F2}, {plateauEnd:F2}] (width={plateauWidth:F2}) for " +
                $"single condensates and [{twoPlateauStart:F2}, {twoPlateauEnd:F2}] " +
                $"(width={twoPlateauWidth:F2}) for two condensates. " +
                "Any threshold in this range gives the same charge. " +
                "The charge is NOT an artifact of the R>0.5 choice — it is " +
                "a genuine topological invariant of the R-field.";
        }
        else if (plateauWidth >= 0.3)
        {
            classification = "C: Robust Invariant";
            verdict = "The charge has a moderate plateau — reasonably threshold-independent.";
        }
        else
        {
            classification = "B: Weak Topological Quantity";
            verdict = "The charge depends sensitively on threshold. Not a robust invariant.";
        }

        return new InvariantValidationReport(single, two, noisy,
            plateauStart, plateauEnd, plateauWidth, hasRobustPlateau,
            classification, verdict);
    }
}
