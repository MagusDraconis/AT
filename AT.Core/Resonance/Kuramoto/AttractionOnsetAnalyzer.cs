using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines the precise moment attraction emerges during
/// synchronization by tracking per-timestep evolution.
///
/// AT-070: Onset of Attraction
/// </summary>
public static class AttractionOnsetAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Per-timestep snapshot of the full system state.
    /// </summary>
    public sealed record SynchronizationPhaseProfile(
        int Timestep,
        double R,              // Global order parameter
        double R_A,            // Group A order parameter
        double R_B,            // Group B order parameter
        double PhaseVariance,  // Circular variance
        double Separation,     // Distance between group centers
        double Velocity,       // |d(separation)/dt|
        double Acceleration,   // |d²(separation)/dt²|
        double AttractionScore,// Cumulative convergence fraction
        double LocalCoherence, // min(R_A, R_B)
        double SepFraction);   // separation / initial_separation

    /// <summary>
    /// Detected onset event.
    /// </summary>
    public sealed record OnsetEvent(
        int Timestep,
        double RAtOnset,
        double LocalCoherenceAtOnset,
        double PhaseVarianceAtOnset,
        double SeparationAtOnset,
        double SepFractionAtOnset,
        bool BeforeFullSync,
        double SyncLead);       // how many timesteps attraction leads sync

    /// <summary>
    /// Full phase diagram with onset analysis.
    /// </summary>
    public sealed record AttractionPhaseDiagram(
        List<SynchronizationPhaseProfile> Profiles,
        OnsetEvent? Onset,
        int SyncTimestep,       // first timestep where R > 0.8
        double InitialSeparation,
        double FinalSeparation,
        double TotalAttraction,
        string LawName,
        int Seed);

    /// <summary>
    /// Aggregate onset report across multiple runs.
    /// </summary>
    public sealed record OnsetReport(
        List<AttractionPhaseDiagram> Diagrams,
        double MeanOnsetR,
        double MeanOnsetLocalCoh,
        double MeanOnsetPhaseVar,
        double MeanOnsetSepFrac,
        double MeanSyncLead,
        double FractionBeforeSync,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Coupling laws for testing
    // ══════════════════════════════════════════════════════════════════

    private static readonly Dictionary<string, Func<double, double>> ForceLaws = new()
    {
        ["cos"]        = d => Math.Cos(d),
        ["cos²"]       = d => Math.Cos(d) * Math.Cos(d),
        ["exp(-|x|)"]  = d => Math.Exp(-Math.Abs(d)),
    };

    // ══════════════════════════════════════════════════════════════════
    // High-Resolution Run
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs a full spatial dynamics simulation with per-timestep
    /// tracking to detect the onset of attraction.
    /// </summary>
    public static AttractionPhaseDiagram RunHighResolution(
        string lawName, Func<double, double> forceFn,
        double k, double lambda, int nPerGroup, int seed,
        int totalIters = 3000, int recordEvery = 1,
        double posStep = 0.001)
    {
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Group A: center (0.3, 0.5), Group B: center (0.7, 0.5).
        for (int i = 0; i < nPerGroup; i++)
        {
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                (rng.NextDouble() - 0.5) * 0.05 + 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }
        for (int i = 0; i < nPerGroup; i++)
        {
            network.AddNode(new TemporalNode(nPerGroup + i, rng.NextDouble() * 2 * Math.PI,
                (rng.NextDouble() - 0.5) * 0.05 + 1.0)
            { X = Math.Clamp(0.7 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        double initSep = GroupSeparation(network, nPerGroup);
        double prevSep = initSep;
        double prevVel = 0;
        double minSep = initSep;

        var profiles = new List<SynchronizationPhaseProfile>();

        for (int iter = 0; iter <= totalIters; iter++)
        {
            // Phase update (standard Kuramoto).
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }

            // Position update.
            double[] nx = new double[n], ny = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx = 0, fy = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double dx = network.Nodes[j].X - network.Nodes[i].X;
                    double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                    double w = network.Matrix.GetCoupling(i, j);
                    double pd = TemporalSimulation.NormalizePhase(
                        network.Nodes[j].Phase - network.Nodes[i].Phase);
                    if (pd > Math.PI) pd -= 2 * Math.PI;
                    double force = forceFn(pd);
                    fx += w * force * dx / d;
                    fy += w * force * dy / d;
                }
                nx[i] = Math.Clamp(network.Nodes[i].X + posStep * fx, 0.01, 0.99);
                ny[i] = Math.Clamp(network.Nodes[i].Y + posStep * fy, 0.01, 0.99);
            }
            for (int i = 0; i < n; i++)
            { network.Nodes[i].X = nx[i]; network.Nodes[i].Y = ny[i]; }

            // Record at specified interval.
            if (iter % recordEvery == 0)
            {
                double sep = GroupSeparation(network, nPerGroup);
                double vel = Math.Abs(sep - prevSep);
                double accel = Math.Abs(vel - prevVel);
                double rA = GroupR(network, 0, nPerGroup);
                double rB = GroupR(network, nPerGroup, nPerGroup);
                double globalR = GlobalR(network);
                double pVar = 1.0 - globalR;
                double attrScore = 1.0 - sep / Math.Max(initSep, 1e-10);
                double localCoh = Math.Min(rA, rB);
                double sepFrac = sep / Math.Max(initSep, 1e-10);

                profiles.Add(new SynchronizationPhaseProfile(
                    iter, globalR, rA, rB, pVar, sep, vel, accel,
                    attrScore, localCoh, sepFrac));

                // Track minimum separation.
                if (sep < minSep) minSep = sep;

                prevSep = sep;
                prevVel = vel;
            }
        }

        // Detect onset.
        double finalSep = profiles[^1].Separation;
        var onset = DetectOnset(profiles, initSep);
        int syncStep = profiles.FirstOrDefault(p => p.R > 0.8)?.Timestep ?? -1;

        return new AttractionPhaseDiagram(profiles, onset, syncStep,
            initSep, finalSep, 1.0 - finalSep / Math.Max(initSep, 1e-10),
            lawName, seed);
    }

    // ══════════════════════════════════════════════════════════════════
    // Onset Detection
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Detects the first moment attraction becomes measurable.
    /// Uses a robust algorithm: finds the earliest timestep after
    /// which separation consistently decreases (sustained attraction).
    /// </summary>
    private static OnsetEvent? DetectOnset(
        List<SynchronizationPhaseProfile> profiles, double initSep)
    {
        if (profiles.Count < 10) return null;

        // Smooth separation using a 5-point moving average to reduce noise.
        var seps = profiles.Select(p => p.Separation).ToList();
        var smoothed = new List<double>();
        for (int i = 0; i < seps.Count; i++)
        {
            int start = Math.Max(0, i - 2), end = Math.Min(seps.Count - 1, i + 2);
            double sum = 0;
            for (int j = start; j <= end; j++) sum += seps[j];
            smoothed.Add(sum / (end - start + 1));
        }

        // Find the first index where separation drops below a threshold
        // AND continues decreasing in the next window.
        double threshold = initSep * 0.995; // 0.5% decrease from initial
        int windowSize = Math.Min(50, profiles.Count / 5);

        for (int i = 5; i < profiles.Count - windowSize; i++)
        {
            // Check if separation is below threshold.
            if (smoothed[i] >= threshold) continue;

            // Check sustained decrease: average over next window is lower.
            double avgNext = 0;
            for (int j = i; j < i + windowSize && j < smoothed.Count; j++)
                avgNext += smoothed[j];
            avgNext /= Math.Min(windowSize, smoothed.Count - i);

            // Also check average over previous window is higher.
            double avgPrev = 0;
            for (int j = Math.Max(0, i - windowSize); j < i; j++)
                avgPrev += smoothed[j];
            avgPrev /= Math.Min(windowSize, i);

            if (avgNext < avgPrev * 0.99)
            {
                // Found onset.
                var p = profiles[i];
                int syncStep = profiles.FirstOrDefault(pr => pr.R > 0.8)?.Timestep ?? int.MaxValue;
                bool beforeSync = syncStep > p.Timestep;
                double syncLead = beforeSync ? syncStep - p.Timestep : -(p.Timestep - syncStep);

                return new OnsetEvent(p.Timestep, p.R, p.LocalCoherence,
                    p.PhaseVariance, p.Separation, p.SepFraction, beforeSync, syncLead);
            }
        }

        // If no clear onset detected but separation did decrease,
        // use the first point where separation dropped 1%.
        for (int i = 1; i < profiles.Count; i++)
        {
            if (profiles[i].SepFraction < 0.99)
            {
                var p = profiles[i];
                int syncStep = profiles.FirstOrDefault(pr => pr.R > 0.8)?.Timestep ?? int.MaxValue;
                bool beforeSync = syncStep > p.Timestep;
                double syncLead = beforeSync ? syncStep - p.Timestep : -(p.Timestep - syncStep);
                return new OnsetEvent(p.Timestep, p.R, p.LocalCoherence,
                    p.PhaseVariance, p.Separation, p.SepFraction, beforeSync, syncLead);
            }
        }

        return null;
    }

    // ══════════════════════════════════════════════════════════════════
    // Aggregate Analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Analyzes onset timing across all diagrams.
    /// </summary>
    public static OnsetReport AnalyzeOnset(List<AttractionPhaseDiagram> diagrams)
    {
        var withOnset = diagrams.Where(d => d.Onset != null).ToList();
        if (withOnset.Count == 0)
            return new OnsetReport(diagrams, 0, 0, 0, 0, 0, 0, "A: No Onset Detected",
                "Attraction onset could not be reliably detected in any run.");

        double meanOnsetR = withOnset.Average(d => d.Onset!.RAtOnset);
        double meanOnsetLC = withOnset.Average(d => d.Onset!.LocalCoherenceAtOnset);
        double meanOnsetPV = withOnset.Average(d => d.Onset!.PhaseVarianceAtOnset);
        double meanOnsetSF = withOnset.Average(d => d.Onset!.SepFractionAtOnset);
        double meanSyncLead = withOnset.Average(d => d.Onset!.SyncLead);
        double fracBeforeSync = (double)withOnset.Count(d => d.Onset!.BeforeFullSync) / withOnset.Count;

        // Classification.
        string classification;
        if (fracBeforeSync > 0.8)
            classification = "D: Attraction Leads Synchronization";
        else if (fracBeforeSync > 0.5)
            classification = "C: Attraction Emerges During Sync";
        else if (fracBeforeSync > 0.2)
            classification = "B: Attraction Follows Sync";
        else
            classification = "A: Attraction Requires Full Sync";

        string interpretation = classification switch
        {
            "D: Attraction Leads Synchronization" =>
                $"Attraction begins at R≈{meanOnsetR:F3} (local coherence {meanOnsetLC:F3}), " +
                $"well before full synchronization (mean lead: {meanSyncLead:F0} timesteps). " +
                "This is the strongest evidence yet that attraction is a fundamental " +
                "dynamical phenomenon that does not require synchronization — it may " +
                "even DRIVE synchronization by bringing oscillators closer together.",
            "C: Attraction Emerges During Sync" =>
                $"Attraction begins at R≈{meanOnsetR:F3} during the synchronization " +
                $"process. Attraction and synchronization co-emerge as the system " +
                "self-organizes. Neither strictly causes the other — they are " +
                "coupled aspects of the same dynamical transition.",
            "B: Attraction Follows Sync" =>
                $"Attraction begins at R≈{meanOnsetR:F3}, after significant " +
                "synchronization has occurred. Synchronization appears to be a " +
                "prerequisite for spatial attraction.",
            _ => $"Attraction begins at R≈{meanOnsetR:F3}, only after full " +
                 "synchronization is achieved. Attraction is a consequence of sync."
        };

        return new OnsetReport(diagrams, meanOnsetR, meanOnsetLC, meanOnsetPV,
            meanOnsetSF, meanSyncLead, fracBeforeSync, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run batch analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs the full onset analysis across multiple coupling laws and seeds.
    /// </summary>
    public static (List<AttractionPhaseDiagram> Diagrams, OnsetReport Report)
    RunFullOnsetAnalysis(
        double k, double lambda, int nPerGroup, int seedsPerLaw,
        int totalIters, int baseSeed)
    {
        var diagrams = new List<AttractionPhaseDiagram>();
        int seedIdx = 0;

        foreach (var (name, fn) in ForceLaws)
        {
            for (int s = 0; s < seedsPerLaw; s++)
            {
                diagrams.Add(RunHighResolution(name, fn, k, lambda, nPerGroup,
                    baseSeed + seedIdx++ * 7919, totalIters));
            }
        }

        var report = AnalyzeOnset(diagrams);
        return (diagrams, report);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double GroupSeparation(TemporalNetwork net, int nPerGroup)
    {
        double cxA = 0, cyA = 0, cxB = 0, cyB = 0;
        for (int i = 0; i < nPerGroup; i++)
        { cxA += net.Nodes[i].X; cyA += net.Nodes[i].Y; }
        for (int i = 0; i < nPerGroup; i++)
        { cxB += net.Nodes[i + nPerGroup].X; cyB += net.Nodes[i + nPerGroup].Y; }
        cxA /= nPerGroup; cxB /= nPerGroup; cyA /= nPerGroup; cyB /= nPerGroup;
        return Math.Sqrt((cxA - cxB) * (cxA - cxB) + (cyA - cyB) * (cyA - cyB));
    }

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double ss = 0, sc = 0;
        for (int i = start; i < start + count; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / count;
    }

    private static double GlobalR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }
}
