using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 230 — Lambda Origin. Derive the sign, existence, and scaling of Λ from Q-events: critical
/// branching, residual actualization pressure, uniform-state instability, information growth, counting-
/// measure vacuum. No new primitives, deterministic. Closes QG229's highest-impact blocker.
/// </summary>
public class TQMQG_Phase230_LambdaOriginTests : ResearchTestBase
{
    public TQMQG_Phase230_LambdaOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2300_LambdaExists()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2300: Λ exists — the critical branching vacuum has growing variance and positive information");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - At criticality (μ=1) the Galton-Watson mean is constant but the variance grows:");
        sb.AppendLine("    Var(Z_k) = k·σ² — the residual actualization pressure (QG228).");
        sb.AppendLine("  - Energy = actualization rate (QG89); the vacuum's positive information ⇒ positive energy.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Var(Z_0) = {LambdaOrigin.VacuumVariance(0):F1}, Var(Z_4) = {LambdaOrigin.VacuumVariance(4):F1}, Var(Z_8) = {LambdaOrigin.VacuumVariance(8):F1}");
        sb.AppendLine($"  Vacuum variance grows? {LambdaOrigin.VacuumVarianceGrows(4)}");
        sb.AppendLine($"  Vacuum info I_vac (fluct 0.05) = {LambdaOrigin.VacuumInformation(0.05, 8):F4} nats");
        sb.AppendLine($"  Vacuum info at zero fluctuation = {LambdaOrigin.VacuumInformation(0.0, 8):F6} (uniform, unattainable)");
        sb.AppendLine($"  Vacuum information positive? {LambdaOrigin.VacuumInformationPositive()}");
        sb.AppendLine($"  Λ exists? {LambdaOrigin.LambdaExists()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The critical vacuum's variance grows (residual pressure) and its information is");
        sb.AppendLine("    strictly positive (the uniform state is unattainable by a discrete counting process).");
        sb.AppendLine("  - Λ exists because the vacuum carries positive residual actualization energy.");

        Output.WriteLine(sb.ToString());

        Assert.True(LambdaOrigin.VacuumVarianceGrows(4), "the vacuum variance must grow");
        Assert.True(LambdaOrigin.VacuumInformationPositive(), "the vacuum information must be positive");
        Assert.True(LambdaOrigin.LambdaExists(), "Λ must exist");
    }

    [Fact]
    public void TQMQG2301_LambdaSignPositive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2301: Λ is positive — a repulsive vacuum drives the accelerating expansion");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The vacuum energy density ρ_Λ ∝ I_vac &gt; 0 (KL ≥ 0, zero only at the unattainable");
        sb.AppendLine("    uniform state).");
        sb.AppendLine("  - In the conformal framework (FRW a = ρ^(1/d), QG77) a constant positive vacuum energy");
        sb.AppendLine("    gives positive scale-factor acceleration (de Sitter-like).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Vacuum energy density ρ_Λ (fluct 0.05) = {LambdaOrigin.VacuumEnergyDensity(0.05, 8):F4} &gt; 0");
        sb.AppendLine($"  Λ positive? {LambdaOrigin.LambdaPositive()}");
        sb.AppendLine($"  Scale-factor acceleration H = √(ρ_Λ/3) = {LambdaOrigin.ScaleFactorAcceleration():F4} &gt; 0");
        sb.AppendLine($"  Vacuum repulsive (accelerating)? {LambdaOrigin.LambdaRepulsive()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The vacuum energy density is strictly positive (KL ≥ 0, zero only at the uniform");
        sb.AppendLine("    state that a discrete process cannot attain).");
        sb.AppendLine("  - A constant positive vacuum energy gives ȧ &gt; 0 — the repulsive vacuum drives the");
        sb.AppendLine("    accelerating expansion. Hence Λ &gt; 0.");

        Output.WriteLine(sb.ToString());

        Assert.True(LambdaOrigin.LambdaPositive(), "Λ must be positive");
        Assert.True(LambdaOrigin.LambdaRepulsive(), "the vacuum must be repulsive (accelerating)");
        Assert.True(LambdaOrigin.ScaleFactorAcceleration() > 0.0, "the scale factor must accelerate");
    }

    [Fact]
    public void TQMQG2302_ClassificationLambdaOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2302: classification — LAMBDA ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Scaling: M ∝ R (QG184) ⇒ ρ̄ ~ 1/R²; the vacuum tracks the single scale R ⇒ Λ ∝ 1/R².");
        sb.AppendLine("  - The cosmological coincidence Λ ~ H² is a structural identity (one scale), not a fit.");
        sb.AppendLine();

        int score = LambdaOrigin.OriginScore();
        string classification = LambdaOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Mean density ρ̄ ~ 1/R²: ρ̄(1) = {LambdaOrigin.MeanDensityScaling(1.0):F2}, ρ̄(2) = {LambdaOrigin.MeanDensityScaling(2.0):F4}");
        sb.AppendLine($"  Vacuum fraction Ω_Λ = {LambdaOrigin.VacuumFraction():F3}");
        sb.AppendLine($"  Λ(R) ∝ ρ̄(R): Λ(1) = {LambdaOrigin.LambdaScaling(1.0):F4}, Λ(2) = {LambdaOrigin.LambdaScaling(2.0):F4}");
        sb.AppendLine($"  Λ scales as 1/R²? {LambdaOrigin.LambdaScalesAsOneOverR2()}");
        sb.AppendLine($"  Coincidence resolved (Λ ~ H² ~ ρ̄)? {LambdaOrigin.CoincidenceResolved()}");
        sb.AppendLine($"  Uniform-state instability (realized vacuum rolls off)? {LambdaOrigin.UniformStateUnstable()}");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 existence (growing variance + positive info) ({LambdaOrigin.LambdaExists()})");
        sb.AppendLine($"    +1 positive / repulsive ({LambdaOrigin.LambdaPositive()} / {LambdaOrigin.LambdaRepulsive()})");
        sb.AppendLine($"    +1 scaling Λ ∝ 1/R² ({LambdaOrigin.LambdaScalesAsOneOverR2()})");
        sb.AppendLine($"    +1 coincidence resolved ({LambdaOrigin.CoincidenceResolved()})");
        sb.AppendLine($"    +1 uniform-state instability ({LambdaOrigin.UniformStateUnstable()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - EXISTENCE: the critical branching vacuum has growing variance (residual pressure) and");
        sb.AppendLine("    positive information — Λ exists because the uniform state is unattainable.");
        sb.AppendLine("  - SIGN: positive — a positive vacuum energy drives the conformal scale factor a = ρ^(1/d)");
        sb.AppendLine("    to accelerate (repulsive vacuum, accelerating expansion).");
        sb.AppendLine("  - SCALING: Λ ∝ 1/R² — the vacuum shares the single counting-measure scale R with matter");
        sb.AppendLine("    (M∝R, QG184 ⇒ ρ̄ ~ 1/R² ⇒ Λ ~ H²). The coincidence is a structural identity.");
        sb.AppendLine($"  ⇒ {classification} — Λ derived, no imported vacuum energy, no fitted Λ.");
        sb.AppendLine("  - This closes QG229's highest-impact blocker (dark energy / Λ).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("LAMBDA ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(LambdaOrigin.LambdaScalesAsOneOverR2(), "Λ must scale as 1/R²");
        Assert.True(LambdaOrigin.CoincidenceResolved(), "the coincidence must be a structural identity");
    }
}
