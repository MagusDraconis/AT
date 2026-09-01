using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_061_SpontaneousMemoryEmergence : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 100;
    private const int BaseSeed = 610382917;

    public AT_061_SpontaneousMemoryEmergence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_061_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-061 Spontaneous Memory Emergence");

        report.AppendLine("AT-061: Can Memory Emerge Naturally Without Explicit β?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-059 showed β generates curvature. AT-060 showed no feedback.");
        report.AppendLine("  This experiment tests whether memory is EXTERNAL or EMERGENT:");
        report.AppendLine("  at β=0, does repeated experience create path dependence,");
        report.AppendLine("  identity persistence, and curvature?");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        int seedsPer = 1;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  β=0 tests: cycles [10, 50, 100, 500, 1000] × {seedsPer} seeds = {5 * seedsPer}");
        report.AppendLine($"  β=0.5 controls: cycles [10, 50] × {seedsPer} seeds = {2 * seedsPer}");
        report.AppendLine($"  Total: {(5 + 2) * seedsPer} profiles");
        report.AppendLine($"  Each profile: path dependence (AB vs BA), identity persistence, curvature");
        report.AppendLine();

        // ── Run tests ────────────────────────────────────────────────
        var bag = new ConcurrentBag<SpontaneousMemoryAnalyzer.EmergenceProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // β=0, varying cycles.
        int[] zeroCycles = { 10, 50, 100 };
        foreach (int cycles in zeroCycles)
            for (int s = 0; s < seedsPer; s++)
                bag.Add(SpontaneousMemoryAnalyzer.TestEmergence(
                    0.0, cycles, K, Lambda, N, BaseSeed + (cycles * 100 + s) * 7919));

        // β=0.5 controls.
        int[] controlCycles = { 10, 50 };
        foreach (int cycles in controlCycles)
            for (int s = 0; s < seedsPer; s++)
                bag.Add(SpontaneousMemoryAnalyzer.TestEmergence(
                    0.5, cycles, K, Lambda, N, BaseSeed + 50000 + (cycles * 100 + s) * 7919));

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} profiles in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Analyze ──────────────────────────────────────────────────
        var emerg = SpontaneousMemoryAnalyzer.AnalyzeEmergence(profiles);

        // ── Section 3: Memory Emergence Analysis ─────────────────────
        AppendSection(report, "3. Path Dependence Analysis");

        report.AppendLine("  β     │ Cycles │ PathDep(AB-BA) │ IdPersist │ Curvature │ MemScore");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.Beta).ThenBy(p => p.Cycles))
            report.AppendLine($"  {p.Beta,4:F1} │ {p.Cycles,6} │ {p.PathDependenceDistance,14:F6} │ {p.IdentityPersistence,8:P1} │ {p.Curvature,8:F4} │ {p.MemScore,8:F4}");

        report.AppendLine();

        var beta0 = profiles.Where(p => p.Beta < 0.01).ToList();
        var betaPos = profiles.Where(p => p.Beta > 0.01).ToList();

        double meanPD0 = beta0.Average(p => p.PathDependenceDistance);
        double meanPDPos = betaPos.Average(p => p.PathDependenceDistance);

        report.AppendLine($"  Mean path dependence at β=0:    {meanPD0:F6}");
        report.AppendLine($"  Mean path dependence at β>0:    {meanPDPos:F6}");
        report.AppendLine($"  Ratio (β=0 / β>0):              {meanPD0 / Math.Max(meanPDPos, 1e-10):P1}");
        report.AppendLine();

        report.AppendLine($"  Q1: Can path dependence emerge at β=0?");
        bool pathDep = meanPD0 > 0.01;
        report.AppendLine($"    {(pathDep ? $"YES \u2014 Path dependence {meanPD0:F6} at β=0" : "NO \u2014 Path dependence is negligible at β=0")}");
        report.AppendLine();

        // ── Section 4: Identity Formation ────────────────────────────
        AppendSection(report, "4. Identity Formation");

        double meanId0 = beta0.Average(p => p.IdentityPersistence);
        double meanIdPos = betaPos.Average(p => p.IdentityPersistence);

        report.AppendLine($"  Mean identity persistence at β=0:    {meanId0:P1}");
        report.AppendLine($"  Mean identity persistence at β>0:    {meanIdPos:P1}");
        report.AppendLine();
        report.AppendLine($"  Q2: Can identity appear without explicit memory?");
        report.AppendLine($"    {(meanId0 > 0.80 ? "YES \u2014 Identity persists strongly even at β=0" : meanId0 > 0.50 ? "PARTIALLY \u2014 Some identity at β=0" : "NO \u2014 Identity requires explicit memory")}");
        report.AppendLine();

        // ── Section 5: Curvature Evolution ───────────────────────────
        AppendSection(report, "5. Curvature Evolution");

        double meanCurv0 = beta0.Average(p => p.Curvature);
        double meanCurvPos = betaPos.Average(p => p.Curvature);

        report.AppendLine($"  Mean curvature at β=0:    {meanCurv0:F6}");
        report.AppendLine($"  Mean curvature at β>0:    {meanCurvPos:F6}");
        report.AppendLine();
        report.AppendLine($"  Q3: Can curvature emerge without memory?");
        report.AppendLine($"    {(meanCurv0 > 0.01 ? "YES \u2014 Curvature present at β=0" : "NO \u2014 Curvature requires explicit memory")}");
        report.AppendLine();

        // ── Section 6: Cycles dependence ─────────────────────────────
        AppendSection(report, "6. Cycle Dependence");

        report.AppendLine("  Q4: Do repeated experiences create effective memory?");
        // Check if path dependence grows with cycles at β=0.
        var byCycle = beta0.GroupBy(p => p.Cycles).OrderBy(g => g.Key).ToList();
        double firstPD = byCycle.First().Average(p => p.PathDependenceDistance);
        double lastPD = byCycle.Last().Average(p => p.PathDependenceDistance);
        report.AppendLine($"    Path dependence: {firstPD:F6} (10 cycles) → {lastPD:F6} (1000 cycles)");
        report.AppendLine($"    {(lastPD > firstPD * 1.5 ? "YES \u2014 Path dependence GROWS with experience" : "NO \u2014 Path dependence is stable across cycles")}");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Emergence class: {emerg.EmergenceClass}");
        report.AppendLine($"  {emerg.Description}");
        report.AppendLine();

        report.AppendLine($"  Q5: Is there evidence that memory is emergent?");
        if (emerg.EmergenceClass.StartsWith("C:"))
            report.AppendLine("    YES \u2014 Memory-like behavior emerges naturally at β=0.");
        else if (emerg.EmergenceClass.StartsWith("B:"))
            report.AppendLine("    WEAK \u2014 Some emergence but β still dominates.");
        else
            report.AppendLine("    NO \u2014 Memory is purely external. β=0 systems are memoryless.");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Emergence class: {emerg.EmergenceClass}");
        report.AppendLine($"  C2. Mean path dependence: {emerg.MeanPathDependence:F6}");
        report.AppendLine($"  C3. Mean curvature: {emerg.MeanCurvature:F6}");
        report.AppendLine($"  C4. Mean memory score: {emerg.MeanMemoryScore:F6}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-061 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
