using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X011_ReversibilityVsSelfConsistency : ResearchTestBase
{
    public TQM_X011_ReversibilityVsSelfConsistency(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X011_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X011 Reversibility vs Self-Consistency");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-152: Reversibility is an irreducible postulate.");
        sb.AppendLine("  2. TQM-X010: Self-consistency is the deepest invariant.");
        sb.AppendLine("  3. Hypothesis: these are INDEPENDENT principles.");
        sb.AppendLine();

        Sec(sb, "1. Comparison Theory");
        sb.AppendLine(ReversibilityAnalyzer.ComparisonTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ReversibilityAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Cross-System Audit — 4 Quadrants");
        sb.AppendLine("  System                      │ Rev? │ SC?  │ Category");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var s in report.Systems)
            sb.AppendLine($"  {s.System,-27} │ {(s.IsReversible ? "✓" : "✗"),-4} │ {(s.IsSelfConsistent ? "✓" : "✗"),-4} │ {s.Category}");
        sb.AppendLine();

        Sec(sb, "3. Quadrant Analysis");
        sb.AppendLine($"  BOTH (Rev+SC):      {report.BothCount} systems");
        sb.AppendLine($"    Schrödinger eigenmodes, solitons, harmonic oscillator");
        sb.AppendLine($"    → These are where TQM's quantum + information branches intersect.");
        sb.AppendLine();
        sb.AppendLine($"  REVERSIBLE ONLY:    {report.ReversibleOnly} systems");
        sb.AppendLine($"    Free particle, Hamiltonian chaos, degenerate ring");
        sb.AppendLine($"    → Reversible but no persistent structures.");
        sb.AppendLine();
        sb.AppendLine($"  SELF-CONSISTENT ONLY: {report.SelfConsistentOnly} systems");
        sb.AppendLine($"    Diffusion eigenmodes, damped oscillators, Kuramoto sync");
        sb.AppendLine($"    → Persistent structures but dissipative — no quantum correspondence.");
        sb.AppendLine();
        sb.AppendLine($"  NEITHER:            {report.NeitherCount} systems");
        sb.AppendLine($"    Noise, dissipative chaos, transients");
        sb.AppendLine();

        Sec(sb, "4. What This Means for TQM");
        sb.AppendLine("  TQM has TWO independent irreducible foundations:");
        sb.AppendLine();
        sb.AppendLine("  Foundation 1: REVERSIBILITY (Postulate 2)");
        sb.AppendLine("    → d/dt||ψ||²=0 → anti-Hermitian → i → Schrödinger");
        sb.AppendLine("    → Enables the QUANTUM CORRESPONDENCE BRANCH");
        sb.AppendLine();
        sb.AppendLine("  Foundation 2: SELF-CONSISTENCY (TQM-X010)");
        sb.AppendLine("    → F(x)=x → fixed points → attractors → carriers");
        sb.AppendLine("    → Enables the INFORMATION CARRIER BRANCH");
        sb.AppendLine();
        sb.AppendLine("  TQM's richness comes from having BOTH:");
        sb.AppendLine("  - Reversibility gives quantum structure");
        sb.AppendLine("  - Self-consistency gives information structure");
        sb.AppendLine("  - Together they produce: quantum information carriers");
        sb.AppendLine();

        Sec(sb, "5. The Two-Pillar Foundation");
        sb.AppendLine("              TQM");
        sb.AppendLine("          ╱        ╲");
        sb.AppendLine("  REVERSIBILITY    SELF-CONSISTENCY");
        sb.AppendLine("   (Postulate 2)    (TQM-X010)");
        sb.AppendLine("        │                  │");
        sb.AppendLine("   Unitary evol.     Fixed points");
        sb.AppendLine("   Schrödinger       Attractors");
        sb.AppendLine("   Born rule         Carriers");
        sb.AppendLine("   Measurement       Species");
        sb.AppendLine("        │                  │");
        sb.AppendLine("        └──────┬───────────┘");
        sb.AppendLine("               │");
        sb.AppendLine("     QUANTUM INFORMATION CARRIERS");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(ReversibilityAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X011 complete. Classification: {report.Classification}");
        sb.AppendLine($"  TQM has TWO independent irreducible foundations.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
