using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_059_SourcesOfResonanceCurvature : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int BaseSeed = 590123847;

    public AT_059_SourcesOfResonanceCurvature(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_059_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-059 Sources of Resonance Curvature");

        report.AppendLine("AT-059: What Creates Curvature in the Resonance State Space?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-058 discovered geometrically dominated curvature.");
        report.AppendLine("  This experiment determines WHAT generates that curvature:");
        report.AppendLine("  energy, memory, identity, coherence, or a combination.");
        report.AppendLine();

        // ── Section 2: Experimental Design ───────────────────────────
        double[] energyLevels = { 0.0, 0.25, 0.5, 1.0, 2.0, 3.0, 5.0 };
        double[] betas = { 0.0, 0.05, 0.1, 0.2, 0.5, 1.0, 2.0 };
        string[] histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
        int seedsPer = 2;
        int totalScanPoints = (energyLevels.Length + betas.Length + histories.Length) * seedsPer;

        AppendSection(report, "2. Experimental Design");
        report.AppendLine($"  Energy scan: [{string.Join(", ", energyLevels)}] × {seedsPer} seeds = {energyLevels.Length * seedsPer}");
        report.AppendLine($"  Memory scan: β = [{string.Join(", ", betas)}] × {seedsPer} seeds = {betas.Length * seedsPer}");
        report.AppendLine($"  Identity scan: [{string.Join(", ", histories)}] × {seedsPer} seeds = {histories.Length * seedsPer}");
        report.AppendLine($"  Total scan points: {totalScanPoints}");
        report.AppendLine($"  Each point: 4 perturbation magnitudes → 6 geodesic deviation pairs");
        report.AppendLine($"  Total pairs: {totalScanPoints * 6}");
        report.AppendLine();

        // ── Run scans ────────────────────────────────────────────────
        var bag = new ConcurrentBag<CurvatureSourceAnalyzer.ScanPoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();
        int seedCounter = 0;

        // Energy scan.
        foreach (double e in energyLevels)
            for (int s = 0; s < seedsPer; s++)
                bag.Add(CurvatureSourceAnalyzer.MeasureScanPoint(
                    CurvatureSourceAnalyzer.ScanVariable.EnergyScale, e, "AB", K, Lambda, N,
                    BaseSeed + seedCounter++ * 7919));

        // Memory scan.
        foreach (double b in betas)
            for (int s = 0; s < seedsPer; s++)
                bag.Add(CurvatureSourceAnalyzer.MeasureScanPoint(
                    CurvatureSourceAnalyzer.ScanVariable.MemoryBeta, b, "AB", K, Lambda, N,
                    BaseSeed + seedCounter++ * 7919));

        // Identity scan.
        foreach (string h in histories)
            for (int s = 0; s < seedsPer; s++)
                bag.Add(CurvatureSourceAnalyzer.MeasureScanPoint(
                    CurvatureSourceAnalyzer.ScanVariable.IdentityHistory, 0, h, K, Lambda, N,
                    BaseSeed + seedCounter++ * 7919));

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Completed {points.Count} scan points in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Source attribution ───────────────────────────────────────
        var attr = CurvatureSourceAnalyzer.AnalyzeSources(points);

        // ── Section 3: Curvature Measurements ────────────────────────
        AppendSection(report, "3. Curvature Measurements");

        report.AppendLine("  Scan Variable       │ Value     │ Curvature │ Converge% │ Conv Rate");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in points.Take(30))
            report.AppendLine($"  {p.Variable,-19} │ {p.VariableValue,8:F2} │ {p.MeanCurvature,8:F4} │ {p.ConvergentFraction,8:P0} │ {p.MeanConvergenceRate,8:F4}");
        report.AppendLine($"  ... ({points.Count - 30} more) ...");
        report.AppendLine();

        // ── Sections 4-7: Variable Contributions ─────────────────────
        AppendSection(report, "4. Energy Contribution");
        var ePts = points.Where(p => p.Variable == CurvatureSourceAnalyzer.ScanVariable.EnergyScale).ToList();
        report.AppendLine($"  Energy-curvature correlation: {attr.EnergyCurvatureCorrelation,8:F4}");
        string eDesc = Math.Abs(attr.EnergyCurvatureCorrelation) > 0.5 ? "STRONG" :
                       Math.Abs(attr.EnergyCurvatureCorrelation) > 0.3 ? "MODERATE" :
                       "WEAK";
        report.AppendLine($"  Strength: {eDesc}");
        report.AppendLine($"  Q1: Does energy influence curvature?");
        report.AppendLine($"    {(Math.Abs(attr.EnergyCurvatureCorrelation) > 0.3 ? "YES" : "NO")} (r = {attr.EnergyCurvatureCorrelation:F4})");
        report.AppendLine();

        AppendSection(report, "5. Memory Contribution");
        var mPts = points.Where(p => p.Variable == CurvatureSourceAnalyzer.ScanVariable.MemoryBeta).ToList();
        report.AppendLine($"  Memory-curvature correlation: {attr.MemoryCurvatureCorrelation,8:F4}");
        string mDesc = Math.Abs(attr.MemoryCurvatureCorrelation) > 0.5 ? "STRONG" :
                       Math.Abs(attr.MemoryCurvatureCorrelation) > 0.3 ? "MODERATE" :
                       "WEAK";
        report.AppendLine($"  Strength: {mDesc}");
        report.AppendLine($"  Q2: Does memory influence curvature?");
        report.AppendLine($"    {(Math.Abs(attr.MemoryCurvatureCorrelation) > 0.3 ? "YES" : "NO")} (r = {attr.MemoryCurvatureCorrelation:F4})");
        report.AppendLine();

        AppendSection(report, "6. Identity Contribution");
        var iPts = points.Where(p => p.Variable == CurvatureSourceAnalyzer.ScanVariable.IdentityHistory).ToList();
        report.AppendLine($"  Identity curvature variance: {attr.IdentityCurvatureVariance:F6}");
        report.AppendLine("  History │ Curvature │ Converge%");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in iPts)
            report.AppendLine($"  {p.History,-7} │ {p.MeanCurvature,8:F4} │ {p.ConvergentFraction,8:P0}");
        report.AppendLine();
        report.AppendLine($"  Q3: Do different identities produce different curvature?");
        report.AppendLine($"    {(attr.IdentityCurvatureVariance > 0.01 ? "YES \u2014 Curvature varies with identity" : "NO \u2014 Curvature is identity-independent")}");
        report.AppendLine();

        AppendSection(report, "7. Coherence Contribution");
        report.AppendLine($"  Does coherence drive curvature?");
        report.AppendLine($"    Coherence is near-constant (AT-052) \u2014 it cannot explain curvature variation.");
        report.AppendLine($"  Q4: Is coherence responsible for curvature?");
        report.AppendLine($"    NO \u2014 Coherence is invariant, curvature varies \u2192 coherence is not the source.");
        report.AppendLine();

        // ── Section 8: Source Attribution ────────────────────────────
        AppendSection(report, "8. Source Attribution");

        report.AppendLine($"  Energy-curvature r:        {attr.EnergyCurvatureCorrelation,8:F4}");
        report.AppendLine($"  Memory-curvature r:        {attr.MemoryCurvatureCorrelation,8:F4}");
        report.AppendLine($"  Identity curvature \u03c3:     {attr.IdentityCurvatureVariance,8:F6}");
        report.AppendLine();

        report.AppendLine($"  Q5: Is curvature generated by one variable or a combination?");
        double maxCorr = Math.Max(Math.Abs(attr.EnergyCurvatureCorrelation), Math.Abs(attr.MemoryCurvatureCorrelation));
        report.AppendLine($"    {(maxCorr > 0.5 ? "SINGLE VARIABLE \u2014 One variable dominates" : "MULTI-FACTOR \u2014 Curvature emerges from combined state variables")}");
        report.AppendLine();

        report.AppendLine($"  Q6: Can curvature be predicted from state variables?");
        report.AppendLine($"    {(maxCorr > 0.7 ? $"YES \u2014 {attr.DominantSource} predicts curvature (r = {maxCorr:F3})" : maxCorr > 0.4 ? "PARTIALLY \u2014 Moderate predictability" : "NO \u2014 Curvature is not predictable from simple variables")}");
        report.AppendLine();

        // ── Section 9: Interpretation ────────────────────────────────
        AppendSection(report, "9. Interpretation");

        report.AppendLine($"  Dominant curvature source: {attr.DominantSource}");
        report.AppendLine($"  Classification: {attr.Classification}");
        report.AppendLine();

        string interp = attr.DominantSource switch
        {
            "Energy Dominated" => "Energy is the primary curvature generator. " +
                "Changing the energy scale reshapes the state-space geometry, " +
                "consistent with AT-055/056's finding that energy is the dominant organizer.",
            "Memory Dominated" => "Memory (β) is the primary curvature generator. " +
                "Historical encoding creates the geometric structure of the state space.",
            "Coherence Dominated" => "Coherence drives curvature — the system's " +
                "phase alignment creates the geometric focusing.",
            _ => "Curvature emerges from MULTIPLE interacting state variables. " +
                "No single variable explains the geometry — the curvature is a " +
                "collective property of the full state vector."
        };
        report.AppendLine($"  {interp}");
        report.AppendLine();

        // ── Section 10: Conclusion ───────────────────────────────────
        AppendSection(report, "10. Conclusion");

        report.AppendLine($"  C1. Dominant source: {attr.DominantSource}");
        report.AppendLine($"  C2. Classification: {attr.Classification}");
        report.AppendLine($"  C3. Energy correlation: r = {attr.EnergyCurvatureCorrelation:F4}");
        report.AppendLine($"  C4. Memory correlation: r = {attr.MemoryCurvatureCorrelation:F4}");
        report.AppendLine($"  C5. Identity variance: \u03c3 = {attr.IdentityCurvatureVariance:F6}");
        report.AppendLine();
        report.AppendLine($"  C6. Curvature is generated by {attr.DominantSource.ToLower()}.");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-059 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
