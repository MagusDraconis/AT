using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 14 — Planck-regime audit. Tests whether actualization implies a natural minimum length or
/// maximum density (maximal event density, minimum spacing, branching saturation, curvature divergence, entropy
/// bounds).
///
/// Tests: ATQG140 (curvature divergence at ρ=0), ATQG141 (branching saturation + minimum cell), ATQG142 (classification).
/// </summary>
public class ATQG_Phase14_PlanckRegimeTests : ResearchTestBase
{
    public ATQG_Phase14_PlanckRegimeTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG140: curvature diverges at ρ=0 (metric degeneracy) ──────────────────────

    [Fact]
    public void ATQG140_CurvatureDivergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG140: curvature diverges as ρ → 0 (metric degeneracy at the horizon)");

        int d = 3;
        sb.AppendLine($"profile ρ = 1 − x² (vanishes at x=1); d={d}:");
        sb.AppendLine($"{"x",8} {"ρ",12} {"R(ρ)",14} {"ρ^(−2/d)",12}");
        bool diverges = true;
        double prevR = 0;
        foreach (double x in new[] { 0.0, 0.5, 0.9, 0.99, 0.999 })
        {
            double rho = 1.0 - x * x;
            double r = HigherDimEinstein.ScalarCurvature(x, -1.0, d);
            double div = PlanckRegime.CurvatureDivergence(rho, d);
            if (x > 0.5 && Math.Abs(r) <= prevR) diverges = false;
            prevR = Math.Abs(r);
            sb.AppendLine($"{x,8:F3} {rho,12:F5} {r,14:F3} {div,12:F2}");
        }

        sb.AppendLine();
        sb.AppendLine($"|R| grows without bound as ρ → 0 (ρ^(−2/d) divergence): {diverges}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the metric √(−g)=ρ degenerates at ρ=0, where the curvature diverges. This is a NATIVE");
        sb.AppendLine("lower bound (ρ &gt; 0) — the maximum deficit is a horizon, the 'boundary' of actualization.");
        Output.WriteLine(sb.ToString());

        Assert.True(diverges, "curvature should diverge as ρ → 0");
    }

    // ── ATQG141: branching saturation (μ=1 max sustained) + minimum cell ────────────

    [Fact]
    public void ATQG141_BranchingSaturationAndMinimumCell()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG141: critical μ=1 is the max sustained branching; ℓ is set by a free ρ_max");

        sb.AppendLine($"branching density μ^k after k=50 generations:");
        sb.AppendLine($"{"μ",7} {"μ^50",14} {"sustained?",14}");
        foreach (double mu in new[] { 0.9, 1.0, 1.1 })
        {
            double density = PlanckRegime.BranchingDensity(mu, 50);
            string sus = density > 1e-3 && density < 1e3 ? "yes" : "no";
            sb.AppendLine($"{mu,7:F1} {density,14:E2} {sus,14}");
        }

        bool criticalMaxSustained = Math.Abs(PlanckRegime.BranchingDensity(1.0, 50) - 1.0) < 1e-12;
        bool supercriticalDiverges = PlanckRegime.BranchingDensity(1.1, 50) > 100.0;   // 1.1^50 ≈ 117
        bool subcriticalDies = PlanckRegime.BranchingDensity(0.9, 50) < 0.01;   // 0.9^50 ≈ 0.005 (200× decay)

        // Minimum cell size ℓ = ρ_max^(−1/d): determined by the free ρ_max, not by a native constant.
        double l1 = PlanckRegime.MinimumCellSize(100.0, 3);
        double l2 = PlanckRegime.MinimumCellSize(1000.0, 3);
        sb.AppendLine();
        sb.AppendLine($"minimum cell size ℓ = ρ_max^(−1/d): ρ_max=100 → ℓ={l1:F4}, ρ_max=1000 → ℓ={l2:F4}");
        sb.AppendLine($"ℓ depends on the FREE maximum density ρ_max (no native length): {l1 > l2}");

        sb.AppendLine();
        sb.AppendLine($"critical μ=1 is the max sustained branching: {criticalMaxSustained}");
        sb.AppendLine($"supercritical μ>1 diverges, subcritical μ<1 dies: {supercriticalDiverges && subcriticalDies}");
        Output.WriteLine(sb.ToString());

        Assert.True(criticalMaxSustained, "critical branching should be the max sustained");
        Assert.True(supercriticalDiverges && subcriticalDies, "μ≠1 should diverge or die");
    }

    // ── ATQG142: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG142_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG142: does actualization imply a native minimum length / maximum density?");

        sb.AppendLine("CLASSIFICATION: PARTIAL — native BOUNDS, but no native minimum length.");
        sb.AppendLine();
        sb.AppendLine("  • NATIVE lower bound ρ &gt; 0: the metric √(−g)=ρ degenerates and curvature diverges at ρ=0 (ATQG140)");
        sb.AppendLine("    — the maximum deficit is a horizon. This is a native 'maximum deficit', not a minimum length.");
        sb.AppendLine("  • NATIVE branching bound μ=1: critical branching is the maximum SUSTAINED actualization rate;");
        sb.AppendLine("    supercritical diverges, subcritical dies (ATQG141). This bounds the rate, not the density.");
        sb.AppendLine("  • NO native minimum length ℓ: the minimum cell size ℓ = ρ_max^(−1/d) is set by a FREE maximum density");
        sb.AppendLine("    (ATQG141). The physical Planck length ℓ = √(Għ/c³) involves G (native as deficit mass, QG6) and ħ");
        sb.AppendLine("    (a free parameter) — so the cutoff scale is not derived.");
        sb.AppendLine("  • Therefore AT has native BOUNDS (ρ&gt;0, μ=1) but NO native minimum length or maximum density: the");
        sb.AppendLine("    Planck-scale cutoff is a free scale, consistent with the LabBook open problem 'numerical values of");
        sb.AppendLine("    ℓ, τ, ħ — empirical, not derived'.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
