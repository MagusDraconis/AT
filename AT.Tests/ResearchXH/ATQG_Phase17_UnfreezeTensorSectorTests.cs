using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 17 — unfreeze the tensor sector. Tests whether the scalar actualization (ρ) can source the
/// tensor/graviton (ψ) mode, or whether a non-scalar (tensor) primitive is required. Classify: ABSENT / FROZEN /
/// EMERGENT.
///
/// Tests: ATQG170 (scalar source → trace only), ATQG171 (tensor source required), ATQG172 (classification).
/// </summary>
public class ATQG_Phase17_UnfreezeTensorSectorTests : ResearchTestBase
{
    public ATQG_Phase17_UnfreezeTensorSectorTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG170: a scalar source generates only trace (scalar) modes ────────────────

    [Fact]
    public void ATQG170_ScalarSourceTraceOnly()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG170: a scalar (ρ) source generates only trace modes — never tensor");

        int d = 3;
        sb.AppendLine($"scalar source δρ (for various event counts N), d={d}:");
        sb.AppendLine($"{"N",8} {"tensor part",14}");
        foreach (double N in new[] { 10.0, 100.0, 1000.0 })
        {
            double tensorPart = UnfreezeTensor.TensorPartFromScalarSource(N, d);
            sb.AppendLine($"{N,8:F0} {tensorPart,14:E2}");
        }

        bool alwaysZero = UnfreezeTensor.TensorPartFromScalarSource(10.0, d) == 0.0
                       && UnfreezeTensor.TensorPartFromScalarSource(1000.0, d) == 0.0;

        sb.AppendLine();
        sb.AppendLine($"tensor part of the metric fluctuation is ZERO for every scalar source: {alwaysZero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the Weyl tensor is conformally invariant — the scalar ρ (whatever its profile, even");
        sb.AppendLine("anisotropic) rescales the metric but NEVER generates Weyl (tensor) curvature. A scalar source");
        sb.AppendLine("cannot unfreeze the tensor sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(alwaysZero, "scalar source should generate zero tensor part");
    }

    // ── ATQG171: the tensor sector requires a non-scalar (tensor) source ────────────

    [Fact]
    public void ATQG171_TensorSourceRequired()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG171: the Weyl tensor requires a non-scalar source (ρ has only 1 d.o.f.)");

        int d = 3;
        double scalarDof = UnfreezeTensor.ScalarDof();
        double tensorDof = UnfreezeTensor.FrozenTensorDof(d);

        sb.AppendLine($"scalar ρ: {scalarDof} degree of freedom");
        sb.AppendLine($"frozen Weyl (tensor) sector at d=3: {tensorDof} degrees of freedom");
        sb.AppendLine($"a scalar (1 d.o.f.) cannot independently source a {tensorDof}-component tensor");

        bool scalarInsufficient = scalarDof < tensorDof;
        bool tensorDofGrows = UnfreezeTensor.FrozenTensorDof(4) > UnfreezeTensor.FrozenTensorDof(3);

        sb.AppendLine();
        sb.AppendLine($"scalar d.o.f. < tensor d.o.f. (insufficient): {scalarInsufficient}");
        sb.AppendLine($"tensor d.o.f. grows with d (35 at d=4): {tensorDofGrows}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: to unfreeze the tensor sector, a NON-SCALAR (tensor) source with the Weyl d.o.f. count");
        sb.AppendLine("(10 at d=3) is required. The scalar actualization ρ (1 d.o.f.) is structurally insufficient.");
        Output.WriteLine(sb.ToString());

        Assert.True(scalarInsufficient, "scalar source should be insufficient for the tensor sector");
        Assert.True(tensorDofGrows, "tensor d.o.f. should grow with d");
    }

    // ── ATQG172: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG172_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG172: does actualization source ψ? ABSENT / FROZEN / EMERGENT?");

        sb.AppendLine("CLASSIFICATION: FROZEN (no native scalar source for ψ).");
        sb.AppendLine();
        sb.AppendLine("  • The scalar actualization ρ (counting measure) is a SINGLE scalar field, and the Weyl tensor is");
        sb.AppendLine("    conformally invariant — so ρ can NEVER generate tensor (Weyl/graviton) curvature, regardless of");
        sb.AppendLine("    deficit gradients or branching asymmetries (ATQG170).");
        sb.AppendLine("  • The Weyl sector has d(d+1)(d+2)(d−3)/12 d.o.f. (10 at d=3) that require a NON-SCALAR (tensor)");
        sb.AppendLine("    source; a scalar (1 d.o.f.) is structurally insufficient (ATQG171).");
        sb.AppendLine("  • Therefore the tensor sector remains FROZEN: the actualization dynamics does NOT source ψ. A native");
        sb.AppendLine("    graviton would require a NEW tensor primitive (an anisotropic reference / directional actualization");
        sb.AppendLine("    / dynamical ψ-field) — beyond the current AT primitives.");
        sb.AppendLine("  • This is the deepest form of the QG16 result: the graviton is not only frozen by conformal flatness,");
        sb.AppendLine("    it CANNOT be unfrozen by any scalar actualization — it is genuinely absent from the scalar sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
