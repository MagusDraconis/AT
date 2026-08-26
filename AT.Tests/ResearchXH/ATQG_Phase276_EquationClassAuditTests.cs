using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 276 — Equation Class Audit. Why do different equation types exist? Is there a layer
/// between Role and Observable? D96 only, no observables.
/// </summary>
public class ATQG_Phase276_EquationClassAuditTests : ResearchTestBase
{
    public ATQG_Phase276_EquationClassAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2760_EquationForms()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2760: the equation forms per sector");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - each sector has a characteristic equation form;");
        sb.AppendLine("  - the form is determined by the measurement class (the class's natural relation).");
        sb.AppendLine();

        foreach (var s in EquationClassAudit.SectorEquations())
            sb.AppendLine($"  {s.Sector,-11} [{s.Form,-15}] example: {s.Example}");
        sb.AppendLine();
        sb.AppendLine($"form determined by class: {EquationClassAudit.EquationFormDeterminedByClass()}");
        sb.AppendLine();
        sb.AppendLine("mass → scalar equality (VALUE); coupling → ratio (STRENGTH);");
        sb.AppendLine("mixing → angle+unitary (ORIENTATION); cosmology → log-ratio (GLOBAL);");
        sb.AppendLine("gravity → power law (GEOMETRY).");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, EquationClassAudit.SectorEquations().Length);
        Assert.True(EquationClassAudit.EquationFormDeterminedByClass(),
            "each equation form is determined by its measurement class");
        Assert.True(EquationClassAudit.SectorEquations().Select(s => s.Form).Distinct().Count() == 5,
            "five distinct equation forms");
    }

    [Fact]
    public void ATQG2761_FormSharing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2761: the form sharing — equation classes are projection classes");

        sb.AppendLine("HYPOTHESIS: the equation forms are NOT sector-unique — the ratio form spans");
        sb.AppendLine("mass, coupling, and mixing sectors.");
        sb.AppendLine();

        sb.AppendLine($"m_τ/m_μ (mass)   = √occMom·λ₂ = {LeptonHierarchyExactLaw.TauMuonRatio():F4}");
        sb.AppendLine($"y_τ/y_μ (coupl)  = √occMom·λ₂ = {YukawaOrigin.TauMuonRatio():F4}");
        sb.AppendLine($"ratio form spans mass & coupling: {EquationClassAudit.RatioFormSpansSectors()}");
        sb.AppendLine($"mixing shares the ratio form (Vus like sin²θ_W): {EquationClassAudit.MixingSharesRatioForm()}");
        sb.AppendLine();
        sb.AppendLine("Vus = #d/(2Σm) has the same ratio structure as sin²θ_W = #g/(2Σm) — the ratio");
        sb.AppendLine("form is shared across mass/coupling/mixing. No equation form is sector-unique.");

        Output.WriteLine(sb.ToString());

        Assert.True(EquationClassAudit.RatioFormSpansSectors(),
            "the ratio form is shared between mass and coupling");
        Assert.True(EquationClassAudit.MixingSharesRatioForm());
    }

    [Fact]
    public void ATQG2762_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2762: the equation-layer determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO EQUATION LAYER (score ≤ 2), PARTIAL EQUATION LAYER (3-4),");
        sb.AppendLine("    EQUATION CLASS LAYER (5-6);");
        sb.AppendLine("  - the question: where is the layer between Role and Observable?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {EquationClassAudit.Summary()}");
        sb.AppendLine($"Equation-layer score: {EquationClassAudit.EquationLayerScore()}/6");
        sb.AppendLine($"Layer structure: {EquationClassAudit.LayerStructure()}");
        sb.AppendLine($"CLASSIFICATION = {EquationClassAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The equation form is the MEASUREMENT CLASS's natural relation: value→equality,");
        sb.AppendLine("    strength→ratio, orientation→unitary, global→log, geometry→power.");
        sb.AppendLine("  - The forms are PROJECTION CLASSES: the ratio form spans mass/coupling/mixing —");
        sb.AppendLine("    no form is sector-unique, so the equation classes are not fundamental.");
        sb.AppendLine("  - The layer: ROLE → EQUATION FORM → OBSERVABLE. The equation form is the bridge");
        sb.AppendLine("    between the role and the observable: the structural relation type the");
        sb.AppendLine("    observable satisfies, set by its measurement class.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("EQUATION CLASS LAYER", EquationClassAudit.Classify());
        Assert.True(EquationClassAudit.EquationLayerScore() >= 5);
        Assert.Contains("EQUATION CLASS LAYER", EquationClassAudit.Summary());
        Assert.Contains("PROJECTION", EquationClassAudit.Summary());
    }
}
