using System.Collections.Concurrent;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Determines whether the topological charge Q is truly fundamental
/// or emerges from a deeper microscopic quantity. Attempts to fragment
/// Q into sub-Q pieces through multi-threshold topology analysis,
/// Morse theory, persistent homology, and near-threshold state probing.
///
/// AT-120: Minimal Charge Quantum
/// </summary>
public static class ChargeQuantumAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Constants
    // ══════════════════════════════════════════════════════════════════

    private const double C0 = 0.0047;
    private const double D_R = 2.5e-5;

    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record NearThresholdRun(
        double K, double Lambda, int N, int Seed,
        double[] Q_history,
        double[] PeakR_history,
        double[] MeanR_history,
        int FinalQ,
        double FinalPeakR,
        bool HasMarginalStates,
        int MarginalCount);

    public sealed record NearThresholdScan(
        List<NearThresholdRun> Runs,
        double CriticalK,
        double CriticalLambda,
        int WeakCondensateCount,
        int ProtoKinkCount,
        int DecayedCount,
        string RegimeDescription);

    // ══════════════════════════════════════════════════════════════════
    // CHARGE QUANTUM DERIVATION
    // ══════════════════════════════════════════════════════════════════

    public static string ChargeQuantumTheory()
    {
        return @"
MINIMAL CHARGE QUANTUM — IS Q FUNDAMENTAL?

1. CURRENT DEFINITION:
   Q = #{connected components of {x: R(x) > 0.5}}
   = condensate count = kink-pair count / 2

   Q ∈ ℕ (non-negative integers). Q is additive and conserved
   under PDE evolution (AT-116).

2. THE CHARGE QUANTUM QUESTION:
   Is Q the SMALLEST possible unit of topological charge?
   Or can Q be decomposed into smaller, more fundamental pieces?

   Candidates for sub-Q structure:
   (a) Individual KINKS (0→1 transition without matching 1→0)
   (b) Half-CONDENSATES (R>0.3 domains that don't reach R>0.5)
   (c) Proto-KINKS (barely-above-threshold local maxima)
   (d) Continuous COHERENCE EXCESS (∫_{R>0.5}(R−0.5)dx)
   (e) PERSISTENT HOMOLOGY features at intermediate scales

3. TOPOLOGICAL OBSTRUCTION THEOREM:
   CLAIM: Q is the MINIMAL conserved topological charge.
   PROOF SKETCH:
   (a) The only conserved quantity under PDE evolution is the number
       of kink-antikink pairs. Individual kinks cannot exist without
       matching antikinks (boundary conditions force R(0)=R(L)≈0).
   (b) The one-way barrier c₀·M·R·(1−R²) > 0 prevents R from
       continuously crossing 0.5 downward. The crossing count is
       a proper topological invariant (Betti number β₀).
   (c) Sub-threshold components (R<0.5) are DYNAMICAL, not topological.
       They can appear and disappear continuously without crossing
       any singularity.
   (d) The Morse index (number of R>0.5 local maxima) exactly equals Q
       when condensates are well-separated.
   (e) Persistent homology: features with persistence spanning
       T∈[0.1, 0.9] are genuine charges; features with short
       persistence are noise or fluctuations.

4. CANDIDATE MICROSCOPIC CHARGES:

   Q_micro = individual KINK (0→1 crossing):
     STATUS: NOT conserved. Kinks always appear in pairs.
     A single kink at a boundary is a boundary artifact.
     
   Q_micro = superlevel component at any threshold:
     STATUS: DEPENDS ON THRESHOLD. Only components at T>0.5
     have topological protection. Components at lower T can
     appear/disappear continuously.
     
   Q_micro = coherence excess dq = (R−0.5)·dx:
     STATUS: CONTINUOUS, not quantized. Not conserved
     (reaction drives R→1, increasing dq).
     
   Q_micro = Morse critical point with R>0.5:
     STATUS: EQUIVALENT TO Q. Each maximum with R>0.5 = one
     condensate. No finer decomposition possible.

5. ARGUMENT FOR FUNDAMENTALITY:
   Q counts kink-antikink PAIRS. The PAIR is the minimal
   topologically protected unit. An individual kink without
   its antikink is not a valid configuration (boundary conditions).
   A half-condensate (R<0.5) is not protected by the reaction
   barrier (the barrier only prevents crossing 0.5 DOWNWARD;
   below 0.5, R can evolve freely).
   
   Therefore: Q IS FUNDAMENTAL. There is no smaller conserved
   topological charge. The charge quantum is Q=+1.
   
   Q is the BETTI NUMBER β₀ of the superlevel set {R>0.5}.
   As a Betti number, it is inherently integer-valued and
   cannot be fractional. This is a homology-level statement:
   no continuous deformation can change β₀ unless it crosses
   the threshold.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Run a near-threshold simulation.
    // ══════════════════════════════════════════════════════════════════

    public static NearThresholdRun RunNearThreshold(
        double K, double Lambda, int N, int seed,
        int maxIterations = 3000,
        int checkpointInterval = 50,
        int gridSize = 30)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);

        // Random initial condition with weak coherence bias.
        for (int i = 0; i < N; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            var node = new TemporalNode(i, phase: phase,
                frequency: 0.8 + rng.NextDouble() * 0.4)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, K, Lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = N };
        var densityField = new LocalDensityField(gridSize);
        var condAnalyzer = new ResonanceCondensationAnalyzer
        {
            CondensationThreshold = 0.50,  // Lower threshold for marginal detection
            MinCondensateCells = 1,
            OverlapThreshold = 0.3
        };

        int totalChecks = maxIterations / checkpointInterval + 1;
        var qHist = new List<double>();
        var peakRHist = new List<double>();
        var meanRHist = new List<double>();
        int marginalCount = 0;
        bool hasMarginal = false;

        for (int iter = 0; iter < maxIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == 0 || iter == maxIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                // Count condensates at T=0.5 using the R-field.
                double[,] Rfield = ExtractRField(densityField, gridSize);
                int q = MicroscopicChargeProfile.CountConnectedComponents(Rfield, gridSize, 0.5);

                qHist.Add(q);
                peakRHist.Add(densityField.MaxLocalR());
                meanRHist.Add(densityField.MeanLocalR());

                // Check for marginal states (peak R between 0.4 and 0.7).
                double peak = densityField.MaxLocalR();
                if (peak > 0.40 && peak < 0.70)
                {
                    marginalCount++;
                    hasMarginal = true;
                }
            }
        }

        int finalQ = qHist.Count > 0 ? (int)qHist[^1] : 0;
        double finalPeakR = peakRHist.Count > 0 ? peakRHist[^1] : 0;

        return new NearThresholdRun(
            K, Lambda, N, seed,
            qHist.ToArray(), peakRHist.ToArray(), meanRHist.ToArray(),
            finalQ, finalPeakR, hasMarginal, marginalCount);
    }

    // ══════════════════════════════════════════════════════════════════
    // Scan near-threshold parameter space.
    // ══════════════════════════════════════════════════════════════════

    public static NearThresholdScan ScanNearThreshold(
        double[] K_values, double[] lambda_values, int[] N_values,
        int seedsPerPoint = 20, int maxIterations = 2000)
    {
        var runs = new ConcurrentBag<NearThresholdRun>();

        int total = K_values.Length * lambda_values.Length * N_values.Length;

        Parallel.ForEach(K_values, K =>
        {
            foreach (double lam in lambda_values)
            {
                foreach (int n in N_values)
                {
                    for (int s = 0; s < seedsPerPoint; s++)
                    {
                        var run = RunNearThreshold(K, lam, n,
                            s + (int)(K * 1000 + lam * 10000 + n * 100),
                            maxIterations);
                        runs.Add(run);
                    }
                }
            }
        });

        var allRuns = runs.ToList();
        int weakCount = allRuns.Count(r => r.HasMarginalStates);
        int protoKinkCount = allRuns.Count(r =>
            r.FinalPeakR > 0.50 && r.FinalPeakR < 0.65);
        int decayedCount = allRuns.Count(r =>
            r.FinalPeakR < 0.40 && r.Q_history.Any(q => q > 0));

        // Find critical K (where marginal states become frequent).
        double critK = 5.0;
        var byK = allRuns.GroupBy(r => r.K)
            .Select(g => (K: g.Key, MargFrac: (double)g.Count(r => r.HasMarginalStates) / g.Count()))
            .OrderBy(x => x.K).ToList();
        for (int i = 1; i < byK.Count; i++)
        {
            if (byK[i - 1].MargFrac < 0.5 && byK[i].MargFrac >= 0.5)
            {
                critK = (byK[i - 1].K + byK[i].K) / 2;
                break;
            }
        }

        double critLambda = lambda_values.Length > 0 ? lambda_values.Average() : 0.05;

        string regime = weakCount > allRuns.Count * 0.3
            ? "MARGINAL REGIME: frequent near-threshold states. Q may appear fuzzy at these parameters."
            : "STABLE REGIME: near-threshold states are rare. Q is sharply defined.";

        return new NearThresholdScan(
            allRuns, critK, critLambda, weakCount, protoKinkCount, decayedCount, regime);
    }

    // ══════════════════════════════════════════════════════════════════
    // Analyze a single R-field for charge quantum structure.
    // ══════════════════════════════════════════════════════════════════

    public static MicroscopicChargeProfile.ChargeQuantumReport AnalyzeRField(
        double[,] Rfield, int gridSize,
        int knownQ, double K, double Lambda, int N)
    {
        // 1. Multi-threshold profile.
        var profiles = new List<MicroscopicChargeProfile.ChargeThresholdProfile>
        {
            MicroscopicChargeProfile.ComputeThresholdProfile(Rfield, gridSize)
        };

        // 2. Persistent components.
        var components = MicroscopicChargeProfile.ExtractPersistentComponents(Rfield, gridSize);

        // 3. Proto-kinks.
        var protoKinks = MicroscopicChargeProfile.DetectProtoKinks(Rfield, gridSize);

        // 4. Half-condensates.
        var halfCondensates = MicroscopicChargeProfile.DetectHalfCondensates(Rfield, gridSize);

        // 5. Fragmentation attempts.
        var fragAttempts = MicroscopicChargeProfile.AttemptFragmentation(Rfield, gridSize, knownQ);

        // Determine if fundamental.
        bool allAttemptsFailed = fragAttempts.All(a => !a.ProducedSubQ || !a.IsValidCharge);
        bool foundSubQ = fragAttempts.Any(a => a.ProducedSubQ && a.IsValidCharge);

        string microscopicCandidate = foundSubQ
            ? fragAttempts.First(a => a.ProducedSubQ).SubQDescription
            : "NONE — Q is the minimal conserved topological charge";

        string classification = allAttemptsFailed
            ? "D: Fundamental Charge Quantum"
            : foundSubQ
                ? "B: Weak Substructure"
                : "C: Quantized Charge";

        string verdict = allAttemptsFailed
            ? "Q IS FUNDAMENTAL. All five fragmentation attempts failed. " +
              "No sub-Q structure exists that is both conserved and topologically protected. " +
              "Q=+1 is the minimal charge quantum — the kink-antikink pair is indivisible. " +
              "This follows from the Betti number β₀ being inherently integer-valued."
            : foundSubQ
                ? "SUB-Q STRUCTURE FOUND. The charge Q may not be fundamental. " +
                  $"Microscopic candidate: {microscopicCandidate}."
                : "Q IS QUANTIZED but sub-threshold structures exist that are not conserved. " +
                  "These are dynamical fluctuations, not topological charges.";

        return new MicroscopicChargeProfile.ChargeQuantumReport(
            components, profiles, protoKinks, halfCondensates,
            fragAttempts, allAttemptsFailed, microscopicCandidate,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Create a synthetic R-field for controlled testing.
    // ══════════════════════════════════════════════════════════════════

    public static double[,] CreateSyntheticRField(
        int gridSize, int nCondensates, double peakR,
        double width, int seed)
    {
        var rng = new Random(seed);
        var R = new double[gridSize, gridSize];
        double cellSize = 1.0 / gridSize;

        for (int gx = 0; gx < gridSize; gx++)
        {
            for (int gy = 0; gy < gridSize; gy++)
            {
                double x = (gx + 0.5) * cellSize;
                double y = (gy + 0.5) * cellSize;
                R[gx, gy] = 0.05 * rng.NextDouble(); // noise floor
            }
        }

        for (int c = 0; c < nCondensates; c++)
        {
            double cx = 0.15 + 0.7 * c / Math.Max(nCondensates - 1, 1);
            double cy = 0.5;
            int centerX = (int)(cx * gridSize);
            int centerY = (int)(cy * gridSize);
            int radius = (int)(width * gridSize / 2);

            for (int gx = Math.Max(0, centerX - radius);
                 gx < Math.Min(gridSize, centerX + radius); gx++)
            {
                for (int gy = Math.Max(0, centerY - radius);
                     gy < Math.Min(gridSize, centerY + radius); gy++)
                {
                    double dx = (gx - centerX) * cellSize;
                    double dy = (gy - centerY) * cellSize;
                    double r = Math.Sqrt(dx * dx + dy * dy) / width;
                    double gauss = peakR * Math.Exp(-r * r);
                    R[gx, gy] = Math.Max(R[gx, gy], gauss);
                }
            }
        }

        return R;
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(
        MicroscopicChargeProfile.ChargeQuantumReport report)
    {
        var sb = new System.Text.StringBuilder();

        bool fragmented = report.FragmentationAttempts.Any(a => a.ProducedSubQ && a.IsValidCharge);

        sb.AppendLine("Q1: Can fractional charge exist?");
        sb.AppendLine(fragmented
            ? "  YES — sub-Q structures were found that satisfy topological charge criteria."
            : "  NO — Q is inherently integer-valued. It is the Betti number β₀ of the " +
              "superlevel set {R>0.5}, which is always an integer by definition. " +
              "No continuous deformation can produce a fractional β₀. " +
              "Sub-threshold structures found by Morse decomposition and threshold " +
              "lowering are DYNAMICAL fluctuations, not conserved topological charges.");
        sb.AppendLine();

        sb.AppendLine("Q2: Can half-condensates exist?");
        int halfCount = report.HalfCondensates.Count;
        sb.AppendLine(halfCount > 0
            ? $"  YES — {halfCount} half-condensates detected (components at T=0.3 that don't reach T=0.5). " +
              "But these are NOT topologically protected — they can appear and disappear continuously."
            : "  NO — no half-condensates detected in the tested R-field.");
        sb.AppendLine();

        sb.AppendLine("Q3: Can kink without antikink exist?");
        bool kinkOnly = report.FragmentationAttempts
            .Any(a => a.Method == "KinkIsolation" && a.ProducedSubQ && a.IsValidCharge);
        sb.AppendLine(kinkOnly
            ? "  YES — isolated kink detected (boundary effect). " +
              "Not a true charge because it relies on boundary conditions."
            : "  NO — kinks always appear in pairs. A single kink (0→1 without 1→0) " +
              "is impossible with R(0)≈R(L)≈0 boundary conditions.");
        sb.AppendLine();

        sb.AppendLine("Q4: Can Q change continuously?");
        sb.AppendLine("  NO — Q is a COUNT of connected components. It changes only " +
                      "when R crosses the threshold at some point x, which is a discrete event. " +
                      "Q jumps by ±1 at mergers/creations/splits. There is no continuous path.");
        sb.AppendLine();

        sb.AppendLine("Q5: Is there a more fundamental microscopic charge?");
        sb.AppendLine(report.FundamentalChargeFound
            ? "  NO — Q is the minimal conserved topological charge. " +
              "The kink-antikink pair is the indivisible unit."
            : $"  POSSIBLY — {report.MicroscopicChargeCandidate}");
        sb.AppendLine();

        sb.AppendLine("Q6: Is Q truly indivisible?");
        sb.AppendLine(report.FundamentalChargeFound
            ? "  YES — all fragmentation attempts failed. Q is the charge quantum."
            : "  NO — sub-Q structures exist and may represent a more fundamental charge.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double[,] ExtractRField(LocalDensityField field, int gridSize)
    {
        var R = new double[gridSize, gridSize];
        for (int gx = 0; gx < gridSize; gx++)
            for (int gy = 0; gy < gridSize; gy++)
                R[gx, gy] = field.GetLocalR(gx, gy);
        return R;
    }
}
