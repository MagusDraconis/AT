using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_141_NonlinearModeComposition : ResearchTestBase
{
    public AT_141_NonlinearModeComposition(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_141_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-141 Nonlinear Mode Composition and Species Emergence");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. 10 eigenmodes from AT-140 form the spectral basis.");
        sb.AppendLine("  2. 13-19 species observed in AT-138/139.");
        sb.AppendLine("  3. Excess species may be nonlinear mode combinations.");
        sb.AppendLine("  4. Assume species are PURE eigenmodes until composites are demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. AT-140 Recap — Spectral Origin");
        sb.AppendLine("  AT-140: 10 eigenmodes, 7/7 species mapped, overlap 0.808.");
        sb.AppendLine("  Gap: 10 modes vs 13-19 species → where do extra 3-9 come from?");
        sb.AppendLine();

        Sec(sb, "2. Composition Theory");
        sb.AppendLine(NonlinearModeCompositionAnalyzer.CompositionTheory());
        sb.AppendLine();

        Sec(sb, "3. Composite Mode Generation");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = NonlinearModeCompositionAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Composites generated: {report.TotalCompositesGenerated}");
        sb.AppendLine($"  Unique composite species: {report.SpeciesCountFromComposites}");
        sb.AppendLine($"  AT-139 species mapped: {report.TotalSpeciesMapped}");
        sb.AppendLine($"  Mean reconstruction overlap: {report.MeanReconstructionOverlap:F3}");
        sb.AppendLine($"  Species coverage: {report.SpeciesCoverage:P0}");
        sb.AppendLine();

        Sec(sb, "4. Species Composition Analysis");
        sb.AppendLine($"  Pure modes:      {report.SpeciesMappings.Count(m => m.IsPureMode)}");
        sb.AppendLine($"  Linear pairs:    {report.SpeciesMappings.Count(m => m.IsLinearPair)}");
        sb.AppendLine($"  Nonlinear pairs: {report.SpeciesMappings.Count(m => m.IsNonlinearPair)}");
        sb.AppendLine($"  Triples:         {report.SpeciesMappings.Count(m => m.IsTriple)}");
        sb.AppendLine($"  Minimum basis:   {report.MinimumBasisSize} eigenmodes");
        sb.AppendLine();

        sb.AppendLine("  Species → Composition mapping:");
        sb.AppendLine("  Species │ Modes │ Overlap │ Type");
        sb.AppendLine("  " + new string('─', 50));
        foreach (var m in report.SpeciesMappings.Take(12))
            sb.AppendLine($"  {m.SpeciesName,-7} │ [{string.Join(",", m.ComposingModes.Take(3))}] │ {m.Overlap,7:F3} │ {m.CompositionType}");
        sb.AppendLine();

        Sec(sb, "5. Mode Coupling Matrix");
        var cm = report.CouplingMatrix;
        sb.AppendLine($"  Mode count: {cm.ModeCount}");
        sb.AppendLine($"  Significant couplings: {cm.TotalSignificantCouplings}");
        sb.AppendLine($"  Strongest pairs: {string.Join(", ", cm.StrongestPairs.Take(5).Select(p => $"({p.i},{p.j})"))}");
        sb.AppendLine();

        Sec(sb, "6. Excess Species Explained");
        sb.AppendLine($"  Eigenmodes (AT-140):  10");
        sb.AppendLine($"  Composite species:     {report.SpeciesCountFromComposites}");
        sb.AppendLine($"  Observed (AT-138/139): ~13-19");
        sb.AppendLine(report.CompositesExplainExcess
            ? "  → Composites EXPLAIN the excess. The species catalog emerges from mode coupling."
            : "  → Composites do NOT fully explain the excess.");
        sb.AppendLine(report.NonlinearEssential
            ? "  → Nonlinear (product) terms ARE necessary for the full catalog."
            : "  → Linear combinations suffice — nonlinear terms not needed.");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(NonlinearModeCompositionAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(NonlinearModeCompositionAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-141 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Composites explain excess: {(report.CompositesExplainExcess ? "YES" : "NO")}");
        sb.AppendLine($"  Nonlinear essential: {(report.NonlinearEssential ? "YES" : "NO")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
