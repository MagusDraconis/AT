namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Constructs charge phase diagrams from collective ensemble data.
/// Maps the (density × coupling) parameter space to collective
/// phases: Vacuum, Dilute Gas, Correlated Gas, Cluster, Percolating,
/// and Dense Matter.
///
/// TQM-123: Proto-Matter Collective Dynamics
/// </summary>
public static class ChargePhaseDiagram
{
    // ══════════════════════════════════════════════════════════════════
    // Build phase diagram from ensemble runs.
    // ══════════════════════════════════════════════════════════════════

    public static CollectiveStateProfile.ChargePhaseDiagram BuildPhaseDiagram(
        List<CollectiveStateProfile.ChargeEnsembleRun> runs,
        int nDensityBins = 8, int nCouplingBins = 8)
    {
        if (runs.Count == 0)
            return new CollectiveStateProfile.ChargePhaseDiagram(
                CollectiveStateProfile.GetKnownPhases(),
                new double[0, 0], new string[0, 0],
                Array.Empty<double>(), Array.Empty<double>(),
                -1, -1, "No data.");

        double minD = runs.Min(r => r.ChargeDensity);
        double maxD = runs.Max(r => r.ChargeDensity);
        double minK = runs.Min(r => r.K);
        double maxK = runs.Max(r => r.K);

        if (maxD - minD < 1e-10) maxD = minD + 1.0;
        if (maxK - minK < 1e-10) maxK = minK + 1.0;

        var densityAxis = new double[nDensityBins];
        var couplingAxis = new double[nCouplingBins];
        var qGrid = new double[nDensityBins, nCouplingBins];
        var phaseGrid = new string[nDensityBins, nCouplingBins];

        for (int d = 0; d < nDensityBins; d++)
            densityAxis[d] = minD + (maxD - minD) * (d + 0.5) / nDensityBins;

        for (int c = 0; c < nCouplingBins; c++)
            couplingAxis[c] = minK + (maxK - minK) * (c + 0.5) / nCouplingBins;

        // Assign runs to bins.
        var binned = new List<CollectiveStateProfile.ChargeEnsembleRun>[nDensityBins, nCouplingBins];
        for (int d = 0; d < nDensityBins; d++)
            for (int c = 0; c < nCouplingBins; c++)
                binned[d, c] = new List<CollectiveStateProfile.ChargeEnsembleRun>();

        foreach (var run in runs)
        {
            int dIdx = Math.Clamp(
                (int)((run.ChargeDensity - minD) / (maxD - minD) * nDensityBins), 0, nDensityBins - 1);
            int cIdx = Math.Clamp(
                (int)((run.K - minK) / (maxK - minK) * nCouplingBins), 0, nCouplingBins - 1);
            binned[dIdx, cIdx].Add(run);
        }

        // Determine phase per bin by majority vote.
        int critD = -1, critC = -1;
        for (int d = 0; d < nDensityBins; d++)
        {
            for (int c = 0; c < nCouplingBins; c++)
            {
                var bin = binned[d, c];
                if (bin.Count == 0)
                {
                    qGrid[d, c] = 0;
                    phaseGrid[d, c] = "NoData";
                    continue;
                }

                qGrid[d, c] = bin.Average(r => r.ChargeDensity);

                var phaseCounts = bin.GroupBy(r => r.PhaseClassification)
                    .ToDictionary(g => g.Key, g => g.Count());
                phaseGrid[d, c] = phaseCounts.OrderByDescending(kv => kv.Value).First().Key;

                // Detect critical transitions.
                if (phaseGrid[d, c] == "Percolating" && critD < 0) critD = d;
                if (phaseGrid[d, c] == "Cluster" && critC < 0) critC = c;
            }
        }

        string description = GeneratePhaseDiagramDescription(
            phaseGrid, densityAxis, couplingAxis, critD, critC);

        return new CollectiveStateProfile.ChargePhaseDiagram(
            CollectiveStateProfile.GetKnownPhases(),
            qGrid, phaseGrid, densityAxis, couplingAxis,
            critD, critC, description);
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate human-readable description.
    // ══════════════════════════════════════════════════════════════════

    private static string GeneratePhaseDiagramDescription(
        string[,] phaseGrid, double[] densityAxis, double[] couplingAxis,
        int critD, int critC)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CHARGE PHASE DIAGRAM");
        sb.AppendLine();

        sb.AppendLine("  Axes: Charge Density ρ_Q (vertical) × Coupling K (horizontal)");
        sb.AppendLine();

        // Summarize phases found.
        var allPhases = new HashSet<string>();
        foreach (var p in phaseGrid) allPhases.Add(p);
        sb.AppendLine($"  Phases identified: {string.Join(", ", allPhases.OrderBy(p => p))}");
        sb.AppendLine();

        if (critD >= 0)
            sb.AppendLine($"  Percolation threshold at ρ_Q ≈ {densityAxis[critD]:F3} (bin {critD}).");
        if (critC >= 0)
            sb.AppendLine($"  Gas→Cluster transition at K ≈ {couplingAxis[critC]:F1} (bin {critC}).");

        sb.AppendLine();
        sb.AppendLine("  PHASE BOUNDARIES:");
        sb.AppendLine("    — Vacuum ↔ Dilute Gas: ρ_Q threshold (first condensate appears)");
        sb.AppendLine("    — Dilute Gas ↔ Correlated Gas: K · λ² exceeds 1/N");
        sb.AppendLine("    — Correlated Gas ↔ Cluster: density exceeds percolation threshold on coupling graph");
        sb.AppendLine("    — Cluster ↔ Percolating: clusters merge into system-spanning structure");
        sb.AppendLine("    — Percolating ↔ Dense: ρ_Q → 1, global R → 1");

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Phase classification from parameters (used for new data points).
    // ══════════════════════════════════════════════════════════════════

    public static string ClassifyFromParameters(
        double chargeDensity, double K, double lambda, double meanSeparation, double corrLength)
    {
        if (chargeDensity < 0.01) return "Vacuum";
        if (chargeDensity < 0.03 && corrLength < 0.05) return "Dilute Gas";
        if (corrLength > 0.25 && chargeDensity > 0.2) return "Dense Matter";
        if (corrLength > 0.15 && chargeDensity > 0.1) return "Percolating Phase";
        if (chargeDensity > 0.06 && meanSeparation < 5 * lambda / 2) return "Cluster Phase";
        if (corrLength > 0.06) return "Correlated Gas";
        return "Dilute Gas";
    }
}
