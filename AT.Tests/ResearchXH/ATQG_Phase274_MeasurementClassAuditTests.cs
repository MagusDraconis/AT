using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 274 — Measurement Class Audit. Do measurement classes exist between projections and
/// sectors? Do sector labels emerge from measurement classes? D96 only, no observables.
/// </summary>
public class ATQG_Phase274_MeasurementClassAuditTests : ResearchTestBase
{
    public ATQG_Phase274_MeasurementClassAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2740_MeasurementClasses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2740: the five measurement classes");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - each measurement class has a UNIQUE structural signature (determinable by form);");
        sb.AppendLine("  - value/strength/orientation/global/geometry are structurally unambiguous.");
        sb.AppendLine();

        foreach (var c in MeasurementClassAudit.Classes())
            sb.AppendLine($"  [{c.Kind,-11}] {c.Signature}");
        sb.AppendLine();
        sb.AppendLine($"structurally unique classes: {MeasurementClassAudit.StructurallyUniqueClassCount()}/5");
        sb.AppendLine();
        sb.AppendLine("CONTRAST: sectors are NOT determinable by form alone (QG273) — a ratio read is");
        sb.AppendLine("ambiguously coupling, mixing, or mass-ratio — but each CLASS is unambiguous.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, MeasurementClassAudit.Classes().Length);
        Assert.True(MeasurementClassAudit.ClassesStructurallyDeterminable(),
            "all five measurement classes are structurally unique");
    }

    [Fact]
    public void ATQG2741_ClassSectorMapping()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2741: the class → sector mapping");

        sb.AppendLine("HYPOTHESIS: each class maps to a dominant sector; the class is structural, the");
        sb.AppendLine("sector is the role the read plays.");
        sb.AppendLine();

        foreach (var (kind, sector, spans) in MeasurementClassAudit.ClassSectorMapping())
            sb.AppendLine($"  {kind,-11} → {sector,-10} spans-other-sectors={spans}");
        sb.AppendLine();
        sb.AppendLine($"all classes map to a distinct sector: {MeasurementClassAudit.AllClassesMapToSector()}");
        sb.AppendLine();
        sb.AppendLine("The STRENGTH class is the structural layer: √occMom·λ₂ (a ratio) is unambiguously a");
        sb.AppendLine("strength read whether it is the mass ratio m_τ/m_μ or the coupling y_τ/y_μ; Vus =");
        sb.AppendLine("#d/(2Σm) is a strength read placed in the unitary mixing matrix.");

        Output.WriteLine(sb.ToString());

        Assert.True(MeasurementClassAudit.AllClassesMapToSector(), "each class maps to a distinct dominant sector");
        Assert.True(MeasurementClassAudit.StrengthReadSpansSectors(), "strength reads span coupling/mixing/mass-ratio");
    }

    [Fact]
    public void ATQG2742_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2742: the class-layer determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO CLASS LAYER (score ≤ 2), PARTIAL CLASS LAYER (3-4),");
        sb.AppendLine("    MEASUREMENT CLASS LAYER (5-6);");
        sb.AppendLine("  - the hypothesis: measurement classes exist between projections and sectors.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {MeasurementClassAudit.Summary()}");
        sb.AppendLine($"Class-layer score: {MeasurementClassAudit.ClassLayerScore()}/6");
        sb.AppendLine($"Layer structure: {MeasurementClassAudit.LayerStructure()}");
        sb.AppendLine($"CLASSIFICATION = {MeasurementClassAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - Each measurement class is STRUCTURALLY UNIQUE: value=dimensional,");
        sb.AppendLine("    strength=ratio, orientation=unitary, global=log, geometry=power/deficit —");
        sb.AppendLine("    determinable by form, unlike sectors (QG273).");
        sb.AppendLine("  - Each class maps to a dominant sector: value→mass, strength→coupling,");
        sb.AppendLine("    orientation→mixing, global→cosmology, geometry→gravity.");
        sb.AppendLine("  - The QG273 ratio ambiguity is RESOLVED: √occMom·λ₂ is unambiguously a STRENGTH");
        sb.AppendLine("    read; only its sector role (mass vs coupling) is assigned by the equation.");
        sb.AppendLine("  - The layer: PROJECTIONS → MEASUREMENT CLASSES → SECTORS. The sector labels");
        sb.AppendLine("    EMERGE from the measurement classes (class determined by form, sector by role).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MEASUREMENT CLASS LAYER", MeasurementClassAudit.Classify());
        Assert.True(MeasurementClassAudit.ClassLayerScore() >= 5);
        Assert.Contains("MEASUREMENT CLASS LAYER", MeasurementClassAudit.Summary());
        Assert.Contains("STRENGTH", MeasurementClassAudit.Summary());
    }
}
