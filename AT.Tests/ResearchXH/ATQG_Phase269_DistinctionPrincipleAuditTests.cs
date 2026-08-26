using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 269 — Distinction Principle Audit. What makes a Q-event a distinguishable unit? Is count
/// more fundamental than distinction, or vice versa? D96 only, no observables.
/// </summary>
public class ATQG_Phase269_DistinctionPrincipleAuditTests : ResearchTestBase
{
    public ATQG_Phase269_DistinctionPrincipleAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2690_CountabilityAndStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2690: countability and the regular network");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the count exists and is self-consistent (Born rule exact);");
        sb.AppendLine("  - the network is REGULAR — distinction cannot come from the structure.");
        sb.AppendLine();

        sb.AppendLine($"N = {DistinctionPrincipleAudit.EventCount()} events");
        sb.AppendLine($"Σm = {DistinctionPrincipleAudit.ModeCount()} modes");
        sb.AppendLine($"Born rule exact (self-consistent count): {DistinctionPrincipleAudit.CountSelfConsistent()}");
        sb.AppendLine($"network is regular: {DistinctionPrincipleAudit.NetworkIsRegular()}");
        sb.AppendLine($"all nodes structurally identical (degree {InvariantOriginAudit.CommonDegree()}): {DistinctionPrincipleAudit.NodesStructurallyIdentical()}");
        sb.AppendLine();
        sb.AppendLine("→ all 96 nodes are structurally identical — there is NO structural label that");
        sb.AppendLine("  distinguishes one node from another. Distinction cannot come from the network.");

        Output.WriteLine(sb.ToString());

        Assert.True(DistinctionPrincipleAudit.CountSelfConsistent());
        Assert.True(DistinctionPrincipleAudit.NetworkIsRegular(), "regular graph — no structural labels");
    }

    [Fact]
    public void ATQG2691_DegeneracyEvidence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2691: the degeneracy evidence — count works without spectral distinction");

        sb.AppendLine("HYPOTHESIS: the multiplicity multiset [42×2, 5, 6] has degenerate pairs with");
        sb.AppendLine("IDENTICAL frequency, yet they are counted as SEPARATE units — so the count does");
        sb.AppendLine("NOT require distinction-by-frequency.");
        sb.AppendLine();

        sb.AppendLine($"degenerate pairs: {DistinctionPrincipleAudit.DegeneratePairs()}");
        sb.AppendLine($"degenerate units counted separately: {DistinctionPrincipleAudit.DegenerateCountedUnits()}");
        sb.AppendLine($"Σm = {DistinctionPrincipleAudit.ModeCount()}");
        sb.AppendLine($"count works without spectral distinction: {DistinctionPrincipleAudit.CountWorksWithoutSpectralDistinction()}");
        sb.AppendLine();
        sb.AppendLine("The 42 pairs have ω = √λ identical (indistinguishable by frequency), yet Σm = 95");
        sb.AppendLine("counts each member separately (84 units in the pairs + 11 in the 5 and 6 groups).");
        sb.AppendLine("Unit-ness (individuation) is PRIOR to distinction-by-frequency.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(42, DistinctionPrincipleAudit.DegeneratePairs());
        Assert.Equal(84, DistinctionPrincipleAudit.DegenerateCountedUnits());
        Assert.True(DistinctionPrincipleAudit.CountWorksWithoutSpectralDistinction());
    }

    [Fact]
    public void ATQG2692_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2692: the individuation determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - COUNT FUNDAMENTAL (score ≤ 2), DISTINCTION FUNDAMENTAL (3-4),");
        sb.AppendLine("    SINGLE INDIVIDUATION PRINCIPLE (5-6);");
        sb.AppendLine("  - the ordering question: count vs distinction.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {DistinctionPrincipleAudit.Summary()}");
        sb.AppendLine($"Distinction score: {DistinctionPrincipleAudit.DistinctionScore()}/6");
        sb.AppendLine($"Q-event is a network transition (the individuating act): {DistinctionPrincipleAudit.QEventIsTransition()}");
        sb.AppendLine($"CLASSIFICATION = {DistinctionPrincipleAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - COUNT cannot be derived from DISTINCTION: the degenerate pairs have identical");
        sb.AppendLine("    frequency, yet Σm = 95 counts each member separately — the count works without");
        sb.AppendLine("    spectral distinction.");
        sb.AppendLine("  - DISTINCTION cannot be derived from COUNT: the network is REGULAR (all nodes");
        sb.AppendLine("    degree 12) — there is no structural order that separates the nodes.");
        sb.AppendLine("  - Both arise from the SAME act: actualization. A Q-event is a distinct tick at a");
        sb.AppendLine("    distinct causal position (branching generation k) — this one act makes the event");
        sb.AppendLine("    simultaneously COUNTABLE (one tick = one unit) and DISTINGUISHABLE (a distinct");
        sb.AppendLine("    position).");
        sb.AppendLine("  - Count and distinction are the two inseparable faces of the single act of");
        sb.AppendLine("    individuation. Neither is more fundamental.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SINGLE INDIVIDUATION PRINCIPLE", DistinctionPrincipleAudit.Classify());
        Assert.True(DistinctionPrincipleAudit.DistinctionScore() >= 5);
        Assert.Contains("SINGLE INDIVIDUATION PRINCIPLE", DistinctionPrincipleAudit.Summary());
    }
}
