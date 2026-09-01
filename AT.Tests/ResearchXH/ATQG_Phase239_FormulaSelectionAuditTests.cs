using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 239 — Formula Selection Audit. Audit the uniqueness of the closed-form derivations in
/// QG203-QG238. Audit only — no new physics.
/// </summary>
public class ATQG_Phase239_FormulaSelectionAuditTests : ResearchTestBase
{
    public ATQG_Phase239_FormulaSelectionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2390_SixRelationsAudited()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2390: the six closed-form relations audited");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Audits QG203-238 closed-form relations: neutrino masses, cosmological fractions,");
        sb.AppendLine("    n_s, acoustic peaks, lepton hierarchy, Lambda origin.");
        sb.AppendLine();

        sb.AppendLine("THE SIX AUDITS:");
        foreach (var a in FormulaSelectionAudit.Audits())
        {
            sb.AppendLine($"  {a.Relation}: {a.Classification}");
            sb.AppendLine($"      formula: {a.Formula}");
            sb.AppendLine($"      candidates: {a.CandidateCount}, alternatives existed: {a.AlternativesExisted}");
            sb.AppendLine($"      selected because: {a.SelectionReason}");
            sb.AppendLine($"      target influenced: {a.TargetInfluenced}, preregistered: {a.Preregistered}");
        }
        sb.AppendLine();
        sb.AppendLine($"Counts: {FormulaSelectionAudit.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, FormulaSelectionAudit.Audits().Length);
        var c = FormulaSelectionAudit.ClassificationCounts();
        Assert.Equal(1, c[FormulaSelectionAudit.Classification.Unique]);
        Assert.Equal(3, c[FormulaSelectionAudit.Classification.Preferred]);
        Assert.Equal(0, c.GetValueOrDefault(FormulaSelectionAudit.Classification.Underdetermined));
        Assert.Equal(2, c[FormulaSelectionAudit.Classification.RetroSelectionRisk]);
    }

    [Fact]
    public void ATQG2391_SelectionIntegrity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2391: selection integrity — target influence and preregistration");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A derivation is strongest when the target did NOT influence selection and it was");
        sb.AppendLine("    preregistered; the weakest is retro-selection on a sharp target without registration.");
        sb.AppendLine();

        sb.AppendLine("TARGET-INFLUENCED SELECTIONS:");
        foreach (var a in FormulaSelectionAudit.Audits().Where(a => a.TargetInfluenced))
            sb.AppendLine($"  • {a.Relation} — {a.Classification}");
        sb.AppendLine();
        sb.AppendLine($"Target-influenced: {FormulaSelectionAudit.TargetInfluencedCount()}/6");
        sb.AppendLine($"Preregistered: {FormulaSelectionAudit.PreregisteredCount()}/6 (none in QG203-238)");
        sb.AppendLine();
        sb.AppendLine("RETRO-SELECTION RISK ITEMS:");
        foreach (var r in FormulaSelectionAudit.RetroSelectionItems())
            sb.AppendLine($"  • {r}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, FormulaSelectionAudit.RetroSelectionCount());
        Assert.Equal(5, FormulaSelectionAudit.TargetInfluencedCount());
        Assert.Equal(0, FormulaSelectionAudit.PreregisteredCount());
        Assert.Contains("Spectral index n_s (QG237)", FormulaSelectionAudit.RetroSelectionItems());
        Assert.Contains("Acoustic peaks (QG238)", FormulaSelectionAudit.RetroSelectionItems());
    }

    [Fact]
    public void ATQG2392_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2392: summary — the formula-risk table");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The two retro-selection items (n_s, acoustic peaks) are the strongest anti-fit");
        sb.AppendLine("    criticism of QG203-238; the Lambda scaling is structurally unique.");
        sb.AppendLine();

        sb.AppendLine("FORMULA-RISK TABLE:");
        foreach (var a in FormulaSelectionAudit.Audits())
            sb.AppendLine($"  {a.Relation.PadRight(30)} → {a.Classification}");
        sb.AppendLine();
        sb.AppendLine($"Summary: {FormulaSelectionAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - 1 UNIQUE (Lambda 1/R² scaling — structurally forced by the single-scale R).");
        sb.AppendLine("  - 3 PREFERRED (neutrino masses, cosmological fractions, lepton hierarchy — natural");
        sb.AppendLine("    D96 normalizations/moments, matched to targets after selection).");
        sb.AppendLine("  - 2 RETRO-SELECTION RISK (n_s QG237, acoustic peaks QG238 — specific D96 combinations");
        sb.AppendLine("    matching sharp observed targets without preregistration or an independent");
        sb.AppendLine("    uniqueness principle). These should be pre-registered or given a uniqueness proof.");

        Output.WriteLine(sb.ToString());

        Assert.StartsWith("1 UNIQUE / 3 PREFERRED / 0 UNDERDETERMINED / 2 RETRO-SELECTION RISK", FormulaSelectionAudit.Summary());
    }
}
