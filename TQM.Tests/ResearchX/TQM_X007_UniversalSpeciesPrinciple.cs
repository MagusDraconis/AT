using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X007_UniversalSpeciesPrinciple : ResearchTestBase
{
    public TQM_X007_UniversalSpeciesPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X007 Universal Species Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Linear species = eigenmodes (TQM-133).");
        sb.AppendLine("  2. Nonlinear species = solitons (TQM-X006).");
        sb.AppendLine("  3. Hypothesis: a common principle unifies both.");
        sb.AppendLine();

        Sec(sb, "1. Universal Species Theory");
        sb.AppendLine(UniversalSpeciesAnalyzer.PrincipleTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = UniversalSpeciesAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Species Criteria — Cross-Class Comparison");
        sb.AppendLine("  Criterion                  │ Eigenmodes │ Solitons │ Necessary?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var c in report.Criteria)
            sb.AppendLine($"  {c.Name,-26} │ {(c.EigenmodesMeet ? "✓" : "✗"),-10} │ {(c.SolitonsMeet ? "✓" : "✗"),-8} │ {(c.IsNecessary ? "YES" : "no")}");
        sb.AppendLine();
        sb.AppendLine($"  Shared criteria: {report.CommonCount}/10");
        sb.AppendLine($"  Necessary criteria: {report.NecessaryCount}");
        sb.AppendLine();

        Sec(sb, "3. The Universal Species Principle");
        sb.AppendLine($"  \"{report.UniversalPrinciple}\"");
        sb.AppendLine();
        sb.AppendLine("  NECESSARY conditions (all must hold):");
        sb.AppendLine("    1. PERSISTENCE — survives indefinitely in time");
        sb.AppendLine("    2. IDENTITY — recognizable, reproducible pattern");
        sb.AppendLine("    3. INFORMATION — encodes/carries information");
        sb.AppendLine("    4. STABILITY — resists small perturbations");
        sb.AppendLine("    5. INTERACTION — can exchange information with others");
        sb.AppendLine();
        sb.AppendLine("  SUFFICIENT condition: All 5 necessary conditions are met.");
        sb.AppendLine();

        Sec(sb, "4. Species Hierarchy");
        sb.AppendLine("  Level 0: PERSISTENT STRUCTURES (stable in time)");
        sb.AppendLine("     ↓ + information encoding");
        sb.AppendLine("  Level 1: INFORMATION CARRIERS (encode data in structure)");
        sb.AppendLine("     ↓ + identity + reproducibility");
        sb.AppendLine("  Level 2: SPECIES (persistent info carriers with identity)");
        sb.AppendLine("     ↓ + interaction + population");
        sb.AppendLine("  Level 3: ECOLOGIES (interacting species populations)");
        sb.AppendLine("     ↓ + variation + selection");
        sb.AppendLine("  Level 4: EVOLUTION (Darwinian dynamics on ecologies)");
        sb.AppendLine();

        Sec(sb, "5. What the Principle Rejects");
        sb.AppendLine("  Structure           │ Persistent? │ Identity? │ Info? │ Species?");
        sb.AppendLine("  " + new string('─', 65));
        sb.AppendLine("  Noise               │ ✗            │ ✗          │ ✗      │ NO");
        sb.AppendLine("  Unstable transient  │ ✗            │ ✗          │ ✗      │ NO");
        sb.AppendLine("  Random fluctuation  │ ✗            │ ✗          │ ✗      │ NO");
        sb.AppendLine("  Eigenmode (linear)  │ ✓            │ ✓          │ ✓      │ YES");
        sb.AppendLine("  Soliton (nonlinear) │ ✓            │ ✓          │ ✓      │ YES");
        sb.AppendLine("  Composite mode      │ ✓            │ ✓          │ ✓      │ YES");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(UniversalSpeciesAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X007 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
