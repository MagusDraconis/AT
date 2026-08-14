using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>Taxonomy stress test: assign, conflict-check, collapse.</summary>
public class TQM_TaxonomyStressTest : ResearchTestBase
{
    public TQM_TaxonomyStressTest(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TaxonomyStressTest_Audit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Taxonomy Stress Test — Phases 148–157");

        S(sb, "Section A — Every result assigned one category"); sb.AppendLine(SectionA());
        S(sb, "Section B — Category conflicts"); sb.AppendLine(SectionB());
        S(sb, "Section C — Category collapse"); sb.AppendLine(SectionC());
        S(sb, "Section D — Minimal taxonomy"); sb.AppendLine(SectionD());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  results: {TaxonomyStressTest.Results().Length}   conflicts: {TaxonomyStressTest.ConflictCount()}   " +
                      $"composites: {TaxonomyStressTest.CompositeCount()}   " +
                      $"minimal categories: {TaxonomyStressTest.MinimalCategories().Length}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "TaxonomyStressTest_Report.txt"), sb.ToString());

        Assert.Equal(14, TaxonomyStressTest.Results().Length);
        Assert.Equal(0, TaxonomyStressTest.ConflictCount());
        Assert.Equal(2, TaxonomyStressTest.CompositeCount());
        Assert.Equal(3, TaxonomyStressTest.MinimalCategories().Length);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Every result from Phases 148–157, assigned to exactly one category:");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-30} {1,-30} {2}", "result", "category", "kind"));
        foreach (var r in TaxonomyStressTest.Results())
            sb.AppendLine(string.Format("  {0,-30} {1,-30} {2}", r.Result, r.Category, r.Kind));
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"CONFLICT COUNT: {TaxonomyStressTest.ConflictCount()}.");
        sb.AppendLine();
        sb.AppendLine("  Phase 157 already resolved the two prior conflicts (the selected↔contingent flip and");
        sb.AppendLine("  the 'contingent' ambiguity). No residual CONTRADICTION remains.");
        sb.AppendLine();
        sb.AppendLine($"  COMPOSITE COUNT: {TaxonomyStressTest.CompositeCount()} — two results are unions of two");
        sb.AppendLine("  categories rather than a single one:");
        sb.AppendLine("    - internal N=3 = DERIVED (lower bound N≥3) ∩ DRAWN (upper bound N≤3)");
        sb.AppendLine("    - SU(3) whole = REAL-UNDERIVED (group structure) + DRAWN (the count 3)");
        sb.AppendLine("  These are NOT contradictions — they are legitimate composites (a value + its two bounds,");
        sb.AppendLine("  or a structure + its multiplicity).");
        return sb.ToString();
    }

    private static string SectionC()
    {
        return
            "CATEGORY COLLAPSE ATTEMPT (minimize the set):\n" +
            "\n" +
            "  " + TaxonomyStressTest.Collapse + ".\n" +
            "\n" +
            "  Attempts:\n" +
            "    DERIVED  → cannot collapse (distinct: computable).\n" +
            "    DRAWN    → cannot collapse (distinct: coincidental).\n" +
            "    EMERGENT → COLLAPSES into REAL-UNDERIVED (both are real + underived).\n" +
            "    STRUCTURED → stays as the no-mechanism sub-type of REAL-UNDERIVED.\n" +
            "\n" +
            "  RESULT: exactly ONE collapse (EMERGENT absorbed). No other category can collapse.";
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("MINIMAL NECESSARY CATEGORIES:");
        sb.AppendLine();
        foreach (var c in TaxonomyStressTest.MinimalCategories())
            sb.AppendLine($"  - {c}");
        sb.AppendLine();
        sb.AppendLine("  This is the minimal set: 3 categories suffice to resolve the ambiguity and separate");
        sb.AppendLine("  coincidence from real structure. 'emergent' and 'structured' survive as MODIFIERS of");
        sb.AppendLine("  REAL-UNDERIVED (with/without a generating mechanism), not as separate categories.");
        return sb.ToString();
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
