using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 231 — Structure Formation Origin. Derive the density-contrast growth law from Q-event
/// statistics: Poisson fluctuations, actualization variance, density contrast growth, critical branching,
/// attractor formation, network clustering. No new primitives, deterministic. Closes QG229's last open
/// cosmology feature.
/// </summary>
public class ATQG_Phase231_StructureFormationOriginTests : ResearchTestBase
{
    public ATQG_Phase231_StructureFormationOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2310_PoissonSeed()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2310: the Poisson counting variance seeds the density contrast");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The initial density field is uniform critical + Poisson counting noise (QG15/228).");
        sb.AppendLine("  - The seed amplitude is the relative Poisson fluctuation δ_i = 1/√⟨N⟩.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  δ_i(⟨N⟩=1e6)  = {StructureFormationOrigin.PoissonSeed(1e6):F6}");
        sb.AppendLine($"  δ_i(⟨N⟩=1e8)  = {StructureFormationOrigin.PoissonSeed(1e8):F6}");
        sb.AppendLine($"  δ_i(⟨N⟩=1e10) = {StructureFormationOrigin.PoissonSeed(1e10):F6}");
        sb.AppendLine($"  Seed variance Var(δ_i) = 1/⟨N⟩ = {StructureFormationOrigin.SeedVariance(1e6):E2}");
        sb.AppendLine($"  Seed scale-free? {StructureFormationOrigin.SeedScaleFree(1e6)}");
        sb.AppendLine($"  Actualization variance scale-free (Var(2k)/Var(k)=2)? {StructureFormationOrigin.ActualizationVarianceScaleFree()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The seed amplitude is derived from the Q-event counting statistics (Poisson).");
        sb.AppendLine("  - At criticality the actualization variance is scale-free — the seed spectrum needs");
        sb.AppendLine("    no inflation.");

        Output.WriteLine(sb.ToString());

        Assert.True(StructureFormationOrigin.PoissonSeed(1e6) > 0.0, "the Poisson seed must be positive");
        Assert.True(StructureFormationOrigin.SeedVariance(1e6) > 0.0, "the seed variance must be positive");
        Assert.True(StructureFormationOrigin.ActualizationVarianceScaleFree(), "the actualization variance must be scale-free");
    }

    [Fact]
    public void ATQG2311_ContrastGrowthLaw()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2311: the growth law δ(a) = δ_i·a/a_i — linear, pressureless dust");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Matter = the deficit dust T_μν = ρ_m·v_μ·v_ν (QG195/196): pressureless, self-gravitating.");
        sb.AppendLine("  - The contrast grows linearly with the scale factor a = ρ^(1/d) (QG77).");
        sb.AppendLine();

        double seed = StructureFormationOrigin.PoissonSeed(1e6);
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Deficit dust pressureless? {StructureFormationOrigin.DeficitDustPressureless()}");
        sb.AppendLine($"  Deficit dust self-gravitating? {StructureFormationOrigin.DeficitDustSelfGravitating()}");
        sb.AppendLine($"  δ(a/a_i=1)   = {StructureFormationOrigin.ContrastGrowth(seed, 1.0):E4}");
        sb.AppendLine($"  δ(a/a_i=10)  = {StructureFormationOrigin.ContrastGrowth(seed, 10.0):E4}");
        sb.AppendLine($"  δ(a/a_i=100) = {StructureFormationOrigin.ContrastGrowth(seed, 100.0):E4}");
        sb.AppendLine($"  Var(δρ/ρ) at a/a_i=10 = {StructureFormationOrigin.ContrastVariance(1e6, 10.0):E2}");
        sb.AppendLine($"  Growth ratio δ(2)/δ(1) = {StructureFormationOrigin.GrowthRatio(1.0, 2.0):F1} (linear)");
        sb.AppendLine($"  Growth is linear? {StructureFormationOrigin.GrowthIsLinear()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The deficit dust is pressureless and self-gravitating ⇒ over-densities amplify.");
        sb.AppendLine("  - The growth law is linear in the scale factor — the canonical dust clustering,");
        sb.AppendLine("    deterministic and independent of the seed amplitude.");

        Output.WriteLine(sb.ToString());

        Assert.True(StructureFormationOrigin.GrowthIsLinear(), "the growth must be linear in the scale factor");
        Assert.True(StructureFormationOrigin.ContrastGrowth(seed, 10.0) > seed, "the contrast must grow");
        Assert.True(StructureFormationOrigin.DeficitDustPressureless(), "the deficit dust must be pressureless");
        Assert.True(StructureFormationOrigin.DeficitDustSelfGravitating(), "the deficit dust must be self-gravitating");
    }

    [Fact]
    public void ATQG2312_ClassificationStructureOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2312: classification — STRUCTURE ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Seed = Poisson (1/√⟨N⟩); growth = linear dust; attractor builds the clustering.");
        sb.AppendLine("  - No inflation, no imported spectrum, no fitted seeds.");
        sb.AppendLine();

        int score = StructureFormationOrigin.OriginScore();
        string classification = StructureFormationOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Attractor builds structure (exact FP + basin ≥ 0.9)? {StructureFormationOrigin.AttractorBuildsStructure()}");
        sb.AppendLine($"  Network spectrum hierarchical (QG104)? {StructureFormationOrigin.NetworkSpectrumHierarchical()}");
        sb.AppendLine($"  No inflation (scale-free seed)? {StructureFormationOrigin.NoInflation()}");
        sb.AppendLine($"  No fitted seeds? {StructureFormationOrigin.NoFittedSeeds()}");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 Poisson seed ({StructureFormationOrigin.PoissonSeed(1e6) > 0.0})");
        sb.AppendLine($"    +1 scale-free actualization variance ({StructureFormationOrigin.ActualizationVarianceScaleFree()})");
        sb.AppendLine($"    +1 pressureless self-gravitating dust ({StructureFormationOrigin.DeficitDustPressureless()})");
        sb.AppendLine($"    +1 linear growth law ({StructureFormationOrigin.GrowthIsLinear()})");
        sb.AppendLine($"    +1 attractor + hierarchy + no imports ({StructureFormationOrigin.AttractorBuildsStructure()})");
        sb.AppendLine($"  Full chain holds? {StructureFormationOrigin.StructureChainHolds()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The density contrast is seeded by the Poisson counting variance of Q-events");
        sb.AppendLine("    (δ_i = 1/√⟨N⟩, scale-free at criticality) and grows linearly with the scale factor");
        sb.AppendLine("    a = ρ^(1/d): δ(a) = δ_i·a/a_i, Var = (1/⟨N⟩)·(a/a_i)².");
        sb.AppendLine("  - No inflation, no imported perturbation spectrum, no fitted seeds.");
        sb.AppendLine($"  ⇒ {classification} — structure formation is derived from Q-event statistics.");
        sb.AppendLine("  - This closes QG229's last open cosmology feature (structure formation).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("STRUCTURE ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(StructureFormationOrigin.StructureChainHolds(), "the full derivation chain must hold");
    }
}
