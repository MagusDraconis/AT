using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-RHO Phase 3 — microscopic origin of entropy maximization. G4-RHO2 showed α=0 emerges from entropy
/// gradient flow; here we ask WHY actualization evolves toward maximal entropy, testing Q-event branching,
/// abundance-law dynamics, counting-measure statistics, random actualization, and maximum-likelihood
/// evolution. Classify: DERIVED / PREFERRED / POSTULATED.
///
/// Tests: G4-RHO30 (counting statistics → maximum likelihood), G4-RHO31 (maximum-likelihood evolution),
///        G4-RHO32 (classification).
/// </summary>
public class G4RHO_Phase3_EntropyOriginTests : ResearchTestBase
{
    public G4RHO_Phase3_EntropyOriginTests(ITestOutputHelper o) : base(o) { }

    private const int K = 8;
    private const double LAMBDA = 1.5;

    private static double Factorial(int n)
    {
        double f = 1.0;
        for (int i = 2; i <= n; i++) f *= i;
        return f;
    }

    private static double Microstates(int[] n)
    {
        double w = Factorial(n.Sum());
        foreach (int nk in n) w /= Factorial(nk);
        return w;
    }

    // ── G4-RHO30: counting statistics — the uniform allocation has the most microstates ─

    [Fact]
    public void G4_RHO30_CountingStatisticsMaximumLikelihood()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO30: counting statistics — the uniform allocation is the maximum-likelihood state");

        // Distributing N identical deficit quanta over K octaves: the number of microstates is the
        // multinomial W = N!/(∏ n_k!). By Stirling, ln W = N·H(α), so max W ⟺ max H ⟺ α=0 (uniform).
        double N = 1000.0;
        sb.AppendLine($"{"α",7} {"ln W = N·H(α)",16}");
        double lnW0 = 0, lnW1 = 0;
        foreach (double a in new[] { 0.0, 0.5, 1.0 })
        {
            double lnW = RhoDynamics.LogMicrostates(a, N, K, LAMBDA);
            if (a == 0.0) lnW0 = lnW;
            if (a == 1.0) lnW1 = lnW;
            sb.AppendLine($"{a,7:F1} {lnW,16:F2}");
        }
        double logRatio = lnW0 - lnW1;   // ln(W(0)/W(1)) = N·(H(0)−H(1))

        bool maxAtZero = lnW0 > RhoDynamics.LogMicrostates(0.5, N, K, LAMBDA)
                      && lnW0 > RhoDynamics.LogMicrostates(1.0, N, K, LAMBDA);
        bool astronomical = logRatio > 100.0;

        sb.AppendLine();
        sb.AppendLine($"ln W maximized at α=0: {maxAtZero}");
        sb.AppendLine($"ln(W(0)/W(1)) = {logRatio:F1} (W(0)/W(1) ≈ e^{logRatio:F0} ≈ 10^{logRatio / Math.Log(10):F0}): {astronomical}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: entropy is the log of the microstate count, H = (1/N) ln W. The uniform α=0");
        sb.AppendLine("allocation has an astronomically larger number of microstates — it is the MAXIMUM-LIKELIHOOD");
        sb.AppendLine("(most probable) configuration of an unbiased (no-preferred-scale) actualization process.");
        Output.WriteLine(sb.ToString());

        Assert.True(maxAtZero, "α=0 should maximize the microstate count");
        Assert.True(astronomical, "the uniform allocation should be astronomically more likely");
    }

    // ── G4-RHO31: maximum-likelihood evolution = the entropy-increasing diffusion ─────

    [Fact]
    public void G4_RHO31_MaximumLikelihoodEvolution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO31: the scale-space diffusion is the maximum-likelihood (entropy-increasing) evolution");

        var A = RhoDynamics.Increments(1.0, K, LAMBDA);   // biased α=1
        double h0 = RhoDynamics.EntropyOf(A);
        double hPrev = h0;
        bool monotonic = true;
        for (int i = 0; i < 3000; i++)
        {
            A = RhoDynamics.DiffuseStep(A, 0.4);
            double h = RhoDynamics.EntropyOf(A);
            if (h < hPrev - 1e-15) monotonic = false;
            hPrev = h;
        }

        sb.AppendLine($"H(α=1) = {h0:F6}  →  H(final) = {hPrev:F6}  (ln K = {Math.Log(K):F6})");
        sb.AppendLine($"H monotonically non-decreasing along the diffusion: {monotonic}");
        bool reachesMax = Math.Abs(hPrev - Math.Log(K)) < 1e-9;

        sb.AppendLine();
        sb.AppendLine($"diffusion drives H to its maximum (uniform, α=0): {reachesMax}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the scale-space diffusion (G4-RHO2) is EXACTLY the maximum-likelihood evolution —");
        sb.AppendLine("each step increases the number of accessible microstates (entropy), converging to the most");
        sb.AppendLine("probable (uniform, α=0) configuration.");
        Output.WriteLine(sb.ToString());

        Assert.True(monotonic, "entropy should not decrease along the diffusion");
        Assert.True(reachesMax, "the diffusion should reach the maximum-entropy state");
    }

    // ── G4-RHO32: exact counting + classification ─────────────────────────────────────

    [Fact]
    public void G4_RHO32_ExactCountingClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO32: exact microstate counting and classification");

        // Exact (small-N) counting: N=12 quanta over K=4 octaves.
        int[] uniform = { 3, 3, 3, 3 };      // α=0 (equal per octave)
        int[] biased = { 4, 3, 3, 2 };       // α>0 (biased toward small octaves)
        double wUniform = Microstates(uniform);
        double wBiased = Microstates(biased);

        sb.AppendLine($"uniform [3,3,3,3] (α=0):  W = {wUniform:F0}");
        sb.AppendLine($"biased  [4,3,3,2] (α>0):  W = {wBiased:F0}");
        bool uniformMoreLikely = wUniform > wBiased;

        sb.AppendLine();
        sb.AppendLine($"uniform has more microstates (more likely): {uniformMoreLikely}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: DERIVED (entropy maximization = maximum likelihood), with one postulate.");
        sb.AppendLine("  • The uniform allocation has the most microstates — a PURE COMBINATORIAL fact (counting).");
        sb.AppendLine("  • The system is most likely in the maximum-microstate configuration (maximum likelihood /");
        sb.AppendLine("    ergodic principle) — the standard statistical-mechanics bridge from counting to probability.");
        sb.AppendLine("  • The one postulate is INDIFFERENCE: actualization is unbiased across scales (no preferred");
        sb.AppendLine("    scale, all microstates equiprobable) — the scale-freeness already native to TQM.");
        sb.AppendLine("  • Therefore entropy maximization is DERIVED (from counting + indifference), not merely postulated;");
        sb.AppendLine("    only the indifference principle itself is postulated (and it is TQM's scale-freeness).");
        Output.WriteLine(sb.ToString());

        Assert.True(uniformMoreLikely, "the uniform allocation should have more microstates");
    }
}
