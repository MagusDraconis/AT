using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 228 — Information Content Origin. Derive why non-zero information appears from the
/// minimum-information state: actualization events, symmetry breaking, branch differentiation, entropy
/// growth, record formation. No new primitives, deterministic. Closes QG226 TOE criterion 8.
/// </summary>
public class TQMQG_Phase228_InformationContentOriginTests : ResearchTestBase
{
    public TQMQG_Phase228_InformationContentOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2280_EntropyDeficitDefinition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2280: information = entropy deficit — zero at uniform, positive for any departure");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Information I = ln K − H(p) = KL(p ‖ uniform) ≥ 0; zero iff p is uniform.");
        sb.AppendLine("  - QG227's minimum-information state is uniform ρ_k = 1/K ⇒ I = 0.");
        sb.AppendLine();

        var uniform = Enumerable.Repeat(1.0 / 8, 8).ToArray();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  I(uniform 1/8) = {InformationContentOrigin.InformationContent(uniform):F6} (should be 0)");
        sb.AppendLine($"  Uniform has zero information? {InformationContentOrigin.UniformHasZeroInformation(8)}");
        sb.AppendLine($"  Branching info I(μ=1) = {InformationContentOrigin.BranchingInformation(1.0, 8):F6} (critical, uniform)");
        sb.AppendLine($"  Branching info I(μ=0.5) = {InformationContentOrigin.BranchingInformation(0.5, 8):F6}");
        sb.AppendLine($"  Branching info I(μ=2.0) = {InformationContentOrigin.BranchingInformation(2.0, 8):F6}");
        sb.AppendLine($"  Critical expected profile uniform? {InformationContentOrigin.CriticalExpectedProfileUniform(8)}");
        sb.AppendLine($"  Non-critical profile carries information? {InformationContentOrigin.NonCriticalProfileCarriesInformation(8)}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The uniform minimum-information state has I = 0 by definition.");
        sb.AppendLine("  - Any non-uniform realized record has positive information (KL divergence).");
        sb.AppendLine("  - The entropy deficit ln K − H is the measure of information content.");

        Output.WriteLine(sb.ToString());

        Assert.True(InformationContentOrigin.UniformHasZeroInformation(8), "uniform must have zero information");
        Assert.True(InformationContentOrigin.CriticalExpectedProfileUniform(8), "critical profile must be uniform");
        Assert.True(InformationContentOrigin.NonCriticalProfileCarriesInformation(8), "non-critical profiles must carry information");
        Assert.Equal(0.0, InformationContentOrigin.InformationContent(uniform), 9);
    }

    [Fact]
    public void TQMQG2281_FluctuationsBreakUniformity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2281: mandatory counting fluctuations break the symmetry and differentiate branches");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Q-event counting is a Poisson process (QG15/30): zero mean, non-zero variance.");
        sb.AppendLine("  - The realized record differs from the uniform expected profile → symmetry breaking.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Fluctuation mean ⟨δρ⟩ = {QEventCorrelations.FluctuationMean():F1} (zero)");
        sb.AppendLine($"  Correlation kernel K = σ² = {QEventCorrelations.CorrelationKernel(0.5):F2} (non-zero variance)");
        sb.AppendLine($"  Fluctuations zero-mean non-zero-variance? {InformationContentOrigin.FluctuationsZeroMeanNonzeroVariance()}");
        sb.AppendLine($"  Poisson variance Var(A)=E[A] = {InformationContentOrigin.PoissonVariance(1.0):F1} (non-zero)");
        sb.AppendLine($"  Realized counts fluctuate? {InformationContentOrigin.RealizedCountsFluctuate(1.0)}");
        sb.AppendLine($"  Realization breaks uniform symmetry? {InformationContentOrigin.RealizationBreaksUniformSymmetry(1.0, 8)}");
        sb.AppendLine($"  Branches differentiate? {InformationContentOrigin.BranchesDifferentiate(8)}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The uniform state is only the EXPECTED profile; the realized counts fluctuate.");
        sb.AppendLine("  - These mandatory fluctuations break the permutation symmetry and differentiate branches.");

        Output.WriteLine(sb.ToString());

        Assert.True(InformationContentOrigin.FluctuationsZeroMeanNonzeroVariance(), "fluctuations must be zero-mean with non-zero variance");
        Assert.True(InformationContentOrigin.RealizedCountsFluctuate(1.0), "realized counts must fluctuate");
        Assert.True(InformationContentOrigin.RealizationBreaksUniformSymmetry(1.0, 8), "realization must break the uniform symmetry");
        Assert.True(InformationContentOrigin.BranchesDifferentiate(8), "branches must differentiate");
    }

    [Fact]
    public void TQMQG2282_ClassificationInformationOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2282: classification — INFORMATION ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The realized record is the D96 octave-occupied spectrum [4,4,87] (95 modes, QG210).");
        sb.AppendLine("  - Information = KL(p ‖ uniform) over the record's occupancy distribution.");
        sb.AppendLine();

        int score = InformationContentOrigin.OriginScore();
        string classification = InformationContentOrigin.Classify();

        var occ = InformationContentOrigin.OctaveOccupancies();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Octave occupancies: [{string.Join(", ", occ)}] (total {occ.Sum()})");
        sb.AppendLine($"  Record information I_occ = {InformationContentOrigin.RecordInformation():F4} nats = {InformationContentOrigin.RecordInformation() / Math.Log(2):F4} bits");
        sb.AppendLine($"  Record carries information (>0.1 nats)? {InformationContentOrigin.RecordCarriesInformation()}");
        sb.AppendLine($"  Record non-uniform (top band > 50%)? {InformationContentOrigin.RecordNonUniform()}");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 uniform/critical zero info ({InformationContentOrigin.UniformHasZeroInformation(8)})");
        sb.AppendLine($"    +1 fluctuations zero-mean/non-zero-var ({InformationContentOrigin.FluctuationsZeroMeanNonzeroVariance()})");
        sb.AppendLine($"    +1 symmetry breaking + branch differentiation ({InformationContentOrigin.RealizationBreaksUniformSymmetry(1.0, 8)})");
        sb.AppendLine($"    +1 entropy deficit I = ln K − H > 0 ({InformationContentOrigin.NonCriticalProfileCarriesInformation(8)})");
        sb.AppendLine($"    +1 record formation [4,4,87] carries info ({InformationContentOrigin.RecordCarriesInformation()})");
        sb.AppendLine($"  Full chain holds? {InformationContentOrigin.InformationChainHolds(8)}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Information appears because actualization is a discrete counting process: its");
        sb.AppendLine("    mandatory Poisson fluctuations break the permutation symmetry of the uniform");
        sb.AppendLine("    minimum-information state, differentiate branches, and create an entropy deficit");
        sb.AppendLine("    I = ln K − H = KL(ρ‖uniform) > 0.");
        sb.AppendLine("  - The realized record — the D96 octave spectrum [4,4,87] — carries ~1.08 bits.");
        sb.AppendLine($"  ⇒ {classification} — information content is derived, not assumed.");
        sb.AppendLine("  - This closes the QG226 TOE criterion 8 (information origin): PARTIAL → DERIVED.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INFORMATION ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(InformationContentOrigin.RecordCarriesInformation(), "the realized record must carry information");
        Assert.True(InformationContentOrigin.InformationChainHolds(8), "the full derivation chain must hold");
    }
}
