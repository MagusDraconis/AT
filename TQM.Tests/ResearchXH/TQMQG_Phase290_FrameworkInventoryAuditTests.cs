using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 290 — Framework Inventory Audit. Are the QG289 framework items {η, 3+1, π} equally
/// fundamental? No observables, no target values, D96 only, deterministic. Each classified DERIVED /
/// FRAMEWORK / BOUNDARY; output the minimum irreducible framework.
/// </summary>
public class TQMQG_Phase290_FrameworkInventoryAuditTests : ResearchTestBase
{
    public TQMQG_Phase290_FrameworkInventoryAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2900_DimensionDerived()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2900: the dimensionality 3+1 is DERIVED, not fundamental");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - d≥3 is a result of the counting measure (the (d−1)(d−2) prefactor, QG2);");
        sb.AppendLine("  - the SAME ρ continued to d=3 gives the non-trivial Einstein structure (QG197).");
        sb.AppendLine();

        sb.AppendLine($"d≥3 derived (QG2, prefactor): {FrameworkInventoryAudit.DimensionDerived()}");
        sb.AppendLine($"d=3 structure native (QG197): {FrameworkInventoryAudit.ThreeDimensionalStructureNative()}");
        sb.AppendLine($"bridge classification: {D2ToD3Bridge.Classify()}");
        sb.AppendLine();
        sb.AppendLine("The spatial dimension is a RESULT of the count structure (G_11 ∝ (d−1)(d−2) ≠ 0");
        sb.AppendLine("requires d ≥ 3); only the +1 time signature is a framework residue (FRW a = ρ^(1/d)).");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameworkInventoryAudit.DimensionDerived(),
            "d≥3 must be derived from the counting measure");
        Assert.True(FrameworkInventoryAudit.ThreeDimensionalStructureNative(),
            "the d=3 Einstein structure must be native (FULL BRIDGE)");
    }

    [Fact]
    public void TQMQG2901_EtaAndPiFramework()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2901: η and π are irreducible FRAMEWORK references");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - η is the conformal reference (defines conformal flatness → Weyl ψ, QG285);");
        sb.AppendLine("  - π is a universal mathematical constant (inherited by every geometry);");
        sb.AppendLine("  - neither is derived from D96 (no count produces them), neither is a physics input.");
        sb.AppendLine();

        foreach (var i in FrameworkInventoryAudit.Items())
        {
            sb.AppendLine($"  [{i.Status.ToString().PadRight(10)}] {i.Name} ({i.Source})");
            sb.AppendLine($"       irreducible={i.IsIrreducible}");
            sb.AppendLine($"       {i.Note}");
        }
        sb.AppendLine();
        sb.AppendLine($"η is the conformal reference: {FrameworkInventoryAudit.EtaIsConformalReference()}");
        sb.AppendLine($"π is universal: {FrameworkInventoryAudit.PiIsUniversal()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameworkInventoryAudit.EtaIsConformalReference(),
            "η must define conformal flatness / the Weyl content");
        Assert.True(FrameworkInventoryAudit.PiIsUniversal(), "π must be a universal constant");
        Assert.Equal(2, FrameworkInventoryAudit.FrameworkCount());
        Assert.Equal(2, FrameworkInventoryAudit.IrreducibleCount());
    }

    [Fact]
    public void TQMQG2902_MinimumIrreducibleFramework()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2902: the minimum irreducible framework {η, π}");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the framework is NOT homogeneous: 3+1 is derived, η and π are irreducible;");
        sb.AppendLine("  - the minimum irreducible framework is {η, π} — smaller than QG289's {η, 3+1, π}.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FrameworkInventoryAudit.Summary()}");
        sb.AppendLine($"Framework score: {FrameworkInventoryAudit.FrameworkScore()}/5");
        sb.AppendLine($"derived={FrameworkInventoryAudit.DerivedCount()} framework={FrameworkInventoryAudit.FrameworkCount()} boundary={FrameworkInventoryAudit.BoundaryCount()}");
        sb.AppendLine($"framework not homogeneous: {FrameworkInventoryAudit.FrameworkNotHomogeneous()}");
        sb.AppendLine($"minimal framework reached: {FrameworkInventoryAudit.MinimalFrameworkReached()}");
        sb.AppendLine($"CLASSIFICATION = {FrameworkInventoryAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("IRREDUCIBLE FRAMEWORK:");
        foreach (var m in FrameworkInventoryAudit.IrreducibleFramework())
            sb.AppendLine($"  - {m}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the framework {η, 3+1, π} is NOT equally fundamental:");
        sb.AppendLine("    · 3+1 → DERIVED — the dimension is a result of the counting measure");
        sb.AppendLine("      (QG2 d≥3, QG197 FULL BRIDGE), only the +1 signature is a residue;");
        sb.AppendLine("    · η → FRAMEWORK — the conformal reference defining Weyl ψ (QG285);");
        sb.AppendLine("    · π → FRAMEWORK — the universal mathematical constant.");
        sb.AppendLine("  - the minimum irreducible framework is {η, π}: the conformal reference and the");
        sb.AppendLine("    universal constant — with the derived dimension as their consequence.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("IRREDUCIBLE FRAMEWORK", FrameworkInventoryAudit.Classify());
        Assert.True(FrameworkInventoryAudit.FrameworkScore() >= 5);
        Assert.True(FrameworkInventoryAudit.MinimalFrameworkReached());
        Assert.Contains("IRREDUCIBLE FRAMEWORK", FrameworkInventoryAudit.Summary());
        Assert.Equal(2, FrameworkInventoryAudit.IrreducibleFramework().Length);
    }
}
