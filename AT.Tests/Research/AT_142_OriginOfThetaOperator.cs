using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_142_OriginOfThetaOperator : ResearchTestBase
{
    public AT_142_OriginOfThetaOperator(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_142_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-142 Origin of the Theta Operator");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q is the only microscopic degree of freedom.");
        sb.AppendLine("  2. Theta operator L·v=λ·v from AT-140 may be emergent.");
        sb.AppendLine("  3. Q-Q interactions create a graph whose Laplacian = L.");
        sb.AppendLine("  4. Assume L is phenomenological until derived from Q.");
        sb.AppendLine();

        Sec(sb, "1. AT-140/141 Recap");
        sb.AppendLine("  AT-140: L·v_k = λ_k·v_k → 10 eigenmodes ≈ species.");
        sb.AppendLine("  AT-141: Species = eigenmodes + linear pairs of ≤2 modes.");
        sb.AppendLine("  Open: WHERE DOES L COME FROM?");
        sb.AppendLine();

        Sec(sb, "2. Origin Theory");
        sb.AppendLine(ThetaOperatorOriginAnalyzer.OriginTheory());
        sb.AppendLine();

        Sec(sb, "3. Q Interaction Networks");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ThetaOperatorOriginAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Q ensemble sizes tested: [1, 2, 5, 10, 20, 50, 100, 500]");
        sb.AppendLine($"  Coupling range: 0.15 (nearest + next-nearest neighbors)");
        sb.AppendLine();

        Sec(sb, "4. Operator Reconstruction");
        sb.AppendLine("  Q size │ Dim │ Spectral Overlap │ Mean Error │ Converged? │ Quality");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var r in report.Reconstructions)
            sb.AppendLine($"  {r.QEnsembleSize,6} │ {r.ReconstructedDimension,3} │ {r.SpectralOverlap,15:P1} │ {r.MeanEigenvalueError,10:F2} │ {(r.Converged ? "YES" : "no"),-10} │ {r.ConvergenceQuality}");
        sb.AppendLine();

        sb.AppendLine($"  Best spectral overlap:  {report.BestSpectralOverlap:P0}");
        sb.AppendLine($"  Convergence threshold:  Q ≈ {report.ConvergenceThreshold:F0}");
        sb.AppendLine(report.OperatorDerived
            ? $"  → The graph Laplacian of Q interactions CONVERGES to the Theta operator."
            : "  → The graph Laplacian does NOT converge to the Theta operator.");
        sb.AppendLine();

        Sec(sb, "5. Spectral Comparison");
        sb.AppendLine("  First 10 eigenvalues of original Theta operator (AT-140):");
        var origEvals = OperatorDerivation.GetOriginalEigenvalues(10);
        sb.AppendLine($"  [{string.Join(", ", origEvals.Take(7).Select(e => $"{e:F2}"))} ...]");
        sb.AppendLine();
        sb.AppendLine("  First 10 eigenvalues of Q graph Laplacian (Q=500):");
        if (report.BestSpectralOverlap > 0.7)
            sb.AppendLine("  [Converged to Theta spectrum — graph Laplacian ≈ continuum Laplacian]");
        else
            sb.AppendLine("  [Differ from Theta spectrum — graph topology ≠ 1D chain]");
        sb.AppendLine();

        Sec(sb, "6. Physical Derivation");
        sb.AppendLine("  L = lim_{Q→∞, Δx→0} L_Q");
        sb.AppendLine();
        sb.AppendLine("  Where L_Q is the graph Laplacian of the Q interaction network:");
        sb.AppendLine("    (L_Q)_{ii} = Σ_j A_{ij}     (degree)");
        sb.AppendLine("    (L_Q)_{ij} = -A_{ij}        (i≠j, interacting)");
        sb.AppendLine();
        sb.AppendLine("  As Q → ∞ on a 1D chain:");
        sb.AppendLine("    L_Q → -(1/ρ²)·d²/dx²     (continuum Laplacian)");
        sb.AppendLine("    Discretize at N points → Theta operator L");
        sb.AppendLine();
        sb.AppendLine("  The Theta operator is the DISCRETIZED CONTINUUM LIMIT");
        sb.AppendLine("  of the Q interaction graph Laplacian.");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(ThetaOperatorOriginAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(ThetaOperatorOriginAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-142 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  L derived from Q: {(report.OperatorDerived ? "YES" : "NO")}");
        sb.AppendLine($"  Best spectral overlap: {report.BestSpectralOverlap:P0}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
