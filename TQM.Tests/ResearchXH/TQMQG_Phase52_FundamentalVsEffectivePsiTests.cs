using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 52 — is ψ fundamental or effective? Determines whether ψ must exist microscopically or can emerge
/// in the continuum limit. Classify: FUNDAMENTAL / EFFECTIVE / UNDECIDED.
///
/// Tests: TQMQG520 (coarse-graining preserves spin), TQMQG521 (no collective tensor mode), TQMQG522 (classification).
/// </summary>
public class TQMQG_Phase52_FundamentalVsEffectivePsiTests : ResearchTestBase
{
    public TQMQG_Phase52_FundamentalVsEffectivePsiTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG520: coarse-graining preserves spin ─────────────────────────────────────

    [Fact]
    public void TQMQG520_CoarseGrainingPreservesSpin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG520: averaging scalars gives a scalar, never a tensor");

        bool preservesSpin = FundamentalVsEffectivePsi.CoarseGrainingPreservesSpin();
        bool scalarMicroscopic = FundamentalVsEffectivePsi.MicroscopicTheoryIsScalar();

        sb.AppendLine($"coarse-graining (averaging) preserves spin: {preservesSpin}");
        sb.AppendLine($"microscopic theory (Q-events) is scalar:   {scalarMicroscopic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: averaging is a spin-preserving operation. A scalar microscopic theory (Q-events → ρ) coarse-grains");
        sb.AppendLine("to a scalar continuum field — it cannot produce a spin-2 field in the continuum limit.");
        Output.WriteLine(sb.ToString());

        Assert.True(preservesSpin, "coarse-graining should preserve spin");
        Assert.True(scalarMicroscopic, "the microscopic theory should be scalar");
    }

    // ── TQMQG521: no collective tensor mode from scalar constituents ─────────────────

    [Fact]
    public void TQMQG521_NoCollectiveTensorMode()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG521: scalar constituents have no transverse-traceless collective mode");

        bool inheritSpin = FundamentalVsEffectivePsi.CollectiveModesInheritMicroscopicSpin();
        bool spin2Emerges = FundamentalVsEffectivePsi.Spin2EmergesFromScalar();

        sb.AppendLine($"collective modes inherit the microscopic spin: {inheritSpin}");
        sb.AppendLine($"spin-2 emerges from scalar constituents:       {spin2Emerges}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: collective modes inherit the symmetry of the microscopic theory. Scalar (isotropic) Q-events have");
        sb.AppendLine("scalar (breathing) collective modes only; a transverse-traceless (spin-2) mode requires microscopic tensor");
        sb.AppendLine("(anisotropic) degrees of freedom, which Q-events do not possess (QG23/QG37/QG49).");
        Output.WriteLine(sb.ToString());

        Assert.True(inheritSpin, "collective modes should inherit microscopic spin");
        Assert.False(spin2Emerges, "spin-2 should not emerge from scalar constituents");
    }

    // ── TQMQG522: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG522_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG522: FUNDAMENTAL / EFFECTIVE / UNDECIDED?");

        bool fundamental = FundamentalVsEffectivePsi.PsiFundamental();
        bool effective = FundamentalVsEffectivePsi.PsiEffective();

        sb.AppendLine($"ψ is FUNDAMENTAL: {fundamental}");
        sb.AppendLine($"ψ is EFFECTIVE:   {effective}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {FundamentalVsEffectivePsi.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT EFFECTIVE: spin-2 cannot emerge from scalar constituents under coarse-graining — averaging is");
        sb.AppendLine("    spin-preserving, and scalar Q-events have no tensor collective mode (QG23/QG37/QG49).");
        sb.AppendLine("  • FUNDAMENTAL: ψ must exist at the microscopic level as a genuine spin-2 degree of freedom. It is not a");
        sb.AppendLine("    continuum-limit artifact of the scalar actualization.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("FUNDAMENTAL", FundamentalVsEffectivePsi.Classify());
        Assert.True(fundamental, "psi should be fundamental");
        Assert.False(effective, "psi should not be effective");
    }
}
