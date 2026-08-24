using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 271 — Post-Resonance Integrity Audit. Re-evaluate all remaining issues through the
/// QG260-270 resonance hierarchy. Deterministic, structure only.
/// </summary>
public class TQMQG_Phase271_PostResonanceIntegrityAuditTests : ResearchTestBase
{
    public TQMQG_Phase271_PostResonanceIntegrityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2710_Reevaluation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2710: re-evaluating the remaining critiques through the resonance hierarchy");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - RESOLVED = the resonance reduction removes the critique;");
        sb.AppendLine("  - REFRAMED = the interpretation changes (the critique becomes structural/derived);");
        sb.AppendLine("  - STILL OPEN = unchanged by the structural reduction;");
        sb.AppendLine("  - FALSE PROBLEM = never was a real critique.");
        sb.AppendLine();

        foreach (var c in PostResonanceIntegrityAudit.Critiques())
            sb.AppendLine($"  [{c.Status,-11}] {c.Name}");
        sb.AppendLine();
        var counts = PostResonanceIntegrityAudit.StatusCounts();
        sb.AppendLine($"Counts: RESOLVED={counts[PostResonanceIntegrityAudit.Status.Resolved]}, "
            + $"REFRAMED={counts[PostResonanceIntegrityAudit.Status.Reframed]}, "
            + $"STILL OPEN={counts[PostResonanceIntegrityAudit.Status.StillOpen]}, "
            + $"FALSE PROBLEM={counts[PostResonanceIntegrityAudit.Status.FalseProblem]}");

        Output.WriteLine(sb.ToString());

        Assert.True(counts[PostResonanceIntegrityAudit.Status.Resolved] >= 2,
            "parameter leakage and octave-grouping circularity are resolved");
        Assert.True(counts[PostResonanceIntegrityAudit.Status.Reframed] >= 5,
            "the selection-principle critiques are reframed");
        Assert.True(counts[PostResonanceIntegrityAudit.Status.StillOpen] >= 5,
            "the assignment/empirical critiques remain open");
        Assert.Equal(0, counts[PostResonanceIntegrityAudit.Status.FalseProblem]);
    }

    [Fact]
    public void TQMQG2711_FocusCritiques()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2711: the special-focus critiques (QG250/252/253/256/257/258)");

        sb.AppendLine("HYPOTHESIS: the resonance reduction changed the interpretation of the focus critiques.");
        sb.AppendLine();

        foreach (var c in PostResonanceIntegrityAudit.Critiques())
        {
            sb.AppendLine($"── {c.Name} [{c.Status}] ──");
            sb.AppendLine($"  {c.ResonanceInterpretation}");
            sb.AppendLine();
        }

        Output.WriteLine(sb.ToString());

        // The resonance chain reframed the selection-principle critiques.
        Assert.Equal(PostResonanceIntegrityAudit.Status.Reframed,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG253")).Status);
        Assert.Equal(PostResonanceIntegrityAudit.Status.Reframed,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG256")).Status);
        Assert.Equal(PostResonanceIntegrityAudit.Status.Reframed,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG257")).Status);
        Assert.Equal(PostResonanceIntegrityAudit.Status.Reframed,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG258")).Status);
        // F1 resolved, F2 still open.
        Assert.Equal(PostResonanceIntegrityAudit.Status.Resolved,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG250-F1")).Status);
        Assert.Equal(PostResonanceIntegrityAudit.Status.StillOpen,
            PostResonanceIntegrityAudit.Critiques().Single(c => c.Name.StartsWith("QG250-F2")).Status);
    }

    [Fact]
    public void TQMQG2712_TrueFrontier()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2712: the true remaining frontier after QG270");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the frontier = what the resonance reduction did NOT resolve/reframe.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {PostResonanceIntegrityAudit.Summary()}");
        sb.AppendLine();
        sb.AppendLine("THE TRUE FRONTIER (after QG270):");
        sb.AppendLine("  1. THE ASSIGNMENT STEP — which operator output maps to which observable/sector");
        sb.AppendLine("     (QG262 caveat): the operator basis is universal, but the structure→physics");
        sb.AppendLine("     label mapping retains target-information (QG257/258/259).");
        sb.AppendLine("  2. THE 5/4 EXCEPTION — the acoustic-peak factor ℓ₁ = Σm·ln(span)·5/4 remains");
        sb.AppendLine("     inconsistent with the Noether rule (QG256 STILL OPEN).");
        sb.AppendLine("  3. THE me = 0.511 ANCHOR — the only genuinely free empirical input (QG251).");
        sb.AppendLine("  4. INDEPENDENT TEMPORAL EVIDENCE — the binding constraint (QG252, 6.7% temporal).");
        sb.AppendLine("  5. STRUCTURAL IMPORTS — conformal η, Bekenstein π, ψ primitive, RG, 3+1.");
        sb.AppendLine();
        sb.AppendLine("The resonance reduction RESOLVED the parameter-leakage premise, DERIVED the");
        sb.AppendLine("selection principles, and EXPLAINED the blind-tournament weakness — but the");
        sb.AppendLine("ASSIGNMENT step (structure → physics labels) is the true frontier.");

        Output.WriteLine(sb.ToString());

        Assert.True(PostResonanceIntegrityAudit.StillOpenCount() >= 5);
        Assert.Contains("ASSIGNMENT", PostResonanceIntegrityAudit.Summary());
        Assert.Contains("STILL OPEN", PostResonanceIntegrityAudit.Summary());
    }
}
