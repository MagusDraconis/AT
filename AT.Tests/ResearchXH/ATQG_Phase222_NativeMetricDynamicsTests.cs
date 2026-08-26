using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 222 — Native Metric Dynamics. Derive gravitational dynamics from Q-event evolution — no new
/// primitives, ρ only, deterministic. Rejects imported BDG/Einstein dynamics.
/// </summary>
public class ATQG_Phase222_NativeMetricDynamicsTests : ResearchTestBase
{
    public ATQG_Phase222_NativeMetricDynamicsTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2220_ActualizationFlowAndCountConservation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2220: the actualization flow — branching densities and count conservation");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Q-events actualize in generations k=0..K-1 via the Galton-Watson branching process (QG1).");
        sb.AppendLine("  - The counting measure is ρ_k = μ^k/S, S = Σ_{j<K} μ^j; the total population is conserved");
        sb.AppendLine("    by construction (S is the normalizer) — this is the native continuity statement.");
        sb.AppendLine();

        double[] traj = NativeMetricDynamics.DensityTrajectory(2.0, 8);
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine("  ρ_k (μ=2, K=8): " + string.Join(", ", traj.Select(t => $"{t:F4}")));
        sb.AppendLine($"  Total population S = {NativeMetricDynamics.TotalPopulation(2.0, 8):F2} (Σ 2^j)");
        sb.AppendLine($"  Σ_k ρ_k = {traj.Sum():F6} (count conservation, μ=2)? {NativeMetricDynamics.CountConserved(2.0, 8)}");
        sb.AppendLine($"  Σ_k ρ_k = {NativeMetricDynamics.DensityTrajectory(1.0, 8).Sum():F6} (count conservation, μ=1)? {NativeMetricDynamics.CountConserved(1.0, 8)}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The actualization flow carries a well-defined density ρ_k at every generation.");
        sb.AppendLine("  - The count is conserved by the branching structure (no sources/sinks) — the native");
        sb.AppendLine("    continuity/Noether statement (matter = deficit conserved, QG194).");

        Output.WriteLine(sb.ToString());

        Assert.True(NativeMetricDynamics.CountConserved(2.0, 8), "count must be conserved (μ=2)");
        Assert.True(NativeMetricDynamics.CountConserved(1.0, 8), "count must be conserved (μ=1)");
        Assert.Equal(255.0, NativeMetricDynamics.TotalPopulation(2.0, 8), 6);
    }

    [Fact]
    public void ATQG2221_BranchingContinuityAndDensityEvolution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2221: branching continuity — the density evolution equation from Q-events");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The density advances generation by generation by the branching ratio: ρ_{k+1} = μ·ρ_k.");
        sb.AppendLine("  - The continuum limit is the exponential flow ∂_t ρ = (ln μ)·ρ; at criticality μ=1");
        sb.AppendLine("    (α=0, QG206) the density is stationary.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Branching continuity ρ_{{k+1}} = μ·ρ_k (μ=2, K=8)? {NativeMetricDynamics.BranchingContinuity(2.0, 8)}");
        sb.AppendLine($"  ρ_5 = {NativeMetricDynamics.Density(2.0, 5, 8):F6}, μ·ρ_4 = {2.0 * NativeMetricDynamics.Density(2.0, 4, 8):F6}");
        sb.AppendLine($"  Density rate ∂_t ρ = ln(μ)·ρ: ln(2) = {NativeMetricDynamics.DensityRate(2.0):F4}");
        sb.AppendLine($"  Density rate ∂_t ρ = ln(1) = {NativeMetricDynamics.DensityRate(1.0):F4} (criticality)");
        sb.AppendLine($"  Density static at criticality (μ=1)? {NativeMetricDynamics.DensityStaticAtCriticality()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The density evolution equation is DERIVED from the Q-event branching process.");
        sb.AppendLine("  - At criticality the flow is stationary — the log-deficit attractor (α=0, QG206).");

        Output.WriteLine(sb.ToString());

        Assert.True(NativeMetricDynamics.BranchingContinuity(2.0, 8), "branching continuity must hold");
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality(), "density is stationary at criticality");
    }

    [Fact]
    public void ATQG2222_ClassificationDynamicsOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2222: classification — DYNAMICS ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - g = ρ^(2/d)η (QG197), so the metric inherits the density flow: g_{k+1} = μ^(2/d)·g_k.");
        sb.AppendLine("  - The Einstein tensor of the flowing ρ is Bianchi-consistent; G = κT holds via the");
        sb.AppendLine("    independent deficit dust (QG195) — no imported action.");
        sb.AppendLine();

        int score = NativeMetricDynamics.OriginScore();
        string classification = NativeMetricDynamics.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Metric scale factor μ^(2/d) (μ=2, d=3) = {NativeMetricDynamics.MetricScaleFactor(2.0, 3):F4}");
        sb.AppendLine($"  Metric rate ∂_t g = (2/d)·ln(μ)·g (μ=2, d=3) = {NativeMetricDynamics.MetricRate(2.0, 3):F4}");
        sb.AppendLine($"  Metric follows density (|∂_t g − (2/d)∂_t ρ|) = {NativeMetricDynamics.MetricFollowsDensity(2.0, 3):E2}");
        sb.AppendLine($"  Metric static at criticality (μ=1)? {NativeMetricDynamics.MetricStaticAtCriticality(3)}");
        sb.AppendLine($"  Max Bianchi residual (a=1.0, d=3) = {NativeMetricDynamics.MaxBianchiResidual(1.0, 3):E2}");
        sb.AppendLine($"  Bianchi consistent? {NativeMetricDynamics.BianchiConsistent(1.0, 3)}");
        sb.AppendLine($"  Matter tensor independent of G? {NativeMetricDynamics.MatterIndependentOfG()}");
        sb.AppendLine($"  Uses ρ only / no imported action? {NativeMetricDynamics.UsesRhoOnly()} / {NativeMetricDynamics.NoImportedAction()}");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The native evolution equations are:");
        sb.AppendLine("      ρ_{k+1} = μ·ρ_k        (density, from branching continuity)");
        sb.AppendLine("      g_{k+1} = μ^(2/d)·g_k  (metric, from g = ρ^(2/d)η)");
        sb.AppendLine("    i.e. ∂_t g = (2/d)(∂_t ρ/ρ)g with ∂_t ρ = (ln μ)ρ — the metric moves because ρ moves.");
        sb.AppendLine("  - No BDG action and no Einstein dynamics are imported; the Einstein tensor of the");
        sb.AppendLine("    flowing ρ is Bianchi-consistent and recovers G = κT via the deficit dust (QG195).");
        sb.AppendLine($"  ⇒ {classification} — this closes the QG221 gap (b) 'native metric dynamics'.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("DYNAMICS ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(NativeMetricDynamics.BianchiConsistent(1.0, 3), "the derived dynamics must be Bianchi-consistent");
    }
}
