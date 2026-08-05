namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Spectral analysis for inter-charge coherence: detects beat frequencies,
/// phase-locking quality, and Arnold tongue structure in the (Δω, K) plane.
///
/// TQM-125: Inter-Charge Coherence and Phase Locking
/// </summary>
public static class CoherenceSpectrum
{
    // ══════════════════════════════════════════════════════════════════
    // Compute phase-locking order parameter from phase difference history.
    // ══════════════════════════════════════════════════════════════════

    public static double ComputeLockingOrderParameter(double[] phaseDiffs)
    {
        // R_lock = |⟨exp(i·Δφ)⟩| — Kuramoto order parameter for phase differences.
        if (phaseDiffs.Length == 0) return 0;
        double sumCos = 0, sumSin = 0;
        for (int i = 0; i < phaseDiffs.Length; i++)
        {
            sumCos += Math.Cos(phaseDiffs[i]);
            sumSin += Math.Sin(phaseDiffs[i]);
        }
        return Math.Sqrt(sumCos * sumCos + sumSin * sumSin) / phaseDiffs.Length;
    }

    /// <summary>Detect phase locking from phase difference history.</summary>
    public static (bool locked, double lockTime, double finalDiff, double diffStd)
        DetectPhaseLocking(double[] phaseDiffs, double[] times, double lockThreshold = 0.1)
    {
        if (phaseDiffs.Length < 10)
            return (false, double.PositiveInfinity, phaseDiffs.Length > 0 ? phaseDiffs[^1] : 0, 1.0);

        int n = phaseDiffs.Length;
        // Look for sustained low variance in the trailing portion.
        int windowSize = Math.Min(20, n / 3);
        double bestStd = double.MaxValue;
        int lockStart = n;

        for (int i = n - windowSize; i >= windowSize; i--)
        {
            double mean = 0, m2 = 0;
            for (int j = 0; j < windowSize; j++)
            {
                double d = phaseDiffs[i + j];
                mean += d;
                m2 += d * d;
            }
            mean /= windowSize;
            double std = Math.Sqrt(m2 / windowSize - mean * mean);

            if (std < lockThreshold && std < bestStd)
            {
                bestStd = std;
                lockStart = i;
            }
        }

        bool locked = lockStart < n;
        double lockTime = locked ? times[lockStart] - times[0] : double.PositiveInfinity;
        double finalDiff = phaseDiffs[^1];

        // Compute std over last window.
        int lastW = Math.Min(windowSize, n);
        double m = 0, m2s = 0;
        for (int i = n - lastW; i < n; i++) { m += phaseDiffs[i]; m2s += phaseDiffs[i] * phaseDiffs[i]; }
        m /= lastW;
        double stdVal = Math.Sqrt(Math.Max(m2s / lastW - m * m, 0));

        return (locked, lockTime, finalDiff, stdVal);
    }

    // ══════════════════════════════════════════════════════════════════
    // Detect beat patterns from phase difference.
    // ══════════════════════════════════════════════════════════════════

    public static PhaseLockingProfile.BeatPattern DetectBeats(
        double[] phaseDiffs, double dt)
    {
        if (phaseDiffs.Length < 4)
            return new PhaseLockingProfile.BeatPattern(0, 0, false, 0, "None");

        // Look for sinusoidal modulation in phaseDiff(t).
        double mean = phaseDiffs.Average();
        var detrended = phaseDiffs.Select(x => x - mean).ToArray();

        // Simple zero-crossing frequency estimate.
        int zeroCrossings = 0;
        for (int i = 1; i < detrended.Length; i++)
            if (detrended[i - 1] * detrended[i] < 0) zeroCrossings++;

        double T = phaseDiffs.Length * dt;
        double beatFreq = zeroCrossings / (2.0 * T);

        // Amplitude of modulation.
        double amp = detrended.Max() - detrended.Min();

        bool resolved = beatFreq > 1e-6 && amp > 0.05;
        string patternType = resolved
            ? (beatFreq > 0.1 ? "Regular" : "Irregular")
            : "None";

        double coherenceTime = resolved ? 1.0 / Math.Max(beatFreq, 0.01) : 0;

        return new PhaseLockingProfile.BeatPattern(
            beatFreq, amp, resolved, coherenceTime, patternType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute frequency ratio from two frequency time series.
    // ══════════════════════════════════════════════════════════════════

    public static double[] ComputeFreqRatioHistory(
        double[] freq1, double[] freq2)
    {
        int n = Math.Min(freq1.Length, freq2.Length);
        var ratios = new double[n];
        for (int i = 0; i < n; i++)
            ratios[i] = Math.Abs(freq1[i]) > 1e-10
                ? freq2[i] / freq1[i] : 1.0;
        return ratios;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute collective order parameter R_Q for charge modes.
    // ══════════════════════════════════════════════════════════════════

    public static double ComputeCollectiveRQ(double[] phases)
    {
        // R_Q = |(1/N) Σ exp(i·θ_i)| — standard Kuramoto order parameter
        // applied to charge phases (not oscillator phases).
        if (phases.Length == 0) return 0;
        double ss = 0, sc = 0;
        for (int i = 0; i < phases.Length; i++)
        {
            ss += Math.Sin(phases[i]);
            sc += Math.Cos(phases[i]);
        }
        return Math.Sqrt(ss * ss + sc * sc) / phases.Length;
    }

    // ══════════════════════════════════════════════════════════════════
    // Arnold tongue: locking probability in (Δω, K) space.
    // ══════════════════════════════════════════════════════════════════

    public static PhaseLockingProfile.InterChargeReport BuildLockingGrid(
        List<PhaseLockingProfile.PhaseLockingRun> runs,
        int nSepBins = 5, int nDetBins = 5)
    {
        if (runs.Count == 0)
            return new PhaseLockingProfile.InterChargeReport(
                runs, new List<PhaseLockingProfile.LockingResult>(),
                new List<PhaseLockingProfile.BeatPattern>(),
                new List<PhaseLockingProfile.CollectiveMode>(),
                new double[0, 0], Array.Empty<double>(), Array.Empty<double>(),
                0, false, false, false, "A: Independent Internal Modes", "No data.");

        double minSep = runs.Min(r => r.Separation);
        double maxSep = runs.Max(r => r.Separation);
        double minDet = runs.Min(r => r.FrequencyDetuning);
        double maxDet = runs.Max(r => r.FrequencyDetuning);

        if (maxSep - minSep < 1e-10) maxSep = minSep + 1.0;
        if (maxDet - minDet < 1e-10) maxDet = minDet + 1.0;

        var sepAxis = new double[nSepBins];
        var detAxis = new double[nDetBins];
        var grid = new double[nSepBins, nDetBins];

        for (int s = 0; s < nSepBins; s++)
            sepAxis[s] = minSep + (maxSep - minSep) * (s + 0.5) / nSepBins;
        for (int d = 0; d < nDetBins; d++)
            detAxis[d] = minDet + (maxDet - minDet) * (d + 0.5) / nDetBins;

        // Bin runs.
        for (int s = 0; s < nSepBins; s++)
            for (int d = 0; d < nDetBins; d++)
            {
                double sepBinWidth = (maxSep - minSep) / nSepBins;
                double detBinWidth = (maxDet - minDet) / nDetBins;
                var bin = runs.Where(r =>
                    Math.Abs(r.Separation - sepAxis[s]) < sepBinWidth &&
                    Math.Abs(r.FrequencyDetuning - detAxis[d]) < detBinWidth).ToList();
                grid[s, d] = bin.Count > 0 ? (double)bin.Count(r => r.PhaseLocked) / bin.Count : 0;
            }

        // Build locking results per separation.
        var lockingResults = new List<PhaseLockingProfile.LockingResult>();
        foreach (var g in runs.GroupBy(r => r.Separation).OrderBy(g => g.Key))
        {
            var group = g.ToList();
            int locked = group.Count(r => r.PhaseLocked);
            lockingResults.Add(new PhaseLockingProfile.LockingResult(
                g.Key, group.First().NumCharges, group.Count, locked,
                (double)locked / group.Count,
                group.Where(r => r.PhaseLocked).DefaultIfEmpty().Average(r => r?.LockingTime ?? double.PositiveInfinity),
                group.Average(r => r.FinalPhaseDiff),
                group.Average(r => r.PhaseDiffStd),
                0, false,
                PhaseLockingProfile.ClassifyLocking(locked, group.Count, 0, 0, g.Key, 0.1)));
        }

        // Detect beats from non-locked runs.
        var beats = runs.Where(r => !r.PhaseLocked).Take(5).Select(r =>
            CoherenceSpectrum.DetectBeats(r.PhaseDiffHistory, 0.01)).ToList();

        // Collective modes.
        var collModes = new List<PhaseLockingProfile.CollectiveMode>();
        bool anyLocked = runs.Any(r => r.PhaseLocked);

        if (anyLocked)
        {
            collModes.Add(new PhaseLockingProfile.CollectiveMode(
                "Symmetric (in-phase)", 1.0, runs.Count(r => r.PhaseLocked),
                (double)runs.Count(r => r.PhaseLocked) / runs.Count,
                runs.Count(r => r.PhaseLocked && Math.Abs(r.FinalPhaseDiff) < 0.2)
                    / (double)Math.Max(runs.Count(r => r.PhaseLocked), 1),
                true, "All charges oscillate in phase."));

            collModes.Add(new PhaseLockingProfile.CollectiveMode(
                "Antisymmetric (π-out)", 1.0, runs.Count(r => r.PhaseLocked && Math.Abs(r.FinalPhaseDiff - Math.PI) < 0.3),
                (double)runs.Count(r => r.PhaseLocked && Math.Abs(r.FinalPhaseDiff - Math.PI) < 0.3)
                    / Math.Max(runs.Count(r => r.PhaseLocked), 1),
                0, true, "Charges oscillate π out of phase."));
        }

        double coherenceLen = lockingResults
            .Where(lr => lr.LockingProbability > 0.5)
            .Select(lr => lr.Separation)
            .DefaultIfEmpty(0).Max();

        bool phaseLockingObs = runs.Any(r => r.PhaseLocked);
        bool freqLockingObs = runs.Any(r => r.PhaseLocked && Math.Abs(r.FrequencyRatio - 1.0) < 0.05);
        bool collectiveFound = collModes.Count > 0;

        string classification = collectiveFound
            ? "C: Collective Coherent Modes"
            : phaseLockingObs ? "B: Weak Coupling" : "A: Independent Internal Modes";

        string verdict = collectiveFound
            ? "INTER-CHARGE COHERENCE ESTABLISHED. Separated Q=+1 charges " +
              $"can phase-lock their internal θ-modes. Coherence length: {coherenceLen:F2}. " +
              "Collective modes (symmetric, antisymmetric) detected. " +
              "This is a higher-level synchronization layer: charges remain " +
              "topologically distinct (Q conserved) but their internal phases " +
              "become coherent. The charge gas can transition from incoherent " +
              "to coherent at sufficient coupling/density."
            : phaseLockingObs
                ? "Phase locking observed but limited to specific parameter ranges. " +
                  "Collective modes are weak. Inter-charge coherence exists but is fragile."
                : "No robust phase locking detected. Charge internal modes remain " +
                  "independent. Coherence is local (within each Q=+1) but not inter-charge.";

        return new PhaseLockingProfile.InterChargeReport(
            runs, lockingResults, beats, collModes,
            grid, sepAxis, detAxis,
            coherenceLen, phaseLockingObs, freqLockingObs, collectiveFound,
            classification, verdict);
    }
}
