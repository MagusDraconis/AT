using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 278 — Fundamental Boundary Audit. Have we reached a true primitive layer? Identify the
/// first concept that cannot be reduced without reintroducing itself.
/// </summary>
public class ATQG_Phase278_FundamentalBoundaryAuditTests : ResearchTestBase
{
    public ATQG_Phase278_FundamentalBoundaryAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2780_ConceptReduction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2780: the four candidate concepts and their reduction");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Actualization, Question, Self-consistency are DERIVABLE (presuppose Difference);");
        sb.AppendLine("  - Difference is a SELF-REFERENTIAL BOUNDARY (unreducible).");
        sb.AppendLine();

        foreach (var c in FundamentalBoundaryAudit.Concepts())
            sb.AppendLine($"  [{c.Status,-24}] {c.Name,-18} → {c.ReducedTo}");
        sb.AppendLine();
        sb.AppendLine($"derivable count: {FundamentalBoundaryAudit.DerivableCount()}/3");
        sb.AppendLine($"difference is self-referential: {FundamentalBoundaryAudit.DifferenceIsSelfReferentialBoundary()}");
        sb.AppendLine($"everything presupposes difference: {FundamentalBoundaryAudit.EverythingPresupposesDifference()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, FundamentalBoundaryAudit.Concepts().Length);
        Assert.True(FundamentalBoundaryAudit.DerivableCount() >= 3,
            "actualization/question/self-consistency are all derivable from difference");
        Assert.True(FundamentalBoundaryAudit.DifferenceIsSelfReferentialBoundary(),
            "difference is the self-referential boundary");
    }

    [Fact]
    public void ATQG2781_SelfReferentialTest()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2781: the self-referential reduction test");

        sb.AppendLine("HYPOTHESIS: every attempt to reduce DIFFERENCE reintroduces difference itself.");
        sb.AppendLine();

        sb.AppendLine("  'difference = two distinct things' → uses 'distinct' (= difference)");
        sb.AppendLine("  'difference = a boundary'           → a boundary is where things differ");
        sb.AppendLine("  'difference = a transition'         → a before/after difference");
        sb.AppendLine("  'difference = empty vs non-empty'   → itself a difference");
        sb.AppendLine();
        sb.AppendLine($"self-referential reduction holds: {FundamentalBoundaryAudit.SelfReferentialReduction()}");
        sb.AppendLine($"dependence graph: {FundamentalBoundaryAudit.DependenceGraph()}");
        sb.AppendLine();
        sb.AppendLine("Actualization → Difference (a change is a difference);");
        sb.AppendLine("Question → Difference (a gap is a difference);");
        sb.AppendLine("Self-consistency → Difference (a comparison is a difference);");
        sb.AppendLine("Difference → unreducible (self-referential).");

        Output.WriteLine(sb.ToString());

        Assert.True(FundamentalBoundaryAudit.SelfReferentialReduction());
        Assert.True(FundamentalBoundaryAudit.EverythingPresupposesDifference());
    }

    [Fact]
    public void ATQG2782_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2782: the fundamental-boundary determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NOT FUNDAMENTAL (score ≤ 2), PARTIALLY FUNDAMENTAL (3-4),");
        sb.AppendLine("    FUNDAMENTAL BOUNDARY (5-6);");
        sb.AppendLine("  - the goal: identify the first concept that cannot be reduced without");
        sb.AppendLine("    reintroducing itself.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FundamentalBoundaryAudit.Summary()}");
        sb.AppendLine($"Boundary score: {FundamentalBoundaryAudit.BoundaryScore()}/6");
        sb.AppendLine($"QG270 confirms (Universal Difference Principle): {FundamentalBoundaryAudit.QG270Confirms()}");
        sb.AppendLine($"QG268 confirms (Universal Self-Consistency): {FundamentalBoundaryAudit.QG268Confirms()}");
        sb.AppendLine($"CLASSIFICATION = {FundamentalBoundaryAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - ACTUALIZATION presupposes Difference: an act of actualizing is a change — a");
        sb.AppendLine("    before→after difference. DERIVABLE from Difference.");
        sb.AppendLine("  - QUESTION presupposes Difference: a question is a gap — the difference between");
        sb.AppendLine("    known and unknown. DERIVABLE from Difference.");
        sb.AppendLine("  - SELF-CONSISTENCY presupposes Difference: non-contradiction is a comparison —");
        sb.AppendLine("    a difference between parts. DERIVABLE from Difference.");
        sb.AppendLine("  - DIFFERENCE cannot be reduced: every attempt ('X differs from Y', 'a boundary',");
        sb.AppendLine("    'a transition') uses difference itself. It is a SELF-REFERENTIAL BOUNDARY —");
        sb.AppendLine("    the first concept that cannot be reduced without reintroducing itself.");
        sb.AppendLine("  - The theory's concepts bottom out at DIFFERENCE: the true primitive layer.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FUNDAMENTAL BOUNDARY", FundamentalBoundaryAudit.Classify());
        Assert.True(FundamentalBoundaryAudit.BoundaryScore() >= 5);
        Assert.Contains("FUNDAMENTAL BOUNDARY", FundamentalBoundaryAudit.Summary());
        Assert.Contains("DIFFERENCE", FundamentalBoundaryAudit.Summary());
    }
}
