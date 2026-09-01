using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 256 — Selection Principle Audit. Determine whether the QG254/QG255 selection rules are
/// forced by D96 or were selected post-hoc. Methodology only.
/// </summary>
public class ATQG_Phase256_SelectionPrincipleAuditTests : ResearchTestBase
{
    public ATQG_Phase256_SelectionPrincipleAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2560_TwoRulesAudited()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2560: the two selection rules audited");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - For each rule: derivable? necessary? alternatives? consistency?");
        sb.AppendLine("  - Classify FORCED / PREFERRED / ARBITRARY.");
        sb.AppendLine();

        foreach (var r in SelectionPrincipleAudit.Rules())
        {
            sb.AppendLine($"  {r.Name}: {r.Status}");
            sb.AppendLine($"      Derivable: {r.Derivable} | Necessary: {r.Necessary}");
            sb.AppendLine($"      Alternatives: {string.Join("; ", r.Alternatives)}");
            sb.AppendLine($"      Consistency: {r.ConsistencyNote}");
        }
        sb.AppendLine();
        sb.AppendLine($"By status: {string.Join(", ", SelectionPrincipleAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        var rules = SelectionPrincipleAudit.Rules();
        Assert.Equal(2, rules.Length);
        Assert.Equal(SelectionPrincipleAudit.Status.Preferred, rules[0].Status);   // octave preservation
        Assert.Equal(SelectionPrincipleAudit.Status.Arbitrary, rules[1].Status);   // moment-closure MDL
        Assert.False(rules[1].Derivable, "MDL is imported, not D96-derived");
    }

    [Fact]
    public void ATQG2561_NoetherInconsistency()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2561: the decisive 5/4 inconsistency");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG255 rejected '5/4·Σ√m/λ₂' because 5/4 is a 'free constant';");
        sb.AppendLine("  - If 5/4 appears in a PUBLISHED AT formula, the exclusion is post-hoc.");
        sb.AppendLine();

        sb.AppendLine($"Published formula using 5/4: {SelectionPrincipleAudit.PublishedFiveQuartersFormula()}");
        sb.AppendLine($"QG255's Noether rule inconsistent with the published formulas? {SelectionPrincipleAudit.NoetherInconsistentWithPublished()}");
        sb.AppendLine();
        sb.AppendLine("The published QG238 acoustic-peak formula ℓ₁ = Σm·ln(span)·(5/4) uses the SAME");
        sb.AppendLine("5/4 that QG255 excluded as a 'free constant'. The exclusion was calibrated on the");
        sb.AppendLine("tie candidate (5/4·Σ√m/λ₂), not on a uniform D96 principle.");

        Output.WriteLine(sb.ToString());

        Assert.True(SelectionPrincipleAudit.NoetherInconsistentWithPublished());
        Assert.Contains("5/4", SelectionPrincipleAudit.PublishedFiveQuartersFormula());
    }

    [Fact]
    public void ATQG2562_Risk()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2562: the selection-principle risk");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The rules were introduced AFTER QG253 revealed the non-uniqueness;");
        sb.AppendLine("  - A rule is FORCED only if it follows from D96 without target or pool calibration.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {SelectionPrincipleAudit.Summary()}");
        sb.AppendLine();
        sb.AppendLine("VERDICT:");
        sb.AppendLine("  - OCTAVE PRESERVATION: PREFERRED — the octave structure occ=[4,4,87] is D96-native,");
        sb.AppendLine("    but the prohibition form was calibrated on the QG253 alternatives; competing");
        sb.AppendLine("    symmetry projections exist.");
        sb.AppendLine("  - MOMENT-CLOSURE MDL: ARBITRARY — MDL is imported, the moment-order ranking is");
        sb.AppendLine("    conventional, and the Noether 5/4 exclusion contradicts the published QG238");
        sb.AppendLine("    ℓ₁ = Σm·ln(span)·5/4 (post-hoc distinction).");
        sb.AppendLine("  - SELECTION-PRINCIPLE RISK: HIGH for the meta-claim that the rules are forced —");
        sb.AppendLine("    they were selected after the fact and carry retro-selection at the meta-level.");

        Output.WriteLine(sb.ToString());

        Assert.Contains("HIGH", SelectionPrincipleAudit.Risk());
        Assert.Contains("HIGH", SelectionPrincipleAudit.Summary());
    }
}
