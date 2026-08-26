using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 257 — Principle Competition Audit. Compare the seven formula-selection principles on
/// selection quality only (power, survivors, consistency, ad-hoc exceptions). No target values.
/// </summary>
public class ATQG_Phase257_PrincipleCompetitionAuditTests : ResearchTestBase
{
    public ATQG_Phase257_PrincipleCompetitionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2570_SevenPrinciples()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2570: the seven candidate principles");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Selection quality only: power (unique/7), survivors, consistency, ad-hoc exceptions;");
        sb.AppendLine("  - No target values enter any measurement.");
        sb.AppendLine();

        sb.AppendLine("THE SEVEN PRINCIPLES:");
        foreach (var p in PrincipleCompetitionAudit.Principles())
        {
            sb.AppendLine($"  {p.Name}");
            sb.AppendLine($"      power {p.SelectionPower / 7.0:P0} | survivors {p.AvgSurvivors:F1} | "
                        + $"consistent {p.Consistent} | exceptions {p.AdHocExceptions}");
            sb.AppendLine($"      {p.Note}");
        }
        sb.AppendLine();
        sb.AppendLine($"Any universal (7/7)? {PrincipleCompetitionAudit.AnyUniversal()}");
        sb.AppendLine($"Any exception-free universal? {PrincipleCompetitionAudit.AnyExceptionFreeUniversal()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(7, PrincipleCompetitionAudit.Principles().Length);
        Assert.False(PrincipleCompetitionAudit.AnyUniversal(), "no single principle selects all 7");
        Assert.False(PrincipleCompetitionAudit.AnyExceptionFreeUniversal(), "no exception-free universal principle");
    }

    [Fact]
    public void ATQG2571_Ranking()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2571: the principle ranking");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Rank by power (desc), survivors (asc), ad-hoc exceptions (asc).");
        sb.AppendLine();

        sb.AppendLine("RANKING (best → worst):");
        foreach (var p in PrincipleCompetitionAudit.Ranked())
            sb.AppendLine($"  {p.Name} — power {p.SelectionPower / 7.0:P0}, survivors {p.AvgSurvivors:F1}, exceptions {p.AdHocExceptions}");

        Output.WriteLine(sb.ToString());

        var ranked = PrincipleCompetitionAudit.Ranked();
        Assert.True(ranked[0].SelectionPower >= ranked[^1].SelectionPower, "ranking must be power-descending");
        // Noether consistency (3/7) should be the top or near-top by power.
        Assert.Equal("Noether consistency (QG255)", ranked[0].Name);
    }

    [Fact]
    public void ATQG2572_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2572: the determination — NO UNIVERSAL PRINCIPLE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - BEST PRINCIPLE requires a single exception-free principle selecting all 7;");
        sb.AppendLine("  - PRINCIPLE SET requires a single principle selecting all 7 (with exceptions);");
        sb.AppendLine("  - NO UNIVERSAL PRINCIPLE: no single principle is universal AND exception-free.");
        sb.AppendLine();

        sb.AppendLine($"BEST single: {PrincipleCompetitionAudit.Best().Name}");
        sb.AppendLine($"SUMMARY: {PrincipleCompetitionAudit.Summary()}");
        sb.AppendLine($"CLASSIFICATION = {PrincipleCompetitionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("VERDICT:");
        sb.AppendLine("  - No single principle uniquely selects all 7 observables;");
        sb.AppendLine("  - the only tie-resolving principle (Noether consistency) is INCONSISTENT");
        sb.AppendLine("    (QG238's published ℓ₁ = Σm·ln(span)·5/4 uses the 5/4 it rejects) — 1 ad-hoc exception;");
        sb.AppendLine("  - the QG255 'unique selection' came only from a SEQUENCE (octave preservation →");
        sb.AppendLine("    MDL → Noether → moment closure), with one inconsistency;");
        sb.AppendLine("  - hence NO UNIVERSAL PRINCIPLE exists among the seven candidates.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NO UNIVERSAL PRINCIPLE", PrincipleCompetitionAudit.Classify());
        Assert.Contains("NO UNIVERSAL PRINCIPLE", PrincipleCompetitionAudit.Summary());
    }
}
