using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 316 — Organization Phase Transition Audit. QG315: the lock identities PRECEDE mature
/// organization. Is there a CRITICAL TRANSITION where the organization structure [operators, locks]
/// emerges, or does it grow CONTINUOUSLY? Sweep a continuous organization parameter g ∈ [0,1] across the
/// four regimes [white noise → weak → medium → strong], measuring the operator basis, the lock
/// coherence, and the organization maturity at every step. Deterministic, no observables, no target
/// values.
/// </summary>
public class ATQG_Phase316_OrganizationPhaseTransitionAuditTests : ResearchTestBase
{
    public ATQG_Phase316_OrganizationPhaseTransitionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3160_TheRampAndMeasures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3160: the 40-step organization ramp and its three measures");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the ramp spans the four regimes [white noise → weak → medium → strong];");
        sb.AppendLine("  - operator count, lock coherence, and maturity are measured at every step;");
        sb.AppendLine("  - the operator basis should complete at a critical parameter.");
        sb.AppendLine();

        foreach (var s in OrganizationPhaseTransitionAudit.Ramp())
        {
            sb.AppendLine($"  g={s.Parameter:F3} α={s.Exponent:F3} span={s.Span,7:F2} dist={s.DistinctValues,3} " +
                          $"ops={s.OperatorCount} all={s.AllOperators} lock={s.LockCoherence:F3} " +
                          $"stable={s.StableLocks} mat={s.Maturity:F3}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(40, OrganizationPhaseTransitionAudit.Ramp().Length);
        Assert.All(OrganizationPhaseTransitionAudit.Ramp(), s =>
            Assert.InRange(s.Parameter, 0.0, 1.0));
        Assert.False(OrganizationPhaseTransitionAudit.Ramp()[0].AllOperators,
            "white noise must NOT carry the full operator basis (CROWDING fails — all distinct)");
    }

    [Fact]
    public void ATQG3161_CriticalBasisCompletion()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3161: the operator basis completes at a critical parameter and persists");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the binary all-four operator screen flips discontinuously at a critical g*;");
        sb.AppendLine("  - the locks emerge at or before the critical window;");
        sb.AppendLine("  - the quantitative maturity grows continuously.");
        sb.AppendLine();

        sb.AppendLine($"basis onset step: {OrganizationPhaseTransitionAudit.BasisOnsetStep()} " +
                      $"g* = {OrganizationPhaseTransitionAudit.BasisOnsetParameter():F3}");
        sb.AppendLine($"basis completes and persists: {OrganizationPhaseTransitionAudit.BasisCompletesAndPersists()}");
        sb.AppendLine($"basis completes sharply: {OrganizationPhaseTransitionAudit.BasisCompletesSharply()}");
        sb.AppendLine($"lock emergence step: {OrganizationPhaseTransitionAudit.LockEmergenceStep()} " +
                      $"near onset: {OrganizationPhaseTransitionAudit.LocksEmergentNearOnset()}");
        sb.AppendLine();
        var op = OrganizationPhaseTransitionAudit.OperatorTrajectory();
        var lk = OrganizationPhaseTransitionAudit.LockTrajectory();
        var mt = OrganizationPhaseTransitionAudit.MaturityTrajectory();
        sb.AppendLine($"operator: max={op.Max} sharpness={op.Sharpness:F1} width={op.Width:F2}");
        sb.AppendLine($"lock: max={lk.Max:F3} sharpness={lk.Sharpness:F1} width={lk.Width:F2}");
        sb.AppendLine($"maturity: max={mt.Max:F2} sharpness={mt.Sharpness:F1} width={mt.Width:F2}");
        sb.AppendLine();
        sb.AppendLine("The binary all-four operator basis flips from incomplete to complete at the critical");
        sb.AppendLine($"parameter g* = {OrganizationPhaseTransitionAudit.BasisOnsetParameter():F3} and stays");
        sb.AppendLine("complete for all stronger organization. The lock coherence emerges at the same");
        sb.AppendLine("critical window. The maturity, by contrast, grows continuously across the whole ramp.");

        Output.WriteLine(sb.ToString());

        Assert.True(OrganizationPhaseTransitionAudit.BasisCompletesAndPersists(),
            "the operator basis must complete at a critical parameter and persist");
        Assert.True(OrganizationPhaseTransitionAudit.BasisCompletesSharply(),
            "the completion must be sharp (the count jumps to 4 in one step)");
        Assert.True(OrganizationPhaseTransitionAudit.LocksEmergentNearOnset(),
            "the locks must emerge at or before the critical window");
        Assert.InRange(OrganizationPhaseTransitionAudit.BasisOnsetParameter(), 0.1, 0.9);
    }

    [Fact]
    public void ATQG3162_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3162: the transition determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ORGANIZATION PHASE TRANSITION: the binary operator basis flips discontinuously");
        sb.AppendLine("    at a critical g*, while the quantitative structure grows continuously.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OrganizationPhaseTransitionAudit.Summary()}");
        sb.AppendLine($"Determination score: {OrganizationPhaseTransitionAudit.DeterminationScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {OrganizationPhaseTransitionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine($"  - the operator basis — a binary order parameter — flips from incomplete to");
        sb.AppendLine($"    complete at g* = {OrganizationPhaseTransitionAudit.BasisOnsetParameter():F3} and");
        sb.AppendLine("    persists: a discontinuous, phase-transition-like onset of the four-operator");
        sb.AppendLine("    structure;");
        sb.AppendLine("  - the lock coherence emerges at the same critical window [consistent with QG315];");
        sb.AppendLine("  - the maturity grows continuously after the onset — the quantitative organization");
        sb.AppendLine("    is gradual, but the BINARY operator basis is a critical order parameter.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("ORGANIZATION PHASE TRANSITION", OrganizationPhaseTransitionAudit.Classify());
        Assert.True(OrganizationPhaseTransitionAudit.DeterminationScore() >= 5);
        Assert.Contains("ORGANIZATION PHASE TRANSITION", OrganizationPhaseTransitionAudit.Summary());
    }
}
