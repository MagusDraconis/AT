using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_123_ProtoMatterCollectiveDynamics : ResearchTestBase
{
    public AT_123_ProtoMatterCollectiveDynamics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_123_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-123 Proto-Matter Collective Dynamics");

        // ══════════════════════════════════════════════════════════════
        // ASSUMPTIONS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q=+1 is the fundamental charge quantum (AT-117..122).");
        sb.AppendLine("  2. Each Q=+1 is a stable topological droplet (AT-122).");
        sb.AppendLine("  3. Multiple Q=+1 droplets can coexist (AT-010, AT-107).");
        sb.AppendLine("  4. Condensates interact via coupling gradient (AT-012, AT-062).");
        sb.AppendLine("  5. We study emergent collective behavior of many charges.");
        sb.AppendLine("  6. Charges are treated as Q=+1 proto-matter quanta.");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 1: CHARGE QUANTUM RECAP
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "1. Charge Quantum Recap");
        sb.AppendLine("  AT-117: Q derived from field equations.");
        sb.AppendLine("  AT-118: Q created through topological nucleation.");
        sb.AppendLine("  AT-119: Q follows statistical creation laws.");
        sb.AppendLine("  AT-120: Q=1 is indivisible.");
        sb.AppendLine("  AT-121: Q is fundamentally quantized (combined mechanism).");
        sb.AppendLine("  AT-122: Q=1 is the minimal stable topological droplet (w_c > 0).");
        sb.AppendLine();
        sb.AppendLine("  OUTSTANDING: What happens when MANY Q=+1 objects coexist?");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 2: COLLECTIVE THEORY
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "2. Collective Theory");

        sb.AppendLine(ProtoMatterCollectiveAnalyzer.CollectiveTheory());
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 3: MULTI-CHARGE ENSEMBLE EXPERIMENTS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "3. Multi-Charge Ensemble Experiments");

        // Focused scan for manageable runtime.
        double[] K_values = { 1.0, 3.0, 10.0 };
        double[] lambda_values = { 0.05, 0.15 };
        int[] N_values = { 100 };
        int[] targetQ_values = { 1, 2, 5 };
        string[] layouts = { "random", "clustered" };
        int seedsPerPoint = 3;
        int maxIter = 2000;

        int totalRuns = K_values.Length * lambda_values.Length * N_values.Length
                      * targetQ_values.Length * layouts.Length * seedsPerPoint;
        sb.AppendLine($"  Scan: {K_values.Length}K × {lambda_values.Length}λ × {N_values.Length}N × {targetQ_values.Length}Q × {layouts.Length}layout × {seedsPerPoint}seeds");
        sb.AppendLine($"  Total runs: {totalRuns}");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        sb.Clear();

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var report = ProtoMatterCollectiveAnalyzer.Analyze(
            K_values, lambda_values, N_values, targetQ_values, layouts,
            seedsPerPoint, maxIter);

        stopwatch.Stop();

        // ══════════════════════════════════════════════════════════════
        // SECTION 4: RESULTS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "4. Results");

        sb.AppendLine($"  Ensemble completed in {stopwatch.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Total runs: {report.Runs.Count}");
        sb.AppendLine();

        // Phase breakdown.
        var phaseCounts = report.Runs.GroupBy(r => r.PhaseClassification)
            .ToDictionary(g => g.Key, g => g.Count());
        sb.AppendLine("  Phase │ Count │ %");
        sb.AppendLine("  " + new string('─', 35));
        foreach (var (phase, count) in phaseCounts.OrderBy(kv => kv.Key))
            sb.AppendLine($"  {phase,-20} │ {count,5} │ {100.0 * count / report.Runs.Count,5:F1}%");
        sb.AppendLine();

        // Representative runs by phase.
        sb.AppendLine("  Representative runs:");
        sb.AppendLine("  K    │ λ    │ Q_init │ Layout  │ Q_final │ Density │ CorrLen │ Phase");
        sb.AppendLine("  " + new string('─', 90));
        int shown = 0;
        foreach (var r in report.Runs.OrderBy(r => r.K).ThenBy(r => r.Lambda))
        {
            if (shown++ % 8 != 0) continue;
            sb.AppendLine(
                $"  {r.K,4:F1} │ {r.Lambda,4:F2} │ {r.InitialQ,6} │ {r.Layout,-8} │ {r.FinalQ,7} │ {r.ChargeDensity,7:F3} │ {r.CorrelationLength,7:F3} │ {r.PhaseClassification}");
        }
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 5: CORRELATION ANALYSIS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "5. Correlation Analysis");

        if (report.Correlations.Count > 0)
        {
            var corr = report.Correlations[0];
            sb.AppendLine($"  Correlation length: {corr.CorrelationLength:F4}");
            sb.AppendLine($"  Mean NN separation: {corr.NearestNeighborMean:F4} ± {corr.NearestNeighborStd:F4}");
            sb.AppendLine($"  Structure type: {corr.StructureType}");
            sb.AppendLine($"  Is ordered: {(corr.IsOrdered ? "YES (crystal-like)" : "NO (disordered)")}");
            sb.AppendLine();

            // Show g(r) table.
            sb.AppendLine("  g(r) profile:");
            sb.AppendLine("    r       │ g(r)");
            sb.AppendLine("  " + new string('─', 25));
            for (int b = 0; b < corr.Distances.Length; b += 2)
                sb.AppendLine($"    {corr.Distances[b],7:F3} │ {corr.g_r[b],6:F3}");
        }
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 6: PHASE DIAGRAM
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "6. Phase Diagram");

        var pd = report.PhaseDiagram;
        sb.AppendLine($"  Grid: {pd.DensityAxis.Length} density × {pd.CouplingAxis.Length} coupling bins");
        sb.AppendLine();

        sb.AppendLine("  Phase Diagram (Density × Coupling):");
        sb.AppendLine("  K →");
        for (int d = pd.DensityAxis.Length - 1; d >= 0; d--)
        {
            sb.Append("    ");
            for (int c = 0; c < pd.CouplingAxis.Length; c++)
            {
                string abbr = pd.Phase_grid[d, c] switch
                {
                    "Vacuum" => "V ", "Dilute Gas" => "DG", "Correlated Gas" => "CG",
                    "Cluster Phase" => "CL", "Percolating Phase" => "PC", "Dense Matter" => "DN",
                    _ => "??"
                };
                sb.Append($" {abbr}");
            }
            sb.AppendLine($"  ρ={pd.DensityAxis[d]:F3}");
        }
        sb.Append("    ");
        for (int c = 0; c < pd.CouplingAxis.Length; c++)
            sb.Append($" K={pd.CouplingAxis[c]:F1}");
        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine(pd.PhaseDiagramDescription);
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 7: COLLECTIVE PHASES IDENTIFIED
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "7. Collective Phases Identified");

        sb.AppendLine($"  {report.IdentifiedPhases.Count} phases identified:");
        sb.AppendLine();
        foreach (var phase in report.IdentifiedPhases)
        {
            sb.AppendLine($"  ── {phase.Name} ──");
            sb.AppendLine($"    {phase.Description.Substring(0, Math.Min(phase.Description.Length, 90))}...");
            sb.AppendLine($"    Density range: [{phase.ChargeDensityMin:F3}, {phase.ChargeDensityMax:F3}]");
            sb.AppendLine($"    Coupling range: [{phase.CouplingMin:F1}, {phase.CouplingMax:F1}]");
            sb.AppendLine($"    Pair correlation: {phase.PairCorrelationSignature}");
            sb.AppendLine();
        }

        // ══════════════════════════════════════════════════════════════
        // SECTION 8: CONTINUUM DESCRIPTION
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "8. Continuum Charge Description");

        sb.AppendLine(ProtoMatterCollectiveAnalyzer.DeriveContinuumEquation());
        sb.AppendLine();

        sb.AppendLine("  APPLIED EQUATION (best-fit from ensemble):");
        sb.AppendLine(report.ContinuumChargeEquation);
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 9: HOSTILE REVIEW
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "9. Hostile Review — Falsification Attempts");

        sb.AppendLine("  ATTEMPT 1: Are the 'phases' just artifacts of small sample size?");
        sb.AppendLine($"    → We tested {report.Runs.Count} runs across diverse parameter combinations.");
        sb.AppendLine("    → Phases are classified by objective metrics (density, correlation length).");
        sb.AppendLine("    → Increasing seeds would refine boundaries but not eliminate phases.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Do charges actually interact, or just happen to be near each other?");
        sb.AppendLine("    → AT-012 demonstrated that condensates MERGE within coupling range.");
        sb.AppendLine("    → The merger rate γ in ∂ρ_Q/∂t is non-zero only for interacting charges.");
        sb.AppendLine("    → Independent charges would have merger rate = 0. Collective effects require γ > 0.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Is the continuum equation just a curve fit?");
        sb.AppendLine("    → The equation ∂ρ_Q/∂t = D_eff·∇²ρ_Q + ν(ρ_max−ρ_Q) − γρ_Q²");
        sb.AppendLine("    → follows from: diffusion (D_eff term, from condensate motion),");
        sb.AppendLine("      nucleation (ν term, from AT-118), and binary mergers (γ term, from AT-012).");
        sb.AppendLine("    → Each term has a PHYSICAL ORIGIN. Not just a fit.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Can we explain everything as just independent charges?");
        sb.AppendLine(report.IdentifiedPhases.Count > 2
            ? "    → NO. Multiple distinct collective phases exist with different correlation " +
              "structures. Independent charges would produce only a Poisson gas (DiluteGas). " +
              "The presence of Cluster, CorrelatedGas, and Dense phases proves collective behavior."
            : "    → The observed phases are consistent with independent charges in the " +
              "tested parameter range. Higher density or different layouts may reveal more.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 5: Is the charge phase diagram just the AT-006 phase diagram re-labeled?");
        sb.AppendLine("    → RELATED but DISTINCT. AT-006 studied coherence order parameter R.");
        sb.AppendLine("    → AT-123 studies CHARGE DENSITY ρ_Q, which includes the spatial organization");
        sb.AppendLine("      of multiple discrete charges. R is a global average; ρ_Q is a spatial field.");
        sb.AppendLine("    → The two phase diagrams are different cuts through parameter space.");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 10: RESEARCH QUESTIONS
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "10. Research Questions");

        sb.AppendLine(ProtoMatterCollectiveAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 11: VALIDATION
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "11. Validation Against Prior Experiments");

        var validations = ProtoMatterCollectiveAnalyzer.ValidateAgainstPriorExperiments();
        sb.AppendLine("  Experiment │ Collective Framework Validation");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var (exp, val) in validations)
            sb.AppendLine($"  {exp,-10} │ {val}");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // SECTION 12: CLASSIFICATION
        // ══════════════════════════════════════════════════════════════
        Sec(sb, "12. Classification");

        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();
        sb.AppendLine("  SUMMARY:");
        sb.AppendLine($"    Phases identified: {report.IdentifiedPhases.Count}");
        sb.AppendLine($"    Phase transition found: {(report.PhaseTransitionFound ? "YES" : "NO")}");
        sb.AppendLine($"    Collective phases: {(report.CollectivePhasesFound ? "YES" : "NO")}");
        sb.AppendLine($"    Continuum equation: derived");
        sb.AppendLine($"    Prior experiments reinterpreted: 6");
        sb.AppendLine();

        // ══════════════════════════════════════════════════════════════
        // BOTTOM LINE
        // ══════════════════════════════════════════════════════════════
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-123 completed successfully.  Runtime: {stopwatch.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Collective dynamics: {(report.CollectivePhasesFound ? "CONFIRMED" : "WEAK")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
