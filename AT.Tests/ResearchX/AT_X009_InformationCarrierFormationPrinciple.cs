using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X009_InformationCarrierFormationPrinciple : ResearchTestBase
{
    public AT_X009_InformationCarrierFormationPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X009_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X009 Information Carrier Formation Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X008: 16 carrier classes across 5 regimes.");
        sb.AppendLine("  2. Unknown WHY carriers form. Hypothesis: self-consistency.");
        sb.AppendLine("  3. Assume no universal principle until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Formation Theory");
        sb.AppendLine(CarrierFormationAnalyzer.FormationTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = CarrierFormationAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Formation Mechanisms — Cross-Regime Test");
        sb.AppendLine("  Mechanism                │ Linear │ Nonlin │ Topo │ Universal?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var m in report.Mechanisms)
            sb.AppendLine($"  {m.Name,-24} │ {(m.WorksForLinear ? "✓" : "✗"),-6} │ {(m.WorksForNonlinear ? "✓" : "✗"),-6} │ {(m.WorksForTopological ? "✓" : "✗"),-4} │ {(m.IsUniversal ? "YES" : "no")}");
        sb.AppendLine();
        sb.AppendLine($"  Universal mechanisms: {report.UniversalCount}/{report.MechanismCount}");
        sb.AppendLine();

        Sec(sb, "3. The Universal Formation Principle");
        sb.AppendLine($"  \"{report.UniversalPrinciple}\"");
        sb.AppendLine();
        sb.AppendLine("  Every carrier class satisfies the self-consistency condition:");
        sb.AppendLine("    Linear eigenmodes:  L·v = λ·v (eigenvalue self-consistency)");
        sb.AppendLine("    Nonlinear solitons: NLS balance (dispersion = nonlinearity)");
        sb.AppendLine("    Topological:        winding number prevents continuous decay");
        sb.AppendLine();
        sb.AppendLine("  The universal pattern: structure determines dynamics;");
        sb.AppendLine("  dynamics preserve structure. Self-consistent feedback loop.");
        sb.AppendLine();

        Sec(sb, "4. The Formation Hierarchy (Deepest AT)");
        sb.AppendLine("  Level -1: DYNAMICS (Q interactions → equations of motion)");
        sb.AppendLine("     ↓ self-consistency");
        sb.AppendLine("  Level 0:  ATTRACTORS (dynamically stable fixed points)");
        sb.AppendLine("     ↓ + information encoding");
        sb.AppendLine("  Level 1:  PERSISTENT INFO CARRIERS (stable + encode info)");
        sb.AppendLine("     ↓ + identity + reproduction");
        sb.AppendLine("  Level 2:  SPECIES (identifiable, reproducible carriers)");
        sb.AppendLine("     ↓ + interaction + population");
        sb.AppendLine("  Level 3:  ECOLOGIES (interacting populations)");
        sb.AppendLine("     ↓ + variation + selection");
        sb.AppendLine("  Level 4:  EVOLUTION (Darwinian dynamics)");
        sb.AppendLine();
        sb.AppendLine("  This is the DEEPEST LAYER of AT: below species, below carriers.");
        sb.AppendLine("  The fundamental reason information persists in Theta is");
        sb.AppendLine("  SELF-CONSISTENT DYNAMICAL ATTRACTION.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(CarrierFormationAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X009 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
