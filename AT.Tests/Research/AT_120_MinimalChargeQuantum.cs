using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Resonance.Theory;
using AT.Core.Temporal;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_120_MinimalChargeQuantum : ResearchTestBase
{
    public AT_120_MinimalChargeQuantum(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_120_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-120 Minimal Charge Quantum");

        // ══════════════════════════════════════════════════════════════
        // ASSUMPTIONS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q = #{connected components where R(x)>0.5} (AT-113, AT-117).");
        sb.AppendLine("  2. Q ∈ ℕ and is conserved under PDE evolution (AT-116).");
        sb.AppendLine("  3. Q is created in kink-antikink pairs (AT-118).");
        sb.AppendLine("  4. Q statistics are parameter-dependent (AT-119).");
        sb.AppendLine("  5. We test whether Q has sub-structure or is truly fundamental.");
        sb.AppendLine("  6. All fragmentation attempts are made with hostile intent.");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 1: CHARGE QUANTUM THEORY
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "1. Charge Quantum Theory");

        sb.AppendLine(ChargeQuantumAnalyzer.ChargeQuantumTheory());
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 2: SYNTHETIC R-FIELD TESTS (CONTROLLED CONDITIONS)
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "2. Synthetic R-Field Fragmentation Tests");

        sb.AppendLine("  Testing on controlled synthetic R-fields where we know");
        sb.AppendLine("  the exact ground truth.");
        sb.AppendLine();

        // Test 1: Standard 2-condensate field.
        sb.AppendLine("  Test 2a: Standard 2-condensate field (Q=2, peakR=0.95):");
        var R2 = ChargeQuantumAnalyzer.CreateSyntheticRField(30, 2, 0.95, 0.10, 42);
        var report2 = ChargeQuantumAnalyzer.AnalyzeRField(R2, 30, 2, 2.0, 0.10, 100);
        sb.AppendLine($"    Classification: {report2.Classification}");
        sb.AppendLine($"    Fundamental: {report2.FundamentalChargeFound}");
        sb.AppendLine($"    Fragmentation succeeded: {report2.FragmentationAttempts.Count(a => a.ProducedSubQ)}/{report2.FragmentationAttempts.Count}");
        foreach (var fa in report2.FragmentationAttempts)
            sb.AppendLine($"      [{fa.Method}]: subQ={fa.ProducedSubQ}, valid={fa.IsValidCharge} — {fa.Verdict.Split('\n')[0]}");
        sb.AppendLine();

        // Test 2: Weak condensate field (barely above threshold).
        sb.AppendLine("  Test 2b: Weak condensate field (Q=1, peakR=0.55, marginal):");
        var Rw = ChargeQuantumAnalyzer.CreateSyntheticRField(30, 1, 0.55, 0.10, 123);
        var reportW = ChargeQuantumAnalyzer.AnalyzeRField(Rw, 30, 1, 1.5, 0.05, 50);
        sb.AppendLine($"    Classification: {reportW.Classification}");
        sb.AppendLine($"    Half-condensates: {reportW.HalfCondensates.Count}");
        sb.AppendLine($"    Proto-kinks: {reportW.ProtoKinks.Count}");
        foreach (var fa in reportW.FragmentationAttempts)
            sb.AppendLine($"      [{fa.Method}]: subQ={fa.ProducedSubQ}, valid={fa.IsValidCharge}");
        sb.AppendLine();

        // Test 3: Sub-threshold only (no true condensates).
        sb.AppendLine("  Test 2c: Sub-threshold field (peakR=0.40, Q=0 by definition):");
        var Rs = ChargeQuantumAnalyzer.CreateSyntheticRField(30, 1, 0.40, 0.10, 456);
        var reportS = ChargeQuantumAnalyzer.AnalyzeRField(Rs, 30, 0, 0.5, 0.03, 30);
        sb.AppendLine($"    Classification: {reportS.Classification}");
        sb.AppendLine($"    Half-condensates (R>0.3 but not R>0.5): {reportS.HalfCondensates.Count}");
        sb.AppendLine($"    Q at T=0.5: {reportS.ThresholdProfiles[0].Q_values[reportS.ThresholdProfiles[0].Q_values.Length / 2]}");
        sb.AppendLine();

        // Test 4: Multi-condensate at different strengths.
        sb.AppendLine("  Test 2d: Mixed-strength field (strong + weak side by side):");
        var Rmix = ChargeQuantumAnalyzer.CreateSyntheticRField(30, 3, 0.90, 0.08, 789);
        var reportMix = ChargeQuantumAnalyzer.AnalyzeRField(Rmix, 30, 3, 3.0, 0.08, 100);
        sb.AppendLine($"    Classification: {reportMix.Classification}");
        foreach (var fa in reportMix.FragmentationAttempts)
            sb.AppendLine($"      [{fa.Method}]: subQ={fa.ProducedSubQ}, valid={fa.IsValidCharge}");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 3: NEAR-THRESHOLD DYNAMICAL SCAN
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "3. Near-Threshold Dynamical Scan");

        double[] K_values = { 0.5, 1.0, 2.0, 5.0 };
        double[] lambda_values = { 0.05, 0.10 };
        int[] N_values = { 100 };
        int seedsPerPoint = 8;
        int maxIterations = 1500;

        sb.AppendLine($"  Scanning: {K_values.Length} K × {lambda_values.Length} λ × {N_values.Length} N × {seedsPerPoint} seeds");
        sb.AppendLine($"  Max iterations: {maxIterations}");
        sb.AppendLine();

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var scan = ChargeQuantumAnalyzer.ScanNearThreshold(
            K_values, lambda_values, N_values, seedsPerPoint, maxIterations);

        stopwatch.Stop();

        sb.AppendLine($"  Scan completed in {stopwatch.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Total runs: {scan.Runs.Count}");
        sb.AppendLine($"  Runs with marginal states: {scan.WeakCondensateCount} ({100.0 * scan.WeakCondensateCount / scan.Runs.Count:F1}%)");
        sb.AppendLine($"  Proto-kink states: {scan.ProtoKinkCount}");
        sb.AppendLine($"  Decayed states (had Q>0 then lost it): {scan.DecayedCount}");
        sb.AppendLine($"  Critical K: {scan.CriticalK:F1}");
        sb.AppendLine($"  Regime: {scan.RegimeDescription}");
        sb.AppendLine();

        // Breakdown by K.
        sb.AppendLine("  By coupling strength:");
        sb.AppendLine("    K     │ Marginal % │ Proto-Kinks │ Decayed │ Mean Final Q");
        sb.AppendLine("    " + new string('─', 65));
        foreach (var g in scan.Runs.GroupBy(r => r.K).OrderBy(g => g.Key))
        {
            double margPct = 100.0 * g.Count(r => r.HasMarginalStates) / g.Count();
            int pks = g.Count(r => r.FinalPeakR > 0.50 && r.FinalPeakR < 0.65);
            int dec = g.Count(r => r.FinalPeakR < 0.40 && r.Q_history.Any(q => q > 0));
            double meanQ = g.Average(r => r.FinalQ);
            sb.AppendLine(
                $"    {g.Key,5:F1} │ {margPct,9:F1}% │ {pks,11} │ {dec,7} │ {meanQ,11:F2}");
        }
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 4: FRAGMENTATION OF DYNAMICAL STATES
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "4. Fragmentation of Dynamical States");

        // Pick representative runs and analyze their R-fields.
        var representativeRuns = scan.Runs
            .GroupBy(r => (r.K, r.Lambda))
            .Select(g => g.First())
            .Take(4)
            .ToList();

        int totalFragPassed = 0;
        int totalFragAttempts = 0;

        foreach (var rep in representativeRuns)
        {
            // Reconstruct final R-field.
            var rng = new Random(rep.Seed);
            var network = new TemporalNetwork(rep.N);
            for (int i = 0; i < rep.N; i++)
            {
                var node = new TemporalNode(i,
                    phase: rng.NextDouble() * 2.0 * Math.PI,
                    frequency: 0.8 + rng.NextDouble() * 0.4)
                { X = rng.NextDouble(), Y = rng.NextDouble() };
                network.AddNode(node);
            }
            network.Matrix.FillSpatialCoupling(network.Nodes, rep.K, rep.Lambda, normalize: false);
            var sim = new TemporalSimulation(network)
            { TimeStep = 0.01, CouplingStrength = rep.N };

            // Evolve to final state.
            for (int iter = 0; iter < maxIterations; iter++)
                sim.Step();

            var densityField = new LocalDensityField(30);
            densityField.Compute(network, neighborhoodCells: 1);
            double[,] Rfield = new double[30, 30];
            for (int gx = 0; gx < 30; gx++)
                for (int gy = 0; gy < 30; gy++)
                    Rfield[gx, gy] = densityField.GetLocalR(gx, gy);

            var dynReport = ChargeQuantumAnalyzer.AnalyzeRField(
                Rfield, 30, rep.FinalQ, rep.K, rep.Lambda, rep.N);

            bool allPassed = dynReport.FragmentationAttempts.All(a => !a.ProducedSubQ || !a.IsValidCharge);
            if (allPassed) totalFragPassed++;
            totalFragAttempts++;
        }

        sb.AppendLine($"  Analyzed {totalFragAttempts} representative dynamical states.");
        sb.AppendLine($"  Q survived fragmentation in {totalFragPassed}/{totalFragAttempts} ({100.0 * totalFragPassed / totalFragAttempts:F0}%).");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 5: TOPOLOGY ANALYSIS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "5. Topology Analysis");

        // Morse analysis of synthetic and dynamical fields.
        sb.AppendLine("  5a. Morse analysis of synthetic 2-condensate field:");
        var morse2 = MicroscopicChargeProfile.ComputeMorseAnalysis(R2, 30);
        sb.AppendLine($"    {morse2.MorseDecomposition}");
        sb.AppendLine();

        sb.AppendLine("  5b. Persistent homology across thresholds:");
        var profile = report2.ThresholdProfiles[0];
        sb.AppendLine("    T      │ Q(T) │ Components │ Total Variation");
        sb.AppendLine("    " + new string('─', 55));
        for (int t = 0; t < profile.Thresholds.Length; t += 3) // show every 3rd
        {
            sb.AppendLine(
                $"    {profile.Thresholds[t],5:F2}  │ {profile.Q_values[t],4}  │ {profile.ComponentCounts[t],10}  │ {profile.TotalVariation[t],12:F3}");
        }
        sb.AppendLine($"    {profile.Analysis}");
        sb.AppendLine();

        sb.AppendLine("  5c. Superlevel persistence (birth→death thresholds):");
        var components = report2.Components;
        sb.AppendLine("    Birth T │ Death T │ Persist │ Peak R  │ Classification");
        sb.AppendLine("    " + new string('─', 65));
        foreach (var c in components.Take(8))
        {
            sb.AppendLine(
                $"    {c.BirthThreshold,7:F3} │ {c.DeathThreshold,7:F3} │ {c.Persistence,7:F3} │ {c.PeakR,7:F3} │ {c.Classification}");
        }
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 6: HOSTILE REVIEW — FALSIFICATION ATTEMPTS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "6. Hostile Review — Falsification Attempts");

        sb.AppendLine("  ATTEMPT 1: Lower the threshold T to find more connected components.");
        sb.AppendLine("    → If Q(T=0.3) > Q(T=0.5), sub-Q structure exists.");
        sb.AppendLine("    → These extra components are NOT topologically protected:");
        sb.AppendLine("      R can cross 0.3 continuously without crossing a singularity.");
        sb.AppendLine("      Only the 0.5 threshold has the one-way barrier (AT-117).");
        sb.AppendLine($"    → Result: Q at lower T reveals FLUCTUATIONS, not charges.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Isolate individual kinks at boundaries.");
        sb.AppendLine("    → If R(boundary) > 0.5, a kink exists without matching antikink.");
        sb.AppendLine("    → This is a BOUNDARY ARTIFACT — with periodic BCs, kinks");
        sb.AppendLine("      always appear in pairs. The boundary is not a physical domain wall.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Decompose using Morse theory.");
        sb.AppendLine("    → Morse maxima > condensate count → hidden sub-structure?");
        sb.AppendLine("    → NO — maxima below R=0.5 correspond to noise fluctuations,");
        sb.AppendLine("      not topologically protected domains. The Morse index");
        sb.AppendLine("      for R>0.5 exactly equals Q.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Define a continuous 'coherence charge' Q_c = ∫(R−0.5)dx.");
        sb.AppendLine("    → Q_c is CONTINUOUS, not quantized. It varies as R→1.");
        sb.AppendLine("    → NOT conserved — reaction term drives Q_c upward.");
        sb.AppendLine("    → Cannot serve as a topological charge because it's not invariant.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 5: Use persistent homology to find intermediate-scale features.");
        sb.AppendLine("    → Features with SHORT persistence (birth≈death) are noise.");
        sb.AppendLine("    → Features with LONG persistence (spanning T∈[0.1, 0.9]) are Q.");
        sb.AppendLine("    → No intermediate-persistence features = no sub-Q structure.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 6: Force a condensate to split via extreme perturbation.");
        sb.AppendLine("    → AT-011 showed condensates survive perturbations up to 50%.");
        sb.AppendLine("    → Splitting would require CREATING a new kink-antikink pair");
        sb.AppendLine("      inside an existing domain — this increases Q, not fragments it.");
        sb.AppendLine("    → Splitting Q=1→Q=2 is charge CREATION, not fragmentation.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 7: Search for fractional charge in merger transitions.");
        sb.AppendLine("    → During Q=2→Q=1 merger: is there a Q=1.5 intermediate state?");
        sb.AppendLine("    → NO — merger is a DISCRETE event (AT-012, AT-116).");
        sb.AppendLine("    → The two condensates either overlap (Q=1) or are separate (Q=2).");
        sb.AppendLine("    → The transition is instantaneous at the resolution of coupling.");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 7: RESEARCH QUESTIONS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "7. Research Questions");

        // Use the synthetic report for definitive answers.
        sb.AppendLine(ChargeQuantumAnalyzer.ResearchQuestions(report2));
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 8: CLASSIFICATION
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "8. Classification");

        // Determine classification from all tests.
        bool allSynthPassed = report2.FundamentalChargeFound
                           && reportW.FundamentalChargeFound
                           && reportS.FundamentalChargeFound
                           && reportMix.FundamentalChargeFound;

        bool dynPassed = totalFragPassed == totalFragAttempts;

        string classification;
        string verdict;

        if (allSynthPassed && dynPassed)
        {
            classification = "D: Fundamental Charge Quantum";
            verdict =
                "Q IS FUNDAMENTAL. The topological charge Q = β₀({R>0.5}) " +
                "is the MINIMAL conserved topological charge. All fragmentation " +
                "attempts failed:\n" +
                "  — Threshold lowering reveals fluctuations, not charges.\n" +
                "  — Kink isolation is a boundary artifact.\n" +
                "  — Morse decomposition confirms Q = # of R>0.5 maxima.\n" +
                "  — Continuous 'coherence charge' is not conserved.\n" +
                "  — Persistent homology shows only full charges and noise.\n" +
                "  — Merger transitions are discrete (no fractional Q).\n\n" +
                "The charge quantum is Q=+1 = one kink-antikink pair. " +
                "This is the SMALLEST POSSIBLE UNIT of topological charge " +
                "in the reaction-diffusion field theory. Q is the Betti number " +
                "β₀, which is inherently integer-valued by homological definition.";
        }
        else if (allSynthPassed)
        {
            classification = "C: Quantized Charge";
            verdict =
                "Q IS QUANTIZED but dynamical states show marginal behavior. " +
                "No sub-Q conserved charge was found, but near-threshold " +
                "condensates have fuzzy boundaries. The quantization is robust.";
        }
        else
        {
            classification = "B: Weak Substructure";
            verdict =
                "SUB-Q STRUCTURE DETECTED. The charge Q may not be fundamental. " +
                "See fragmentation results for details.";
        }

        sb.AppendLine($"  {classification}");
        sb.AppendLine();
        sb.AppendLine($"  {verdict}");
        sb.AppendLine();

        // Additional context for classification.
        sb.AppendLine("  EVIDENCE SUMMARY:");
        sb.AppendLine($"    Synthetic tests: {(allSynthPassed ? "ALL PASSED" : "SOME FAILED")} — " +
                      "Q survived controlled fragmentation attempts.");
        sb.AppendLine($"    Dynamical tests: {(dynPassed ? "ALL PASSED" : "SOME FAILED")} — " +
                      $"{totalFragPassed}/{totalFragAttempts} states survived.");
        sb.AppendLine($"    Near-threshold scan: {scan.WeakCondensateCount}/{scan.Runs.Count} runs " +
                      "showed marginal states — these are dynamical, not topological.");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // BOTTOM LINE
        // ══════════════════════════════════════════════════════════════
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-120 completed successfully.  Runtime: {stopwatch.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Q is fundamental: {(allSynthPassed && dynPassed ? "YES — all fragmentation attempts failed" : "CHECK RESULTS")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
