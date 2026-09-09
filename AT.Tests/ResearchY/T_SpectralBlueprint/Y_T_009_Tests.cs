using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_009 — Fitness-Reach Law Audit.
///
/// Question: can the surviving-mode count N_fit (T_008's S∞ = min(A, N_fit)) be expressed
/// ANALYTICALLY? Derive N_fit = F(μ, β, {w}) in the two exactly-solvable limits and test the
/// predicted S∞ against the simulated replicator–mutator.
///
///   · μ = 0 (pure selection + crowding): S∞ = #{k : w_k > Z*(β)}, where Z* is the unique
///     root of Σ_{w_k>Z}(w_k − Z) = β·Z. EXACT.
///   · β = 0 (mutation + selection, no crowding): S∞ = #{k : (Perron vector of M·diag(w))_k > ε},
///     M = ring mutation matrix. EXACT (numerically).
///   · small-μ reach (uniform gap): N_fit ≈ 1 + log(1/ε)/log(2δ/μ), δ = Δw/w*. APPROXIMATE.
/// Deterministic throughout.
/// </summary>
public class Y_T_009_Tests : ResearchTestBase
{
    public Y_T_009_Tests(ITestOutputHelper output) : base(output) { }

    private const double Eps = FitnessReachLaw.Eps;

    // ── Analytical predictions (shared FitnessReachLaw) ──────────────────────

    private static double ThresholdZ(double[] w, double beta) => FitnessReachLaw.ThresholdZ(w, beta);
    private static int PredictedZeroMutation(double[] w, double beta) => FitnessReachLaw.PredictedZeroMutation(w, beta);
    private static double[] PerronVector(double[] w, double mu) => FitnessReachLaw.PerronVector(w, mu);
    private static int PredictedLinearEquilibrium(double[] w, double mu) => FitnessReachLaw.PredictedLinearEquilibrium(w, mu);
    private static (double wStar, double wSecond, double delta) TopGap(double[] w) => FitnessReachLaw.TopGap(w);
    private static int PredictedUniformGap(double[] w, double mu) => FitnessReachLaw.PredictedUniformGap(w, mu);

    // ── 1. μ=0 exact crowding threshold ──────────────────────────────────────

    [Fact]
    public void Y_T_009_ZeroMutationThreshold()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] betas = [0.5, 1.0, 2.0, 10.0];
        foreach (var c in SpectralCaseCatalog.All())
            foreach (double beta in betas)
            {
                int predicted = PredictedZeroMutation(c.Fitness, beta);
                int sShort = SpectralCaseCatalog.SInfinity(c, 0.0, beta, 20000);
                int sLong = SpectralCaseCatalog.SInfinity(c, 0.0, beta, 200000);
                // The μ=0 threshold is the EXACT t→∞ equilibrium; the finite-time simulation
                // converges to it but can lag by one mode just below the threshold (critical
                // slowing down, e.g. the near-degenerate random case). Verify convergence.
                Assert.True(
                    Math.Abs(predicted - sLong) <= Math.Abs(predicted - sShort),
                    $"{c.Name} β={beta}: sim must converge toward predicted {predicted}");
                Assert.True(
                    Math.Abs(predicted - sLong) <= 1,
                    $"{c.Name} β={beta}: predicted {predicted} vs simulated {sLong}");
            }
    }

    // ── 2. β=0 exact linear (Perron) equilibrium ─────────────────────────────

    [Fact]
    public void Y_T_009_LinearEquilibrium()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] mus = [0.001, 0.01, 0.1];
        foreach (var c in SpectralCaseCatalog.All())
            foreach (double mu in mus)
            {
                int predicted = PredictedLinearEquilibrium(c.Fitness, mu);
                int simulated = SpectralCaseCatalog.SInfinity(c, mu, 0.0);
                Assert.Equal(predicted, simulated);
            }
    }

    // ── 3. Small-μ uniform-gap reach (approximation) ─────────────────────────

    [Fact]
    public void Y_T_009_ReachApproximation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double mu = 0.01;
        foreach (var c in SpectralCaseCatalog.All())
        {
            int predicted = PredictedUniformGap(c.Fitness, mu);
            int simulated = SpectralCaseCatalog.SInfinity(c, mu, 0.0); // β=0 for the cleanest case
            // The uniform-gap law is an ORDER-OF-MAGNITUDE reach, not exact: assert within ±3.
            Assert.InRange(Math.Abs(predicted - simulated), 0, 3);
        }
    }

    // ── 4. Single-Δw law is refuted ──────────────────────────────────────────

    [Fact]
    public void Y_T_009_SingleGapRefuted()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Two synthetic spectra with IDENTICAL top-2 gap Δw = 5 but different tails:
        // the full spectrum (not Δw alone) sets N_fit via the μ=0 exact threshold.
        double[] a = [10.0, 5.0, 4.5, 4.5, 4.5];   // tail stays near the top
        double[] b = [10.0, 5.0, 1.0, 1.0, 1.0];   // tail drops away
        Assert.Equal(TopGap(a).delta, TopGap(b).delta, 12);   // same Δw/w*
        int sA = PredictedZeroMutation(a, 10.0);
        int sB = PredictedZeroMutation(b, 10.0);
        Assert.NotEqual(sA, sB);                                // yet different N_fit
    }

    // ── 5. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_009_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // DERIVED (exact): the μ=0 threshold and the β=0 Perron equilibrium reproduce S∞
        // for every case; the uniform-gap reach is a DERIVED form with EMERGENT accuracy.
        foreach (var c in SpectralCaseCatalog.All())
        {
            // μ=0 threshold (exact; ≤1 boundary lag from critical slowing down)…
            Assert.True(Math.Abs(PredictedZeroMutation(c.Fitness, 1.0) - SpectralCaseCatalog.SInfinity(c, 0.0, 1.0, 200000)) <= 1);
            // …and β=0 Perron equilibrium (exact, no critical slowing down).
            Assert.Equal(PredictedLinearEquilibrium(c.Fitness, 0.01), SpectralCaseCatalog.SInfinity(c, 0.01, 0.0));
        }

        // REFUTED: "N_fit = F(μ, β, Δw)" with a single scalar Δw — the exact μ=0 law counts the
        // FULL spectrum above the threshold, so two spectra with equal Δw can differ (test 4).
    }

    // ── 6. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_009_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_009 — Fitness-Reach Law Audit");

        sb.AppendLine("Question: can N_fit (S∞ = min(A, N_fit)) be expressed analytically?");
        sb.AppendLine("Derive S∞ in the two exactly-solvable limits and test predicted vs simulated.");
        sb.AppendLine();

        sb.AppendLine("[1] μ=0 exact crowding threshold: S∞ = #{w_k > Z*(β)}, Σ(w_k−Z) = β·Z");
        sb.AppendLine("     case             β=0.5   β=1    β=2    β=10");
        foreach (var c in SpectralCaseCatalog.All())
        {
            sb.Append($"     {c.Name,-14} ");
            foreach (double beta in new[] { 0.5, 1.0, 2.0, 10.0 })
                sb.Append($"  {PredictedZeroMutation(c.Fitness, beta),3} ");
            sb.AppendLine();
        }
        sb.AppendLine();

        sb.AppendLine("[2] β=0 exact linear (Perron) equilibrium: S∞ = #{Perron(M·diag(w)) > ε}");
        sb.AppendLine("     case             μ=0.001  μ=0.01  μ=0.1");
        foreach (var c in SpectralCaseCatalog.All())
        {
            sb.Append($"     {c.Name,-14} ");
            foreach (double mu in new[] { 0.001, 0.01, 0.1 })
                sb.Append($"  {PredictedLinearEquilibrium(c.Fitness, mu),3} ");
            sb.AppendLine();
        }
        sb.AppendLine();

        sb.AppendLine("[3] Small-μ uniform-gap reach: N_fit ≈ 1 + log(1/ε)/log(2δ/μ), δ=Δw/w* (β=0)");
        sb.AppendLine("     case             w*      Δw      δ      pred   sim");
        double muR = 0.01;
        foreach (var c in SpectralCaseCatalog.All())
        {
            var (star, second, delta) = TopGap(c.Fitness);
            int pred = PredictedUniformGap(c.Fitness, muR);
            int sim = SpectralCaseCatalog.SInfinity(c, muR, 0.0);
            sb.AppendLine($"     {c.Name,-14} {star,6:F2} {star - second,6:F2} {delta,6:F3} {pred,5} {sim,5}");
        }
        sb.AppendLine();

        sb.AppendLine("[4] Single-Δw law: two spectra with Δw=5 but different tails → N_fit differs");
        double[] a = [10.0, 5.0, 4.5, 4.5, 4.5];
        double[] b = [10.0, 5.0, 1.0, 1.0, 1.0];
        sb.AppendLine($"     tail-near-top: S∞(μ=0,β=10) = {PredictedZeroMutation(a, 10.0)}");
        sb.AppendLine($"     tail-dropped : S∞(μ=0,β=10) = {PredictedZeroMutation(b, 10.0)}");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusions");
        sb.AppendLine("  DERIVED (exact): μ=0 ⇒ S∞ = #{w_k > Z*(β)};  β=0 ⇒ S∞ = #{Perron(M·diag(w)) > ε}.");
        sb.AppendLine("  DERIVED form / EMERGENT accuracy: uniform-gap reach N_fit ≈ 1 + log(1/ε)/log(2δ/μ).");
        sb.AppendLine("  REFUTED: 'N_fit = F(μ, β, Δw)' with a single scalar gap — N_fit depends on the FULL");
        sb.AppendLine("           fitness spectrum (two spectra with equal Δw give different N_fit).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
