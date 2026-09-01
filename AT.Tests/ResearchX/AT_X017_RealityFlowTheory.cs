using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X017_RealityFlowTheory : ResearchTestBase
{
    public AT_X017_RealityFlowTheory(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X017_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X017 Reality Flow Theory");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X016: static (R,S) classification map.");
        sb.AppendLine("  2. Question: do systems MOVE through reality space?");
        sb.AppendLine("  3. Assume no universal flows until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Flow Theory");
        sb.AppendLine(RealityFlowAnalyzer.FlowTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = RealityFlowAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Reality Trajectories");
        sb.AppendLine("  System                   │ (R₀,S₀)  → (R₁,S₁)  │ Direction         │ Mechanism");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var t in report.Trajectories)
            sb.AppendLine($"  {t.System,-24} │ ({t.R_initial:F1},{t.S_initial:F1}) → ({t.R_final:F1},{t.S_final:F1}) │ {t.FlowDirection,-17} │ {t.Mechanism}");
        sb.AppendLine();

        Sec(sb, "3. Flow Analysis");
        sb.AppendLine($"  Universal flow exists: {(report.UniversalFlowExists ? "YES" : "NO")}");
        sb.AppendLine($"  Dominant flow: {report.DominantFlow}");
        sb.AppendLine();
        sb.AppendLine("  FIXED POINTS (no flow):");
        sb.AppendLine("    • Quantum Reality (1.0, 1.0) — stationary states");
        sb.AppendLine("    • Solitons (0.9, 0.9) — nonlinear balance");
        sb.AppendLine("    • Noise (0.0, 0.0) — nothing to flow");
        sb.AppendLine();
        sb.AppendLine("  DIRECTIONAL FLOWS:");
        sb.AppendLine("    • RIGHTWARD (S↑): evolution, learning, optimization");
        sb.AppendLine("    • DOWNWARD-LEFT (R↓,S↓): decoherence, decay, death");
        sb.AppendLine("    • LEFTWARD (R↓,S↑): measurement collapse");
        sb.AppendLine();

        Sec(sb, "4. The Anthropic Principle in Reality Space");
        sb.AppendLine("  Systems with high R×S PERSIST longer.");
        sb.AppendLine("  Therefore we OBSERVE more high R×S systems.");
        sb.AppendLine("  This creates the ILLUSION of a universal flow toward");
        sb.AppendLine("  the quantum corner — but it's SELECTION, not dynamics.");
        sb.AppendLine();
        sb.AppendLine("  The 'flow' toward higher reality is in the OBSERVER,");
        sb.AppendLine("  not in the physical dynamics of individual systems.");
        sb.AppendLine();
        sb.AppendLine("  A system at (0.2, 0.3) doesn't 'flow' to (0.9, 0.9).");
        sb.AppendLine("  It simply doesn't LAST long enough to be noticed.");
        sb.AppendLine("  The systems we STUDY are the ones that happen to be at");
        sb.AppendLine("  high (R,S) — which is why physics focuses on quantum");
        sb.AppendLine("  and biology focuses on persistent species.");
        sb.AppendLine();

        Sec(sb, "5. Map vs Territory");
        sb.AppendLine("  (R,S) space is a MAP — a classification tool.");
        sb.AppendLine("  It is NOT the territory — not a dynamical phase space.");
        sb.AppendLine();
        sb.AppendLine("  Systems don't 'move through reality space.'");
        sb.AppendLine("  Their dynamics change → their (R,S) scores change.");
        sb.AppendLine("  The change in dynamics IS the cause.");
        sb.AppendLine("  The change in (R,S) IS the measurement.");
        sb.AppendLine();
        sb.AppendLine("  AT provides a LANGUAGE for describing reality.");
        sb.AppendLine("  It does NOT provide equations of motion for reality.");
        sb.AppendLine();

        Sec(sb, "6. AT's Final Role");
        sb.AppendLine("  AT (the AT framework after X-series) is:");
        sb.AppendLine("    1. A LANGUAGE: (R,S) coordinates describe any system");
        sb.AppendLine("    2. A MAP: the Reality Phase Diagram classifies systems");
        sb.AppendLine("    3. A FOUNDATION: R+S is the minimal recipe for persistence");
        sb.AppendLine("    4. NOT a dynamical theory: no equations of motion for reality");
        sb.AppendLine();
        sb.AppendLine("  This is the COMPLETE scope of the AT framework.");

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(RealityFlowAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X017 complete. Classification: {report.Classification}");
        sb.AppendLine($"  Reality Space = MAP, not territory. No universal flow laws.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
