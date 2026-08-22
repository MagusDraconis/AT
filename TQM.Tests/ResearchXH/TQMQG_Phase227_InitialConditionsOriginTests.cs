using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 227 — Initial Conditions Origin. Derive why the universe starts in its specific initial
/// state: critical branching, fixed points, attractors, minimum-information states. No new primitives,
/// deterministic. Closes the QG226 TOE criterion 6 (initial conditions).
/// </summary>
public class TQMQG_Phase227_InitialConditionsOriginTests : ResearchTestBase
{
    public TQMQG_Phase227_InitialConditionsOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2270_StationarityAndScaleFreeness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2270: the initial state must be a stationary, scale-free fixed point");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A non-stationary state (∂_t ρ ≠ 0) is a transient, not an initial state (QG222).");
        sb.AppendLine("  - α=0 (equal deficit per octave) is the unique scale-free state (QG206).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ∂_t ρ at μ=1 (critical)   = {InitialConditionsOrigin.CriticalStateStationary()} (stationary)");
        sb.AppendLine($"  ∂_t ρ at μ=0.5, μ=2 (transients) = {InitialConditionsOrigin.NonCriticalStatesAreTransients()}");
        sb.AppendLine($"  Stationary requires criticality? {InitialConditionsOrigin.StationaryRequiresCriticality(1.0)}");
        sb.AppendLine($"  Spread of deficit fractions at α=0  = {InitialConditionsOrigin.SpreadAt(0.0):F6}");
        sb.AppendLine($"  Spread at α=+0.3 = {InitialConditionsOrigin.SpreadAt(0.3):F6}, α=−0.3 = {InitialConditionsOrigin.SpreadAt(-0.3):F6}");
        sb.AppendLine($"  α=0 unique scale-free? {InitialConditionsOrigin.AlphaZeroUniqueScaleFree()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The initial state must be a fixed point of the actualization flow ⇒ μ=1 (critical).");
        sb.AppendLine("  - Criticality (α=0) is the unique scale-free state — any α≠0 introduces a preferred");
        sb.AppendLine("    scale, i.e. information the theory has no source for.");

        Output.WriteLine(sb.ToString());

        Assert.True(InitialConditionsOrigin.CriticalStateStationary(), "the critical state must be stationary");
        Assert.True(InitialConditionsOrigin.NonCriticalStatesAreTransients(), "non-critical states must be transients");
        Assert.True(InitialConditionsOrigin.AlphaZeroUniqueScaleFree(), "α=0 must be the unique scale-free state");
    }

    [Fact]
    public void TQMQG2271_MinimumInformationAndAttractor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2271: minimum-information selection and the attractor's erasure of initial data");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The minimum-information state is the maximum-entropy allocation (G4-RHO).");
        sb.AppendLine("  - The universal attractor (QG116b) erases residual content → no fine-tuning needed.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  H(α=0) = {InitialConditionsOrigin.EntropyAt(0.0):F6}  (should equal ln 8 = {Math.Log(8):F6})");
        sb.AppendLine($"  H(α=0.5) = {InitialConditionsOrigin.EntropyAt(0.5):F6}");
        sb.AppendLine($"  Uniform entropy = ln K? {InitialConditionsOrigin.UniformEntropyIsLnK(8)}");
        sb.AppendLine($"  Uniform is max-entropy? {InitialConditionsOrigin.UniformIsMaxEntropy(8)}");
        var rho = InitialConditionsOrigin.MinimumInformationState(8);
        sb.AppendLine($"  Minimum-information state ρ_k = 1/K: {string.Join(", ", rho.Select(r => $"{r:F4}"))}");
        sb.AppendLine($"  Uniform = critical branching state (QG216 at μ=1)? {InitialConditionsOrigin.UniformIsCriticalBranchingState(8)}");
        sb.AppendLine($"  Attractor erases initial data (exact fixed point + basin ≥ 0.9)? {InitialConditionsOrigin.AttractorErasesInitialData()}");
        sb.AppendLine($"  Late-time insensitive to initial state? {InitialConditionsOrigin.LateTimeInsensitiveToInitialState()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The minimum-information state is uniform (ρ_k = 1/K): it maximizes the native entropy");
        sb.AppendLine("    and needs zero initial-condition input (no preferred scale, no preferred generation).");
        sb.AppendLine("  - The universal attractor erases residual content — fine-tuning is unnecessary.");

        Output.WriteLine(sb.ToString());

        Assert.True(InitialConditionsOrigin.UniformIsMaxEntropy(8), "uniform must maximize the entropy");
        Assert.True(InitialConditionsOrigin.UniformEntropyIsLnK(8), "H(0) must equal ln K");
        Assert.True(InitialConditionsOrigin.UniformIsCriticalBranchingState(8), "uniform = critical branching state");
        Assert.True(InitialConditionsOrigin.AttractorErasesInitialData(), "the attractor must erase initial data");
    }

    [Fact]
    public void TQMQG2272_ClassificationInitialConditionOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2272: classification — INITIAL-CONDITION ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The derived initial state: μ=1 (critical), α=0 (scale-free), ρ_k = 1/K (uniform).");
        sb.AppendLine("  - All five conditions must hold for a full origin.");
        sb.AppendLine();

        int score = InitialConditionsOrigin.OriginScore(8);
        string classification = InitialConditionsOrigin.Classify(8);
        var (mu, alpha, rho) = InitialConditionsOrigin.InitialState(8);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 stationarity ⇒ μ=1 ({InitialConditionsOrigin.CriticalStateStationary()})");
        sb.AppendLine($"    +1 α=0 unique scale-free ({InitialConditionsOrigin.AlphaZeroUniqueScaleFree(8)})");
        sb.AppendLine($"    +1 uniform max-entropy, H(0)=ln K ({InitialConditionsOrigin.UniformIsMaxEntropy(8)})");
        sb.AppendLine($"    +1 uniform = critical branching state ({InitialConditionsOrigin.UniformIsCriticalBranchingState(8)})");
        sb.AppendLine($"    +1 attractor erases initial data ({InitialConditionsOrigin.AttractorErasesInitialData()})");
        sb.AppendLine($"  Derived initial state: μ = {mu:F1}, α = {alpha:F1}, ρ_k = 1/{rho.Length}");
        sb.AppendLine($"  Initial state derived (all conditions)? {InitialConditionsOrigin.InitialStateIsDerived(8)}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The universe's initial state is the UNIFORM CRITICAL STATE ρ_k = 1/K (μ=1, α=0):");
        sb.AppendLine("    stationarity (fixed point of the actualization flow, QG222) forces criticality;");
        sb.AppendLine("    scale-freeness forces α=0 (QG206); minimum-information (maximum entropy, G4-RHO)");
        sb.AppendLine("    selects the uniform allocation; the universal attractor (QG116b) erases residual");
        sb.AppendLine("    content, so no fine-tuning is required.");
        sb.AppendLine($"  ⇒ {classification} — initial conditions are DERIVED, not assumed.");
        sb.AppendLine("  - This closes the QG226 TOE criterion 6 (initial conditions): OPEN → DERIVED.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INITIAL-CONDITION ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(InitialConditionsOrigin.InitialStateIsDerived(8), "all five conditions must hold");
    }
}
