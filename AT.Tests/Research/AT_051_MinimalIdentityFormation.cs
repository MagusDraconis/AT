using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_051_MinimalIdentityFormation : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const double Beta = 0.5;
    private const int N = 200;
    private const int Seeds = 20;
    private const int BaseSeed = 510947263;

    public AT_051_MinimalIdentityFormation(ITestOutputHelper o) : base(o) { }

    // ── History definitions ──────────────────────────────────────────
    // Each history is a sequence of (delayBefore, amplitude, evolveAfter) steps.
    // "A" baseline: single phase shift +0.4, evolve 400.

    private static readonly Dictionary<string, IdentityFormationAnalyzer.PerturbationStep[]> Histories = new()
    {
        ["A_baseline"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.4, 400)
        },
        ["A_amp_0.2"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.2, 400)
        },
        ["A_amp_0.1"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.1, 400)
        },
        ["A_amp_0.05"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.05, 400)
        },
        ["A_delay_1"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(1, 0.4, 399)
        },
        ["A_delay_5"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(5, 0.4, 395)
        },
        ["A_delay_10"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(10, 0.4, 390)
        },
        ["A_delay_20"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(20, 0.4, 380)
        },
        ["A_delay_50"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(50, 0.4, 350)
        },
        ["A_2pulse"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.4, 200),
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.2, 200)
        },
        ["A_3pulse"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.4, 133),
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.2, 133),
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.1, 134)
        },
        ["A_short"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.4, 200)
        },
        ["A_long"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, 0.4, 800)
        },
        ["A_negative"] = new[] {
            new IdentityFormationAnalyzer.PerturbationStep(0, -0.4, 400)
        },
    };

    [Fact]
    public void AT_051_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-051 Minimal Identity Formation");

        report.AppendLine("AT-051: What Is the Minimum History That Creates Identity?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-044 showed AB ≠ BA. AT-050 showed identities repel.");
        report.AppendLine("  This experiment determines the SMALLEST perturbation that");
        report.AppendLine("  produces a DISTINGUISHABLE identity — the 'identity quantum.'");
        report.AppendLine();
        report.AppendLine("  Hypothesis: A critical historical difference threshold exists.");
        report.AppendLine("  Below it → same identity. Above it → distinguishable identity.");
        report.AppendLine();

        // ── Section 2: History Design ────────────────────────────────
        int totalRuns = Histories.Count * Seeds;

        AppendSection(report, "2. History Design");
        report.AppendLine($"  Baseline: A = single phase shift +0.4, evolve 400 iterations");
        report.AppendLine($"  Variants ({Histories.Count} total):");
        report.AppendLine($"    Amplitude: 0.2, 0.1, 0.05 (weaker perturbations)");
        report.AppendLine($"    Timing: delay 1, 5, 10, 20, 50 (shifted perturbation timing)");
        report.AppendLine($"    Multi-pulse: 2-pulse, 3-pulse (additional perturbations)");
        report.AppendLine($"    Duration: evolve 200 (short), evolve 800 (long)");
        report.AppendLine($"    Sign flip: -0.4 (opposite direction)");
        report.AppendLine($"  β = {Beta}, K = {K}, N = {N}");
        report.AppendLine($"  Seeds per history: {Seeds}, Total runs: {totalRuns}");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<IdentityFormationAnalyzer.FormationResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, totalRuns, idx =>
        {
            int hi = idx / Seeds, si = idx % Seeds;
            var kv = Histories.ElementAt(hi);
            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(IdentityFormationAnalyzer.GenerateFingerprint(
                kv.Key, kv.Value, Beta, K, Lambda, N, combinedSeed));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed {results.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Compute distance matrix ──────────────────────────────────
        var matrix = IdentityFormationAnalyzer.ComputeDistanceMatrix(results, Seeds);

        report.AppendLine($"  Noise floor (max intra-history dist): {matrix.NoiseFloor,10:F6}");
        report.AppendLine($"  Noise floor std:                      {matrix.NoiseFloorStd,10:F6}");
        report.AppendLine($"  Minimum distinguishable threshold:    {matrix.MinDistinguishableThreshold,10:F6}");
        report.AppendLine($"    (\u2265 noise_floor + 2\u03c3)");
        report.AppendLine();

        // ── Section 3: Identity Distances ────────────────────────────
        AppendSection(report, "3. Identity Distances (vs A_baseline)");

        int baselineIdx = matrix.HistoryNames.IndexOf("A_baseline");
        report.AppendLine("  History          │ Mean Dist │ ±Std   │ Distinguishable? │ Threshold Gap");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        var ranked = new List<(string Name, double MeanDist, double Std, bool Distinguishable, double Gap)>();
        for (int i = 0; i < matrix.HistoryNames.Count; i++)
        {
            if (i == baselineIdx) continue;
            double md = matrix.MeanDistances[baselineIdx, i];
            double sd = matrix.StdDistances[baselineIdx, i];
            bool dist = md >= matrix.MinDistinguishableThreshold;
            double gap = md - matrix.MinDistinguishableThreshold;
            ranked.Add((matrix.HistoryNames[i], md, sd, dist, gap));
        }

        foreach (var (name, md, sd, dist, gap) in ranked.OrderBy(r => r.MeanDist))
        {
            string label = dist ? "\u2713 YES" : "  no ";
            string gapStr = gap >= 0 ? $"+{gap:F6}" : $"{gap:F6}";
            report.AppendLine($"  {name,-16} │ {md,9:F6} │ {sd,6:F6} │ {label}            │ {gapStr}");
        }

        report.AppendLine();

        // ── Section 4: Sensitivity Analysis ──────────────────────────
        AppendSection(report, "4. Sensitivity Analysis");

        // Q1: How small a change creates a new identity?
        var distinguishable = ranked.Where(r => r.Distinguishable).OrderBy(r => r.MeanDist).ToList();
        var indistinguishable = ranked.Where(r => !r.Distinguishable).OrderByDescending(r => r.MeanDist).ToList();

        report.AppendLine($"  Q1: Smallest history that creates distinguishable identity:");
        if (distinguishable.Any())
        {
            var min = distinguishable.First();
            report.AppendLine($"    '{min.Name}' at distance {min.MeanDist:F6} (gap: +{min.Gap:F6})");
        }
        else
            report.AppendLine("    NONE \u2014 all variants are indistinguishable from baseline");

        report.AppendLine($"  Largest history that does NOT create distinguishable identity:");
        if (indistinguishable.Any())
        {
            var max = indistinguishable.First();
            report.AppendLine($"    '{max.Name}' at distance {max.MeanDist:F6} (gap: {max.Gap:F6})");
        }
        else
            report.AppendLine("    NONE \u2014 all variants are distinguishable from baseline");
        report.AppendLine();

        // Q2: Timing vs amplitude
        var timingDists = ranked.Where(r => r.Name.StartsWith("A_delay")).ToList();
        var ampDists = ranked.Where(r => r.Name.StartsWith("A_amp")).ToList();
        double meanTiming = timingDists.Any() ? timingDists.Average(r => r.MeanDist) : 0;
        double meanAmp = ampDists.Any() ? ampDists.Average(r => r.MeanDist) : 0;

        report.AppendLine($"  Q2: Does timing matter more than amplitude?");
        report.AppendLine($"    Mean timing variant distance:  {meanTiming:F6}");
        report.AppendLine($"    Mean amplitude variant distance: {meanAmp:F6}");
        report.AppendLine($"    {(meanTiming > meanAmp * 1.5 ? "TIMING dominates" : meanAmp > meanTiming * 1.5 ? "AMPLITUDE dominates" : "TIMING and AMPLITUDE are comparable")}");
        report.AppendLine();

        // Q3: Pulse count vs timing
        var pulseDists = ranked.Where(r => r.Name.Contains("pulse")).ToList();
        double meanPulse = pulseDists.Any() ? pulseDists.Average(r => r.MeanDist) : 0;
        report.AppendLine($"  Q3: Does pulse count matter more than timing?");
        report.AppendLine($"    Mean multi-pulse distance:     {meanPulse:F6}");
        report.AppendLine($"    Mean timing distance:          {meanTiming:F6}");
        report.AppendLine($"    {(meanPulse > meanTiming * 1.5 ? "PULSE COUNT dominates" : meanTiming > meanPulse * 1.5 ? "TIMING dominates" : "Comparable")}");
        report.AppendLine();

        // Q4: Continuous or threshold?
        var sortedDists = ranked.OrderBy(r => r.MeanDist).ToList();
        double maxBelow = indistinguishable.Any() ? indistinguishable.Max(r => r.MeanDist) : 0;
        double minAbove = distinguishable.Any() ? distinguishable.Min(r => r.MeanDist) : double.MaxValue;

        report.AppendLine($"  Q4: Is identity formation continuous or threshold-like?");
        report.AppendLine($"    Max below-threshold distance:  {maxBelow:F6}");
        if (distinguishable.Any())
        {
            report.AppendLine($"    Min above-threshold distance:  {minAbove:F6}");
            double regimeGap = minAbove - maxBelow;
            report.AppendLine($"    Gap between regimes:           {regimeGap:F6}");
            report.AppendLine($"    {(regimeGap > 0.01 ? "THRESHOLD \u2014 sharp transition between indistinguishable and distinguishable" : "CONTINUOUS \u2014 smooth gradient, no sharp threshold")}");
        }
        else
        {
            report.AppendLine($"    Min above-threshold distance:  N/A (no distinguishable variants)");
            report.AppendLine($"    Gap between regimes:           N/A");
            report.AppendLine($"    All variants fall within noise \u2014 no threshold found within tested range.");
        }
        report.AppendLine();

        // Q5: Minimum history quantum?
        report.AppendLine($"  Q5: Does a minimum history quantum exist?");
        if (distinguishable.Any())
        {
            var quantum = distinguishable.First();
            report.AppendLine($"    YES \u2014 The smallest distinguishable perturbation is '{quantum.Name}'");
            report.AppendLine($"    Quantum size: identity distance {quantum.MeanDist:F6} from baseline");
        }
        else
        {
            report.AppendLine($"    NO \u2014 No perturbation within tested range produces a distinguishable identity");
            report.AppendLine($"    The identity quantum is larger than the tested perturbations.");
        }
        report.AppendLine();

        // ── Section 5: Threshold Detection ───────────────────────────
        AppendSection(report, "5. Threshold Detection");

        report.AppendLine("  Identity Distance vs History Perturbation:");
        report.AppendLine("  (sorted by distance from baseline)");
        report.AppendLine();
        report.AppendLine("  History          │ Distance  │ Threshold Status");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (name, md, _, dist, _) in sortedDists)
        {
            string bar = new string('\u2588', Math.Min(40, (int)(md / (matrix.MinDistinguishableThreshold * 2) * 40)));
            string status = dist
                ? $"\u25C6 ABOVE threshold (+{(md - matrix.MinDistinguishableThreshold):F6})"
                : $"\u25CB BELOW threshold ({(md - matrix.MinDistinguishableThreshold):F6})";
            report.AppendLine($"  {name,-16} │ {md,8:F6} │ {bar} {status}");
        }

        report.AppendLine();
        report.AppendLine($"  Threshold line: {matrix.MinDistinguishableThreshold:F6} (noise + 2\u03c3)");
        report.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        AppendSection(report, "6. Interpretation");

        report.AppendLine($"  Threshold: {matrix.MinDistinguishableThreshold:F6}");
        report.AppendLine($"  {distinguishable.Count}/{ranked.Count} histories distinguishable from baseline");
        report.AppendLine($"  {indistinguishable.Count}/{ranked.Count} histories indistinguishable from baseline");
        report.AppendLine();

        string interpretation;
        if (distinguishable.Count == ranked.Count)
        {
            interpretation = "ALL perturbations create distinguishable identities " +
                "\u2014 even the smallest tested perturbation exceeds the noise floor.";
            report.AppendLine($"  {interpretation}");
            report.AppendLine("  The identity quantum is SMALLER than the tested perturbations.");
        }
        else if (distinguishable.Count == 0)
        {
            interpretation = "NO perturbation creates a distinguishable identity " +
                "\u2014 all tested variations fall within the noise floor.";
            report.AppendLine($"  {interpretation}");
            report.AppendLine("  The identity quantum is LARGER than the tested perturbations.");
        }
        else
        {
            interpretation = "A THRESHOLD exists between indistinguishable and distinguishable.";
            report.AppendLine($"  {interpretation}");
            report.AppendLine($"  The identity quantum lies between '{indistinguishable.First().Name}'");
            report.AppendLine($"  and '{distinguishable.First().Name}'.");
        }
        report.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        AppendSection(report, "7. Conclusion");

        report.AppendLine($"  C1. Noise floor: {matrix.NoiseFloor:F6} \u00b1 {matrix.NoiseFloorStd:F6}");
        report.AppendLine($"  C2. Minimum distinguishable threshold: {matrix.MinDistinguishableThreshold:F6}");
        report.AppendLine();

        if (distinguishable.Any())
        {
            report.AppendLine($"  C3. Smallest distinguishable perturbation: '{distinguishable.First().Name}'");
            report.AppendLine($"      at identity distance {distinguishable.First().MeanDist:F6}");
            report.AppendLine($"  C4. An identity quantum EXISTS \u2014 historical perturbations below");
            report.AppendLine($"      a critical magnitude produce the same identity; above it,");
            report.AppendLine($"      a new distinguishable identity emerges.");
        }
        else
        {
            report.AppendLine($"  C3. No identity quantum found within tested range.");
            report.AppendLine($"  C4. The identity quantum is larger than the tested");
            report.AppendLine($"      perturbations \u2014 identity is robust against small");
            report.AppendLine($"      historical variations.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-051 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
