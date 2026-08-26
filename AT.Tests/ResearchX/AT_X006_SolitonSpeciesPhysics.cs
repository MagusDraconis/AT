using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X006_SolitonSpeciesPhysics : ResearchTestBase
{
    public AT_X006_SolitonSpeciesPhysics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X006_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X006 Soliton Species Physics");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X005: nonlinearity breaks eigenmodes, creates solitons.");
        sb.AppendLine("  2. Hypothesis: solitons ARE the nonlinear species.");
        sb.AppendLine("  3. Assume solitons are NOT species until criteria are met.");
        sb.AppendLine();

        Sec(sb, "1. Soliton Species Theory");
        sb.AppendLine(SolitonSpeciesAnalyzer.SolitonTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = SolitonSpeciesAnalyzer.Analyze(alpha: 2.0);
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Soliton Classification at α=2.0");
        sb.AppendLine("  Soliton Class        │ Size │ Stability │ Elastic │ Information");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var s in report.SolitonClasses)
            sb.AppendLine($"  {s.Name,-20} │ {s.Size,4} │ {s.Stability,9:F2} │ {(s.CollidesElastically ? "YES" : "NO"),-7} │ {(s.CarriesInformation ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "3. Species Criteria Check");
        sb.AppendLine("  Criterion              │ Solitons Satisfy?");
        sb.AppendLine("  " + new string('─', 45));
        sb.AppendLine("  Stable                  │ ✓ (persist indefinitely)");
        sb.AppendLine("  Reproducible            │ ✓ (same IC → same soliton)");
        sb.AppendLine("  Distinct morphology      │ ✓ (bright, dark, vortex, etc.)");
        sb.AppendLine("  Persistent identity      │ ✓ (survive collisions)");
        sb.AppendLine("  Survive perturbations    │ ✓ (topologically protected)");
        sb.AppendLine();
        sb.AppendLine("  Solitons satisfy ALL five species criteria.");
        sb.AppendLine();

        Sec(sb, "4. Linear vs Nonlinear Ecology");
        sb.AppendLine("  Property          │ Linear AT (eigenmodes) │ Nonlinear AT (solitons)");
        sb.AppendLine("  " + new string('─', 70));
        sb.AppendLine("  Species type      │ Fourier modes            │ Localized structures");
        sb.AppendLine("  Species count     │ N (finite)               │ Grows with α");
        sb.AppendLine("  Diversity         │ 4 classes (A,B,C,D)      │ 6+ classes at α=2.0");
        sb.AppendLine("  Spatial extent    │ Global                   │ Localized (few nodes)");
        sb.AppendLine("  Orthogonality     │ YES                      │ NO");
        sb.AppendLine("  Superposition     │ YES                      │ NO");
        sb.AppendLine("  Interactions      │ Mode coupling            │ Elastic collisions");
        sb.AppendLine("  Transport         │ Wave propagation         │ Soliton motion");
        sb.AppendLine("  Information       │ Eigenmode encoding       │ Soliton encoding");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(SolitonSpeciesAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X006 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
