using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

/// <summary>Minimal taxonomy review: resolves selected/contingent ambiguity and coincidence vs structure.</summary>
public class AT_MinimalTaxonomyReview : ResearchTestBase
{
    public AT_MinimalTaxonomyReview(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void MinimalTaxonomy_Review()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Minimal Classification Taxonomy Review");

        S(sb, "Section A — The four categories"); sb.AppendLine(SectionA());
        S(sb, "Section B — Upgrade path (Phases 148–156)"); sb.AppendLine(SectionB());
        S(sb, "Section C — How it resolves the two soft spots"); sb.AppendLine(SectionC());
        S(sb, "Section D — Consistency score"); sb.AppendLine(SectionD());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  categories: 4   changed: {MinimalTaxonomy.ChangedCount()}   " +
                      $"consistency {MinimalTaxonomy.OldConsistency:F2} → {MinimalTaxonomy.NewConsistency:F2}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "MinimalTaxonomy_Report.txt"), sb.ToString());

        Assert.Equal(4, MinimalTaxonomy.Categories().Length);
        Assert.Equal(13, MinimalTaxonomy.UpgradePath().Length);
        Assert.True(MinimalTaxonomy.NewConsistency > MinimalTaxonomy.OldConsistency);
        // Koide must be upgraded to STRUCTURED-UNDERIVED (the key change).
        var koide = MinimalTaxonomy.UpgradePath().First(u => u.Object.Contains("Koide"));
        Assert.Equal("STRUCTURED-UNDERIVED", koide.New);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Four categories (no new physics primitives):");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-22} {1}", "category", "definition"));
        foreach (var c in MinimalTaxonomy.Categories())
            sb.AppendLine(string.Format("  {0,-22} {1}", c.Category, c.Definition));
        sb.AppendLine();
        sb.AppendLine("  'SELECTED' is ELIMINATED: it decomposes into 'derived lower bound ∩ drawn upper bound'.");
        sb.AppendLine("  This removes the selected/contingent flip (Phase 156 soft spot #1).");
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Upgrade path (old → new):");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-32} {1,-24} {2,-22}", "object", "old", "new"));
        foreach (var u in MinimalTaxonomy.UpgradePath())
            sb.AppendLine(string.Format("  {0,-32} {1,-24} {2,-22} {3}",
                u.Object, u.Old, u.New, u.Changed ? "(CHANGED)" : ""));
        return sb.ToString();
    }

    private static string SectionC()
    {
        return
            "HOW THE TWO SOFT SPOTS ARE RESOLVED:\n" +
            "\n" +
            "  SOFT SPOT #1 (selected↔contingent flip for internal 3):\n" +
            "    RESOLVED. The internal N=3 is now unambiguously 'DERIVED-lower ∩ DRAWN-upper':\n" +
            "    the lower bound N≥3 is DERIVED (CP theorem), the upper bound N≤3 is DRAWN\n" +
            "    (empirical). No more flip — the two components are separately classified.\n" +
            "\n" +
            "  SOFT SPOT #2 (contingent = origin vs coincidence):\n" +
            "    RESOLVED. 'Contingent' is SPLIT into two categories:\n" +
            "      STRUCTURED-UNDERIVED  (Koide: real, precise, predictive, origin underived)\n" +
            "      DRAWN                (Yukawas/couplings/Ω_DM: log-normal draws, no structure)\n" +
            "    The boundary is set by the Bayesian balance (Phase 154: Koide BF≈3e4 → structured;\n" +
            "    Yukawas show no such precision → drawn).\n" +
            "\n" +
            "  This is the MINIMAL fix: it adds NO physics primitive, only disambiguates the labels.";
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Consistency score:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  OLD (4-category, ambiguous): {0:F2}", MinimalTaxonomy.OldConsistency));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  NEW (4-category, disambiguated): {0:F2}", MinimalTaxonomy.NewConsistency));
        sb.AppendLine();
        sb.AppendLine("  The improvement reflects the elimination of the two taxonomy ambiguities. The remaining");
        sb.AppendLine("  0.05 is NOT a taxonomy issue — it is the physics-level underdetermination (a single");
        sb.AppendLine("  universe cannot distinguish one cascade from three), which no classification can remove.");
        return sb.ToString();
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
