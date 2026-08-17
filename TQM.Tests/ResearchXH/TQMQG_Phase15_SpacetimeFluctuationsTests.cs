using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 15 — spacetime fluctuations. Tests whether Poisson event-count fluctuations δρ/ρ = 1/√N
/// propagate to metric and curvature fluctuations, and whether they are graviton-like (tensor) or scalar
/// (conformal).
///
/// Tests: TQMQG150 (Poisson variance/scaling), TQMQG151 (metric/curvature propagation + correlation length),
///        TQMQG152 (classification: scalar, not graviton).
/// </summary>
public class TQMQG_Phase15_SpacetimeFluctuationsTests : ResearchTestBase
{
    public TQMQG_Phase15_SpacetimeFluctuationsTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG150: Poisson variance — δρ/ρ = 1/√N ─────────────────────────────────────

    [Fact]
    public void TQMQG150_PoissonVariance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG150: Poisson event-count fluctuations — δρ/ρ = 1/√N");

        sb.AppendLine($"{"N",8} {"δρ/ρ = 1/√N",14}");
        foreach (double N in new[] { 10.0, 100.0, 1000.0, 10000.0 })
        {
            sb.AppendLine($"{N,8:F0} {SpacetimeFluctuations.DensityFluctuation(N),14:F5}");
        }

        bool poissonScaling = Math.Abs(SpacetimeFluctuations.DensityFluctuation(100.0) - 0.1) < 1e-9;
        bool decreasesWithN = SpacetimeFluctuations.DensityFluctuation(1000.0) < SpacetimeFluctuations.DensityFluctuation(100.0);

        sb.AppendLine();
        sb.AppendLine($"δρ/ρ = 1/√N (Poisson): {poissonScaling}");
        sb.AppendLine($"fluctuations decrease with N (suppressed at large scale): {decreasesWithN}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the counting measure has Poisson statistics (Var N = N), so δρ/ρ = 1/√N — the");
        sb.AppendLine("relative fluctuation is suppressed as 1/√N (spacetime-foam scaling).");
        Output.WriteLine(sb.ToString());

        Assert.True(poissonScaling, "density fluctuation should be 1/√N");
        Assert.True(decreasesWithN, "fluctuations should decrease with N");
    }

    // ── TQMQG151: metric and curvature inherit the fluctuation ──────────────────────

    [Fact]
    public void TQMQG151_MetricCurvaturePropagation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG151: δρ propagates to δg and δR");

        int d = 3;
        sb.AppendLine($"{"N",8} {"δρ/ρ",12} {"δg/g",12} {"δR/R",12}");
        foreach (double N in new[] { 100.0, 1000.0, 10000.0 })
        {
            double drho = SpacetimeFluctuations.DensityFluctuation(N);
            double dg = SpacetimeFluctuations.MetricFluctuation(N, d);
            double dr = SpacetimeFluctuations.CurvatureFluctuation(N);
            sb.AppendLine($"{N,8:F0} {drho,12:F5} {dg,12:F5} {dr,12:F5}");
        }

        bool metricPropagates = Math.Abs(SpacetimeFluctuations.MetricFluctuation(1000.0, d) - (2.0 / d) * 0.0316227766) < 1e-6;
        bool curvaturePropagates = Math.Abs(SpacetimeFluctuations.CurvatureFluctuation(1000.0) - 0.0316227766) < 1e-6;

        sb.AppendLine();
        sb.AppendLine($"δg/g = (2/d)·δρ/ρ: {metricPropagates}");
        sb.AppendLine($"δR/R ≈ δρ/ρ: {curvaturePropagates}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the metric g = ρ^(2/d)η and curvature R(ρ) inherit the Poisson fluctuation 1/√N, with");
        sb.AppendLine("the correlation length set by the cell size ℓ (Poisson events are uncorrelated beyond one cell).");
        Output.WriteLine(sb.ToString());

        Assert.True(metricPropagates, "metric fluctuation should equal (2/d)·δρ/ρ");
        Assert.True(curvaturePropagates, "curvature fluctuation should equal δρ/ρ");
    }

    // ── TQMQG152: classification — scalar, not graviton ─────────────────────────────

    [Fact]
    public void TQMQG152_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG152: are the fluctuations graviton-like (tensor) or scalar (conformal)?");

        int d = 3;
        double trace = SpacetimeFluctuations.MetricFluctuationTrace(1000.0, d);
        double traceless = SpacetimeFluctuations.MetricFluctuationTraceless(1000.0, d);

        sb.AppendLine($"metric fluctuation δg_μν = (2/d)(δρ/ρ)g_μν is PROPORTIONAL to the metric:");
        sb.AppendLine($"  trace δg^μ_μ = {trace:F5} (NON-zero → scalar/conformal)");
        sb.AppendLine($"  traceless (graviton) part = {traceless:E2} (ZERO → no tensor modes)");

        bool scalarOnly = Math.Abs(traceless) < 1e-9;
        bool hasTrace = Math.Abs(trace) > 1e-3;

        sb.AppendLine();
        sb.AppendLine($"fluctuations are pure-trace (scalar), traceless part vanishes: {scalarOnly && hasTrace}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL — scalar fluctuations emerge, but NOT graviton-like.");
        sb.AppendLine("  • The fluctuations EMERGE statistically: Poisson δρ/ρ = 1/√N propagates to δg and δR (TQMQG150/151),");
        sb.AppendLine("    with the correct 1/√N (spacetime-foam) scaling.");
        sb.AppendLine("  • But they are SCALAR (conformal): δg_μν = (2/d)(δρ/ρ)g_μν is pure trace, so the traceless");
        sb.AppendLine("    (transverse-traceless) graviton modes do NOT fluctuate — they are frozen by conformal flatness");
        sb.AppendLine("    (Weyl = 0, QG10).");
        sb.AppendLine("  • Graviton-like (tensor) fluctuations would require relaxing conformal flatness (admitting a");
        sb.AppendLine("    dynamical Weyl/ψ-field), which TQM does not provide.");
        Output.WriteLine(sb.ToString());

        Assert.True(scalarOnly && hasTrace, "fluctuations should be pure trace (scalar), no graviton part");
    }
}
