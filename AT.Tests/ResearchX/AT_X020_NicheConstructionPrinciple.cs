using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X020_NicheConstructionPrinciple : ResearchTestBase
{
    public AT_X020_NicheConstructionPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X020_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X020 Niche Construction Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X019: L6 requires new CARRIER CLASSES.");
        sb.AppendLine("  2. Hypothesis: niche construction can create new classes.");
        sb.AppendLine("  3. Assume niche construction is insufficient until proven.");
        sb.AppendLine();

        Sec(sb, "1. Niche Construction Theory");
        sb.AppendLine(NicheConstructionAnalyzer.NicheTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = NicheConstructionAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Feedback Mechanism Evaluation");
        sb.AppendLine("  Mechanism                          │ New Classes? │ New Species? │ L6? │ Bottleneck");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var r in report.Results)
            sb.AppendLine($"  {r.Mechanism,-35} │ {(r.NewCarrierClasses ? "YES" : "NO"),-12} │ {(r.NewSpeciesClasses ? "YES" : "NO"),-12} │ {(r.NonSaturating ? "YES" : "NO"),-4} │ {r.Bottleneck}");
        sb.AppendLine();

        Sec(sb, "3. The Operator Barrier");
        sb.AppendLine("  THE FUNDAMENTAL INSIGHT:");
        sb.AppendLine("  Carrier CLASS = determined by OPERATOR FAMILY.");
        sb.AppendLine("  Graph Laplacian L_Q → sinusoidal eigenmodes (Fourier class).");
        sb.AppendLine("  NLS operator → solitons (nonlinear class).");
        sb.AppendLine("  Magnetic Laplacian → Landau levels (topological class).");
        sb.AppendLine();
        sb.AppendLine("  Niche construction (graph modification):");
        sb.AppendLine("    → Changes L_Q parameters (edges, degrees, spectrum)");
        sb.AppendLine("    → Does NOT change L_Q type (still graph Laplacian)");
        sb.AppendLine("    → Eigenmodes are still SINUSOIDAL");
        sb.AppendLine("    → No new CARRIER CLASSES");
        sb.AppendLine();
        sb.AppendLine("  To create new carrier classes, species must:");
        sb.AppendLine("    → Change OPERATOR FAMILY (not just parameters)");
        sb.AppendLine("    → Example: add magnetic field → magnetic Laplacian");
        sb.AppendLine("    → Example: increase nonlinearity → NLS regime");
        sb.AppendLine("    → This is OPERATOR EVOLUTION, not graph evolution");
        sb.AppendLine();

        Sec(sb, "4. What Niche Construction CAN Do");
        sb.AppendLine("  ✓ Create new SPECIES (more eigenmodes within Fourier family)");
        sb.AppendLine("  ✓ Fragment ecologies (disconnected graph components)");
        sb.AppendLine("  ✓ Shift fitness landscapes (different spectrum → different fitness)");
        sb.AppendLine("  ✓ Create topological defects (vortices, domain walls)");
        sb.AppendLine();
        sb.AppendLine("  ✗ Create new CARRIER CLASSES (still graph Laplacian)");
        sb.AppendLine("  ✗ Non-saturating innovation (finite graph → finite spectrum)");
        sb.AppendLine("  ✗ Enable L6 Open-Ended Evolution");
        sb.AppendLine();

        Sec(sb, "5. The L6 Bottleneck — Revisited");
        sb.AppendLine("  Previous analysis (X019):");
        sb.AppendLine("    L6 requires new CARRIER CLASSES.");
        sb.AppendLine();
        sb.AppendLine("  X020 refinement:");
        sb.AppendLine("    L6 requires new OPERATOR FAMILIES.");
        sb.AppendLine("    This is a DEEPER bottleneck than previously thought.");
        sb.AppendLine();
        sb.AppendLine("  New carrier classes = new operator families:");
        sb.AppendLine("    Graph Laplacian → Magnetic Laplacian → Nonlinear → ...");
        sb.AppendLine("    This requires the SYSTEM to change its fundamental dynamics.");
        sb.AppendLine();
        sb.AppendLine("  L6 is NOT just 'complex graph evolution.'");
        sb.AppendLine("  L6 is 'operator space exploration.'");
        sb.AppendLine("  This is a fundamentally harder problem.");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(NicheConstructionAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X020 complete. Classification: {report.Classification}");
        sb.AppendLine($"  L6 bottleneck = OPERATOR EVOLUTION, not graph evolution.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
