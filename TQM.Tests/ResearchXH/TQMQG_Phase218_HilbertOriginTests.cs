using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 218 — Hilbert Origin. Show why quantum states must be complex, from the derived amplitude
/// magnitude (QG216) and the U(1) link phase (QG63). No new primitives, deterministic.
/// </summary>
public class TQMQG_Phase218_HilbertOriginTests : ResearchTestBase
{
    public TQMQG_Phase218_HilbertOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2180_TwoRealDegreesOfFreedom()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2180: a quantum state carries magnitude AND phase — two real DOFs");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Magnitude |ψ| = √ρ from the branching counting measure (QG216).");
        sb.AppendLine("  - Phase θ from the U(1) link connection (QG63).");
        sb.AppendLine();

        double mag = HilbertOrigin.Magnitude(2.0, 3, 8);   // √(8/255)
        var (re, im) = HilbertOrigin.ComplexAmplitude(mag, Math.PI / 3.0);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  |ψ| = √ρ = √(μ³/S) = {mag:F4}  (branching magnitude)");
        sb.AppendLine($"  θ = π/3  (U(1) link phase)");
        sb.AppendLine($"  ψ = |ψ|e^(iθ) = {re:F4} + {im:F4}·i  — a COMPLEX number");
        sb.AppendLine($"  State is complex (Re ≠ |ψ|, Im ≠ 0)? {HilbertOrigin.StateIsComplex()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The two independent real DOFs (magnitude, phase) combine into a complex amplitude.");
        sb.AppendLine("  - Neither is reducible to the other (node vs link property).");

        Output.WriteLine(sb.ToString());

        Assert.True(HilbertOrigin.StateIsComplex(), "a state with magnitude and phase must be complex");
        Assert.Equal(Math.Sqrt(8.0 / 255.0), mag, 6);
    }

    [Fact]
    public void TQMQG2181_InterferenceRequiresThePhase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2181: interference requires the phase — impossible with real-only states");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG65: P = |e^(iθ₁) + e^(iθ₂)|² = 2 + 2cos(θ₁−θ₂).");
        sb.AppendLine("  - A real-only state gives classical addition P = P₁ + P₂ (no interference).");
        sb.AppendLine();

        double p0 = HilbertOrigin.InterferenceProbability(0.0, 0.0);
        double p90 = HilbertOrigin.InterferenceProbability(0.0, Math.PI / 2.0);
        double real = HilbertOrigin.RealOnlyProbability();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  P(θ₁=0, θ₂=0)   = {p0:F4}  (constructive)");
        sb.AppendLine($"  P(θ₁=0, θ₂=π/2) = {p90:F4}  (intermediate)");
        sb.AppendLine($"  Real-only P     = {real:F1}  (classical, no θ dependence)");
        sb.AppendLine($"  Interference is phase-dependent? {HilbertOrigin.InterferenceRequiresPhase()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Interference varies with the relative phase — a real-only state space cannot");
        sb.AppendLine("    reproduce it (P would be fixed at P₁+P₂).");

        Output.WriteLine(sb.ToString());

        Assert.True(HilbertOrigin.InterferenceRequiresPhase(), "interference must be phase-dependent");
        Assert.NotEqual(p0, p90);
    }

    [Fact]
    public void TQMQG2182_ClassificationHilbertOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2182: classification — HILBERT ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The ℂ Hilbert space is forced by the (magnitude, phase) pair; the Born rule is the");
        sb.AppendLine("    ℂ inner product; QG74's unitary general measurement is ℂ-linear.");
        sb.AppendLine();

        int score = HilbertOrigin.OriginScore();
        string classification = HilbertOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 interference phase-dependent ({HilbertOrigin.InterferenceRequiresPhase()})");
        sb.AppendLine($"    +1 state is complex ({HilbertOrigin.StateIsComplex()})");
        sb.AppendLine($"    +1 complexity forced by two DOFs ({HilbertOrigin.ComplexityForcedByTwoDof()})");
        sb.AppendLine($"    +1 unitary ops complex-linear ({HilbertOrigin.UnitaryOperationsComplexLinear()})");
        sb.AppendLine($"  Born probability from ℂ inner product (example): {HilbertOrigin.BornProbability(new[]{1.0},new[]{0.0},new[]{0.0},new[]{1.0}):F4}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Quantum states MUST be complex: magnitude (branching) + phase (U(1) links) = a");
        sb.AppendLine("    complex amplitude, and only a ℂ Hilbert space reproduces interference and the Born rule.");
        sb.AppendLine("  - No new primitive — the complexity is forced by the (magnitude, phase) pair.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("HILBERT ORIGIN", classification);
        Assert.Equal(4, score);
    }
}
