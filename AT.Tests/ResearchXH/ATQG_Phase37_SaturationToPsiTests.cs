using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 37 — can saturation generate ψ? Tests whether nonlinear saturation of the Q-event network can
/// generate an effective anisotropic/tensor sector. Classify: NEW PRIMITIVE / EMERGENT / PARTIAL MATCH.
///
/// Tests: ATQG370 (spin census), ATQG371 (no independent d.o.f.), ATQG372 (classification).
/// </summary>
public class ATQG_Phase37_SaturationToPsiTests : ResearchTestBase
{
    public ATQG_Phase37_SaturationToPsiTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG370: spin census of saturation ───────────────────────────────────────────

    [Fact]
    public void ATQG370_SpinCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG370: what spin can each saturation mechanism reach?");

        double scalarFn = SaturationToPsi.SpinOfScalarFunction();
        double gradient = SaturationToPsi.SpinOfGradient();
        double front = SaturationToPsi.SpinOfAnisotropicFront();
        double tensor = SaturationToPsi.TensorSpin();

        sb.AppendLine($"nonlinear scalar function ρ→f(ρ) : spin {scalarFn}");
        sb.AppendLine($"saturation gradient ∇f(ρ)        : spin {gradient}");
        sb.AppendLine($"anisotropic saturation front      : spin {front}");
        sb.AppendLine($"tensor (ψ/Weyl) sector            : spin {tensor}  (2 helicities)");

        bool canReachTensor = SaturationToPsi.SaturationGeneratesTensor();

        sb.AppendLine();
        sb.AppendLine($"scalar saturation can reach spin 2: {canReachTensor}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a nonlinear function of a scalar is still a scalar (spin 0); its gradient and any anisotropic");
        sb.AppendLine("front reach at most spin 1 (a direction). No scalar saturation mechanism reaches spin 2.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0.0, scalarFn);
        Assert.Equal(1.0, gradient);
        Assert.Equal(1.0, front);
        Assert.Equal(2.0, tensor);
        Assert.False(canReachTensor, "scalar saturation cannot reach spin 2");
    }

    // ── ATQG371: no independent degree of freedom ─────────────────────────────────────

    [Fact]
    public void ATQG371_NoIndependentDof()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG371: does saturation add an independent degree of freedom?");

        double rho = 0.8;
        double sat = SaturationToPsi.Saturate(rho);
        bool addsDof = SaturationToPsi.SaturationAddsIndependentDof();
        bool scalarProfile = SaturationToPsi.SaturationGeneratesScalarProfile();

        sb.AppendLine($"saturation f(ρ) = 1−e^(−ρ): f({rho}) = {sat:F6}");
        sb.AppendLine($"adds an INDEPENDENT d.o.f. beyond ρ: {addsDof}  (f(ρ) is determined by ρ)");
        sb.AppendLine($"generates the scalar regular-core profile: {scalarProfile}  (QG36)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: saturation is a SCALAR reparameterization ρ → f(ρ); it renormalizes the conformal factor and");
        sb.AppendLine("produces the regular-core profile, but it introduces NO new independent field. A tensor (2 d.o.f.) cannot");
        sb.AppendLine("be manufactured from a function of one scalar.");
        Output.WriteLine(sb.ToString());

        Assert.False(addsDof, "saturation should not add an independent d.o.f.");
        Assert.True(scalarProfile, "saturation should generate the scalar profile");
        Assert.True(sat > 0.0 && sat < 1.0, "saturation should be in (0,1)");
    }

    // ── ATQG372: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG372_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG372: NEW PRIMITIVE / EMERGENT / PARTIAL MATCH?");

        sb.AppendLine($"CLASSIFICATION: {SaturationToPsi.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT EMERGENT: saturation is a scalar map ρ→f(ρ) — no scalar nonlinearity (gradient, anisotropic front)");
        sb.AppendLine("    reaches spin 2, so the tensor (ψ/Weyl) sector does not emerge from saturation.");
        sb.AppendLine("  • The PARTIAL content: saturation DOES generate the scalar regular-core profile (QG36) — a scalar");
        sb.AppendLine("    renormalization, not a tensor. That is a 'partial' contribution to the ψ-extension's SCALAR side only.");
        sb.AppendLine("  • ψ (the tensor sector) remains a NEW PRIMITIVE: it cannot be derived or emerge from saturation; the");
        sb.AppendLine("    graviton still needs a genuinely independent rank-2 field (QG23/QG24/QG28/QG34).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW PRIMITIVE", SaturationToPsi.Classify());
        Assert.False(SaturationToPsi.SaturationGeneratesTensor());
        Assert.True(SaturationToPsi.SaturationGeneratesScalarProfile());
    }
}
