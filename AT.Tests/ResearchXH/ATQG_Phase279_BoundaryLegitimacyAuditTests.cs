using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 279 — Boundary Legitimacy Audit. Is the Difference boundary genuine or an artifact of
/// language? Attempt independent reductions. Structure only.
/// </summary>
public class ATQG_Phase279_BoundaryLegitimacyAuditTests : ResearchTestBase
{
    public ATQG_Phase279_BoundaryLegitimacyAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2790_ReductionAttempts()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2790: the independent reduction attempts");

        sb.AppendLine("HYPOTHESIS: difference cannot be reduced to identity, distinction, relation, or");
        sb.AppendLine("information — every attempt fails because each presupposes difference.");
        sb.AppendLine();

        foreach (var a in BoundaryLegitimacyAudit.Attempts())
            sb.AppendLine($"  [{a.Outcome,-7}] reduce difference to {a.Target}: {a.Attempt}");
        sb.AppendLine();
        sb.AppendLine($"successful reductions: {BoundaryLegitimacyAudit.SuccessfulReductions()}/4");
        sb.AppendLine($"every reduction fails: {BoundaryLegitimacyAudit.EveryReductionFails()}");
        sb.AppendLine();
        sb.AppendLine("  identity → needs difference (comparing X and Y requires a difference);");
        sb.AppendLine("  distinction → is difference in action (detecting a difference presupposes it);");
        sb.AppendLine("  relation → needs relata (two distinct things, requiring difference);");
        sb.AppendLine("  information → IS the registration of a difference (Bateson's definition).");

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, BoundaryLegitimacyAudit.Attempts().Length);
        Assert.True(BoundaryLegitimacyAudit.EveryReductionFails(),
            "no independent reduction of difference succeeds");
    }

    [Fact]
    public void ATQG2791_RealReferent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2791: the reality check — difference has a concrete D96 referent");

        sb.AppendLine("HYPOTHESIS: difference is NOT a linguistic artifact — it has a real referent");
        sb.AppendLine("in the D96 spectrum.");
        sb.AppendLine();

        sb.AppendLine($"distinct frequencies: {BoundaryLegitimacyAudit.DistinctFrequencies()}");
        sb.AppendLine($"degenerate pairs counted separately: {BoundaryLegitimacyAudit.DegeneratePairs()}");
        sb.AppendLine($"positive modes (differences from the zero background): {BoundaryLegitimacyAudit.PositiveModes()}");
        sb.AppendLine($"zero mode in kernel (the background): {BoundaryLegitimacyAudit.ZeroModeBackground()}");
        sb.AppendLine($"difference has a real referent: {BoundaryLegitimacyAudit.DifferenceHasRealReferent()}");

        Output.WriteLine(sb.ToString());

        Assert.True(BoundaryLegitimacyAudit.DifferenceHasRealReferent(),
            "difference has a concrete D96 referent — not a linguistic artifact");
    }

    [Fact]
    public void ATQG2792_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2792: the boundary-legitimacy determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no D96 formulas):");
        sb.AppendLine("  - FALSE BOUNDARY (score ≤ 2), PARTIAL BOUNDARY (3-4),");
        sb.AppendLine("    TRUE FUNDAMENTAL BOUNDARY (5-6);");
        sb.AppendLine("  - the question: is the Difference boundary genuine or a language artifact?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {BoundaryLegitimacyAudit.Summary()}");
        sb.AppendLine($"Legitimacy score: {BoundaryLegitimacyAudit.LegitimacyScore()}/6");
        sb.AppendLine($"mathematical primitive: {BoundaryLegitimacyAudit.MathematicalPrimitive()}");
        sb.AppendLine($"physical primitive: {BoundaryLegitimacyAudit.PhysicalPrimitive()}");
        sb.AppendLine($"true primitive (irreducible + real referent): {BoundaryLegitimacyAudit.TruePrimitive()}");
        sb.AppendLine($"CLASSIFICATION = {BoundaryLegitimacyAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - 1. TRUE PRIMITIVE: every independent reduction attempt fails — identity,");
        sb.AppendLine("    distinction, relation, and information all presuppose difference.");
        sb.AppendLine("  - 2. NOT LINGUISTIC: difference has a real D96 referent (44 distinct frequencies,");
        sb.AppendLine("    42 degenerate pairs counted separately, 95 positive modes differing from the");
        sb.AppendLine("    zero background) — it is not just a word.");
        sb.AppendLine("  - 3. MATHEMATICAL PRIMITIVE: numbers are differences from zero; sets/categories");
        sb.AppendLine("    need distinct objects.");
        sb.AppendLine("  - 4. PHYSICAL PRIMITIVE: every quantity is a difference from a background (mass =");
        sb.AppendLine("    the deficit ρ̄−ρ, QG194; positive modes = differences from the zero mode, QG270).");
        sb.AppendLine("  - CONCLUSION: the boundary is genuine — difference is the true first concept, not");
        sb.AppendLine("    an artifact of language.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("TRUE FUNDAMENTAL BOUNDARY", BoundaryLegitimacyAudit.Classify());
        Assert.True(BoundaryLegitimacyAudit.LegitimacyScore() >= 5);
        Assert.Contains("TRUE FUNDAMENTAL BOUNDARY", BoundaryLegitimacyAudit.Summary());
        Assert.Contains("difference", BoundaryLegitimacyAudit.Summary());
    }
}
