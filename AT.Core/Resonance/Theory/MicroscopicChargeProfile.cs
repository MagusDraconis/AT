namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for sub-Q microscopic charge structures:
/// partial domains, proto-kinks, sub-threshold structures,
/// and incomplete condensates detected through multi-threshold
/// topology analysis.
///
/// AT-120: Minimal Charge Quantum
/// </summary>
public static class MicroscopicChargeProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A superlevel-set component at a specific threshold.
    /// Tracks how connected components appear and disappear as
    /// the threshold varies.
    /// </summary>
    public sealed record SuperlevelComponent(
        double BirthThreshold,     // threshold where component first appears
        double DeathThreshold,     // threshold where component merges/disappears
        double Persistence,        // birth − death (lifetime in threshold space)
        int PeakGridX,
        int PeakGridY,
        double PeakR,
        double MeanR,
        int CellCount,
        string Classification      // "FullCondensate", "ProtoKink", "SubThreshold", "Noise"
    );

    /// <summary>
    /// Multi-threshold charge profile: Q(T) for T ∈ [T_min, T_max].
    /// </summary>
    public sealed record ChargeThresholdProfile(
        double[] Thresholds,
        int[] Q_values,           // Q = #{connected components where R>T}
        int[] ComponentCounts,    // total # components above each threshold
        double[] TotalVariation,  // ∫|∇R| at each threshold
        string Analysis
    );

    /// <summary>
    /// A proto-kink: a region where R crosses the 0.5 threshold
    /// but the peak is barely above it — a marginal condensate.
    /// </summary>
    public sealed record ProtoKink(
        int GridX, int GridY,
        double PeakR,
        double MarginAboveThreshold,  // PeakR − 0.5
        bool IsViable,                // survives >1 checkpoint
        int Lifetime,
        string Fate                   // "Stable", "Decayed", "Merged"
    );

    /// <summary>
    /// A half-condensate: connected component at T=0.3
    /// that does NOT survive to T=0.5.
    /// </summary>
    public sealed record HalfCondensate(
        int GridX, int GridY,
        double PeakR,
        int CellCount,
        bool SurvivesToHalf,      // survives at T=0.5
        double Persistence,       // birth threshold
        string Classification     // "HalfCondensate", "WeakFluctuation", "ProtoStructure"
    );

    /// <summary>
    /// A fragmentation experiment: attempts to split a condensate
    /// into sub-Q pieces.
    /// </summary>
    public sealed record FragmentationAttempt(
        string Method,             // "ThresholdLowering", "KinkIsolation", "MorseDecomposition"
        bool ProducedSubQ,         // did we find anything smaller than Q=+1?
        string SubQDescription,    // what was found (if any)
        double SubQSize,           // size of the sub-Q structure
        bool IsValidCharge,        // would SubQ qualify as a conserved topological charge?
        string Verdict             // detailed explanation
    );

    /// <summary>
    /// Complete charge quantum report.
    /// </summary>
    public sealed record ChargeQuantumReport(
        List<SuperlevelComponent> Components,
        List<ChargeThresholdProfile> ThresholdProfiles,
        List<ProtoKink> ProtoKinks,
        List<HalfCondensate> HalfCondensates,
        List<FragmentationAttempt> FragmentationAttempts,
        bool FundamentalChargeFound,
        string MicroscopicChargeCandidate,
        string Classification,
        string Verdict
    );

    /// <summary>
    /// Morse-theoretic analysis of the R-field.
    /// Critical points and their indices.
    /// </summary>
    public sealed record MorseAnalysis(
        int LocalMaxima,           // peaks (index 0 in 1D, index 2 in 2D)
        int LocalMinima,           // valleys
        int Saddles,               // saddle points (1D: index 1; 2D: index 1)
        double[] CriticalValues,   // R values at critical points
        int[] CriticalTypes,       // +1 max, -1 min, 0 saddle
        string MorseDecomposition  // description of gradient flow structure
    );

    // ══════════════════════════════════════════════════════════════════
    // Compute multi-threshold Q profile
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Computes Q(T) = #{connected components where R(x) > T}
    /// for a range of thresholds. Returns the profile and
    /// the list of superlevel components with birth/death info.
    /// </summary>
    public static ChargeThresholdProfile ComputeThresholdProfile(
        double[,] Rfield, int gridSize, int nThresholds = 20,
        double T_min = 0.10, double T_max = 0.90)
    {
        double[] thresholds = new double[nThresholds];
        int[] Q = new int[nThresholds];
        int[] compCounts = new int[nThresholds];
        double[] totalVar = new double[nThresholds];

        for (int t = 0; t < nThresholds; t++)
        {
            thresholds[t] = T_min + (T_max - T_min) * t / (nThresholds - 1);
            Q[t] = CountConnectedComponents(Rfield, gridSize, thresholds[t]);

            // Count ALL components (not just those qualifying as condensates).
            compCounts[t] = CountAllComponents(Rfield, gridSize, thresholds[t]);

            // Total variation: sum of |∇R| for cells above threshold.
            totalVar[t] = ComputeTotalVariationAbove(Rfield, gridSize, thresholds[t]);
        }

        string analysis = AnalyzeQProfile(thresholds, Q);

        return new ChargeThresholdProfile(thresholds, Q, compCounts, totalVar, analysis);
    }

    // ══════════════════════════════════════════════════════════════════
    // Extract superlevel components with birth/death (persistent homology).
    // ══════════════════════════════════════════════════════════════════

    public static List<SuperlevelComponent> ExtractPersistentComponents(
        double[,] Rfield, int gridSize, int nThresholds = 30)
    {
        double[] Ts = new double[nThresholds];
        for (int t = 0; t < nThresholds; t++)
            Ts[t] = 0.10 + 0.80 * t / (nThresholds - 1);

        var components = new List<SuperlevelComponent>();

        // For each threshold, find all components.
        // Track birth/death by comparing across thresholds.
        var prevComponents = new Dictionary<(int, int), SuperlevelComponent>();

        for (int ti = 0; ti < nThresholds; ti++)
        {
            double T = Ts[ti];
            var current = FindAllComponentsWithPeaks(Rfield, gridSize, T);

            // Match current components to previous ones.
            var matched = new HashSet<int>();
            foreach (var curr in current)
            {
                // Find previous component with closest peak.
                double bestDist = double.MaxValue;
                SuperlevelComponent? bestPrev = null;

                foreach (var (_, prev) in prevComponents)
                {
                    double dist = Math.Sqrt(
                        (curr.PeakGridX - prev.PeakGridX) * (curr.PeakGridX - prev.PeakGridX) +
                        (curr.PeakGridY - prev.PeakGridY) * (curr.PeakGridY - prev.PeakGridY));
                    if (dist < bestDist && dist < 3.0) // within 3 grid cells
                    {
                        bestDist = dist;
                        bestPrev = prev;
                    }
                }

                if (bestPrev != null)
                {
                    // Component persists — update death threshold.
                    // (It's still alive at this T.)
                }
                else
                {
                    // New component born at this threshold.
                    string classification = ClassifyComponent(curr, T);
                    components.Add(new SuperlevelComponent(
                        T, T, 0, // death will be updated when it disappears
                        curr.PeakGridX, curr.PeakGridY,
                        curr.PeakR, curr.MeanR, curr.CellCount,
                        classification));
                }
            }

            // Update previous for next iteration.
            prevComponents.Clear();
            foreach (var curr in current)
                prevComponents[(curr.PeakGridX, curr.PeakGridY)] = new SuperlevelComponent(
                    T, T, 0, curr.PeakGridX, curr.PeakGridY,
                    curr.PeakR, curr.MeanR, curr.CellCount, "");
        }

        // Compute persistence for components that died (all, since we end at T_max).
        for (int i = 0; i < components.Count; i++)
        {
            var c = components[i];
            // Find death threshold: last T where this component exists.
            double deathT = 0.90; // default end
            // Scan backward to find where this peak disappears.
            for (int ti = nThresholds - 1; ti >= 0; ti--)
            {
                double T = Ts[ti];
                if (Rfield[c.PeakGridX, c.PeakGridY] < T)
                {
                    deathT = T;
                    break;
                }
            }

            components[i] = c with
            {
                DeathThreshold = Math.Min(deathT, c.BirthThreshold + 0.01),
                Persistence = c.BirthThreshold - Math.Min(deathT, c.BirthThreshold + 0.01)
            };
        }

        // Filter: only keep components with meaningful persistence.
        return components
            .Where(c => c.Persistence > 0.02)
            .OrderByDescending(c => c.Persistence)
            .ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Proto-kink detection: marginal condensates barely above 0.5.
    // ══════════════════════════════════════════════════════════════════

    public static List<ProtoKink> DetectProtoKinks(
        double[,] Rfield, int gridSize, List<int[]>? lifetimes = null)
    {
        var protoKinks = new List<ProtoKink>();

        // Find all local maxima.
        for (int gx = 1; gx < gridSize - 1; gx++)
        {
            for (int gy = 1; gy < gridSize - 1; gy++)
            {
                double r = Rfield[gx, gy];
                bool isMax = r > Rfield[gx - 1, gy] && r > Rfield[gx + 1, gy]
                          && r > Rfield[gx, gy - 1] && r > Rfield[gx, gy + 1];

                if (!isMax) continue;

                // Check if this peak is in the marginal range.
                if (r > 0.3 && r < 0.8)
                {
                    double margin = r - 0.5;
                    bool viable = r > 0.55; // needs 5% above threshold

                    // Determine fate based on peak R.
                    string fate = r > 0.7 ? "Stable"
                                : r > 0.55 ? "Marginal"
                                : r > 0.4 ? "Weak"
                                : "Decayed";

                    int lifetime = lifetimes != null && gx < lifetimes.Count
                        ? lifetimes[gx][gy] : -1;

                    protoKinks.Add(new ProtoKink(
                        gx, gy, r, margin, viable, lifetime, fate));
                }
            }
        }

        return protoKinks;
    }

    // ══════════════════════════════════════════════════════════════════
    // Half-condensate detection: components at T=0.3 that don't reach T=0.5.
    // ══════════════════════════════════════════════════════════════════

    public static List<HalfCondensate> DetectHalfCondensates(
        double[,] Rfield, int gridSize)
    {
        var halfCondensates = new List<HalfCondensate>();

        // Find components at T=0.3.
        var lowComps = FindAllComponentsWithPeaks(Rfield, gridSize, 0.30);
        var highComps = FindAllComponentsWithPeaks(Rfield, gridSize, 0.50);

        foreach (var low in lowComps)
        {
            // Check if this component survives at T=0.5.
            bool survives = highComps.Any(h =>
                Math.Abs(h.PeakGridX - low.PeakGridX) < 3 &&
                Math.Abs(h.PeakGridY - low.PeakGridY) < 3);

            if (!survives && low.PeakR > 0.35)
            {
                string classification = low.PeakR > 0.45 ? "ProtoStructure"
                                     : low.PeakR > 0.40 ? "WeakFluctuation"
                                     : "HalfCondensate";

                halfCondensates.Add(new HalfCondensate(
                    low.PeakGridX, low.PeakGridY,
                    low.PeakR, low.CellCount,
                    survives, low.PeakR - 0.30, classification));
            }
        }

        return halfCondensates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Morse analysis of the R-field.
    // ══════════════════════════════════════════════════════════════════

    public static MorseAnalysis ComputeMorseAnalysis(double[,] Rfield, int gridSize)
    {
        var maxima = new List<double>();
        var minima = new List<double>();
        var saddles = new List<double>();
        var values = new List<double>();
        var types = new List<int>();

        for (int gx = 1; gx < gridSize - 1; gx++)
        {
            for (int gy = 1; gy < gridSize - 1; gy++)
            {
                double r = Rfield[gx, gy];
                bool isMax = r > Rfield[gx - 1, gy] && r > Rfield[gx + 1, gy]
                          && r > Rfield[gx, gy - 1] && r > Rfield[gx, gy + 1];
                bool isMin = r < Rfield[gx - 1, gy] && r < Rfield[gx + 1, gy]
                          && r < Rfield[gx, gy - 1] && r < Rfield[gx, gy + 1];

                if (isMax) { maxima.Add(r); values.Add(r); types.Add(1); }
                if (isMin) { minima.Add(r); values.Add(r); types.Add(-1); }

                // Check for saddle (one direction up, one down).
                bool saddle = (r > Rfield[gx - 1, gy] && r < Rfield[gx + 1, gy]
                            && r < Rfield[gx, gy - 1] && r > Rfield[gx, gy + 1])
                           || (r < Rfield[gx - 1, gy] && r > Rfield[gx + 1, gy]
                            && r > Rfield[gx, gy - 1] && r < Rfield[gx, gy + 1]);

                if (saddle) { saddles.Add(r); values.Add(r); types.Add(0); }
            }
        }

        string decomposition =
            $"Morse decomposition: {maxima.Count} maxima, {minima.Count} minima, {saddles.Count} saddles.\n" +
            $"Max R values: [{string.Join(", ", maxima.OrderByDescending(x => x).Take(5).Select(v => $"{v:F3}"))}...]\n" +
            $"Euler characteristic χ = max − saddle + min = {maxima.Count - saddles.Count + minima.Count}\n" +
            (maxima.Count >= 2
                ? $"  Each maximum with R>0.5 = one condensate candidate. " +
                  $"Maxima > 0.5: {maxima.Count(m => m > 0.5)}. " +
                  $"This is the MORSE-THEORETIC Q."
                : "  Single maximum — trivial topology.");

        return new MorseAnalysis(
            maxima.Count, minima.Count, saddles.Count,
            values.ToArray(), types.ToArray(), decomposition);
    }

    // ══════════════════════════════════════════════════════════════════
    // Fragmentation attempts.
    // ══════════════════════════════════════════════════════════════════

    public static List<FragmentationAttempt> AttemptFragmentation(
        double[,] Rfield, int gridSize, int condensateCount)
    {
        var attempts = new List<FragmentationAttempt>();

        // Attempt 1: Lower the threshold.
        int Q_at_05 = CountConnectedComponents(Rfield, gridSize, 0.50);
        int Q_at_04 = CountConnectedComponents(Rfield, gridSize, 0.40);
        int Q_at_03 = CountConnectedComponents(Rfield, gridSize, 0.30);
        int Q_at_02 = CountConnectedComponents(Rfield, gridSize, 0.20);

        attempts.Add(new FragmentationAttempt(
            "ThresholdLowering",
            Q_at_04 > Q_at_05 || Q_at_03 > Q_at_05,
            $"Q(T=0.5)={Q_at_05}, Q(T=0.4)={Q_at_04}, Q(T=0.3)={Q_at_03}, Q(T=0.2)={Q_at_02}",
            (double)(Q_at_03 - Q_at_05),
            false, // Sub-threshold components are not conserved.
            Q_at_04 > Q_at_05
                ? "WARNING: Lowering threshold reveals ADDITIONAL components. " +
                  "These are SUB-THRESHOLD structures that don't qualify as Q. " +
                  "They are NOT topologically protected (can disappear continuously)."
                : "PASS: Q is stable across threshold changes. No sub-Q structures disguised as noise."
        ));

        // Attempt 2: Kink isolation — can we separate a kink from an antikink?
        bool kinkOnly = CheckKinkWithoutAntikink(Rfield, gridSize);
        attempts.Add(new FragmentationAttempt(
            "KinkIsolation",
            kinkOnly,
            kinkOnly ? "Isolated kink detected (0→1 transition without 1→0 return)" : "No isolated kinks",
            0.5,
            false,
            kinkOnly
                ? "FOUND: A kink without matching antikink detected at boundary. " +
                  "This is a BOUNDARY EFFECT, not a true half-charge. " +
                  "R(x) goes from 0 to >0.5 at one boundary but system is not periodic."
                : "PASS: Every kink (0→1 crossing) has a matching antikink (1→0 crossing). " +
                  "Kinks always appear in pairs. Q is integer."
        ));

        // Attempt 3: Morse decomposition.
        var morse = ComputeMorseAnalysis(Rfield, gridSize);
        bool morseSubQ = morse.LocalMaxima > condensateCount;
        attempts.Add(new FragmentationAttempt(
            "MorseDecomposition",
            morseSubQ,
            morseSubQ
                ? $"Morse maxima ({morse.LocalMaxima}) > condensate count ({condensateCount})"
                : "Morse maxima match condensate count",
            morse.LocalMaxima - condensateCount,
            false,
            morseSubQ
                ? "CAUTION: More Morse maxima than condensates. Some peaks are below R=0.5 " +
                  "and do NOT produce topologically protected domains. They are DYNAMICAL, " +
                  "not topological — can appear and disappear continuously."
                : "PASS: Morse maxima exactly match Q. No hidden sub-structure."
        ));

        // Attempt 4: Continuous Q change — can Q change by non-integer amounts?
        // By definition, Q is a count of components → always integer.
        // But could there be a continuous "coherence charge" Q_c?
        double totalCoherence = 0;
        for (int gx = 0; gx < gridSize; gx++)
            for (int gy = 0; gy < gridSize; gy++)
                if (Rfield[gx, gy] > 0.5)
                    totalCoherence += Rfield[gx, gy] - 0.5;

        attempts.Add(new FragmentationAttempt(
            "ContinuousCharge",
            totalCoherence > 0.1,
            totalCoherence > 0.1
                ? $"Continuous coherence excess = {totalCoherence:F3} (non-integer)"
                : "No continuous excess coherence",
            totalCoherence,
            false,
            totalCoherence > 0.1
                ? "FOUND: Continuous 'coherence excess' ∫_{R>0.5}(R-0.5)dx > 0. " +
                  "This is a CONTINUOUS quantity, not quantized. But it is NOT conserved " +
                  "(reaction drives R→1, increasing it). Cannot serve as a topological charge."
                : "No continuous charge found."
        ));

        // Attempt 5: Persistent homology — do features with intermediate persistence exist?
        var persistent = ExtractPersistentComponents(Rfield, gridSize);
        bool hasIntermediate = persistent.Any(c =>
            c.Persistence > 0.05 && c.Persistence < 0.15 &&
            c.BirthThreshold < 0.5);

        attempts.Add(new FragmentationAttempt(
            "PersistentHomology",
            hasIntermediate,
            hasIntermediate
                ? $"Intermediate-persistence features found: " +
                  $"{persistent.Count(c => c.Persistence > 0.05 && c.Persistence < 0.15)}"
                : "No intermediate-persistence features",
                persistent.Where(c => c.Persistence > 0.05 && c.Persistence < 0.15).Count(),
            !hasIntermediate,
            hasIntermediate
                ? "FOUND: Topological features with intermediate persistence. " +
                  "These are SHORT-LIVED in threshold space — they appear and disappear " +
                  "as T varies. They represent FLUCTUATIONS, not stable topological charges. " +
                  "True charges have persistence spanning T∈[0.1, 0.9]."
                : "PASS: All topological features have either very high persistence (condensates) " +
                  "or very low persistence (noise). No intermediate structures."
        ));

        return attempts;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    public static int CountConnectedComponents(
        double[,] R, int gs, double threshold)
    {
        var visited = new bool[gs, gs];
        int count = 0;

        for (int gx = 0; gx < gs; gx++)
        {
            for (int gy = 0; gy < gs; gy++)
            {
                if (visited[gx, gy] || R[gx, gy] <= threshold) continue;

                // BFS.
                var queue = new Queue<(int, int)>();
                queue.Enqueue((gx, gy));
                visited[gx, gy] = true;

                while (queue.Count > 0)
                {
                    var (cx, cy) = queue.Dequeue();
                    foreach (var (nx, ny) in new[] {
                        (cx - 1, cy), (cx + 1, cy), (cx, cy - 1), (cx, cy + 1) })
                    {
                        if (nx >= 0 && nx < gs && ny >= 0 && ny < gs
                            && !visited[nx, ny] && R[nx, ny] > threshold)
                        {
                            visited[nx, ny] = true;
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
                count++;
            }
        }
        return count;
    }

    private static int CountAllComponents(
        double[,] R, int gs, double threshold)
    {
        // Same as CountConnectedComponents but with no size filter.
        return CountConnectedComponents(R, gs, threshold);
    }

    private static double ComputeTotalVariationAbove(
        double[,] R, int gs, double threshold)
    {
        double sum = 0;
        for (int gx = 0; gx < gs - 1; gx++)
        {
            for (int gy = 0; gy < gs; gy++)
            {
                if (R[gx, gy] > threshold || R[gx + 1, gy] > threshold)
                    sum += Math.Abs(R[gx + 1, gy] - R[gx, gy]);
            }
        }
        for (int gx = 0; gx < gs; gx++)
        {
            for (int gy = 0; gy < gs - 1; gy++)
            {
                if (R[gx, gy] > threshold || R[gx, gy + 1] > threshold)
                    sum += Math.Abs(R[gx, gy + 1] - R[gx, gy]);
            }
        }
        return sum;
    }

    private static List<SuperlevelComponent> FindAllComponentsWithPeaks(
        double[,] R, int gs, double threshold)
    {
        var visited = new bool[gs, gs];
        var components = new List<SuperlevelComponent>();

        for (int gx = 0; gx < gs; gx++)
        {
            for (int gy = 0; gy < gs; gy++)
            {
                if (visited[gx, gy] || R[gx, gy] <= threshold) continue;

                var cells = new List<(int, int)>();
                var queue = new Queue<(int, int)>();
                queue.Enqueue((gx, gy));
                visited[gx, gy] = true;

                double sumR = 0, peakR = 0;
                int peakX = gx, peakY = gy;

                while (queue.Count > 0)
                {
                    var (cx, cy) = queue.Dequeue();
                    cells.Add((cx, cy));
                    double r = R[cx, cy];
                    sumR += r;
                    if (r > peakR) { peakR = r; peakX = cx; peakY = cy; }

                    foreach (var (nx, ny) in new[] {
                        (cx - 1, cy), (cx + 1, cy), (cx, cy - 1), (cx, cy + 1) })
                    {
                        if (nx >= 0 && nx < gs && ny >= 0 && ny < gs
                            && !visited[nx, ny] && R[nx, ny] > threshold)
                        {
                            visited[nx, ny] = true;
                            queue.Enqueue((nx, ny));
                        }
                    }
                }

                components.Add(new SuperlevelComponent(
                    threshold, threshold, 0,
                    peakX, peakY, peakR, sumR / cells.Count, cells.Count, ""));
            }
        }
        return components;
    }

    private static string AnalyzeQProfile(double[] thresholds, int[] Q)
    {
        bool isConstant = Q.All(q => q == Q[0]);
        bool isMonotonic = true;
        for (int i = 1; i < Q.Length; i++)
            if (Q[i] > Q[i - 1]) { isMonotonic = false; break; }

        // Q should be non-increasing as T increases (fewer components at higher T).
        int stablePlateauStart = -1;
        for (int i = 0; i < Q.Length - 3; i++)
        {
            if (Q[i] == Q[i + 1] && Q[i + 1] == Q[i + 2] && Q[i + 2] == Q[i + 3])
            {
                stablePlateauStart = i;
                break;
            }
        }

        if (isConstant)
            return "Q(T) is CONSTANT across all thresholds — charge is threshold-independent. " +
                   "This supports Q as a fundamental topological invariant (AT-115).";
        else if (isMonotonic && stablePlateauStart >= 0)
            return "Q(T) has stable plateaus with discrete jumps. " +
                   "Each plateau = region where thresholds don't change Q. " +
                   "Jumps occur at critical R values (boundary between domains). " +
                   "This is EXPECTED for a topological charge.";
        else
            return "Q(T) is NON-MONOTONIC — this would be anomalous and suggests " +
                   "sub-threshold structure or measurement artifacts.";
    }

    private static string ClassifyComponent(SuperlevelComponent c, double T)
    {
        if (c.PeakR > 0.80) return "FullCondensate";
        if (c.PeakR > 0.60) return "ProtoKink";
        if (c.PeakR > 0.40) return "SubThreshold";
        return "Noise";
    }

    private static bool CheckKinkWithoutAntikink(double[,] R, int gs)
    {
        // Check boundaries: can a domain start at x=0 or end at x=gs-1?
        // If R > 0.5 at a boundary, the kink-antikink pair is broken.
        for (int gy = 0; gy < gs; gy++)
        {
            if (R[0, gy] > 0.5) return true;    // kink at x=0 without antikink
            if (R[gs - 1, gy] > 0.5) return true; // antikink at right boundary
        }
        for (int gx = 0; gx < gs; gx++)
        {
            if (R[gx, 0] > 0.5) return true;
            if (R[gx, gs - 1] > 0.5) return true;
        }
        return false;
    }
}
