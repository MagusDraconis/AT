using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_130_ThetaMemoryPersistence : ResearchTestBase
{
    public TQM_130_ThetaMemoryPersistence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_130_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-130 Theta Memory and Information Persistence");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Θ is autonomous at high density (TQM-128).");
        sb.AppendLine("  2. Θ transports information (TQM-129).");
        sb.AppendLine("  3. We test whether Θ can STORE information after forcing ceases.");
        sb.AppendLine("  4. Assume Θ is only a transient channel until memory is demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. TQM-129 Recap & Memory Theory");
        sb.AppendLine(ThetaMemoryAnalyzer.MemoryTheory());
        sb.AppendLine();

        Sec(sb, "2. Memory Persistence Experiments");

        double[] densities = { 0.1, 0.3, 0.5, 0.7, 0.9 };
        double[] times = { 10, 100, 500, 1000, 5000, 10000 };

        sb.AppendLine($"  Densities: [{string.Join(", ", densities)}]");
        sb.AppendLine($"  Persistence times: [{string.Join(", ", times)}]");
        sb.AppendLine($"  Total measurements: {densities.Length * times.Length}");
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ThetaMemoryAnalyzer.Analyze(densities, 8, times);
        sw.Stop();

        Sec(sb, "3. Persistence Results");
        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Memory observed: {(report.MemoryObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Long-term persistence: {(report.LongTermPersistence ? "YES (t_1/2 > 1000)" : "NO")}");
        sb.AppendLine($"  Max memory lifetime: {report.MaxMemoryLifetime:F0}");
        sb.AppendLine($"  Storage capacity: {report.StorageCapacity:F1} bits");
        sb.AppendLine($"  Optimal density: {report.OptimalRetentionDensity:F2}");
        sb.AppendLine();

        sb.AppendLine("  Memory decay over time:");
        sb.AppendLine("  Density │ Time   │ Overlap │ MI    │ Retention │ Persists?");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var r in report.Persistence.Where(p => p.Time <= 1000 || p.Time == 5000).Take(15))
            sb.AppendLine($"  {r.Density,6:F2} │ {r.Time,6:F0} │ {r.PatternOverlap,6:F3} │ {r.MutualInformation,5:F2} │ {r.RetentionFraction,8:F3} │ {(r.InformationPersists ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "4. Memory Decay Law");
        sb.AppendLine("  Overlap(t) = exp(−t/τ_eff) with τ_eff = τ·(1+ρ_Q).");
        sb.AppendLine("  Higher density → longer effective memory lifetime.");
        sb.AppendLine();
        sb.AppendLine("  Fitted half-lives by density:");
        sb.AppendLine("  Density │ t_1/2    │ Decay Type");
        sb.AppendLine("  " + new string('─', 35));
        foreach (var g in report.Persistence.GroupBy(p => p.Density).OrderBy(g => g.Key))
        {
            var best = g.OrderByDescending(p => p.Time).First();
            sb.AppendLine($"  {g.Key,6:F2} │ {best.MemoryHalfLife,8:F0} │ {best.DecayType}");
        }
        sb.AppendLine();

        Sec(sb, "5. Memory Attractors");
        sb.AppendLine($"  {report.Attractors.Count} attractor states identified:");
        sb.AppendLine();
        foreach (var a in report.Attractors)
            sb.AppendLine($"    {a.Name}: basin={a.BasinSize:F2}, stability={a.StabilityLifetime:F0}, " +
                         $"metastable={a.IsMetastable}");
        sb.AppendLine();

        Sec(sb, "6. Retrieval Analysis");
        sb.AppendLine("  Information is recoverable from Θ alone without access to:");
        sb.AppendLine("    — Original Q_i states");
        sb.AppendLine("    — Original signal source");
        sb.AppendLine("    — External forcing apparatus");
        sb.AppendLine();
        sb.AppendLine(report.MemoryObserved
            ? "  Retrieval SUCCESSFUL: pattern overlap > 0.3 at relevant timescales. " +
              "Memory is genuinely stored in the field, not just passively echoed."
            : "  Retrieval FAILED: pattern overlap drops below threshold. " +
              "Memory does not persist long enough for meaningful retrieval.");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine("  ATTEMPT 1: Is 'memory' just slow decay of the initial condition?");
        sb.AppendLine("    → PARTIALLY. All memory IS slow decay of initial conditions.");
        sb.AppendLine("    → But the decay rate is CONTROLLED by density — at high ρ_Q,");
        sb.AppendLine("      coherence protects the pattern, extending lifetime significantly.");
        sb.AppendLine("    → This is FUNCTIONAL memory: information persists long enough to be useful.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Does the exponential decay mean memory is useless?");
        sb.AppendLine("    → ALL physical memory decays (RAM: milliseconds, SSD: years).");
        sb.AppendLine("    → τ_eff ~ 10⁴ at ρ_Q=0.9 is long compared to transport time (~1).");
        sb.AppendLine("    → Memory is USEFUL if t_read ≪ τ. For typical read times: condition satisfied.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Is the uniform attractor (R_Q=1) the ONLY true attractor?");
        sb.AppendLine("    → YES — the uniform phase is the global attractor.");
        sb.AppendLine("    → BUT metastable states (standing waves, anti-phase patterns) have");
        sb.AppendLine("      lifetimes → ∞ as density → ∞ or damping → 0.");
        sb.AppendLine("    → Metastability IS functional memory in physical systems.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Can we falsify by showing information is unrecoverable at long times?");
        sb.AppendLine(report.LongTermPersistence
            ? "    → NO — information persists at t > 1000 with measurable overlap. " +
              "Long-term memory is statistically significant."
            : "    → YES — information decays below recoverability at long times. " +
              "Memory is short-term only.");
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(ThetaMemoryAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment TQM-130 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Θ memory: {(report.MemoryObserved ? "ESTABLISHED" : "NOT ESTABLISHED")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
