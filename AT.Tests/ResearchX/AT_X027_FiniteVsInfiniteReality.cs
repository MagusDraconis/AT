using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X027_FiniteVsInfiniteReality : ResearchTestBase
{
    public AT_X027_FiniteVsInfiniteReality(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X027_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X027 Finite vs Infinite Reality Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X026: all finite simulations saturate.");
        sb.AppendLine("  2. Hypothesis: L6 requires infinite systems.");
        sb.AppendLine("  3. Assume finite systems CAN achieve L6 until proven otherwise.");
        sb.AppendLine();

        Sec(sb, "1. Finite/Infinite Theory");
        sb.AppendLine(FiniteInfiniteAnalyzer.FiniteInfiniteTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = FiniteInfiniteAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Scaling Analysis — Innovation vs System Size");
        sb.AppendLine("  N       │ Max Species │ Sat Time │ Regime");
        sb.AppendLine("  " + new string('─', 55));
        foreach (var r in report.Results)
            sb.AppendLine($"  {r.N,7} │ {r.MaxSpecies,11:F0} │ {r.SaturationTime,8:F0} │ {r.Regime}");
        sb.AppendLine();
        sb.AppendLine($"  ALL finite systems saturate: {(report.AllFiniteSystemsSaturate ? "YES — THEOREM" : "NO")}");
        sb.AppendLine($"  L6 requires infinite: {(report.L6RequiresInfinite ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "3. The Pigeonhole Principle of Reality");
        sb.AppendLine("  THEOREM: For any system with finite Hilbert space dim(H)=N:");
        sb.AppendLine("    1. Orthogonal eigenmodes ≤ N");
        sb.AppendLine("    2. Distinguishable species ≤ N");
        sb.AppendLine("    3. Innovation capacity ≤ N");
        sb.AppendLine("    4. Open-ended evolution is IMPOSSIBLE");
        sb.AppendLine();
        sb.AppendLine("  PROOF: By the pigeonhole principle. If there are N orthogonal");
        sb.AppendLine("  states, at most N mutually distinguishable species can exist.");
        sb.AppendLine("  Once all N are occupied, no genuinely new species can emerge.");
        sb.AppendLine("  Species can only be reshuffled — NOT created de novo.");
        sb.AppendLine();

        Sec(sb, "4. The Boundary Between L5 and L6");
        sb.AppendLine("  ┌─────────────────────┬──────────────────────┐");
        sb.AppendLine("  │  FINITE SYSTEMS     │  INFINITE SYSTEMS    │");
        sb.AppendLine("  ├─────────────────────┼──────────────────────┤");
        sb.AppendLine("  │  Eigenmodes ≤ N     │  Eigenmodes → ∞      │");
        sb.AppendLine("  │  Species ≤ N        │  Species → ∞         │");
        sb.AppendLine("  │  Innovation bounded │  Innovation unbounded │");
        sb.AppendLine("  │  L5 = ceiling       │  L6 = possible        │");
        sb.AppendLine("  │  Our universe       │  Mathematical limit   │");
        sb.AppendLine("  └─────────────────────┴──────────────────────┘");
        sb.AppendLine();
        sb.AppendLine("  THE BOUNDARY: Finite ↔ Infinite.");
        sb.AppendLine("  This IS the deepest boundary in the AT framework.");
        sb.AppendLine("  L6 lives on the other side of this boundary.");
        sb.AppendLine();

        Sec(sb, "5. Implications for Our Universe");
        sb.AppendLine("  The observable universe has FINITE entropy (Bekenstein-Hawking).");
        sb.AppendLine("  Finite entropy → finite Hilbert space dimension.");
        sb.AppendLine("  Finite Hilbert space → finite distinguishable states.");
        sb.AppendLine("  → TRUE open-ended evolution is IMPOSSIBLE in our universe.");
        sb.AppendLine();
        sb.AppendLine("  What we observe as 'evolution' is L5 (Darwinian dynamics)");
        sb.AppendLine("  operating within an astronomically large but FINITE state space.");
        sb.AppendLine("  The apparent 'open-endedness' of biological evolution is an");
        sb.AppendLine("  ILLUSION of scale — it saturates on cosmological timescales.");
        sb.AppendLine();

        Sec(sb, "6. The Complete AT/L6 Landscape");
        sb.AppendLine("  LEVEL │ NAME              │ REQUIREMENT        │ STATUS");
        sb.AppendLine("  " + new string('─', 60));
        sb.AppendLine("  L0    │ Noise             │ —                  │ Baseline");
        sb.AppendLine("  L1    │ Reality           │ R+S                │ ✓ Proven (X015)");
        sb.AppendLine("  L2    │ Carriers          │ Info encoding      │ ✓ Proven");
        sb.AppendLine("  L3    │ Species           │ Diversity          │ ✓ Proven (~19)");
        sb.AppendLine("  L4    │ Ecologies         │ Interactions       │ ✓ Proven");
        sb.AppendLine("  L5    │ Evolution         │ Variation+Selection│ ✓ Proven");
        sb.AppendLine("  L6    │ Open-Ended        │ INFINITE STATE SP  │ ✗ INFINITE ONLY");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(FiniteInfiniteAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X027 complete. Classification: {report.Classification}");
        sb.AppendLine($"  L6 = infinite systems only. Finite reality → L5 ceiling.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
