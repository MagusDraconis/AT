using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_145_PhysicalObservables : ResearchTestBase
{
    public AT_145_PhysicalObservables(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_145_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-145 Physical Observables from Topological Charge");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q is the fundamental topological charge (AT-117-122).");
        sb.AppendLine("  2. Theta hierarchy = graph Laplacian physics (AT-142/144).");
        sb.AppendLine("  3. Q may directly generate physical observables via L_Q.");
        sb.AppendLine("  4. Assume Q is purely topological until observables are shown.");
        sb.AppendLine();

        Sec(sb, "1. AT-144 Recap");
        sb.AppendLine("  Theta spectra = graph Laplacian = tight-binding = lattice physics.");
        sb.AppendLine("  Q: Can Q DIRECTLY predict physical observables?");
        sb.AppendLine();

        Sec(sb, "2. Observable Theory");
        sb.AppendLine(PhysicalObservableAnalyzer.ObservableTheory());
        sb.AppendLine();

        Sec(sb, "3. Observable Computation");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = PhysicalObservableAnalyzer.Analyze();
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Q values: [1, 2, 5, 10, 20, 50, 100]");
        sb.AppendLine($"  Geometries: 1D chain, 2D square");
        sb.AppendLine();

        Sec(sb, "4. Physical Observables from Q");
        sb.AppendLine("  Observable              │ Scaling Law        │ Exponent │ R²     │ Universal?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var o in report.Observables)
            sb.AppendLine($"  {o.Name,-23} │ O ∝ Q^{o.ScalingExponent,5:F2}       │ {o.ScalingExponent,8:F3} │ {o.R2,6:F3} │ {(o.IsUniversal ? "YES" : "no")}");
        sb.AppendLine();

        Sec(sb, "5. Key Scaling Laws");
        sb.AppendLine("  1D Chain (analytic):");
        sb.AppendLine("    λ_k = 2 - 2·cos(πk/(Q+1))");
        sb.AppendLine("    λ_1 ≈ π²/Q²  for large Q");
        sb.AppendLine("    → m_eff = 1/λ_1 ∝ Q²");
        sb.AppendLine("    → Δ = λ_2 - λ_1 ∝ 1/Q²");
        sb.AppendLine("    → E = trace(L) = 2(Q-1) ∝ Q");
        sb.AppendLine("    → ξ = 1/√(λ_1) ∝ Q");
        sb.AppendLine("    → D = λ_1 ∝ 1/Q²");
        sb.AppendLine("    → C = log₂(Q) ∝ log(Q)");
        sb.AppendLine("    → ρ = 1 (constant)");
        sb.AppendLine();
        sb.AppendLine("  2D Square:");
        sb.AppendLine("    λ_{kx,ky} = 4 - 2cos(πkx/(n+1)) - 2cos(πky/(n+1))");
        sb.AppendLine("    λ_1 ∝ 1/Q → m_eff ∝ Q (different from 1D!)");
        sb.AppendLine();

        Sec(sb, "6. Quantitative Results");
        sb.AppendLine($"  Observables found (R²>0.8):    {report.ObservablesFound}/{report.Observables.Count}");
        sb.AppendLine($"  Universal observables:          {report.UniversalObservables}");
        sb.AppendLine($"  Mean R²:                        {report.MeanR2:F3}");
        sb.AppendLine($"  Direct observables exist:       {(report.DirectObservablesExist ? "YES" : "NO")}");
        sb.AppendLine($"  Universal scaling found:        {(report.UniversalScalingFound ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(PhysicalObservableAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(PhysicalObservableAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-145 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Direct observables: {(report.DirectObservablesExist ? "YES" : "NO")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
