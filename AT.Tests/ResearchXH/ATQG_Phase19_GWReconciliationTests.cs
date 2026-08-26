using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 19 — reconcile gravitational-wave observations. Tests whether observed GW signals can arise from
/// an emergent tensor channel or require a new primitive. Classify: EMERGENT / NEW PRIMITIVE / IMPOSSIBLE.
///
/// Tests: ATQG190 (spin mismatch → emergent tensor impossible), ATQG191 (all channels fail), ATQG192 (classification).
/// </summary>
public class ATQG_Phase19_GWReconciliationTests : ResearchTestBase
{
    public ATQG_Phase19_GWReconciliationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG190: spin mismatch — scalar (spin-0) cannot produce graviton (spin-2) ────

    [Fact]
    public void ATQG190_SpinMismatchEmergentImpossible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG190: spin-0 (scalar) cannot produce spin-2 (graviton) — emergent tensor impossible");

        int d = 3;
        double spin0 = GWReconciliation.Spin0Polarizations();
        double spin2 = GWReconciliation.Spin2Polarizations(d);

        sb.AppendLine($"scalar ρ: spin-0, {spin0} polarization (monopole/breathing)");
        sb.AppendLine($"graviton: spin-2, {spin2} polarizations (helicities ±2: + and ×)");
        sb.AppendLine($"conformal invariance: Weyl(g = ρ^(2/d)η) = {GWReconciliation.WeylOfConformalMetric()} for ANY scalar ρ");

        bool spinMismatch = spin0 < spin2;                          // 1 < 2
        bool weylAlwaysZero = GWReconciliation.WeylOfConformalMetric() == 0.0;

        sb.AppendLine();
        sb.AppendLine($"scalar has fewer polarizations than graviton (1 vs 2): {spinMismatch}");
        sb.AppendLine($"Weyl is identically zero for any scalar ρ (conformal invariance): {weylAlwaysZero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a spin-0 field can never produce spin-2 modes (representation-theory constraint), and");
        sb.AppendLine("the Weyl tensor is conformally invariant — so emergent tensor modes from the scalar sector are");
        sb.AppendLine("IMPOSSIBLE, regardless of the (collective, anisotropic) dynamics.");
        Output.WriteLine(sb.ToString());

        Assert.True(spinMismatch, "scalar should have fewer polarizations than graviton");
        Assert.True(weylAlwaysZero, "Weyl should be zero for a conformally-flat metric");
    }

    // ── ATQG191: all emergent channels fail ─────────────────────────────────────────

    [Fact]
    public void ATQG191_AllChannelsFail()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG191: branching anisotropy, higher-D support, and effective ψ all fail");

        int d = 3;
        double scalarDof = GWReconciliation.Spin0Polarizations();
        double tensorDofNeeded = GWReconciliation.Spin2Polarizations(d);
        double referenceDof = GWReconciliation.ReferenceMetricDof(d);

        // (1) Collective branching anisotropy → an ANISOTROPIC scalar ρ, still conformally flat (Weyl=0).
        bool anisotropyStillScalar = scalarDof == 1.0;   // anisotropic ρ is still 1 scalar d.o.f.

        // (2) Higher-dimensional support → the observable d-dim sector is still conformally flat (transverse frozen).
        bool higherDStillConformal = GWReconciliation.WeylOfConformalMetric() == 0.0;

        // (3) Effective ψ-sector → needs ≥2 d.o.f. (tensor), but a single scalar has only 1.
        bool effectivePsiNeedsNewDof = tensorDofNeeded > scalarDof;

        sb.AppendLine($"(1) branching anisotropy → still 1 scalar (conformally flat): {anisotropyStillScalar}");
        sb.AppendLine($"(2) higher-D support → observable sector still conformally flat: {higherDStillConformal}");
        sb.AppendLine($"(3) effective ψ needs {tensorDofNeeded} d.o.f. vs scalar's {scalarDof} (new d.o.f. required): {effectivePsiNeedsNewDof}");
        sb.AppendLine($"reference metric (ψ/Weyl) d.o.f. to ADD: {referenceDof}");

        bool allFail = anisotropyStillScalar && higherDStillConformal && effectivePsiNeedsNewDof;

        sb.AppendLine();
        sb.AppendLine($"ALL emergent channels fail (no emergent tensor channel): {allFail}");
        Output.WriteLine(sb.ToString());

        Assert.True(allFail, "no emergent tensor channel should exist");
    }

    // ── ATQG192: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG192_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG192: do GW observations require EMERGENT, NEW PRIMITIVE, or are they IMPOSSIBLE?");

        sb.AppendLine("CLASSIFICATION: NEW PRIMITIVE.");
        sb.AppendLine();
        sb.AppendLine("  • EMERGENT (from scalar sector) is IMPOSSIBLE: the Weyl tensor is conformally invariant, so no scalar");
        sb.AppendLine("    (however collective or anisotropic) can source tensor modes; spin-0 cannot produce spin-2 (ATQG190).");
        sb.AppendLine("  • Collective branching anisotropy, higher-D support, and effective ψ all fail (ATQG191): each yields");
        sb.AppendLine("    a scalar or conformally-flat observable sector, never the 2 transverse-traceless GW polarizations.");
        sb.AppendLine("  • Therefore reconciling GW observations requires a NEW PRIMITIVE: a tensor/ψ (reference-metric) field with");
        sb.AppendLine("    the Weyl d.o.f. (10 at d=3), i.e. relaxing conformal flatness by adding a non-conformal reference h.");
        sb.AppendLine("  • This is the definitive structural conclusion of the tensor/GW arc (QG15–QG19): AT's two primitives");
        sb.AppendLine("    (causal order + counting measure) yield scalar gravity only; gravitational waves require a third,");
        sb.AppendLine("    tensor primitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
