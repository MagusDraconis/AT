using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

/// <summary>
/// TQM-006: Resonance Phase Transition
///
/// Parameter sweep over (N, connection density ρ, coupling strength K)
/// to determine whether a critical resonance density ρc exists above which
/// stable, long-lived synchronization clusters emerge from transient noise.
/// </summary>
public class TQM_006_ResonancePhaseTransition : ResearchTestBase
{
    // Reduced parameter space for research-test runtime.
    private static readonly int[] Ns = { 25, 50, 100, 200 };
    private static readonly double[] Densities = { 0.05, 0.10, 0.15, 0.20, 0.25, 0.30, 0.40, 0.50, 0.60, 0.80, 1.00 };
    private static readonly double[] Couplings = { 0.5, 1.0, 2.0, 3.0, 5.0 };

    public TQM_006_ResonancePhaseTransition(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TQM_006_RunResonancePhaseTransition()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
            ExecuteExperiment();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();

        // ── Header ──────────────────────────────────────────────
        PrintHeader("TQM-006 Resonance Phase Transition");
        report.AppendLine("TQM-006: Critical Resonance Density in Kuramoto Networks");
        report.AppendLine();

        // ── 1. Objective ────────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  Investigate whether a critical connection density ρc exists above which");
        report.AppendLine("  stable, long-lived synchronization clusters emerge spontaneously in a");
        report.AppendLine("  Kuramoto-coupled oscillator network.");
        report.AppendLine();
        report.AppendLine("  Central question:");
        report.AppendLine("    Is there a sharp phase transition between transient synchronization");
        report.AppendLine("    (low ρ) and persistent, coherent cluster formation (high ρ)?");
        report.AppendLine();

        // ── 2. Experimental Setup ───────────────────────────────
        AppendSection(report, "2. Experimental Setup");

        report.AppendLine($"  Parameter space:");
        report.AppendLine($"    N (oscillators)    : [{string.Join(", ", Ns)}]");
        report.AppendLine($"    ρ (density)        : [{string.Join(", ", Densities.Select(d => d.ToString("F2")))}]");
        report.AppendLine($"    K (coupling)       : [{string.Join(", ", Couplings)}]");
        report.AppendLine($"    Total combinations : {Ns.Length} × {Densities.Length} × {Couplings.Length} = {Ns.Length * Densities.Length * Couplings.Length}");
        report.AppendLine();
        report.AppendLine($"  Simulation:");
        report.AppendLine($"    Iterations         : 5000");
        report.AppendLine($"    Time step Δt       : 0.01");
        report.AppendLine($"    Dynamics           : Kuramoto (direct coupling, no field)");
        report.AppendLine($"    Coupling matrix    : sparse symmetric, density ρ");
        report.AppendLine($"    Cluster detection  : phase-proximity graph (window = 0.3 rad)");
        report.AppendLine($"    Cluster threshold  : internal sync R ≥ 0.90, size ≥ 2");
        report.AppendLine();

        // ── Run sweep ───────────────────────────────────────────
        var scanner = new CriticalDensityScanner
        {
            SimulationIterations = 5000,
            RandomSeed = 314,
            TimeStep = 0.01,
            CheckpointInterval = 500
        };

        report.AppendLine("  Running parameter sweep...");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var results = scanner.Sweep(Ns, Densities, Couplings);
        sw.Stop();
        report.AppendLine($"  Sweep completed in {sw.ElapsedMilliseconds} ms ({results.Count} parameter points).");
        report.AppendLine();

        // ── 3. Density Scan Results ─────────────────────────────
        AppendSection(report, "3. Density Scan Results");

        // Show summary for K=2.0 (representative coupling).
        double showcaseK = 2.0;
        report.AppendLine($"  Global synchronization R for K = {showcaseK}:");
        report.AppendLine("  N \\ ρ │ " + string.Join(" ", Densities.Select(d => $"{d,6:F2}")));
        report.AppendLine("  ──────┼" + new string('─', Densities.Length * 7));

        foreach (int n in Ns)
        {
            report.Append($"  {n,5} │");
            foreach (double rho in Densities)
            {
                var key = new CriticalDensityScanner.ScanPoint(n, rho, showcaseK);
                if (results.TryGetValue(key, out var r))
                    report.Append($" {r.GlobalR,6:F3}");
                else
                    report.Append("     -");
            }
            report.AppendLine();
        }

        report.AppendLine();

        // Full cross-section: max cluster lifetime for all N, all K.
        report.AppendLine("  Max cluster lifetime (iterations) — full matrix:");
        report.AppendLine("  N=100, all K:");

        report.Append("  ρ \\ K  │");
        foreach (double k in Couplings)
            report.Append($"{k,8:F1} ");
        report.AppendLine();
        report.Append("  ───────┼");
        report.Append(new string('─', Couplings.Length * 9));
        report.AppendLine();

        int nShow = 100;
        foreach (double rho in Densities)
        {
            report.Append($"  {rho,5:F2} │");
            foreach (double k in Couplings)
            {
                var key = new CriticalDensityScanner.ScanPoint(nShow, rho, k);
                if (results.TryGetValue(key, out var r))
                    report.Append($"{r.MaxClusterLifetime,8:F0} ");
                else
                    report.Append("       - ");
            }
            report.AppendLine();
        }

        report.AppendLine();

        // ── 4. Cluster Lifetime Analysis ────────────────────────
        AppendSection(report, "4. Cluster Lifetime Analysis");

        report.AppendLine("  Mean cluster lifetime vs. connection density (K = 2.0):");
        report.AppendLine("  N \\ ρ │ " + string.Join(" ", Densities.Select(d => $"{d,6:F2}")));
        report.AppendLine("  ──────┼" + new string('─', Densities.Length * 7));

        foreach (int n in Ns)
        {
            report.Append($"  {n,5} │");
            foreach (double rho in Densities)
            {
                var key = new CriticalDensityScanner.ScanPoint(n, rho, showcaseK);
                if (results.TryGetValue(key, out var r))
                    report.Append($" {r.MeanClusterLifetime,6:F0}");
                else
                    report.Append("     -");
            }
            report.AppendLine();
        }

        report.AppendLine();

        // ── 5. Critical Density Detection ───────────────────────
        AppendSection(report, "5. Critical Density Detection");

        int lifetimeThreshold = 1000; // 20% of total iterations

        report.AppendLine($"  Critical density ρc: first ρ where max lifetime ≥ {lifetimeThreshold}");
        report.AppendLine("  K \\ N │ " + string.Join(" ", Ns.Select(n => $"{n,8}")));
        report.AppendLine("  ──────┼" + new string('─', Ns.Length * 9));

        var criticalDensities = new Dictionary<(double K, int N), double>();

        foreach (double k in Couplings)
        {
            report.Append($"  {k,5:F1} │");
            foreach (int n in Ns)
            {
                double rhoC = double.NaN;
                foreach (double rho in Densities)
                {
                    var key = new CriticalDensityScanner.ScanPoint(n, rho, k);
                    if (results.TryGetValue(key, out var r) && r.MaxClusterLifetime >= lifetimeThreshold)
                    {
                        rhoC = rho;
                        break;
                    }
                }

                if (!double.IsNaN(rhoC))
                {
                    criticalDensities[(k, n)] = rhoC;
                    report.Append($" {rhoC,8:F2}");
                }
                else
                {
                    report.Append($"       -");
                }
            }
            report.AppendLine();
        }

        report.AppendLine();

        if (criticalDensities.Count > 0)
        {
            double avgRhoC = criticalDensities.Values.Average();
            double minRhoC = criticalDensities.Values.Min();
            double maxRhoC = criticalDensities.Values.Max();

            report.AppendLine($"  Candidate critical density range: ρc ∈ [{minRhoC:F2}, {maxRhoC:F2}]");
            report.AppendLine($"  Mean critical density            : ρ̄c = {avgRhoC:F2}");
            report.AppendLine();
            report.AppendLine($"  Critical densities found in {criticalDensities.Count} of {Couplings.Length * Ns.Length}");
            report.AppendLine($"  (N, K) combinations.");
        }
        else
        {
            report.AppendLine("  No critical density found — long-lived clusters did not form");
            report.AppendLine("  in any parameter combination.");
        }

        report.AppendLine();

        // ── 6. Persistence Analysis ─────────────────────────────
        AppendSection(report, "6. Persistence Analysis");

        report.AppendLine("  Persistence score vs. density for N=100 (K=2.0):");
        report.AppendLine("    ρ    │ Total Clusters │ Long-Lived │ Mean Persistence │ Max Lifetime");
        report.AppendLine("  ───────┼────────────────┼────────────┼──────────────────┼──────────────");

        foreach (double rho in Densities)
        {
            var key = new CriticalDensityScanner.ScanPoint(100, rho, 2.0);
            if (results.TryGetValue(key, out var r))
            {
                report.AppendLine(
                    $"  {rho,5:F2} │ {r.TotalClustersDetected,14} │ {r.LongLivedClusterCount,10} │ {r.MeanPersistenceScore,16:F4} │ {r.MaxClusterLifetime,12}");
            }
        }

        report.AppendLine();

        // Show cluster count vs N at fixed density.
        double fixedRho = 0.30;
        report.AppendLine($"  Cluster count vs. N at ρ = {fixedRho:F2} (K=2.0):");
        report.AppendLine("    N   │ Total Clusters │ Max Size │ Max Lifetime │ Mean Persistence");
        report.AppendLine("  ──────┼────────────────┼──────────┼──────────────┼─────────────────");

        foreach (int n in Ns)
        {
            var key = new CriticalDensityScanner.ScanPoint(n, fixedRho, 2.0);
            if (results.TryGetValue(key, out var r))
            {
                report.AppendLine(
                    $"  {n,5} │ {r.TotalClustersDetected,14} │ {r.MaxClusterSize,8:F0} │ {r.MaxClusterLifetime,12:F0} │ {r.MeanPersistenceScore,15:F4}");
            }
        }

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        bool hasPhaseTransition = criticalDensities.Count >= 5; // at least 5 (K,N) combos show transition
        bool hasSharpTransition = criticalDensities.Count > 0
            && criticalDensities.Values.Max() - criticalDensities.Values.Min() < 0.3;

        report.AppendLine("  Phase transition assessment:");
        report.AppendLine();

        if (hasPhaseTransition)
        {
            report.AppendLine($"    A critical density transition IS observed. At low ρ,");
            report.AppendLine($"    synchronization is transient — clusters form briefly and dissolve.");
            report.AppendLine($"    Above ρc ≈ {criticalDensities.Values.Average():F2}, long-lived coherent clusters");
            report.AppendLine($"    emerge and persist for thousands of iterations.");
            report.AppendLine();

            if (hasSharpTransition)
            {
                report.AppendLine("    The transition is SHARP — the critical density is well-defined");
                report.AppendLine("    across different (N, K) values, suggesting a true phase transition.");
            }
            else
            {
                report.AppendLine("    The transition is GRADUAL — ρc varies significantly across");
                report.AppendLine("    (N, K) values, suggesting the transition is sensitive to parameters.");
            }

            report.AppendLine();
            report.AppendLine("    Physical analogy: This resembles the percolation threshold in random");
            report.AppendLine("    networks and the synchronization transition in the Kuramoto model.");
            report.AppendLine("    The critical density ρc marks the point where the coupling graph");
            report.AppendLine("    becomes sufficiently connected to support global coherent modes.");
        }
        else
        {
            report.AppendLine("    No clear critical density transition was observed. Clusters remained");
            report.AppendLine("    transient across the parameter space, suggesting that either:");
            report.AppendLine("      • More iterations are needed (5000 may be insufficient)");
            report.AppendLine("      • Higher coupling strengths or densities are needed");
            report.AppendLine("      • The random coupling graph topology inhibits stable clustering");
        }

        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. {(hasPhaseTransition ? "A critical density transition IS" : "No clear critical density transition was")} identified.");
        report.AppendLine();

        if (hasPhaseTransition)
        {
            report.AppendLine($"  C2. The candidate critical density is ρc ≈ {criticalDensities.Values.Average():F2}.");
            report.AppendLine($"      Below this threshold, synchronization clusters are transient.");
            report.AppendLine($"      Above this threshold, stable long-lived clusters form spontaneously.");
            report.AppendLine();
            report.AppendLine("  C3. The existence of a phase transition supports the hypothesis that");
            report.AppendLine("      stable resonant structures emerge only above a critical coupling density —");
            report.AppendLine("      a necessary condition for matter-like self-organization in the TQM framework.");
        }
        else
        {
            report.AppendLine("  C2. The absence of a sharp transition at N ≤ 200 and K ≤ 5.0 suggests");
            report.AppendLine("      that the random-graph Kuramoto model may require larger systems or");
            report.AppendLine("      structured (non-random) coupling topologies to produce persistent clusters.");
            report.AppendLine();
            report.AppendLine("  C3. This negative result is valuable: it constrains the parameter regime");
            report.AppendLine("      where matter-like structures CAN form, guiding future TQM experiments");
            report.AppendLine("      toward structured coupling and field-mediated resonance.");
        }

        report.AppendLine();
        report.AppendLine("  C4. Cluster formation requires:");
        report.AppendLine("      • Sufficient connection density (ρ > ρc)");
        report.AppendLine("      • Adequate coupling strength (K ≥ threshold)");
        report.AppendLine("      • Enough oscillators (N affects statistical significance)");
        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • TQM-007: Structured coupling topologies at critical density.");
        report.AppendLine("    • TQM-008: Cluster-cluster interactions (proto-particle collisions).");
        report.AppendLine("    • TQM-009: Field-mediated cluster formation (resonance + synchronization).");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-006 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
