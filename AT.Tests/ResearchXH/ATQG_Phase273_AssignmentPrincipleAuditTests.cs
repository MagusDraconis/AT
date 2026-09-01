using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 273 — Assignment Principle Audit. Why does a projection become mass, coupling, mixing,
/// gravity, or cosmology instead of another sector? Is there a D96-native assignment rule?
/// </summary>
public class ATQG_Phase273_AssignmentPrincipleAuditTests : ResearchTestBase
{
    public ATQG_Phase273_AssignmentPrincipleAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2730_AssignmentFeatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2730: the structural assignment features");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - some read features are D96-native and determine a sector by form;");
        sb.AppendLine("  - the ratio class is ambiguous (shared across sectors).");
        sb.AppendLine();

        foreach (var f in AssignmentPrincipleAudit.Features())
            sb.AppendLine($"  [{f.Feature,-10}] {f.Name,-30} native={f.D96Native,-5} determines={f.DeterminesSector,-5} → {f.Sector}");
        sb.AppendLine();
        sb.AppendLine($"D96-native features: {AssignmentPrincipleAudit.D96NativeFeatureCount()}/5");
        sb.AppendLine($"form-determining features: {AssignmentPrincipleAudit.DeterminingFeatureCount()}/5");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, AssignmentPrincipleAudit.Features().Length);
        Assert.True(AssignmentPrincipleAudit.D96NativeFeatureCount() >= 4, "4 of 5 features are D96-native");
        Assert.True(AssignmentPrincipleAudit.DeterminingFeatureCount() >= 4,
            "dimension/unitarity/log/power each determine a sector");
    }

    [Fact]
    public void ATQG2731_DecisiveDuplication()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2731: the decisive evidence — identical form in two sectors");

        sb.AppendLine("HYPOTHESIS: if the SAME formula is assigned to two different sectors, the");
        sb.AppendLine("assignment is NOT determined by the read structure alone.");
        sb.AppendLine();

        sb.AppendLine($"m_τ/m_μ (MASSES)   = √occMom·λ₂ = {AssignmentPrincipleAudit.TauMuonMassRatio():F6}");
        sb.AppendLine($"y_τ/y_μ (COUPLINGS) = √occMom·λ₂ = {AssignmentPrincipleAudit.TauMuonYukawaRatio():F6}");
        sb.AppendLine($"identical form in two sectors: {AssignmentPrincipleAudit.IdenticalFormInTwoSectors()}");
        sb.AppendLine($"ratio class ambiguous: {AssignmentPrincipleAudit.RatioClassAmbiguous()}");
        sb.AppendLine();
        sb.AppendLine("Vus = #d/(2Σm) is structurally a coupling-like ratio; only its placement in the");
        sb.AppendLine("unitary CKM matrix makes it a mixing angle. The ratio form alone cannot separate");
        sb.AppendLine("coupling, mixing, and mass-ratio reads.");

        Output.WriteLine(sb.ToString());

        Assert.True(AssignmentPrincipleAudit.IdenticalFormInTwoSectors(),
            "the identical form √occMom·λ₂ is both the mass ratio and the Yukawa coupling");
        Assert.True(AssignmentPrincipleAudit.RatioClassAmbiguous());
    }

    [Fact]
    public void ATQG2732_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2732: the assignment-rule determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO ASSIGNMENT (score ≤ 2), PARTIAL ASSIGNMENT (3-4),");
        sb.AppendLine("    ASSIGNMENT PRINCIPLE (5-6);");
        sb.AppendLine("  - the assignment rule must be D96-native and target-free.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AssignmentPrincipleAudit.Summary()}");
        sb.AppendLine($"Assignment score: {AssignmentPrincipleAudit.AssignmentScore()}/6");
        sb.AppendLine($"Assignment rule: {AssignmentPrincipleAudit.AssignmentRule()}");
        sb.AppendLine($"CLASSIFICATION = {AssignmentPrincipleAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("SECTOR DETERMINABILITY BY FORM:");
        foreach (var (sector, det, note) in AssignmentPrincipleAudit.Determinability())
            sb.AppendLine($"  {sector,-10} determinable={det,-5} ({note})");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - 4 structural rules are D96-native: dimension→mass, unitarity→mixing,");
        sb.AppendLine("    log→cosmology, power≥2→gravity — 3/5 sectors determinable by form alone.");
        sb.AppendLine("  - The ratio-class is NOT separable: the identical form √occMom·λ₂ is both");
        sb.AppendLine("    m_τ/m_μ (mass) and y_τ/y_μ (coupling) — the assignment is role-based there.");
        sb.AppendLine("  - The assignment is PARTIAL: 4 structural rules + a residual role-based step,");
        sb.AppendLine("    which is the precise location of the QG271 frontier.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ASSIGNMENT", AssignmentPrincipleAudit.Classify());
        Assert.True(AssignmentPrincipleAudit.AssignmentScore() >= 3,
            "the structural rules cover ≥ 3 sectors (dimension/log/power)");
        Assert.Contains("PARTIAL ASSIGNMENT", AssignmentPrincipleAudit.Summary());
        Assert.Contains("√occMom·λ₂", AssignmentPrincipleAudit.Summary());
    }
}
