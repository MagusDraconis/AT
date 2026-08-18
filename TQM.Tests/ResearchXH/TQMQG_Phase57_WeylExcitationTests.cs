using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 57 — excitation of the traceless link sector. Determines what excites ψ.
/// Classify: DERIVED / PREFERRED / OBSERVATION-TRIGGERED.
///
/// Tests: TQMQG570 (quadrupole sourcing), TQMQG571 (mechanism vs instances), TQMQG572 (classification).
/// </summary>
public class TQMQG_Phase57_WeylExcitationTests : ResearchTestBase
{
    public TQMQG_Phase57_WeylExcitationTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG570: quadrupole sources excite Weyl ─────────────────────────────────────

    [Fact]
    public void TQMQG570_QuadrupoleSourcing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG570: anisotropic (quadrupole) sources excite the Weyl content");

        int quadrupole = 0, notSource = 0;
        foreach (var c in WeylExcitation.Candidates)
        {
            bool q = WeylExcitation.HasQuadrupole(c);
            sb.AppendLine($"{c,-24} -> quadrupole (source): {q}");
            if (q) quadrupole++; else notSource++;
        }

        bool quadrupoleSourcesWeyl = WeylExcitation.QuadrupoleSourcesWeyl();

        sb.AppendLine();
        sb.AppendLine($"quadrupole sources: {quadrupole}   non-source properties: {notSource}");
        sb.AppendLine($"quadrupole → Weyl coupling holds: {quadrupoleSourcesWeyl}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: spin-2 couples to the full T_μν, so the traceless (quadrupole) part of matter — anisotropic");
        sb.AppendLine("sources, moving deficits, binary systems, network stress — excites the traceless (Weyl) link content.");
        sb.AppendLine("Propagation stability is a necessary PROPERTY (massless, light-speed), not a source.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, quadrupole);
        Assert.Equal(1, notSource);
        Assert.True(quadrupoleSourcesWeyl, "quadrupole should source Weyl");
    }

    // ── TQMQG571: mechanism vs instances ─────────────────────────────────────────────

    [Fact]
    public void TQMQG571_MechanismVsInstances()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG571: derived mechanism, observation-triggered instances");

        bool mechanismDerived = WeylExcitation.MechanismDerived();
        bool instancesTriggered = WeylExcitation.InstancesObservationTriggered();
        bool stabilitySource = WeylExcitation.StabilityIsSource();

        sb.AppendLine($"excitation MECHANISM is DERIVED (spin-2 coupling): {mechanismDerived}");
        sb.AppendLine($"specific instances are OBSERVATION-TRIGGERED:      {instancesTriggered}");
        sb.AppendLine($"propagation stability is a source:                  {stabilitySource}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the quadrupole → Weyl sourcing is a DERIVED consequence of spin-2 coupling to T_μν; the specific");
        sb.AppendLine("excitations (binary mergers, etc.) are observation-triggered astrophysical events.");
        Output.WriteLine(sb.ToString());

        Assert.True(mechanismDerived, "the mechanism should be derived");
        Assert.True(instancesTriggered, "instances should be observation-triggered");
        Assert.False(stabilitySource, "stability should not be a source");
    }

    // ── TQMQG572: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG572_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG572: DERIVED / PREFERRED / OBSERVATION-TRIGGERED?");

        sb.AppendLine($"CLASSIFICATION: {WeylExcitation.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • DERIVED (mechanism): a spin-2 field couples to the full stress-energy T_μν, so the traceless (quadrupole)");
        sb.AppendLine("    part of matter necessarily excites the traceless (Weyl) part of ψ — this is a rigorous consequence of");
        sb.AppendLine("    spin-2 gauge/Lorentz consistency (Weinberg).");
        sb.AppendLine("  • OBSERVATION-TRIGGERED (instances): which specific sources excite ψ (binary mergers, supernovae) is set by");
        sb.AppendLine("    the astrophysical events we observe.");
        sb.AppendLine("  • So ψ excitation is DERIVED in mechanism and OBSERVATION-TRIGGERED in its instances.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", WeylExcitation.Classify());
        Assert.True(WeylExcitation.QuadrupoleSourcesWeyl());
        Assert.True(WeylExcitation.MechanismDerived());
    }
}
