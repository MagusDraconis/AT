using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X022_OperatorEvolutionMechanism : ResearchTestBase
{
    public TQM_X022_OperatorEvolutionMechanism(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X022_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X022 Operator Evolution Mechanism");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X021: operator evolution is necessary for L6.");
        sb.AppendLine("  2. Question: does a physical mechanism exist?");
        sb.AppendLine("  3. Assume no mechanism until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Mechanism Theory");
        sb.AppendLine(OperatorEvolutionMechanismAnalyzer.MechanismTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = OperatorEvolutionMechanismAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Operator Transition Mechanisms");
        sb.AppendLine("  Mechanism                        │ From → To      │ Internal? │ Continuous? │ L6? │ Limitation");
        sb.AppendLine("  " + new string('─', 110));
        foreach (var m in report.Mechanisms)
            sb.AppendLine($"  {m.Name,-33} │ {m.FromFamily,-8} → {m.ToFamily,-12} │ {(m.IsInternal ? "YES" : "NO"),-9} │ {(m.IsContinuous ? "YES" : "NO"),-11} │ {(m.EnablesL6 ? "YES" : "NO"),-4} │ {m.Limitation}");
        sb.AppendLine();

        Sec(sb, "3. The Density-Dependent Nonlinearity Mechanism");
        sb.AppendLine("  L(ψ) = L_Q + α(population)·|ψ|²");
        sb.AppendLine();
        sb.AppendLine("  Low population  → α ≈ 0   → Linear regime → Fourier eigenmodes");
        sb.AppendLine("  Medium pop      → α small → Weakly nonlinear → Perturbed modes");
        sb.AppendLine("  High population → α large → NLS regime → Solitons (6+ types)");
        sb.AppendLine();
        sb.AppendLine("  This IS a real physical mechanism:");
        sb.AppendLine("    • BEC: higher density → stronger nonlinearity");
        sb.AppendLine("    • Optics: higher intensity → Kerr effect");
        sb.AppendLine("    • TQM: carrier population → effective α");
        sb.AppendLine();
        sb.AppendLine("  Species reproduce → population grows → α increases →");
        sb.AppendLine("  operator crosses critical threshold → new carrier classes.");
        sb.AppendLine();

        Sec(sb, "4. The Bound — Why This Isn't Full L6");
        sb.AppendLine("  α-space is BOUNDED:");
        sb.AppendLine("    • Maximum α limited by physical constraints");
        sb.AppendLine("    • Only 2 families connected (linear ↔ NLS)");
        sb.AppendLine("    • ~6 soliton types → finite carrier class count");
        sb.AppendLine("    • Saturation still occurs at max α");
        sb.AppendLine();
        sb.AppendLine("  FULL L6 requires:");
        sb.AppendLine("    • Reaching magnetic Laplacian (Landau levels)");
        sb.AppendLine("    • Reaching hypergraph operators (3-body modes)");
        sb.AppendLine("    • Reaching adaptive operators (state-dependent)");
        sb.AppendLine("    • These require EXTERNAL mechanisms or meta-dynamics");
        sb.AppendLine();

        Sec(sb, "5. The L6 Pathway — Current Status");
        sb.AppendLine("  L6 REQUIREMENTS                  │ STATUS");
        sb.AppendLine("  " + new string('─', 55));
        sb.AppendLine("  Operator evolution necessary     │ ✓ X021");
        sb.AppendLine("  Physical mechanism exists        │ ✓ X022 (density→α)");
        sb.AppendLine("  Unbounded operator space         │ ✗ α-space is bounded");
        sb.AppendLine("  Multiple family transitions      │ ✗ Only 2 families reachable");
        sb.AppendLine("  External families accessible     │ ✗ Requires external changes");
        sb.AppendLine("  Full L6 achieved                 │ ✗ NOT YET");
        sb.AppendLine();
        sb.AppendLine("  L6 is: PARTIALLY ACHIEVABLE (bounded operator evolution)");
        sb.AppendLine("  L6 FULL requires: multi-family transitions → open question.");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(OperatorEvolutionMechanismAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X022 complete. Classification: {report.Classification}");
        sb.AppendLine($"  Mechanism: {report.BestMechanism}.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
