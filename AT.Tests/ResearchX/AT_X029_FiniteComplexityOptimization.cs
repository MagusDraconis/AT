using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X029_FiniteComplexityOptimization : ResearchTestBase
{
    public AT_X029_FiniteComplexityOptimization(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X029_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X029 Finite Complexity Optimization Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X027: all finite systems saturate.");
        sb.AppendLine("  2. AT-X028: ceilings are astronomically vast.");
        sb.AppendLine("  3. Question: which finite architecture gets closest to L6?");
        sb.AppendLine();

        var report = FiniteComplexityOptimizer.Analyze();

        Sec(sb, "1. Architecture Comparison (N=100)");
        sb.AppendLine("  Architecture               │ Classes │ Max Species │ Complexity │ Efficiency │ Optimal?");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var a in report.Architectures.OrderByDescending(x => x.ComplexityScore))
            sb.AppendLine($"  {a.Architecture,-26} │ {a.CarrierClasses,7} │ {a.MaxSpecies,11} │ {a.ComplexityScore,10:F0} │ {a.Efficiency,10:F2} │ {(a.IsOptimal ? "✓" : "")}");
        sb.AppendLine();
        sb.AppendLine($"  Best: {report.BestArchitecture} (efficiency {report.BestEfficiency:F2})");
        sb.AppendLine($"  Hybrid is optimal: {(report.HybridIsOptimal ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "2. The Carrier Diversity Principle");
        sb.AppendLine("  Finite complexity ∝ Carrier Class Diversity.");
        sb.AppendLine("  Each carrier class exploits an orthogonal subspace.");
        sb.AppendLine("  More classes = more efficient packing of Hilbert space.");
        sb.AppendLine();
        sb.AppendLine("  Pure Fourier (1 class):    wastes most of its state space.");
        sb.AppendLine("  Pure NLS (6 classes):      better — 6× species capacity.");
        sb.AppendLine("  Universal Hybrid (16):     BEST — all classes simultaneously.");
        sb.AppendLine();

        Sec(sb, "3. Practical Optimization Strategy");
        sb.AppendLine("  1. START with Quantum Reality (Rev∩SC) — already near-optimal.");
        sb.AppendLine("     Supports 7 carrier classes naturally (eigenmodes + solitons).");
        sb.AppendLine();
        sb.AppendLine("  2. ADD nonlinearity (α > 0) to unlock soliton classes.");
        sb.AppendLine("     Bright, dark, vector, vortex, breather — all emerge.");
        sb.AppendLine();
        sb.AppendLine("  3. ADD topology where possible for protected edge states.");
        sb.AppendLine("");
        sb.AppendLine("  4. AVOID over-engineering: each added class has overhead.");
        sb.AppendLine("     7-10 classes is the practical sweet spot.");
        sb.AppendLine();

        Sec(sb, "4. The Closest Finite Approximation to L6");
        sb.AppendLine("  TRUE L6: unbounded innovation (infinite systems only).");
        sb.AppendLine("  BEST FINITE APPROXIMATION: Universal Hybrid architecture.");
        sb.AppendLine("    - 16 carrier classes (all known families)");
        sb.AppendLine("    - Each class generates species within its subspace");
        sb.AppendLine("    - Species diversity ~16N for system size N");
        sb.AppendLine("    - Innovation saturates at ~16N, but ceiling is maximized");
        sb.AppendLine();
        sb.AppendLine("  This does NOT achieve L6 (requires infinity).");
        sb.AppendLine("  But it MAXIMIZES what finite systems CAN achieve.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(FiniteComplexityOptimizer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X029 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
