using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 289 — Anchor Inventory Audit. Inventories the anchors identified by QG288 (me, MZ,
/// 5/4, Bekenstein 1/4, η, π, RG, 3+1), classifying each as STRUCTURAL / EMPIRICAL / BOUNDARY /
/// REMOVABLE and answering which are true theory inputs, which are only calibration, and which are
/// replaceable. Output: the minimal anchor inventory.
/// </summary>
public class ATQG_Phase289_AnchorInventoryAuditTests : ResearchTestBase
{
    public ATQG_Phase289_AnchorInventoryAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2890_AnchorValuesVerified()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2890: the eight anchors and their verified values");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - every anchor value is deterministic (from the established D96 classes);");
        sb.AppendLine("  - the anchors split into structural references, calibration values, a free");
        sb.AppendLine("    constant, a method import, and a boundary target.");
        sb.AppendLine();

        sb.AppendLine($"me = {AnchorInventoryAudit.MeValue():F3} MeV (electron anchor, QG140/251)");
        sb.AppendLine($"MZ = {AnchorInventoryAudit.MzValue():F2} GeV (Z-anchor, QG130)");
        sb.AppendLine($"5/4 = {AnchorInventoryAudit.FiveFourths():F2} (acoustic-peak factor, QG238)");
        sb.AppendLine($"Bekenstein 1/4 = {AnchorInventoryAudit.BekensteinQuarter():F2} (target, QG185)");
        sb.AppendLine($"π = {AnchorInventoryAudit.Pi():F6} (universal constant)");
        sb.AppendLine($"η = diag({string.Join(", ", AnchorInventoryAudit.EtaMetric())}) (conformal reference)");
        sb.AppendLine($"d = {AnchorInventoryAudit.SpatialDimension()} (3+1, QG2/QG197)");
        sb.AppendLine($"RG running derived from D96: {AnchorInventoryAudit.RgRunningDerived()}");
        sb.AppendLine($"Bekenstein structure derived (1/4 not): {AnchorInventoryAudit.BekensteinStructureDerived()} / {AnchorInventoryAudit.BekensteinQuarterNotDerived()}");
        sb.AppendLine($"d≥3 derived: {AnchorInventoryAudit.DimensionalityDerived()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(0.511, AnchorInventoryAudit.MeValue());
        Assert.Equal(91.19, AnchorInventoryAudit.MzValue());
        Assert.Equal(1.25, AnchorInventoryAudit.FiveFourths());
        Assert.Equal(0.25, AnchorInventoryAudit.BekensteinQuarter());
        Assert.True(AnchorInventoryAudit.RgRunningDerived(), "the coupling running must emerge from D96");
        Assert.True(AnchorInventoryAudit.BekensteinStructureDerived() && AnchorInventoryAudit.BekensteinQuarterNotDerived(),
            "Bekenstein structure is derived but the exact 1/4 is not");
        Assert.True(AnchorInventoryAudit.DimensionalityDerived(), "d≥3 must be derived");
    }

    [Fact]
    public void ATQG2891_Classifications()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2891: STRUCTURAL / EMPIRICAL / BOUNDARY / REMOVABLE");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - STRUCTURAL = true theory input (framework/reference, not a free constant);");
        sb.AppendLine("  - EMPIRICAL = calibration value (measured absolute scale);");
        sb.AppendLine("  - BOUNDARY = accepted limit / target (not a flaw, not an input);");
        sb.AppendLine("  - REMOVABLE = not a true input (free constant / method).");
        sb.AppendLine();

        foreach (var a in AnchorInventoryAudit.Inventory())
        {
            sb.AppendLine($"  [{a.Kind.ToString().PadRight(10)}] {a.Name} ({a.Source})");
            sb.AppendLine($"       true input={a.IsTrueInput}  calibration={a.IsCalibration}  replaceable={a.IsReplaceable}");
            sb.AppendLine($"       {a.Note}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, AnchorInventoryAudit.StructuralCount());   // η, π, 3+1
        Assert.Equal(2, AnchorInventoryAudit.EmpiricalCount());    // me, MZ
        Assert.Equal(1, AnchorInventoryAudit.BoundaryCount());     // Bekenstein 1/4
        Assert.Equal(2, AnchorInventoryAudit.RemovableCount());    // 5/4, RG
        Assert.Equal(3, AnchorInventoryAudit.TrueInputCount());
        Assert.Equal(4, AnchorInventoryAudit.ReplaceableCount());  // me, MZ, 5/4, RG → 4? (me, MZ, 5/4, RG)
    }

    [Fact]
    public void ATQG2892_MinimalInventoryAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2892: the minimal anchor inventory");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the true anchors are the structural references (η, 3+1, π) + ONE scale;");
        sb.AppendLine("  - 5/4 and RG are replaceable; the second scale is redundant;");
        sb.AppendLine("  - Bekenstein 1/4 is a boundary target (not an input).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AnchorInventoryAudit.Summary()}");
        sb.AppendLine($"Anchor score: {AnchorInventoryAudit.AnchorScore()}/5");
        sb.AppendLine($"structural={AnchorInventoryAudit.StructuralCount()} empirical={AnchorInventoryAudit.EmpiricalCount()} boundary={AnchorInventoryAudit.BoundaryCount()} removable={AnchorInventoryAudit.RemovableCount()}");
        sb.AppendLine($"true inputs={AnchorInventoryAudit.TrueInputCount()} calibration={AnchorInventoryAudit.CalibrationCount()} replaceable={AnchorInventoryAudit.ReplaceableCount()}");
        sb.AppendLine($"minimal set reachable: {AnchorInventoryAudit.MinimalSetReachable()}");
        sb.AppendLine($"CLASSIFICATION = {AnchorInventoryAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("MINIMAL ANCHOR INVENTORY:");
        foreach (var m in AnchorInventoryAudit.MinimalInventory())
            sb.AppendLine($"  - {m}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - TRUE THEORY INPUTS: η (conformal reference), 3+1 (derived d≥3), π (universal) —");
        sb.AppendLine("    the framework's structural references, not free constants.");
        sb.AppendLine("  - ONLY CALIBRATION: me and MZ — absolute scales; all ratios are chain-derived");
        sb.AppendLine("    (QG288), so only ONE empirical scale is strictly needed.");
        sb.AppendLine("  - REPLACEABLE: 5/4 (free constant — derive or absorb), RG (the running EMERGES");
        sb.AppendLine("    from D96, QG204), and the redundant second scale anchor.");
        sb.AppendLine("  - BOUNDARY: Bekenstein 1/4 (target coefficient — the 2π gap, QG185/QG259).");
        sb.AppendLine("  - The theory needs NO free physics constant: framework + one scale.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MINIMAL INVENTORY", AnchorInventoryAudit.Classify());
        Assert.True(AnchorInventoryAudit.AnchorScore() >= 5);
        Assert.True(AnchorInventoryAudit.MinimalSetReachable());
        Assert.Contains("MINIMAL INVENTORY", AnchorInventoryAudit.Summary());
        Assert.Equal(4, AnchorInventoryAudit.MinimalInventory().Length);
    }
}
