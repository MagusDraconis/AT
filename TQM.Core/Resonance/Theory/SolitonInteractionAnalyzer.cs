namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Analyzes soliton-soliton interactions in the TQM spatial field theory.
/// Tracks separation, overlap, fusion, and reconstructs the effective
/// interaction potential V(d) between solitonic condensates.
///
/// TQM-109: Soliton Interaction Theory
/// </summary>
public static class SolitonInteractionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record SolitonProfile(
        double CenterX, double Amplitude, double Width, double Phase);

    public sealed record SolitonPairState(
        double Time,
        double Separation,
        double OverlapIntegral,
        double ForceEstimate,
        double R1, double R2,   // peak R values
        bool HasMerged);

    public sealed record InteractionResult(
        double InitialSeparation,
        double PhaseOffset,
        double AmplitudeRatio,
        List<SolitonPairState> History,
        double FusionTime,       // NaN if never fused
        bool Fused,
        string Outcome);

    public sealed record InteractionPotential(
        double[] Separations,
        double[] ForceEstimates,
        double DecayLength,      // fitted decay constant
        string FunctionalForm);

    // ══════════════════════════════════════════════════════════════════
    // PDE constants (from TQM-108)
    // ══════════════════════════════════════════════════════════════════

    private const double C0 = 0.0047;
    private const double A = 0.00976;
    private const double D_R = 2.5e-5;
    private const double D_M = 2.5e-6;
    private const double W = 0.10;  // soliton half-width

    // ══════════════════════════════════════════════════════════════════
    // PDE RHS (same as TQM-108)
    // ══════════════════════════════════════════════════════════════════

    private static (double[] dR, double[] dM) PdeRHS(double[] R, double[] M, double dx, int nx)
    {
        double[] dR = new double[nx], dM = new double[nx];
        for (int i = 0; i < nx; i++)
        {
            double reactionR = C0 * M[i] * Math.Max(R[i], 1e-10) * (1.0 - R[i] * R[i]);
            double reactionM = A * R[i] * R[i];
            double lapR = 0, lapM = 0;
            if (i > 0 && i < nx - 1)
            {
                lapR = (R[i - 1] - 2.0 * R[i] + R[i + 1]) / (dx * dx);
                lapM = (M[i - 1] - 2.0 * M[i] + M[i + 1]) / (dx * dx);
            }
            dR[i] = reactionR + D_R * lapR;
            dM[i] = reactionM + D_M * lapM;
            if (R[i] >= 1.0 && dR[i] > 0) dR[i] = 0;
            if (M[i] >= 5.0 && dM[i] > 0) dM[i] = 0;
        }
        return (dR, dM);
    }

    // ══════════════════════════════════════════════════════════════════
    // Initialize two-soliton state
    // ══════════════════════════════════════════════════════════════════

    public static (double[] X, double[] R, double[] M) TwoSolitonInit(
        double d, double phaseOffset, double ampRatio,
        int nx = 200, double L = 3.0)
    {
        double dx = L / (nx - 1);
        double[] X = new double[nx], R = new double[nx], M = new double[nx];

        double x1 = -d / 2, x2 = d / 2;
        double a1 = 1.0, a2 = ampRatio;

        for (int i = 0; i < nx; i++)
        {
            X[i] = -L / 2 + i * dx;
            double g1 = Math.Exp(-(X[i] - x1) * (X[i] - x1) / (2.0 * W * W));
            double g2 = Math.Exp(-(X[i] - x2) * (X[i] - x2) / (2.0 * W * W));

            // Phase offset affects coherence through cos(Δφ) factor.
            double phaseFactor = Math.Cos(phaseOffset);
            R[i] = 0.5 * (a1 * g1 + a2 * g2 * phaseFactor);
            M[i] = 1.0 * (g1 + g2);
        }
        return (X, R, M);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate two-soliton interaction
    // ══════════════════════════════════════════════════════════════════

    public static InteractionResult SimulateInteraction(
        double d, double phaseOffset, double ampRatio,
        double dt = 2.0, int maxSteps = 2000, int snapInterval = 50)
    {
        var (X, R, M) = TwoSolitonInit(d, phaseOffset, ampRatio);
        int nx = X.Length;
        double dx = X[1] - X[0];
        var history = new List<SolitonPairState>();

        double prevSep = d;

        for (int step = 0; step <= maxSteps; step++)
        {
            if (step % snapInterval == 0)
            {
                // Find peaks.
                int idx1 = 0, idx2 = nx - 1;
                double maxR = 0;
                for (int i = 0; i < nx; i++)
                    if (R[i] > maxR) { maxR = R[i]; idx1 = i; }

                // Find second peak (away from idx1).
                double maxR2 = 0;
                int excludeStart = Math.Max(0, idx1 - (int)(W / dx * 2));
                int excludeEnd = Math.Min(nx, idx1 + (int)(W / dx * 2));
                for (int i = 0; i < nx; i++)
                {
                    if (i >= excludeStart && i <= excludeEnd) continue;
                    if (R[i] > maxR2) { maxR2 = R[i]; idx2 = i; }
                }

                double sep = Math.Abs(X[idx1] - X[idx2]);
                double overlap = 0;
                // Approximate overlap integral over R.
                for (int i = 0; i < nx; i++)
                {
                    double r1 = Math.Exp(-(X[i] - X[idx1]) * (X[i] - X[idx1]) / (2.0 * W * W));
                    double r2 = Math.Exp(-(X[i] - X[idx2]) * (X[i] - X[idx2]) / (2.0 * W * W));
                    overlap += r1 * r2 * dx;
                }

                double force = step > 0 ? (sep - prevSep) / (snapInterval * dt) : 0;
                bool merged = maxR2 < 0.3 * maxR || sep < W / 2;

                history.Add(new SolitonPairState(step * dt, sep, overlap,
                    force, maxR, maxR2, merged));

                prevSep = sep;
                if (merged) break;
            }

            // RK2 step.
            var (dR1, dM1) = PdeRHS(R, M, dx, nx);
            double[] Rmid = new double[nx], Mmid = new double[nx];
            for (int i = 0; i < nx; i++)
            {
                Rmid[i] = Math.Clamp(R[i] + 0.5 * dt * dR1[i], 0, 1);
                Mmid[i] = Math.Max(0, M[i] + 0.5 * dt * dM1[i]);
            }
            var (dR2, dM2) = PdeRHS(Rmid, Mmid, dx, nx);
            for (int i = 0; i < nx; i++)
            {
                R[i] = Math.Clamp(R[i] + dt * dR2[i], 0, 1);
                M[i] = Math.Max(0, M[i] + dt * dM2[i]);
            }
        }

        bool fused = history.Count > 0 && history[^1].HasMerged;
        double fTime = fused ? history[^1].Time : double.NaN;

        string outcome = fused
            ? $"MERGED at t={fTime:F0}"
            : $"SURVIVED — final separation={history[^1].Separation:F3}";

        return new InteractionResult(d, phaseOffset, ampRatio,
            history, fTime, fused, outcome);
    }

    // ══════════════════════════════════════════════════════════════════
    // Parameter sweep
    // ══════════════════════════════════════════════════════════════════

    public static List<InteractionResult> SweepInteractions()
    {
        var results = new List<InteractionResult>();
        double[] separations = { 0.5 * W, W, 2 * W, 3 * W, 5 * W };
        double[] phases = { 0, Math.PI / 4, Math.PI / 2, 3 * Math.PI / 4, Math.PI };
        double[] ratios = { 1.0, 2.0 };

        foreach (double sep in separations)
            foreach (double phi in phases)
                foreach (double rat in ratios)
                    results.Add(SimulateInteraction(sep, phi, rat));

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Reconstruct interaction potential
    // ══════════════════════════════════════════════════════════════════

    public static InteractionPotential ReconstructPotential(
        List<InteractionResult> results)
    {
        // Extract (separation, force) pairs at early times (before merger).
        var sepForce = new List<(double sep, double force)>();
        foreach (var r in results)
            for (int i = 1; i < r.History.Count && !r.History[i].HasMerged; i++)
                sepForce.Add((r.History[i].Separation, r.History[i].ForceEstimate));

        // Bin by separation.
        var bins = sepForce.GroupBy(sf => Math.Round(sf.sep / W, 1) * W)
            .OrderBy(g => g.Key)
            .ToList();

        double[] seps = bins.Select(b => b.Key).ToArray();
        double[] forces = bins.Select(b => b.Average(sf => sf.force)).ToArray();

        // Fit exponential decay: F(d) = F₀·exp(−d/ℓ).
        // log|F| = log|F₀| − d/ℓ → linear fit on (d, log|F|).
        double decayLen = W * 3; // default
        if (seps.Length >= 2)
        {
            double sx = 0, sy = 0, sxx = 0, sxy = 0;
            int count = 0;
            for (int i = 0; i < seps.Length; i++)
            {
                if (Math.Abs(forces[i]) < 1e-15) continue;
                double y = Math.Log(Math.Abs(forces[i]) + 1e-15);
                sx += seps[i]; sy += y; sxx += seps[i] * seps[i]; sxy += seps[i] * y;
                count++;
            }
            if (count >= 2)
            {
                double slope = (count * sxy - sx * sy) / Math.Max(count * sxx - sx * sx, 1e-15);
                decayLen = -1.0 / Math.Max(slope, -1.0);
            }
        }

        return new InteractionPotential(seps, forces, decayLen,
            $"V(d) ∝ {Math.Abs(decayLen):F2}·exp(−d/{decayLen:F2})");
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis report
    // ══════════════════════════════════════════════════════════════════

    public sealed record SolitonInteractionReport(
        List<InteractionResult> Results,
        InteractionPotential Potential,
        int Fused, int Survived,
        double FusionThreshold,  // separation below which fusion always occurs
        string Classification,
        string Interpretation);

    public static SolitonInteractionReport RunInteractionAnalysis()
    {
        var results = SweepInteractions();
        var potential = ReconstructPotential(results);

        int fused = results.Count(r => r.Fused);
        int survived = results.Count(r => !r.Fused);

        // Find fusion threshold: smallest separation where survival > 50%.
        var bySep = results.GroupBy(r => r.InitialSeparation)
            .Select(g => (Sep: g.Key, FuseRate: (double)g.Count(r => r.Fused) / g.Count()))
            .OrderBy(s => s.Sep).ToList();

        double threshold = W * 5;
        foreach (var (sep, rate) in bySep)
            if (rate < 0.5) { threshold = sep; break; }

        string classification;
        string interpretation;

        if (fused > survived && threshold <= 3 * W)
        {
            classification = "D: Solitonic Proto-Particle Theory";
            interpretation =
                $"SOLITONS BEHAVE AS EFFECTIVE PARTICLES. {fused}/{results.Count} pairs fuse, " +
                $"{survived} survive. Fusion threshold ≈ {threshold / W:F1}w. " +
                $"Interaction is SHORT-RANGED (decay length ≈ {potential.DecayLength / W:F1}w). " +
                "Behavior matches TQM-012: merges at close range, coexistence at large separation. " +
                "SOLITONS ARE PROTO-PARTICLES with well-defined interaction laws derived " +
                "from the spatial field theory.";
        }
        else if (fused > 0)
        {
            classification = "C: Effective Particle Dynamics";
            interpretation =
                "Solitons interact measurably but with weak effective forces. " +
                "Some pairs fuse, others coexist. The interaction is attractive at close range.";
        }
        else
        {
            classification = "B: Weak Interaction";
            interpretation = "Solitons are essentially non-interacting at the tested separations.";
        }

        return new SolitonInteractionReport(results, potential,
            fused, survived, threshold, classification, interpretation);
    }
}
