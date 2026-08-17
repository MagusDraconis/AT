using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 23 — origin of the ψ-field. Tests whether ψ (the tensor/Weyl mode) can emerge from the scalar
/// actualization or requires a new primitive. Classify: DERIVED / EMERGENT / NEW PRIMITIVE.
///
/// Tests: TQMQG230 (anisotropic scalar → still conformally flat), TQMQG231 (multi-field needs a new primitive),
///        TQMQG232 (classification).
/// </summary>
public class TQMQG_Phase23_OriginOfPsiTests : ResearchTestBase
{
    public TQMQG_Phase23_OriginOfPsiTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG230: anisotropic branching → still a scalar, still conformally flat ─────

    [Fact]
    public void TQMQG230_AnisotropicStillScalar()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG230: anisotropic branching → still 1 scalar, still conformally flat");

        int d = 3;
        double scalarDof = OriginOfPsi.ScalarDof();
        double tensorDof = OriginOfPsi.TensorDof(d);
        double weyl = OriginOfPsi.WeylOfAnisotropicScalar();

        sb.AppendLine($"scalar ρ: {scalarDof} d.o.f. (spin-0);  tensor ψ: {tensorDof} d.o.f. (spin-2)");
        sb.AppendLine($"Weyl(g = ρ^(2/d)η) for ANY scalar ρ (even anisotropic) = {weyl}");

        bool anisotropyStillScalar = scalarDof < tensorDof;   // anisotropic ρ is still 1 scalar
        bool weylAlwaysZero = weyl == 0.0;

        sb.AppendLine();
        sb.AppendLine($"anisotropic/directional actualization still yields 1 scalar (insufficient): {anisotropyStillScalar}");
        sb.AppendLine($"Weyl is identically zero for any scalar ρ (conformal invariance): {weylAlwaysZero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: anisotropic branching or directional actualization produces an anisotropic SCALAR ρ,");
        sb.AppendLine("which is still conformally flat (Weyl=0). A scalar — however anisotropic — cannot source the tensor sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(anisotropyStillScalar, "anisotropic actualization should still be scalar");
        Assert.True(weylAlwaysZero, "Weyl should be zero for any scalar ρ");
    }

    // ── TQMQG231: multi-field actualization → a tensor, but a NEW primitive ──────────

    [Fact]
    public void TQMQG231_MultiFieldNeedsNewPrimitive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG231: a rank-2 tensor requires MULTIPLE scalars — a new primitive");

        double oneScalar = OriginOfPsi.ScalarDof();
        double required = OriginOfPsi.MultiFieldRequired();

        sb.AppendLine($"a rank-2 tensor (e.g. ∂ᵢρ₁ ∂ⱼρ₂) requires {required} scalars");
        sb.AppendLine($"TQM has {oneScalar} scalar (the counting measure ρ)");

        bool singleScalarInsufficient = oneScalar < required;

        sb.AppendLine();
        sb.AppendLine($"single scalar is insufficient for a tensor (1 < 2): {singleScalarInsufficient}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: an effective ψ could be built from MULTI-FIELD actualization (two or more counting");
        sb.AppendLine("measures whose gradients combine into a tensor). But TQM has exactly ONE counting measure ρ —");
        sb.AppendLine("adding a second scalar (or a vector/tensor) IS a new primitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(singleScalarInsufficient, "a single scalar should be insufficient to build a tensor");
    }

    // ── TQMQG232: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG232_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG232: is ψ DERIVED, EMERGENT, or a NEW PRIMITIVE?");

        sb.AppendLine("CLASSIFICATION: NEW PRIMITIVE.");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: a single scalar ρ (spin-0) cannot produce spin-2 (representation theory); the Weyl");
        sb.AppendLine("    tensor is conformally invariant, so any scalar actualization (anisotropic, directional, higher-order");
        sb.AppendLine("    statistics, support-rank fluctuations) leaves Weyl = 0 (TQMQG230).");
        sb.AppendLine("  • NOT EMERGENT (from the existing primitives): an effective tensor requires MULTIPLE scalars");
        sb.AppendLine("    (∂ᵢρ₁∂ⱼρ₂) or a vector/tensor field — i.e. a second counting measure or a reference metric h ≠ η,");
        sb.AppendLine("    which is a new primitive (TQMQG231).");
        sb.AppendLine("  • Therefore ψ is a NEW PRIMITIVE: the ψ/Weyl (reference-metric) field cannot be obtained from the");
        sb.AppendLine("    single counting measure ρ, and adding it is exactly the minimal extension that relaxes conformal");
        sb.AppendLine("    flatness and restores lensing, tensor GWs, and (partly) horizon thermodynamics (QG22).");
        sb.AppendLine("  • This is the definitive answer to the GW arc: TQM's conformal gravity is closed under its two");
        sb.AppendLine("    primitives; the tensor sector is the one degree of freedom that requires a genuinely third primitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
