namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Constructs the charge-wave phase diagram (density × coupling)
/// showing the transition from dilute gas to coherent wave medium.
///
/// TQM-127: Emergent Collective Charge Waves
/// </summary>
public static class ChargeWavePhaseDiagram
{
    public static ChargeWaveProfile.CoherenceTransition DetectTransition(
        List<ChargeWaveProfile.ChargeWaveRun> runs)
    {
        if (runs.Count < 5)
            return new ChargeWaveProfile.CoherenceTransition(
                false, 0, 0, 0, 0, "None", "Insufficient data.");

        // Sort by density and look for R_Q jump.
        var byDensity = runs.GroupBy(r =>
            Math.Round(r.ChargeDensity, 2))
            .OrderBy(g => g.Key)
            .Select(g => (Density: g.Key,
                          RQ: g.Average(r => r.R_Q),
                          CohLen: g.Average(r => r.CoherenceLength),
                          Count: g.Count()))
            .Where(x => x.Count >= 2)
            .ToList();

        if (byDensity.Count < 3)
            return new ChargeWaveProfile.CoherenceTransition(
                false, 0, 0, 0, 0, "Crossover", "Too few density bins.");

        // Find largest jump in R_Q.
        double maxJump = 0;
        double critDensity = 0;
        double critRQjump = 0;
        double critCohLen = 0;

        for (int i = 1; i < byDensity.Count; i++)
        {
            double jump = byDensity[i].RQ - byDensity[i - 1].RQ;
            if (jump > maxJump && byDensity[i].RQ > 0.3)
            {
                maxJump = jump;
                critDensity = (byDensity[i].Density + byDensity[i - 1].Density) / 2;
                critRQjump = jump;
                critCohLen = byDensity[i].CohLen;
            }
        }

        bool found = maxJump > 0.2;
        string type = maxJump > 0.5 ? "Continuous" : maxJump > 0.2 ? "Crossover" : "None";

        string scaling = found
            ? $"R_Q rises from ~{byDensity[0].RQ:F2} to ~{byDensity[^1].RQ:F2} " +
              $"across density [{byDensity[0].Density:F2}, {byDensity[^1].Density:F2}]. " +
              $"Jump ΔR_Q={maxJump:F2} at ρ_c≈{critDensity:F2}."
            : "No sharp transition. R_Q varies smoothly with density.";

        return new ChargeWaveProfile.CoherenceTransition(
            found, critDensity, 5.0, critRQjump, critCohLen, type, scaling);
    }

    public static ChargeWaveProfile.ChargeWavePhaseDiagram Build(
        List<ChargeWaveProfile.ChargeWaveRun> runs,
        int nDens = 6, int nCoup = 4)
    {
        if (runs.Count == 0)
            return new ChargeWaveProfile.ChargeWavePhaseDiagram(
                Array.Empty<double>(), Array.Empty<double>(),
                new double[0, 0], new string[0, 0],
                new ChargeWaveProfile.CoherenceTransition(false, 0, 0, 0, 0, "None", ""),
                "No data.");

        double minD = runs.Min(r => r.ChargeDensity);
        double maxD = runs.Max(r => r.ChargeDensity);
        double minK = runs.Min(r => r.K);
        double maxK = runs.Max(r => r.K);
        if (maxD - minD < 1e-10) maxD = minD + 1.0;
        if (maxK - minK < 1e-10) maxK = minK + 1.0;

        var dAxis = new double[nDens];
        var cAxis = new double[nCoup];
        var rqGrid = new double[nDens, nCoup];
        var regGrid = new string[nDens, nCoup];

        for (int d = 0; d < nDens; d++) dAxis[d] = minD + (maxD - minD) * (d + 0.5) / nDens;
        for (int c = 0; c < nCoup; c++) cAxis[c] = minK + (maxK - minK) * (c + 0.5) / nCoup;

        double dW = (maxD - minD) / nDens;
        double cW = (maxK - minK) / nCoup;

        for (int d = 0; d < nDens; d++)
            for (int c = 0; c < nCoup; c++)
            {
                var bin = runs.Where(r =>
                    Math.Abs(r.ChargeDensity - dAxis[d]) < dW &&
                    Math.Abs(r.K - cAxis[c]) < cW).ToList();
                if (bin.Count > 0)
                {
                    rqGrid[d, c] = bin.Average(r => r.R_Q);
                    var regimes = bin.GroupBy(r => r.Regime)
                        .ToDictionary(g => g.Key, g => g.Count());
                    regGrid[d, c] = regimes.OrderByDescending(kv => kv.Value).First().Key;
                }
                else { rqGrid[d, c] = 0; regGrid[d, c] = "NoData"; }
            }

        var transition = DetectTransition(runs);

        string desc = "CHARGE-WAVE PHASE DIAGRAM (density × coupling):\n" +
            $"  Phases: Dilute → Correlated → " +
            (transition.TransitionFound ? $"CoherentWave (ρ_c≈{transition.CriticalDensity:F2})" : "CoherentWave") +
            $"\n  R_Q range: [{rqGrid.Cast<double>().Min():F2}, {rqGrid.Cast<double>().Max():F2}]";

        return new ChargeWaveProfile.ChargeWavePhaseDiagram(
            dAxis, cAxis, rqGrid, regGrid, transition, desc);
    }
}
